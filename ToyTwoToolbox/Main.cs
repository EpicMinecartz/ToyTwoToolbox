using FolderDialog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {

    public partial class Main : Form {
        /// <summary>This tag specifies that the function requires extra support to prevent damage/incorrect usage</summary>
        public class Unsafe : Attribute { }
        public bool PrintMessages = false;
        public int LSID;
        public string wlid;
        public bool UnsavedWork;
        public bool SMSC = false;
        public TabController TabControl; //all the fun stuff in here
        public MessageFade MessageFader;

        public Main() {
            InitializeComponent();
            TabControl = new TabController(tabControl1);
            tabControl1.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, tabControl1, new object[] { true });
            menuStrip1.Renderer = new DarkThemeMenuRender();
            toolStrip1.Renderer = new DarkThemeMenuRender();
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            tabControl1.TabRequestDestroy += new TabRequestDestroyEventHandler(TabControl.TabRequestDestroy);
            tabControl1.SelectedIndexChanging += new SelectedTabPageChangeEventHandler(ProcessBaseTabControlState);
            this.firstOpenPanel1.OpenFile += new EventHandler(FOPM);
            this.firstOpenPanel1.CreateFile += new EventHandler(FOPM);
            this.toolStripComboBoxMatSel.SelectedIndex = 0;

            XF.CenterObject(firstOpenPanel1);
            MessageFader = new MessageFade(this);
            debugToolStripMenuItem.Visible = SessionManager.Debug;
        }

        private void ProcessBaseTabControlState(Object sender, TabPageChangeEventArgs e) {

            //i messed up, this invoke needs to go to TabController.CS and NOT T2TabControl.CS
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
                TabControl.CreateTab(FileProcessor.ProcessFile(path));
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
            CreateNewFile c = new CreateNewFile(this);
            c.StartPosition = FormStartPosition.CenterScreen;
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
                bool val = loadedNGN.Schema.NGNFunctions[i].FunctionOffset == validater.Schema.NGNFunctions[i].FunctionOffset;
                SessionManager.Report(Enum.GetName(typeof(F_NGN.NGNFunction), loadedNGN.Schema.NGNFunctions[i].FunctionType) + ((val) ? " successfully validated" : " did not validate succesfully"), ((val) ? SessionManager.RType.DEBUG : SessionManager.RType.WARN));//, Color.DarkGreen);
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
            TabControl.ReloadTab(FileProcessor.ProcessFile(TabControl.Tabs[tabControl1.SelectedIndex].File.FilePath));
            SessionManager.GCC();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (Globals.MultiMaterialTab != null) {
                //in this case we assume the tab for this editor is already open, so we can try to duplicate it
                Globals.MultiMaterialTab.Clone();
            }
            Globals.MultiMaterialTab = TabControl.CreateTab(overriedEditor: FileProcessor.EditorTypes.MultiMatEditor);
        }

        private void allOpenedLevelsToolStripMenuItem_Click(object sender, EventArgs e) {
            Globals.gMMaterials.AddRange(F_NGN.GetMaterialsFromLevels(TabControl.GetAllLevels(), toolStripComboBoxMatSel.SelectedIndex));
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e) {

        }

        private void multiMaterialEditorToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void allShapesInSelectedLevelToolStripMenuItem_Click(object sender, EventArgs e) {
            List<F_NGN> lev = new List<F_NGN>();
            lev.Add(TabControl.GetLevel(tabControl1.SelectedIndex));
            Globals.gMMaterials.AddRange(F_NGN.GetMaterialsFromLevels(lev, toolStripComboBoxMatSel.SelectedIndex));
        }

        private void fromSelectedShapeInSelectedLevelToolStripMenuItem_Click(object sender, EventArgs e) {
            //agh this is gonna be rough, we need a handle to the editor in the selected tab, to access to both the char and geom selectors
            //unlike in the other func, we cannot use GetMaterialsFromLevels, as this is too specific :(
            //get ready...
            T2Control_NGNEditor editor = ((T2Control_NGNEditor)TabControl.Tabs[tabControl1.SelectedIndex].editor.main);
            F_NGN file = (F_NGN)TabControl.Tabs[tabControl1.SelectedIndex].File;
            if (toolStripComboBoxMatSel.SelectedIndex == 0 || toolStripComboBoxMatSel.SelectedIndex == 1) {
                for (int i = 0;i < editor.listCharShapes.SelectedItems.Count;i++) {
                    Globals.gMMaterials.AddRange(file.characters[editor.comboCharacters.SelectedIndex].shapes[editor.listCharShapes.SelectedIndices[i]].materials);
                }
            }
            if (toolStripComboBoxMatSel.SelectedIndex == 0 || toolStripComboBoxMatSel.SelectedIndex == 2) {
                for (int i = 0;i < editor.listGeomShapes.SelectedItems.Count;i++) {
                    Globals.gMMaterials.AddRange(file.Geometries[editor.comboGeometry.SelectedIndex].shapes[editor.listGeomShapes.SelectedIndices[i]].materials);
                }
            }
        }

        private void openNewToolboxWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            SessionManager.SMptr.RequestNewSession();
        }

        public void RegisterMultiMaterial(Material mat) {
            Globals.gMMaterials.Add(mat);
        }

        /// <summary>This class holds any file data that needs to persist across tabs</summary>
        public sealed class Globals {
            /// <summary>Used for the Multi-Material Editor</summary>
            public static List<Material> gMMaterials = new List<Material>();
            public static TabController.TCTab MultiMaterialTab = null;

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("ToyTwoToolbox " + SessionManager.ver, "Hello :)", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void firstOpenPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void setAllShadingToColorToolStripMenuItem_Click(object sender, EventArgs e) {
            F_NGN lev = TabControl.GetLevel();
            if (lev != null) {
                ColorDialog cd = new ColorDialog { FullOpen = true };
                if (cd.ShowDialog() == DialogResult.OK) {
                    foreach (Geometry geom in lev.Geometries) {
                        foreach (Shape shape in geom.shapes) {
                            for (int i = 0;i < shape.rawVertexShading.Count;i++) {
                                shape.rawVertexShading[i] = cd.Color;
                            }
                        }
                    }
                }
            }
        }

        private void generateNameMapToolStripMenuItem_Click(object sender, EventArgs e) {
            F_NGN lev = TabControl.GetLevel();
            StringBuilder sb = new StringBuilder();
            if (lev != null) {
                Geometry geom = lev.Geometries[0];
                for (int i = 0;i < geom.shapes.Count;i++) {
                    if (i < 3) {
                        geom.shapes[i].name = "Buzz";
                    } else if (i > 2 && i < 8) {
                        geom.shapes[i].name = "OOB Token";

                    } else {
                        geom.shapes[i].name = "Geometry" + i;
                    }
                    sb.Append(i.ToString() + " : " + geom.shapes[i].name + "\n");
                }
            }
            Console.WriteLine(sb.ToString());
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            F_NGN lev = TabControl.GetLevel();
            if (lev != null) {
                ColorDialog cd = new ColorDialog { FullOpen = true };
                if (cd.ShowDialog() == DialogResult.OK) {
                    foreach (Character chr in lev.characters) {
                        foreach (Shape shape in chr.shapes) {
                            for (int i = 0;i < shape.rawVertexShading.Count;i++) {
                                shape.rawVertexShading[i] = cd.Color;
                                shape.rawVertexData[i] = new Vector3();
                            }
                        }
                    }
                }
            }
        }

        private void findMaterialsThatUseTextureOfNameToolStripMenuItem_Click(object sender, EventArgs e) {
            F_NGN lev = TabControl.GetLevel();
            if (lev != null) {
                InputDialog id = new InputDialog();
                List<string> tlocations = new List<string>();
                if (id.ShowDialog() == DialogResult.OK) {
                    string inp = id.input;
                    int reltexid = lev.TexNameToGlobalID(inp);
                    foreach (Character chr in lev.characters) {
                        foreach (Shape shape in chr.shapes) {
                            for (int i = 0;i < shape.materials.Count;i++) {
                                if (shape.materials[i].textureIndexRelative == reltexid) {
                                    tlocations.Add(String.Format("Found Texture : {0} > {1} > Material {2}", chr.name, (shape.name == "") ? "<Shape" + i + ">" : shape.name, i));
                                }
                            }
                        }
                    }
                    foreach (Geometry geo in lev.Geometries) {
                        for (int s = 0;s < geo.shapes.Count;s++) {
                            Shape shape = geo.shapes[s];
                            for (int i = 0;i < shape.materials.Count;i++) {
                                if (shape.materials[i].textureIndexRelative == reltexid) {
                                    tlocations.Add(String.Format("Found Texture : {0} > {1} > Material {2}", geo.name, (shape.name=="")? "<Shape" + i + ">" : shape.name, i));
                                }
                            }
                        }
                    }
                    SessionManager.Report("Search for "+inp+" in level returned " + tlocations.Count + " results :\n" + string.Join("\n", tlocations));
                }
            }
        }
    }
}