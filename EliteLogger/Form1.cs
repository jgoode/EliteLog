using EliteLogger.Services;
using EliteModels;
using Repository;
using Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EliteParse.Repository;

namespace EliteLogger {
    public partial class Form1 : Form {
        //public NetLogClass netlog = new NetLogClass();
        public string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Frontier_Developments\\Products";
        public List<SystemPosition> visitedSystems = new List<SystemPosition>();
        private Dictionary<string, NetLogFileInfo> netlogfiles = new Dictionary<string, NetLogFileInfo>();
        private NetLogFileInfo lastnfi = null;
        private IExpeditionRepository _expeditionRepository = new ExpeditionRespository();
        private IStarSystemRepository _starSystemRepository;
        private ISystemPointerRepository _systemPointerRepository;
        private Expedition _expedition;
        private ParseObject _systemPointer;

        private string _user = "john";

        public Form1() {
            InitializeComponent();
            Process[] processes = Process.GetProcessesByName("EliteDangerous32");
            //netlog.OnNewPosition += new NetLogEventHandler(NewPosition);

        }

        private async void GetExpedition() {
            _expedition = await _expeditionRepository.GetCurrent(_user);

            LogText(string.Format("{0}: Starting Expedition {1}", DateTime.Now, _expedition.Name), Color.Blue);
            _starSystemRepository = new StarSystemRepository(_expedition);

            _systemPointerRepository = new SystemPointerRepository(_expedition);
            _systemPointer = await _systemPointerRepository.GetCurrent();

        }

        public void LogText(string text) {
            LogText(text, Color.Black);
        }

        public void LogText(string text, Color color) {
            try {

                Log.SelectionStart = Log.TextLength;
                Log.SelectionLength = 0;

                Log.SelectionColor = color;
                Log.AppendText(text);
                Log.AppendText("\n");
                Log.SelectionColor = Log.ForeColor;

                Log.SelectionStart = Log.Text.Length;
                Log.SelectionLength = 0;
                Log.ScrollToCaret();
                Log.Refresh();
            } catch (Exception ex) {
                Trace.WriteLine("Exception SystemClass: " + ex.Message);
                Trace.WriteLine("Trace: " + ex.StackTrace);
            }
        }

        internal void NewPosition(object source) {
            try {
                //string name = netlog.visitedSystems.Last().Name;
                //Invoke((MethodInvoker)delegate {
                //    LogText("Arrived to system: ");
                //    SystemClass sys1 = SystemData.GetSystem(name);
                //    if (sys1 == null || sys1.HasCoordinate == false)
                //        LogText(name, Color.Blue);
                //    else
                //        LogText(name);


                //    int count = GetVisitsCount(name);

                //    LogText("  : Vist nr " + count.ToString() + Environment.NewLine);
                //    System.Diagnostics.Trace.WriteLine("Arrived to system: " + name + " " + count.ToString() + ":th visit.");

                //    var result = visitedSystems.OrderByDescending(a => a.time).ToList<SystemPosition>();

                //    //if (TrilaterationControl.Visible)
                //    //{
                //    //    CloseTrilateration();
                //    //    MessageBox.Show("You have arrived to another system while trilaterating."
                //    //                    + " As a pre-caution to prevent any mistakes with submitting wrong systems or distances"
                //    //                    + ", your trilateration was aborted.");
                //    //}


                //    SystemPosition item = result[0];
                //    SystemPosition item2;

                //    if (result.Count > 1)
                //        item2 = result[1];
                //    else
                //        item2 = null;

                //    // grab distance to next (this) system
                //    textBoxDistanceToNextSystem.Enabled = false;
                //    if (textBoxDistanceToNextSystem.Text.Length > 0 && item2 != null) {
                //        SystemClass currentSystem = null, previousSystem = null;
                //        SystemData.SystemList.ForEach(s => {
                //            if (s.name == item.Name) currentSystem = s;
                //            if (s.name == item2.Name) previousSystem = s;
                //        });

                //        if (currentSystem == null || previousSystem == null || !currentSystem.HasCoordinate || !previousSystem.HasCoordinate) {
                //            var presetDistance = DistanceAsDouble(textBoxDistanceToNextSystem.Text.Trim(), 45);
                //            if (presetDistance.HasValue) {
                //                var distance = new DistanceClass {
                //                    Dist = presetDistance.Value,
                //                    CreateTime = DateTime.UtcNow,
                //                    CommanderCreate = textBoxCmdrName.Text.Trim(),
                //                    NameA = item.Name,
                //                    NameB = item2.Name,
                //                    Status = DistancsEnum.EDDiscovery
                //                };
                //                Console.Write("Pre-set distance " + distance.NameA + " -> " + distance.NameB + " = " + distance.Dist);
                //                distance.Store();
                //                SQLiteDBClass.AddDistanceToCache(distance);
                //            }
                //        }
                //    }
                //    textBoxDistanceToNextSystem.Clear();
                //    textBoxDistanceToNextSystem.Enabled = true;

                //    AddHistoryRow(true, item, item2);
                //    lastRowIndex += 1;
                //    StoreSystemNote();
                //});
            } catch (Exception ex) {
                System.Diagnostics.Trace.WriteLine("Exception NewPosition: " + ex.Message);
                System.Diagnostics.Trace.WriteLine("Trace: " + ex.StackTrace);
            }
        }

