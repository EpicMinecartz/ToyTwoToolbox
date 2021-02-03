using System;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class InputDialog : Form {
        public string input = "";
        public InputDialog(string defaultText = "") {
            InitializeComponent();
            if(defaultText!="") { richTextBox1.Text = defaultText; }
        }

        private void butAddPrim_Click(object sender, EventArgs e) {
            input = richTextBox1.Text;
        }

        private void InputDialog_Load(object sender, EventArgs e) {
            richTextBox1.Focus();
        }
    }
}
