using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EliteModels;

namespace EliteService {

    public class NetLogWatcherEventArgs : EventArgs {
        public CurrentSystem CurrentSystem { get; set; }
    }

    public delegate void NetLogWatcherHandler(object source);

    public class NetLogWatcher : INetLogWatcher {
        public NetLogWatcherStatus Status { get; private set; }
        public FileSystemWatcher Watcher { get; private set; }
        public List<SystemPosition> VisitedSystems { get; private set; }
        public CurrentSystem CurrentSystem { get; set; }

        private Dictionary<string, NetLogFileInfo> _netlogfiles;
        private NetLogFileInfo _lastnfi = null;
        private IPersistentStore _persistentStore;

        public event NetLogWatcherHandler OnNewPosition;
        //public event EventHandler<NetLogWatcherEventArgs> SystemFound;

        public NetLogWatcher(IPersistentStore persistentStore) {
            if (persistentStore == null) throw new ArgumentNullException("persistentStore");

            _persistentStore = persistentStore;
            Watcher = new FileSystemWatcher();
            VisitedSystems = new List<SystemPosition>();
            _netlogfiles = new Dictionary<string, NetLogFileInfo>();
            Watcher.Changed += Watcher_Changed;
            Status = NetLogWatcherStatus.Initialized;
        }

        //protected virtual void OnSystemFound(NetLogWatcherEventArgs e) {
        //    EventHandler<NetLogWatcherEventArgs> handler = SystemFound;
        //    if (handler != null) {
        //        handler(this, e);
        //    }
        //}

        private async void Watcher_Changed(object sender, FileSystemEventArgs e) {
            if (e.ChangeType == System.IO.WatcherChangeTypes.Changed) {
                await ParseFile(new FileInfo(e.FullPath));
            }
        }

        private async Task<int> ParseFile(FileInfo fileInfo) {
            if (fileInfo == null) throw new ArgumentNullException("fileInfo");

            int count = 0, nrsystems = VisitedSystems.Count;
            try {
                using (Stream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    using (StreamReader sr = new StreamReader(fs)) {
                        count = await ReadData(fileInfo, sr);
                    }
                }
            } catch {
                return 0;
            }

            //if (nrsystems < VisitedSystems.Count) {
            //    //if (!NoEvents)
            //    //    OnNewPosition(this);
            //}
            return count;
        }

        private async Task<int> ReadData(FileInfo fileInfo, StreamReader sr) {
            if (fileInfo == null) throw new ArgumentNullException("fileInfo");
            if (sr == null) throw new ArgumentNullException("sr");

            DateTime gammastart = new DateTime(2014, 11, 22, 13, 00, 00);
            var count = 0;
            DateTime filetime = DateTime.Now.AddDays(-500);
            string FirstLine = sr.ReadLine();
            string line, str;
            NetLogFileInfo nfi = null;

            str = "20" + FirstLine.Substring(0, 8) + " " + FirstLine.Substring(9, 5);

            filetime = DateTime.Parse(str);

            if (_netlogfiles.ContainsKey(fileInfo.FullName)) {
                nfi = _netlogfiles[fileInfo.FullName];
                sr.BaseStream.Position = nfi.filePos;
                sr.DiscardBufferedData();
            }

            while ((line = sr.ReadLine()) != null) {
                if (line.Contains(" System:")) {
                    SystemPosition ps = SystemPosition.Parse(filetime, line);
                    if (ps != null) {
                        if (ps.Name.Equals("Training"))
                            continue;
                        if (ps.Name.Equals("Destination"))
                            continue;

                        filetime = ps.Time;

                        if (VisitedSystems.Count > 0)
                            if (VisitedSystems[VisitedSystems.Count - 1].Name.Equals(ps.Name))
                                continue;

                        if (ps.Time.Subtract(gammastart).TotalMinutes > 0) { // Ta bara med efter gamma. 
                            if (!VisitedSystems.Contains(ps)) {
                                VisitedSystems.Add(ps);
                                await AddNewSystem(ps);
                            }
                            count++;
                        }
                    }
                }
            }


            if (nfi == null)
                nfi = new NetLogFileInfo();

            nfi.FileName = fileInfo.FullName;
            nfi.lastchanged = File.GetLastWriteTimeUtc(nfi.FileName);
            nfi.filePos = sr.BaseStream.Position;
            nfi.fileSize = fileInfo.Length;

            _netlogfiles[nfi.FileName] = nfi;
            _lastnfi = nfi;

            return count;
        }

        private async Task AddNewSystem(SystemPosition ps) {
            if (ps == null) throw new ArgumentNullException("ps");

            CurrentSystem = await _persistentStore.AddNewStarSystem(ps);
            OnNewPosition(this);

            //ss.Name = ps.Name;
            //var sys = await _starSystemRepository.Save(ss);
            //LogText(string.Format("{0}: Adding system: {1}", DateTime.Now, ps.Name));
            //var current = _systemPointer.Get<string>("currentObjectId"); ;
            //_systemPointer["lastObjectId"] = current;
            //_systemPointer["currentObjectId"] = sys.ObjectId;
            //_systemPointer = await _systemPointerRepository.Save(_systemPointer);
        }

        /// <summary>
        /// Wrapper method around the FileSystemWatcher EnableRaisingEvents property 
        /// After setting to true. Essentially like setting the component Start event. 
        /// Requires that the client to define the FileSystemWatcher.Path property
        /// </summary>
        public void Start() {
            if (string.IsNullOrWhiteSpace(Watcher.Path)) {
                Status = NetLogWatcherStatus.NoPath;
                return;
            }

            Watcher.EnableRaisingEvents = true;
            Status = NetLogWatcherStatus.Started;
        }

        /// <summary>
        /// Wrapper method around the FileSystemWatcher EnableRaisingEvents property 
        /// after setting to false. Essentially like setting the component Stop event. 
        /// </summary>
        public void Stop() {
            Watcher.EnableRaisingEvents = false;
            Status = NetLogWatcherStatus.Stopped;
        }


    }
}
