using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ToyTwoToolbox {

    public partial class Main : Form {
        /// <summary>
        /// This tag specifies that the function requires extra support to prevent damage
        /// </summary>
        public class Unsafe : Attribute {}
        public bool Debug = false;
        public bool PrintMessages = false;
        public int LSID;
        public string wlid;
        public bool UnsavedWork;
        public bool SMSC = false;
        public TabController TabControl;
        public MessageFade MessageFader;

        public Main() {
            InitializeComponent();
            TabControl = new TabController(tabControl1);
            tabControl1.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, tabControl1, new object[] { true });
            menuStrip1.Renderer = new DarkThemeMenuRender();
            toolStrip1.Renderer = new DarkThemeMenuRender();
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            tabControl1.TabRequestDestroy += new TabRequestDestroyEventHandler(TabControl.TabRequestDestroy);
            this.firstOpenPanel1.OpenFile += new EventHandler(FOPM);
            this.firstOpenPanel1.CreateFile += new EventHandler(FOPM);
            
            XF.CenterObject(firstOpenPanel1);
            MessageFader = new MessageFade(this);
        }

        public void FOPM(object sender, EventArgs e) {
            FOPEA F = (FOPEA)e;
            if (F._FilePath != null) {
                OpenFile(F._FilePath);
            } 
            if (F._FileType != FileProcessor.FileTypes.NULL) {
                CreateFile(F._FileType);
            }

        }

        void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
                //if (((string[])e.Data.GetData(DataFormats.FileDrop)).Length > 1) {

                //}
                MessageFader.FadeIn();
            }
        }

        void Form1_DragDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) {
                OpenFile(file);
            }
            MessageFader.FadeOut();
        }

        private void Form1_DragLeave(object sender, EventArgs e) {
            MessageFader.FadeOut();
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
            CreateFile(FileProcessor.FileTypes.NGN);
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

        private void toolStripButton5_Click(object sender, EventArgs e) {
            closeFileToolStripMenuItem.PerformClick();
        }

        private void nGNToolStripMenuItem1_Click(object sender, EventArgs e) {
            nGNToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// Create a new file within T2T <para/>
        /// Tabs are linked to files, but files arent linked to tabs, allowing views to be duplicated
        /// </summary>
        public void CreateFile(FileProcessor.FileTypes fileType) {

            firstOpenPanel1.Visible = false;
            tabControl1.Visible = true;

            F_Base file = FileProcessor.CreateFile(fileType, TabControl);
            TabControl.CreateTab(file);
        }

        public void OpenFile(string path = "") {
            if (path == "") {
                OpenFileDialog OFD = new OpenFileDialog {
                    Filter = "T2T Files | *.NGN;*.SAV|Level File | *.NGN|Save File | *.SAV|All files (*.*)|*.*"
                };
                if (OFD.ShowDialog() == DialogResult.OK) {
                    path = OFD.FileName;
                }
            }
            if (path != "") {
                firstOpenPanel1.Visible = false;
                tabControl1.Visible = true;
                F_Base file = FileProcessor.ProcessFile(path);
                TabControl.CreateTab(file);
            }

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

        }

        private void NewtoolStripSplitButton_ButtonClick(object sender, EventArgs e) {
            CreateNewFile c = new CreateNewFile();
            c.ShowDialog();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e) {
            CreateFile(FileProcessor.FileTypes.Save);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFile();
        }

        private void firstOpenPanel1_Load(object sender, EventArgs e) {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath);
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            SaveFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath);
        }

        public void SaveFile(string path = "") {
            //TabControl.Tabs[tabControl1.SelectedIndex].File.Export(path);
            TabControl.Tabs[tabControl1.SelectedIndex].Save(path);
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            SaveFile();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            openToolStripMenuItem.PerformClick();
        }

        private void closeFileToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to close this file?", "Close?", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (msg == DialogResult.Yes) {
                TabControl.CloseTab();
            }
        }
    }
}