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

        public string ExpeditionName { get { return NameTextBox.Text; } }
        public string Description { get { return DescriptionTextBox.Text; } }
        public DateTime StartDate { get { return StartDatePicker.Value.Date; } }
        public DateTime EndDate { get { return EndDatePicker.Value.Date; } }
        public string StartSystem { get { return StartSystemTextBox.Text; } }
        public string EndSystem { get { return EndSystemTextBox.Text; } }

        public AddExpedition(ExpeditionFormType formType) {
            InitializeComponent();

            if (formType == ExpeditionFormType.Add) {
                this.Text = "Add Expedition";
            } else {
                this.Text = "Edit Expedition";
            }
        }
    }
}
