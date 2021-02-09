using System;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class InputDialog : Form {
        public bool intonly = false;
        public string input = "";
        public InputDialog(string defaultText = "") {
            InitializeComponent();
            if(defaultText!="") { richTextBox1.Text = defaultText; }
        }

        /// <summary>Use this method to open the dialog.</summary>
        /// <param name="integerOnly">Whether to verify if the input is an integer or not</param>
        public DialogResult ShowDialogEx(bool integerOnly = false, Form owner = null) {
            intonly = integerOnly;
            return this.ShowDialog();
        }

        private void butAddPrim_Click(object sender, EventArgs e) {
            if(intonly) {
                if(!Int32.TryParse(richTextBox1.Text, out _)) {
                    MessageBox.Show("Input was not numerically correct", "Integer Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            input = richTextBox1.Text;
        }

        private void InputDialog_Load(object sender, EventArgs e) {
            richTextBox1.Focus();
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e) {
            //if (e.KeyCode == Keys.Enter && e.Modifiers != Keys.Shift) {
            //    butConfirm.PerformClick();
            //}
        }
    }
}
