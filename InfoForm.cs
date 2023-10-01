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

namespace bruhshot {
    public partial class InfoForm : Form {
        public InfoForm() {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
