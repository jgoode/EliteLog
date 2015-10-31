using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public class NetLogWatcher : INetLogWatcher {
        public NetLogWatcherStatus Status { get; private set; }
        public FileSystemWatcher Watcher { get; private set; }

        public NetLogWatcher () {
            Status = NetLogWatcherStatus.Initialized;
            Watcher = new FileSystemWatcher();
            Watcher.Changed += Watcher_Changed;
        }

        private async void Watcher_Changed(object sender, FileSystemEventArgs e) {
            if (e.ChangeType == System.IO.WatcherChangeTypes.Changed) {
                await ParseFile(new FileInfo(e.FullPath), visitedSystems);
            }
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
