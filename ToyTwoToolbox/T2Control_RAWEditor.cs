using FolderDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ToyTwoToolbox {
    public partial class T2Control_RAWEditor : UserControl, IEditor {
        Shape[] ui_selectedCharShapes;
        public string tempName { get; set; }
        public string filePath { get; set; }
        public UserControl main { get; set; }
        public TabController.TCTab owner { get; set; }

        public T2Control_RAWEditor(F_BIN file = null) {
            InitializeComponent();
            this.DoubleBuffered = true;
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            contextTexture.Renderer = new DarkThemeMenuRender();
            this.Dock = DockStyle.Fill;
            this.main = this;
            if (file != null) { LoadFile(file); } //if this doesnt load in constructor, you will have to do it yourself later
        }

        /// <summary>
        /// Late init
        /// </summary>
        /// <param name="file">If a file is needed, then import it here</param>
        public void Init(F_Base file = null) {
            LoadFile((F_BIN)file);
        }


        F_BIN loadedRAW = null;
        List<Panel> colorTablePanels = new List<Panel>();
        public void LoadFile(F_BIN bin) {
            loadedRAW = bin;
            SessionManager.Report("Importing RAW contents into editor...");
            LoadTextureData();
            SessionManager.Report("Successfully Imported RAW contents into editor!");
        }

        public void updateListBoxItem(ListBox listBox, int index, string name) {
            listBox.Items[index] = name;
        }

        #region "Textures"

        public void LoadTextureData() {
            SessionManager.Report("[FDR@T2Control_RAWEditor -> LoadTextureData] Processing RAW texture");
            //first generate the image list for the texture viewer list and populate the imagelist with textures
            this.loadedRAW.Process();
            this.pictureTexture.Image = this.loadedRAW.RawBitmap;
            this.labelTextureInfo.Text = "Texture: " + System.IO.Path.GetFileNameWithoutExtension(loadedRAW.FilePath) + " Resolution: " + this.loadedRAW.RawBitmap.Size.ToString();
            this.splitContainer2.Panel2.Controls.Clear();
            RenderColorTable();
            SessionManager.Report("[FDR@T2Control_RAWEditor -> LoadTextureData] Processed");
        }

        void RenderColorTable() {
            int h;
            int colcount = 0;
            foreach (List<Color> colors in loadedRAW.ColorTables) {
                h = 0;
                foreach (Color color in colors) {
                    Panel cpanel = new Panel {
                        BackColor = color,
                        BorderStyle = BorderStyle.FixedSingle,
                        Location = new Point(16 * h, 16 * colcount),
                        Size = new Size(14, 14)
                    };
                    this.splitContainer2.Panel2.Controls.Add(cpanel);
                    
                    h++;
                }
                colcount++;
            }
        }

        private void butTextureImport_Click(object sender, EventArgs e) {
            //ImportTexture();
        }

        //public void ImportTexture(int ReplacementIndex = -1) {
        //    OpenFileDialog OFD = new OpenFileDialog {
        //        Filter = "Image Files | *.bmp;*.png;*.jpg;*.gif;*.tiff|BMP Image (.bmp)|*.bmp|GIF Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|PNG Image (.png)|*.png|TIFF Image (.tiff)|*.tiff|All files (*.*)|*.*"
        //    };
        //    if (OFD.ShowDialog() == DialogResult.OK) {
        //        Texture tex = new Texture {
        //            name = Path.GetFileNameWithoutExtension(OFD.FileName),
        //            image = (Bitmap)XF.ProParseImage(Image.FromFile(OFD.FileName))
        //        };
        //    }
        //}

        private void ToolStripMenuItem1_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog {
                AddExtension = true,
                FileName = System.IO.Path.GetFileNameWithoutExtension(this.loadedRAW.FilePath),
                DefaultExt = ".bmp",
                Filter = SessionManager.str_ImageFormatsStandard,
                Title = "Save texture"
            };
            if (SFD.ShowDialog() == DialogResult.OK) {
                XF.ExportImage(SFD.FileName, loadedRAW.RawBitmap, Path.GetExtension(SFD.FileName).ToLower());
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) {
            Clipboard.SetImage(loadedRAW.RawBitmap);
        }

        private void ToFileToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog {
                AddExtension = true,
                FileName = System.IO.Path.GetFileNameWithoutExtension(this.loadedRAW.FilePath) + "_a",
                DefaultExt = ".bmp",
                Filter = SessionManager.str_ImageFormatsStandard,
                Title = "Save texture"
            };

            if (SFD.ShowDialog() == DialogResult.OK) {
                Bitmap CLIPIMG = (Bitmap)loadedRAW.RawBitmap.Clone();
                XF.GenerateAlphaMap(CLIPIMG);
                //XF.ExportImage(SFD.FileName, CLIPIMG, ImageFormat.Bmp, Path.GetExtension(SFD.FileName).ToLower());
                CLIPIMG.Dispose();
            }

        }

        private void ToClipboardToolStripMenuItem_Click(object sender, EventArgs e) {
            Bitmap CLIPIMG = (Bitmap)loadedRAW.RawBitmap.Clone();
            XF.GenerateAlphaMap(CLIPIMG);
            Clipboard.SetImage(CLIPIMG);
            CLIPIMG.Dispose();
        }

        private void listviewTextures_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.Item.ListView != null) {
                if (e.IsSelected) {
                    e.Item.BackColor = Color.FromArgb(45, 45, 45);
                } else {
                    e.Item.BackColor = e.Item.ListView.BackColor;
                }
            }

        }

        public void ReselectListviewItem(ListView list) {
            int cslvi = list.SelectedIndices[0];
            list.SelectedItems.Clear();
            list.Items[cslvi].Selected = true;
        }

        private void butTextureSaveSelected_Click(object sender, EventArgs e) {
            FolderSelectDialog fsd = new FolderSelectDialog();
            if (fsd.ShowDialog() == true)
                    XF.ExportImage(fsd.FileName + "\\" + System.IO.Path.GetFileNameWithoutExtension(loadedRAW.FilePath) + ".bmp", loadedRAW.RawBitmap, "bmp");
        }

        #endregion


        private void T2Control_RAWEditor_Resize(object sender, EventArgs e) {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        public bool SaveChanges(bool inMemory, string path) {
            MessageBox.Show("Sorry, cant save this file yet!");
            return false;
        }
    }
}
