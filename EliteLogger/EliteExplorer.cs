using EliteModels;
using EliteParse.Mappers;
using EliteService;
using Parse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteLogger {
    public partial class EliteExplorer : Form {
        private NetLogWatcher _watcher;
        private IPersistentStore _persistentStore;
        private User _user;

        private static RichTextBox static_richTextBox;

        public EliteExplorer() {
            InitializeComponent();

            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");
            ParseClient.Initialize("oZ5uASGFBuliW7gkydvGPoKfB0ePMcS2pnsrmIyp", "kXDwODYgrhsfAd39x0LEmak3a204B0qWuNeCpR5k");
            static_richTextBox = Log;
        }

        private async void EliteExplorer_Load(object sender, EventArgs e) {
            if (ParseUser.CurrentUser != null) {
                // do stuff with the user
                _user = UserMapper.Map(ParseUser.CurrentUser);
            } else {
                // show the signup or login screen
                if (!ConfigurationManager.AppSettings.HasKeys()) {
                    // TODO: Form Popup to signup 
                    ConfigurationManager.AppSettings.Add("commander", "The Mule");
                } else {
                    // TODO: Login screen popup
                    var login = new Login();
                    var a = true;
                    while (a)
                        if (login.ShowDialog() == DialogResult.OK) {
                            var commander = login.UsernameText;
                            var pass = login.PasswordText;

                            try {
                                await ParseUser.LogInAsync(commander, pass);
                                // Login was successful.
                                a = !a;
                            } catch (Exception ex) {
                                // The login failed. Check the error to see why.
                                MessageBox.Show(ex.Message);
                            }
                        }
                }
            }

            _persistentStore = new ParsePersistentStore(_user);
            _watcher = new NetLogWatcher(_persistentStore);
            _watcher.Watcher.Path = @"C:\Users\John Goode\AppData\Local\Frontier_Developments\Products\FORC-FDEV-D-1003\Logs";
            // C:\Users\John Goode\AppData\Local\Frontier_Developments\Products\FORC-FDEV-D-1003\Logs
            _watcher.OnNewPosition += new NetLogWatcherHandler(this.NewPosition);
            //_watcher.SystemFound += _watcher_SystemFound;
            _watcher.Start();


        }


        internal void NewPosition(object source) {
            Invoke((MethodInvoker)delegate {
                LogText(_watcher.CurrentSystem.StarSystem.Name);
            });
        }

        //private async void _watcher_SystemFound(object sender, NetLogWatcherEventArgs e) {
        //    await LogText(e.CurrentSystem.StarSystem.Name);
        //}


        static void LogText(string text) {
            EliteExplorer.LogText(text, Color.Black);
        }

        static void LogText(string text, Color color) {
            try {

                static_richTextBox.SelectionStart = static_richTextBox.TextLength;
                static_richTextBox.SelectionLength = 0;

                static_richTextBox.SelectionColor = color;
                static_richTextBox.AppendText(text);
                static_richTextBox.AppendText("\n");
                static_richTextBox.SelectionColor = static_richTextBox.ForeColor;

                static_richTextBox.SelectionStart = static_richTextBox.Text.Length;
                static_richTextBox.SelectionLength = 0;
                static_richTextBox.ScrollToCaret();
                static_richTextBox.Refresh();
            } catch (Exception ex) {
                Trace.WriteLine("Exception SystemClass: " + ex.Message);
                Trace.WriteLine("Trace: " + ex.StackTrace);
            }
        }

        private void EliteExplorer_Shown(object sender, EventArgs e) {
            Invoke((MethodInvoker)delegate {
                EliteExplorer.LogText("Starting file watch...", Color.Red);
            });
        }
    }
}
