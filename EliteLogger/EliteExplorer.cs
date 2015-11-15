using EliteModels;
using EliteModels.Enums;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteLogger {
    public partial class EliteExplorer : Form {
        private NetLogWatcher _watcher;
        private static IPersistentStore _persistentStore;
        private static User _user;
        private static IEnumerable<Expedition> _expeditions;
        private static Expedition _currentExpedition;

        private static RichTextBox static_richTextBox;
        private static ComboBox static_expedition_combo;
        private static DataGridView static_system_grid;

        private static bool _userSelectsExpedition;

        public EliteExplorer() {
            InitializeComponent();

            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");
            ParseClient.Initialize("oZ5uASGFBuliW7gkydvGPoKfB0ePMcS2pnsrmIyp", "kXDwODYgrhsfAd39x0LEmak3a204B0qWuNeCpR5k");
            static_richTextBox = Log;
            static_expedition_combo = ExpeditionComboBox;
            static_system_grid = SystemGridView;
        }

        // methods
        internal void NewPosition(object source, NetLogWatcherEventArgs args) {
            Invoke(new Action<string, Color>(LogText), string.Format("Added Star System {0}...", args.CurrentSystem.Name), Color.Black);
            PopulateSystemGrid();
        }

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

        static async void RefreshExpeditionDropDown() {
            _userSelectsExpedition = false;
            if (null == _expeditions) {
                _expeditions = await _persistentStore.GetAllExpeditions();
            }
            static_expedition_combo.DataSource = _expeditions.ToList();
            static_expedition_combo.DisplayMember = "Name";
            var currentExpedition = _expeditions.Where(a => a.Current).FirstOrDefault();
            int itemIndex = -1;
            for (int index = 0; index < static_expedition_combo.Items.Count; index++) {
                var exp = (Expedition)static_expedition_combo.Items[index];
                if (exp.ObjectId == currentExpedition.ObjectId) {
                    itemIndex = index;
                    _currentExpedition = exp;
                    await _persistentStore.SetCurrentExpedition(exp);
                    LogText(string.Format("Expedition {1} has {0} systems...", _persistentStore.StarSystems.Count(), _currentExpedition.Name), Color.Red);
                    break;
                }
            }
            static_expedition_combo.SelectedIndex = itemIndex;
            PopulateSystemGrid();
            _userSelectsExpedition = true;
        }

        private static void PopulateSystemGrid() {
            static_system_grid.DataSource = StarSystemToGridRowService.ConvertToGridRows(_persistentStore.StarSystems, 20);
            static_system_grid.Columns[1].Width = 110;
            static_system_grid.Columns[2].Width = 130;
            static_system_grid.Columns[3].Width = 60;
            static_system_grid.Columns[0].Visible = false;
        }

        // events
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

        private void EliteExplorer_Shown(object sender, EventArgs e) {

            Invoke(new Action<string, Color>(LogText), "Starting file watch...", Color.Red);
            _userSelectsExpedition = false;

            Invoke(new Action(RefreshExpeditionDropDown));
        }

        private async void AddExpeditionButton_Click(object sender, EventArgs e) {
            var addExpeditionForm = new AddExpedition(ExpeditionFormType.Add, _user);
            if (addExpeditionForm.ShowDialog() == DialogResult.OK) {
                if (null == addExpeditionForm.Expedition) return;

                var expedition = addExpeditionForm.Expedition;

                var match = (from p in _expeditions
                             where p.Name == expedition.Name
                             select p).FirstOrDefault();

                if (match != null) return;

                List<Expedition> temp = new List<Expedition>();
                foreach(var exp in _expeditions) {
                    exp.Current = false;
                    temp.Add(exp);
                }
                
                await _persistentStore.ClearExpeditionCurrentFlags();
                var expSaved = await _persistentStore.InsertExpedition(expedition);
                
                temp.Add(expSaved);

                _expeditions = temp;

                Invoke(new Action<string, Color>(LogText), string.Format("Added expedition {0} ({1})...", expSaved.Name, expSaved.ObjectId), Color.Red);
                Invoke(new Action(RefreshExpeditionDropDown));

            }
        }

        private async void ExpeditionComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (!_userSelectsExpedition) return;
            var exp = (Expedition)ExpeditionComboBox.SelectedItem;
            _currentExpedition = exp;
            await _persistentStore.SetCurrentExpedition(exp);
            Invoke(new Action<string, Color>(LogText), string.Format("Expedition {1} has {0} systems...", _persistentStore.StarSystems.Count(), _currentExpedition.Name), Color.Red);
            PopulateSystemGrid();
        }
    }
}
