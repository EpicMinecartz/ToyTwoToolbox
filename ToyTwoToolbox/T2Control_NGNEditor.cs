using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ToyTwoToolbox {
    public partial class T2Control_NGNEditor : UserControl {
        public T2Control_NGNEditor(F_NGN file) {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            contextTexture.Renderer = new DarkThemeMenuRender();
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            dgvChar.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvChar.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvChar.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvAnimationData.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvAnimationData.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvAnimationData.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvGeometry.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvGeometry.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dgvGeometry.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;




            this.Dock = DockStyle.Fill;
            LoadFile(file);
        }

        F_NGN loadedNGN = null;
        public void LoadFile(F_NGN ngn) {
            loadedNGN = ngn;
            //seperate functions handle each tab, that way they can be called in the future to "reset" a tab
            LoadTextureData();
            LoadCharacterData();
            LoadGeometryData();
            LoadAreaPortals();
        }

        public bool SaveChanges(bool JustMemory = false, string path = "") {
            if (path == "" || path == null) {
                SaveFileDialog SFD = new SaveFileDialog { 
                    Filter = "Level File | *.NGN|All files (*.*)|*.*", 
                    DefaultExt = ".ngn", 
                    FileName = (loadedNGN.FilePath == null) ? loadedNGN.TempName : System.IO.Path.GetFileNameWithoutExtension(loadedNGN.FilePath) 
                };
                if (SFD.ShowDialog() == DialogResult.OK) {
                    path = SFD.FileName;
                    if (JustMemory == false) {
                        return loadedNGN.Export((path == null) ? loadedNGN.FilePath : path);
                    }
                }
            }
            return false;
        }

        #region "Textures"

        public void LoadTextureData() {
            //first generate the image list for the texture viewer list and populate the imagelist with textures
            ImageList TIL = new ImageList {
                ImageSize = new Size(64, 64),
                ColorDepth = ColorDepth.Depth32Bit
            };
            for (int i = 0;i < loadedNGN.textures.Count;i++) {
                TIL.Images.Add(loadedNGN.textures[i].image);
                listviewTextures.Items.Add(new ListViewItem {
                    ImageIndex = i,
                    Text = loadedNGN.textures[i].name
                });
            }
            listviewTextures.SmallImageList = TIL;
            listviewTextures.LargeImageList = TIL;
            listviewTextures.Columns[0].Width = -2;
        }

        private void listviewTextures_SelectedIndexChanged(object sender, EventArgs e) {
            if (listviewTextures.SelectedIndices.Count > 0) {
                Texture tex = loadedNGN.textures[listviewTextures.SelectedIndices[0]];
                pictureTexture.Image = tex.image;
                labelTextureInfo.Text = "Texture: "+tex.name+" Resolution: " + tex.image.Size.ToString() + " Index: " + listviewTextures.SelectedIndices[0];
            }
        }

        private void butTextureImport_Click(object sender, EventArgs e) {
            ImportTexture();
        }

        public void ImportTexture(int ReplacementIndex = -1) {
            OpenFileDialog OFD = new OpenFileDialog {
                Filter = "Image Files | *.bmp;*.png;*.jpg;*.gif;*.tiff|BMP Image (.bmp)|*.bmp|GIF Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|PNG Image (.png)|*.png|TIFF Image (.tiff)|*.tiff|All files (*.*)|*.*"
            };
            if (OFD.ShowDialog() == DialogResult.OK) {
                Texture tex = new Texture {
                    name = Path.GetFileNameWithoutExtension(OFD.FileName),
                    image = (Bitmap)XF.ProParseImage(Image.FromFile(OFD.FileName))
                };
                if (ReplacementIndex != -1) {
                    loadedNGN.textures[ReplacementIndex].image = tex.image;
                    listviewTextures.LargeImageList.Images[ReplacementIndex] = tex.image;

                    listviewTextures.SelectedItems.Clear();
                    listviewTextures.Items[ReplacementIndex].Selected = true;
                } else {
                    loadedNGN.textures.Add(tex);
                    listviewTextures.LargeImageList.Images.Add(tex.image);
                    listviewTextures.Items.Add(
                        new ListViewItem {
                            ImageIndex = loadedNGN.textures.Count - 1,
                            Text = tex.name
                        }
                    );
                    listviewTextures.Items[listviewTextures.Items.Count - 1].Selected = true;
                }
            }
        }

        private void butTextureRemove_Click(object sender, EventArgs e) {
            loadedNGN.textures.RemoveAt(listviewTextures.SelectedIndices[0]);
            listviewTextures.Items.RemoveAt(listviewTextures.SelectedIndices[0]);
            if (loadedNGN.textures.Count > 0) { listviewTextures.Items[listviewTextures.Items.Count - 1].Selected = true; } else { pictureTexture.Image = null; }
        }

        private void butTextureMoveUp_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.textures, listviewTextures.SelectedIndices[0], -1);
            XF.ListMoveItem(listviewTextures.Items, listviewTextures.SelectedIndices[0], -1);
        }

        private void butTextureMoveDown_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.textures, listviewTextures.SelectedIndices[0], 1);
            XF.ListMoveItem(listviewTextures.Items, listviewTextures.SelectedIndices[0], 1);
        }


        private void ToolStripMenuItem1_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog {
                AddExtension = true,
                FileName = loadedNGN.textures[listviewTextures.SelectedIndices[0]].name,
                DefaultExt = ".bmp",
                Filter = "Bitmap Files (*.bmp)|*.bmp|Portable Network Graphic Files (*.png)|*.png|Joint Photographic Experts Group Files (*.jpg)|*.jpg|Graphics Interchange Format Files (*.gif)|*.gif|All Files (*.*)|*.*",
                Title = "Save texture"
            };
            if (SFD.ShowDialog() == DialogResult.OK) {
                XF.ExportImage(SFD.FileName, loadedNGN.textures[listviewTextures.SelectedIndices[0]].image, ImageFormat.Bmp, Path.GetExtension(SFD.FileName).ToLower());
            }
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e) {
            ImportTexture(listviewTextures.SelectedIndices[0]);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) {
            Clipboard.SetImage(loadedNGN.textures[listviewTextures.SelectedItems[0].Index].image);
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to replace this texture with clipboard contents?", "Image Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                int cslvi = listviewTextures.SelectedIndices[0];
                Bitmap bmp = (Bitmap)Clipboard.GetImage();
                loadedNGN.textures[cslvi].image = bmp;
                listviewTextures.LargeImageList.Images[cslvi] = bmp;

                ReselectListviewItem(listviewTextures);
            }
        }

        private void ToFileToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog SFD = new SaveFileDialog {
                AddExtension = true,
                FileName = loadedNGN.textures[listviewTextures.SelectedIndices[0]].name + "_a",
                DefaultExt = ".bmp",
                Filter = "Bitmap Files (*.bmp)|*.bmp|Portable Network Graphic Files (*.png)|*.png|Joint Photographic Experts Group Files (*.jpg)|*.jpg|Graphics Interchange Format Files (*.gif)|*.gif|All Files (*.*)|*.*",
                Title = "Save texture"
            };

            if (SFD.ShowDialog() == DialogResult.OK) {
                Bitmap CLIPIMG = (Bitmap)loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.Clone();
                XF.GenerateAlphaMap(CLIPIMG);
                XF.ExportImage(SFD.FileName, CLIPIMG, ImageFormat.Bmp, Path.GetExtension(SFD.FileName).ToLower());
                CLIPIMG.Dispose();
            }

        }

        private void ToClipboardToolStripMenuItem_Click(object sender, EventArgs e) {
            Bitmap CLIPIMG = (Bitmap)loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.Clone();
            XF.GenerateAlphaMap(CLIPIMG);
            Clipboard.SetImage(CLIPIMG);
            CLIPIMG.Dispose();
        }

        private void RotateCWToolStripMenuItem_Click(object sender, EventArgs e) {
            loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            listviewTextures.LargeImageList.Images[listviewTextures.SelectedIndices[0]].RotateFlip(RotateFlipType.Rotate90FlipNone);
            ReselectListviewItem(listviewTextures);
        }

        private void RotateCCWToolStripMenuItem_Click(object sender, EventArgs e) {
            loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            listviewTextures.LargeImageList.Images[listviewTextures.SelectedIndices[0]].RotateFlip(RotateFlipType.Rotate270FlipNone);
            ReselectListviewItem(listviewTextures);
        }

        private void FlipHorizontallyToolStripMenuItem_Click(object sender, EventArgs e) {
            loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            listviewTextures.LargeImageList.Images[listviewTextures.SelectedIndices[0]].RotateFlip(RotateFlipType.RotateNoneFlipX);
            ReselectListviewItem(listviewTextures);
        }

        private void FlipVerticallyToolStripMenuItem_Click(object sender, EventArgs e) {
            loadedNGN.textures[listviewTextures.SelectedIndices[0]].image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            listviewTextures.LargeImageList.Images[listviewTextures.SelectedIndices[0]].RotateFlip(RotateFlipType.RotateNoneFlipY);
            ReselectListviewItem(listviewTextures);
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) {
            butTextureRemove.PerformClick();
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e) {
            butTextureMoveUp.PerformClick();
        }

        private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e) {
            butTextureMoveDown.PerformClick();
        }

        private void contextTexture_Opening(object sender, CancelEventArgs e) {
            if (listviewTextures.SelectedItems.Count < 1) {
                e.Cancel = true;
            }
        }
        private void listviewTextures_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            loadedNGN.textures[listviewTextures.SelectedIndices[0]].name = e.Label;
        }

        private void listviewTextures_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            if (e.IsSelected) {
                e.Item.BackColor = Color.FromArgb(45,45,45);
            } else {
                e.Item.BackColor = e.Item.ListView.BackColor;
            }
        }

        public void ReselectListviewItem(ListView list) {
            int cslvi = list.SelectedIndices[0];
            list.SelectedItems.Clear();
            list.Items[cslvi].Selected = true;
        }


        #endregion

        #region "Characters"

        public void LoadCharacterData() {
            //well, i guess we only gotta populate the character list, the rest is on select basis
            comboCharacters.Items.Clear();
            foreach (Character chr in loadedNGN.characters) {
                comboCharacters.Items.Add(chr.name);
            }
            comboCharacters.SelectedIndex = 0;
        }

        public void LoadCharacter(int id) {
            listCharShapes.Items.Clear();
            for (int i = 0;i < loadedNGN.characters[id].model.shapes.Count;i++) {
                listCharShapes.Items.Add((loadedNGN.characters[id].model.shapes[i].name == "") ? "Shape " + i.ToString().PadLeft(2,'0') : loadedNGN.characters[id].model.shapes[i].name);
            }
        }

        public void LoadCharShape(int charID, int shapeID) {
            Shape shape = loadedNGN.characters[charID].model.shapes[shapeID];
            fieldShapeName.Text = shape.name;
            numericCharShapeID.Value = shape.type;
            numericCharShapeID2.Value = shape.type2;
            numericMaterialID.Value = (shape.materials.Count > 0) ? 0 : -1;
            comboMaterialTexture.Items.Clear();
            foreach (Texture tex in loadedNGN.textures) {
                comboMaterialTexture.Items.Add(tex.name);
            }
            comboMaterialTexture.SelectedIndex = 0;
        }

        private void comboCharacters_SelectedIndexChanged(object sender, EventArgs e) {
            LoadCharacter(comboCharacters.SelectedIndex);
        }

        private void listShapes_SelectedIndexChanged(object sender, EventArgs e) {
            LoadCharShape(comboCharacters.SelectedIndex, listCharShapes.SelectedIndex);
            
        }

        private void numericMaterialID_ValueChanged(object sender, EventArgs e) {
            Shape shape = loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes[listCharShapes.SelectedIndex];
            groupMaterialProperties.Enabled = (numericMaterialID.Value != -1);
            if (numericMaterialID.Value != -1) {
                Material mat = shape.materials[(int)numericMaterialID.Value];
                List<double> AmbColor = mat.RGB;
                butAmbColorPicker.BackColor = Color.FromArgb((int)AmbColor[0], (int)AmbColor[1], (int)AmbColor[2]);
                comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535) ? mat.textureIndex : 0;

            }
        }


        private void butNewChar_Click(object sender, EventArgs e) {
            loadedNGN.characters.Add(new Character { name = "Untitled" });
            comboCharacters.Items.Add("Untitled");
        }
        private void butRemoveChar_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this character?", "Character remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.characters.RemoveAt(comboCharacters.SelectedIndex);
                comboCharacters.Items.RemoveAt(comboCharacters.SelectedIndex);
            }
        }
        private void butImportChar_Click(object sender, EventArgs e) {

        }
        private void butNewShape_Click(object sender, EventArgs e) {
            loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes.Add(new Shape());
            LoadCharacter(comboCharacters.SelectedIndex);
        }
        private void butMoveShapeDown_Click(object sender, EventArgs e) {

        }
        private void butMoveShapeUp_Click(object sender, EventArgs e) {

        }
        private void butRemoveShape_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this shape?", "Character remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes.RemoveAt(listCharShapes.SelectedIndex);
                listCharShapes.Items.RemoveAt(listCharShapes.SelectedIndex);
            }
        }
        private void butImportShape_Click(object sender, EventArgs e) {

        }

        #endregion

        #region "Geometry"
        public void LoadGeometryData() {
            //well, i guess we only gotta populate the character list, the rest is on select basis
            comboGeometry.Items.Clear();
            foreach (Geometry geom in loadedNGN.Geometries) {
                comboGeometry.Items.Add(geom.name);
            }
            comboGeometry.SelectedIndex = 0;
        }


        public void LoadGeometry(int id) {
            listGeomShapes.Items.Clear();
            for (int i = 0;i < loadedNGN.Geometries[id].shapes.Count;i++) {
                listGeomShapes.Items.Add((loadedNGN.Geometries[id].shapes[i].name == "") ? "Shape " + i.ToString().PadLeft(2, '0') : loadedNGN.Geometries[id].shapes[i].name);
            }
        }

        public void LoadGeomShape(int charID, int shapeID) {
            Shape shape = loadedNGN.Geometries[charID].shapes[shapeID];
            fieldGeomShapeName.Text = shape.name;
            numericGeomShapeID.Value = shape.type;
            numericGeomShapeID2.Value = shape.type2;
            numericGeomMaterialID.Value = (shape.materials.Count > 0) ? 0 : -1;
            comboGeomMaterialTexture.Items.Clear();
            foreach (Texture tex in loadedNGN.textures) {
                comboGeomMaterialTexture.Items.Add(tex.name);
            }
            comboGeomMaterialTexture.SelectedIndex = 0;
        }

        private void comboGeometry_SelectedIndexChanged(object sender, EventArgs e) {
            LoadGeometry(comboGeometry.SelectedIndex);
        }

        private void listGeomShapes_SelectedIndexChanged(object sender, EventArgs e) {
            LoadGeomShape(comboGeometry.SelectedIndex, listGeomShapes.SelectedIndex);
        }

        private void butNewGeomShape_Click(object sender, EventArgs e) {
            loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes.Add(new Shape());
            LoadCharacter(comboGeometry.SelectedIndex);
        }

        private void butImportGeomShape_Click(object sender, EventArgs e) {

        }

        private void butRemoveGeomShape_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this shape?", "Geometry shape remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes.RemoveAt(listGeomShapes.SelectedIndex);
                listGeomShapes.Items.RemoveAt(listGeomShapes.SelectedIndex);
            }
        }

        private void butMoveGeomShapeUp_Click(object sender, EventArgs e) {

        }

        private void butMoveGeomShapeDown_Click(object sender, EventArgs e) {

        }

        private void butNewGeometry_Click(object sender, EventArgs e) {
            loadedNGN.Geometries.Add(new Geometry { name = "Untitled" });
            comboGeometry.Items.Add("Untitled");
        }

        private void butImportGeometry_Click(object sender, EventArgs e) {

        }

        private void butRemoveGeometry_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this geometry?", "Geometry remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.Geometries.RemoveAt(comboGeometry.SelectedIndex);
                comboGeometry.Items.RemoveAt(comboGeometry.SelectedIndex);
            }
        }

        private void butNewGeomMaterial_Click(object sender, EventArgs e) {

        }

        private void butRemoveGeomMaterial_Click(object sender, EventArgs e) {

        }

        #endregion

        #region "Area Portals"
        public void LoadAreaPortals() {
            listAreaPortals.Items.Clear();
            for (int i = 0;i < loadedNGN.areaPortals.Count;i++) {
                listAreaPortals.Items.Add("Area Portal " + i);
            }
            listAreaPortals.SelectedIndex = 0;
        }


        private void butNewAreaPortal_Click(object sender, EventArgs e) {
            loadedNGN.areaPortals.Add(new AreaPortal());
            listAreaPortals.Items.Add("Area Portal " + (loadedNGN.areaPortals.Count + 1));
        }

        private void butRemoveAreaPortal_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this area portal?", "Area Portal remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.areaPortals.RemoveAt(listAreaPortals.SelectedIndex);
                listAreaPortals.Items.RemoveAt(listAreaPortals.SelectedIndex);
            }
        }

        private void butMoveAreaPortalUp_Click(object sender, EventArgs e) {

        }

        private void butMoveAreaPortalDown_Click(object sender, EventArgs e) {

        }


        #endregion

        private void numericAnimationID_ValueChanged(object sender, EventArgs e) {

        }

        private void numericNodeID_ValueChanged(object sender, EventArgs e) {
            foreach (AnimationFrame frame in loadedNGN.characters[comboCharacters.SelectedIndex].Anims[(int)numericAnimationID.Value].Nodes[(int)numericNodeID.Value].frames) {
                dgvAnimationData.Rows.Add(1);
                DataGridViewRow DGVRow = (DataGridViewRow)dgvAnimationData.Rows[dgvAnimationData.Rows.Count - 1];
                DGVRow.Cells[0].Value = frame.Position.X;
                DGVRow.Cells[1].Value = frame.Position.Y;
                DGVRow.Cells[2].Value = frame.Position.Z;
                DGVRow.Cells[3].Value = frame.Rotation.X;
                DGVRow.Cells[4].Value = frame.Rotation.Y;
            }
        }

        private void listAreaPortals_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (Vector3 position in loadedNGN.areaPortals[listAreaPortals.SelectedIndex].Vertices) {
                dgvAnimationData.Rows.Add(1);
                DataGridViewRow DGVRow = (DataGridViewRow)dvgAP.Rows[dvgAP.Rows.Count - 1];
                DGVRow.Cells[0].Value = position.X;
                DGVRow.Cells[1].Value = position.Y;
                DGVRow.Cells[2].Value = position.Z;
            }
        }
    }
}
