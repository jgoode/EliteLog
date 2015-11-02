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
    public partial class Login : Form {

        public string UsernameText {
            get {
                return Username.Text;
            }
        }

        public string PasswordText {
            get { return Password.Text;  }
        }

        public Login() {
            InitializeComponent();
        }
    }
}
