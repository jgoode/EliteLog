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

        public EliteExplorer() {
            InitializeComponent();

            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");
            ParseClient.Initialize("oZ5uASGFBuliW7gkydvGPoKfB0ePMcS2pnsrmIyp", "kXDwODYgrhsfAd39x0LEmak3a204B0qWuNeCpR5k");

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
            _watcher.Watcher.Path = @"C:\Users\John\AppData\Local\Frontier_Developments\Products\FORC-FDEV-D-1003\Logs";
            _watcher.SystemFound += _watcher_SystemFound;
            _watcher.Start();

        }

        private void _watcher_SystemFound(object sender, NetLogWatcherEventArgs e) {
            LogText(e.CurrentSystem.StarSystem.Name);
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
    }
}
