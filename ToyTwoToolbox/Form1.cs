using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {

    public partial class Form1 : Form {
        public bool Debug = false;
        public bool PrintMessages = false;
        public int LSID;
        public string wlid;
        public bool UnsavedWork;
        public bool SMSC = false;

        public Form1() {
            InitializeComponent();
            menuStrip1.Renderer = new DarkThemeMenuRender();
            toolStrip1.Renderer = new DarkThemeMenuRender();
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);

            XF.CenterObject(firstOpenPanel1);
        }

        public void Destroy() {
            SMSC = true;
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (LSID > 0) { this.Text += " : " + LSID; wlid = "[" + LSID + "] "; }
            vlab.Text = "T2T-" + SessionManager.ver;

            //foreach (ContextMenuStrip i in this.components.Components) {
            //    if (i is ContextMenuStrip) {
            //        i.Renderer = new DarkThemeMenuRender();
            //    }
            //}
        }

        public void Init(string Param = "") {
            this.Show();
            //if (Param != "") { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private void fileToolStripMenuItem1_Click(object sender, EventArgs e) {
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void nGNToolStripMenuItem_Click(object sender, EventArgs e) {
            tabControl1.Visible = true;
            tabControl1.TabPages.Add("New File");
            T2Control_NGNEditor NewNGN = new T2Control_NGNEditor();
            NewNGN.Dock = DockStyle.Fill;
            tabControl1.TabPages[tabControl1.TabCount - 1].Controls.Add(NewNGN);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        public string wl(string TX, string Pre = "", Color color = new Color(), bool NOTD = true, bool nl = true, bool NoInstName = false) {
            return SessionManager.SMptr.SM(TX, ((NoInstName == false) ? "SM >" : "") + (string.IsNullOrEmpty(Pre) ? Pre : " " + Pre), color, NOTD, nl);
        }

        private void form1_Closing(object sender, FormClosingEventArgs e) {
            if (SMSC == true) {
                e.Cancel = false;
                this.Dispose();
            } else {
                e.Cancel = !SessionManager.SMptr.RedirectShutdown(LSID);
            }
        }
    }
}