using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EliteLogger {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            string datapath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Frontier_Development_s\\Products"; // \\FORC-FDEV-D-1001\\Logs\\";
            Process[] processes = Process.GetProcessesByName("EliteDangerous32");
        }
    }
}
