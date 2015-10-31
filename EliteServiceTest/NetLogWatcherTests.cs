using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EliteService;
using System.IO;

namespace EliteServiceTest {
    [TestClass]
    public class NetLogWatcherTests {

        [TestMethod]
        public void TestStart() {
            INetLogWatcher logWatcher = new NetLogWatcher();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.Initialized);

            logWatcher.Start();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.NoPath);
            var path = @"c:\EliteLogTestFiles";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            logWatcher.Watcher.Path = path;
            Assert.IsTrue(logWatcher.Watcher.Path == path);
            logWatcher.Start();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.Started);
            Assert.IsTrue(logWatcher.Watcher.EnableRaisingEvents);

        }

        [TestMethod]
        public void TestStop() {
            INetLogWatcher logWatcher = new NetLogWatcher();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.Initialized);

            logWatcher.Start();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.NoPath);
            var path = @"c:\EliteLogTestFiles";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            logWatcher.Watcher.Path = path;
            logWatcher.Start();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.Started);

            logWatcher.Stop();
            Assert.IsTrue(logWatcher.Status == NetLogWatcherStatus.Stopped);
            Assert.IsFalse(logWatcher.Watcher.EnableRaisingEvents);
        }
    }
}
