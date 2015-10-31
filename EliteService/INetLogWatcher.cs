using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteService {
    public interface INetLogWatcher {
        NetLogWatcherStatus Status { get;  }
        FileSystemWatcher Watcher { get;  }
        void Start();
        void Stop();
    }
}