        private async Task<int> ParseFile(FileInfo fi, List<SystemPosition> visitedSystems) {

            int count = 0, nrsystems = visitedSystems.Count;
            try {
                using (Stream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    using (StreamReader sr = new StreamReader(fs)) {
                        count = await ReadData(fi, visitedSystems, count, sr);
                    }
                }
            } catch {
                return 0;
            }

            if (nrsystems < visitedSystems.Count) {
                //if (!NoEvents)
                //    OnNewPosition(this);
            }
            return count;
        }

        private async Task<int> ReadData(FileInfo fi, List<SystemPosition> visitedSystems, int count, StreamReader sr) {
            DateTime gammastart = new DateTime(2014, 11, 22, 13, 00, 00);

            DateTime filetime = DateTime.Now.AddDays(-500);
            string FirstLine = sr.ReadLine();
            string line, str;
            NetLogFileInfo nfi = null;

            str = "20" + FirstLine.Substring(0, 8) + " " + FirstLine.Substring(9, 5);

            filetime = DateTime.Parse(str);

            if (netlogfiles.ContainsKey(fi.FullName)) {
                nfi = netlogfiles[fi.FullName];
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

                        filetime = ps.time;

                        if (visitedSystems.Count > 0)
                            if (visitedSystems[visitedSystems.Count - 1].Name.Equals(ps.Name))
                                continue;

                        if (ps.time.Subtract(gammastart).TotalMinutes > 0) { // Ta bara med efter gamma. 
                            if (!visitedSystems.Contains(ps)) {
                                visitedSystems.Add(ps);
                                await AddNewSystem(ps);
                            }
                            count++;
                        }
                    }
                }
            }


            if (nfi == null)
                nfi = new NetLogFileInfo();

            nfi.FileName = fi.FullName;
            nfi.lastchanged = File.GetLastWriteTimeUtc(nfi.FileName);
            nfi.filePos = sr.BaseStream.Position;
            nfi.fileSize = fi.Length;

            netlogfiles[nfi.FileName] = nfi;
            lastnfi = nfi;

            return count;
        }

        private async Task AddNewSystem(SystemPosition ps) {
            StarSystem ss = new StarSystem();
            ss.Name = ps.Name;
            var sys = await _starSystemRepository.Save(ss);
            LogText(string.Format("{0}: Adding system: {1}", DateTime.Now, ps.Name));
            var current = _systemPointer.Get<string>("currentObjectId"); ;
            _systemPointer["lastObjectId"] = current;
            _systemPointer["currentObjectId"] = sys.ObjectId;
            _systemPointer = await _systemPointerRepository.Save(_systemPointer);

        }

        private void StartButton_Click(object sender, EventArgs e) {
            FileWatcher.EnableRaisingEvents = true;
            LogText(string.Format("{0}: File watch started..", DateTime.Now), Color.Red);
            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");
            ParseClient.Initialize(appKey, netKey);
            GetExpedition();

        }

        private void StopButton_Click(object sender, EventArgs e) {
            FileWatcher.EnableRaisingEvents = false;
            LogText(string.Format("{0}: File watch stopped..", DateTime.Now), Color.Red);
        }

        private async void FileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e) {
            if (e.ChangeType == System.IO.WatcherChangeTypes.Changed) {
                await ParseFile(new FileInfo(e.FullPath), visitedSystems);
            }
        }
    }
}
