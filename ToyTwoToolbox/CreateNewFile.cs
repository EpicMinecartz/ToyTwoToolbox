using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class CreateNewFile : Form {
        Main owner = null;
        public CreateNewFile(Main Parent) {
            InitializeComponent();
            owner = Parent;
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void LLNewNGN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            owner.CreateFile(FileProcessor.FileTypes.NGN);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            owner.CreateFile(FileProcessor.FileTypes.Save);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }
    }
}
