using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {

    public partial class Main : Form {
        /// <summary>
        /// This tag specifies that the function requires extra support to prevent damage
        /// </summary>
        public class Unsafe : Attribute { }
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
            debugToolStripMenuItem.Visible = SessionManager.Debug;
        }



        void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
                MessageFader.FadeIn();
            }
        }

        void Form1_DragDrop(object sender, DragEventArgs e) {
            foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop)) {
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
        }

        public void Init(string Param = "") {
            this.Show();
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

        public void FOPM(object sender, EventArgs e) {
            FOPEA F = (FOPEA)e;
            if (F._FilePath != null) {
                OpenFile(F._FilePath);
            }
            if (F._FileType != FileProcessor.FileTypes.NULL) {
                CreateFile(F._FileType);
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
            TabControl.CloseTab();
        }

        private void saveToolStripMenuItem2_Click(object sender, EventArgs e) {
            saveToolStripMenuItem1.PerformClick();
        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e) {
            e.Cancel = true;
            StatusHelp(ToolTip1.GetToolTip(e.AssociatedControl));
        }

        public void StatusHelp(string text) {
            labelHelp.Text = text;
        }

        private void validateSaveToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK) {
                SessionManager.Report("Writing to save...", SessionManager.RType.DEBUG);
                SaveFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath);
            }
            F_NGN loadedNGN = (F_NGN)TabControl.Tabs[tabControl1.SelectedIndex].File;
            F_NGN validater = (F_NGN)FileProcessor.ProcessFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath);
            SessionManager.Report("Validating save file...", SessionManager.RType.DEBUG);
            for (int i = 0;i < loadedNGN.Schema.NGNFunctions.Count;i++) {
                if (loadedNGN.Schema.NGNFunctions[i].FunctionOffset == validater.Schema.NGNFunctions[i].FunctionOffset) {
                    SessionManager.Report(Enum.GetName(typeof(F_NGN.NGNFunction), loadedNGN.Schema.NGNFunctions[i].FunctionType) + " successfully validated", SessionManager.RType.DEBUG, Color.DarkGreen);
                } else {
                    SessionManager.Report(Enum.GetName(typeof(F_NGN.NGNFunction), loadedNGN.Schema.NGNFunctions[i].FunctionType) + " did not validate succesfully", SessionManager.RType.WARN);
                }
            }
        }

        private void showPreviewWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            //var window = new Render(800, 600, "LearnOpenTK - Lighting maps");
            //window.Run(60.0);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFile();
        }

        private void toolStripButton4_Click(object sender, EventArgs e) {
            reloadFileToolStripMenuItem.PerformClick();
        }

        private void reloadFileToolStripMenuItem_Click(object sender, EventArgs e) {
            F_Base file = FileProcessor.ProcessFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath);
            TabControl.ReloadTab(file);
            SessionManager.GCC();
        }
    }
}