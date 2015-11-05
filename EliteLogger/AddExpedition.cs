using EliteModels;
using EliteModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteLogger {
    public partial class AddExpedition : Form {
        private readonly User _user;

        public Expedition Expedition { get {
                var expedition = new Expedition();
                expedition.Name = NameTextBox.Text;
                expedition.Description = DescriptionTextBox.Text;
                expedition.StartDate = StartDatePicker.Value.Date;
                expedition.EndDate = EndDatePicker.Value.Date;
                expedition.StartSystem = StartSystemTextBox.Text;
                expedition.EndSystem = EndSystemTextBox.Text;
                expedition.User = _user.UserName;
                expedition.Current = true;
                return expedition;
            } }

        public AddExpedition(ExpeditionFormType formType, User user) {
            InitializeComponent();

            _user = user;
            if (formType == ExpeditionFormType.Add) {
                this.Text = "Add Expedition";
            } else {
                this.Text = "Edit Expedition";
            }
        }
    }
}
