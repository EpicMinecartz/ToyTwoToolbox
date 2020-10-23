using FolderDialog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_NGNEditor : UserControl {
        public T2Control_NGNEditor(F_NGN file) {
            InitializeComponent();
            this.DoubleBuffered = true;
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            contextTexture.Renderer = new DarkThemeMenuRender();
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            dvgAP.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, dvgAP, new object[] { true });

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
            LoadShapeLinks();
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
                    goto lmao;
                }
            }
        lmao: //bug 23/10/2020 - using goto for now to allow for both save and saveas to work cuz im a dumbo, fix soon
            if (JustMemory == false) {
                return loadedNGN.Export((path == null) ? loadedNGN.FilePath : path);
            }
            return false;
        }

        private void CharShapeEditor_ReportShapeNameUpdate(object sender, string text) {
            updateListBoxItem(listCharShapes, listCharShapes.SelectedIndex, text);
        }


        public void updateListBoxItem(ListBox listBox, int index, string name) {
            listBox.Items[index] = name;
        }

        #region "Textures"

        public void LoadTextureData() {
            //first generate the image list for the texture viewer list and populate the imagelist with textures
            if (loadedNGN.textures.Count > 0) {
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
                listviewTextures.Items[0].Selected = true;
            } else {

            }

        }

        private void listviewTextures_SelectedIndexChanged(object sender, EventArgs e) {
            if (listviewTextures.SelectedIndices.Count > 0) {
                Texture tex = loadedNGN.textures[listviewTextures.SelectedIndices[0]];
                pictureTexture.Image = tex.image;
                labelTextureInfo.Text = "Texture: " + tex.name + " Resolution: " + tex.image.Size.ToString() + " Index: " + listviewTextures.SelectedIndices[0];
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
            if (loadedNGN.textures.Count > 0) { listviewTextures.Items[listviewTextures.Items.Count - 1].Selected = true; } else { pictureTexture.Image = null; labelTextureInfo.Text = "Texture: <none> Resolution: 0x0 Index: 0"; }
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
                Filter = SessionManager.str_ImageFormatsStandard,
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
                Filter = SessionManager.str_ImageFormatsStandard,
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
            if (e.Label != null) {
                loadedNGN.textures[e.Item].name = e.Label;
            }
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

        private void butTextureSaveAll_Click(object sender, EventArgs e) {
            SaveTextures();
        }

        private void butTextureSaveSelected_Click(object sender, EventArgs e) {
            SaveTextures(listviewTextures.SelectedIndices.Cast<int>().ToArray());
        }

        public void SaveTextures(int[] textureIDs = null) {
            if (textureIDs == null || textureIDs.Length == 0) { textureIDs = Enumerable.Range(0, loadedNGN.textures.Count).ToArray(); }
            FolderSelectDialog fsd = new FolderSelectDialog();
            if (fsd.ShowDialog() == true) {
                for (int i = 0;i < textureIDs.Length;i++) {
                    XF.ExportImage(fsd.FileName + "\\" + loadedNGN.textures[i].name + ".bmp", loadedNGN.textures[i].image, ImageFormat.Bmp);
                }
            }
        }


        #endregion

        #region "Characters"

        public void LoadCharacterData() {
            //well, i guess we only gotta populate the character list, the rest is on select basis
            comboCharacters.Items.Clear();
            if (loadedNGN.characters.Count > 0) {
                foreach (Character chr in loadedNGN.characters) {
                    comboCharacters.Items.Add(chr.name);
                }
                comboCharacters.SelectedIndex = 0;
            }
        }

        public void LoadCharacter(int id) {
            listCharShapes.Items.Clear();
            CharEditor.Visible = true;
            comboAnimID.Items.Clear();
            if (loadedNGN.characters[comboCharacters.SelectedIndex].Anims.Count > 0) {
                for (int i = 0;i < loadedNGN.characters[comboCharacters.SelectedIndex].Anims.Count;i++) {
                    comboAnimID.Items.Add("Animation " + i);
                }
                comboAnimID.SelectedIndex = 0;
            }


            if (loadedNGN.characters[id].model.shapes.Count > 0) {
                for (int i = 0;i < loadedNGN.characters[id].model.shapes.Count;i++) {
                    listCharShapes.Items.Add((loadedNGN.characters[id].model.shapes[i].name == "") ? "<Shape " + i.ToString().PadLeft(2, '0') + ">" : loadedNGN.characters[id].model.shapes[i].name);
                }
                listCharShapes.SelectedIndex = 0;
            }
        }

        private void comboCharacters_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboCharacters.SelectedIndex != -1) {
                LoadCharacter(comboCharacters.SelectedIndex);
            } else {
                CharEditor.Visible = false;
            }
        }

        private void listShapes_SelectedIndexChanged(object sender, EventArgs e) {
            //LoadCharShape(comboCharacters.SelectedIndex, listCharShapes.SelectedIndex);
            if (listCharShapes.SelectedIndex != -1) {
                CharShapeEditor.Visible = true;
                Shape shp = loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes[listCharShapes.SelectedIndex];
                CharShapeEditor.ImportShape(ref shp, ref loadedNGN, listCharShapes.SelectedItem.ToString());
            } else {
                CharShapeEditor.Visible = false;
            }

        }

        //private void numericMaterialID_ValueChanged(object sender, EventArgs e) {
        //    Shape shape = loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes[listCharShapes.SelectedIndex];
        //    groupMaterialProperties.Enabled = (numericMaterialID.Value != -1);
        //    if (numericMaterialID.Value != -1) {
        //        Material mat = shape.materials[(int)numericMaterialID.Value];
        //        List<double> AmbColor = mat.RGB;
        //        butAmbColorPicker.BackColor = Color.FromArgb((int)AmbColor[0], (int)AmbColor[1], (int)AmbColor[2]);
        //        comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535) ? mat.textureIndex : 0;

        //    }
        //}


        private void butNewChar_Click(object sender, EventArgs e) {
            loadedNGN.characters.Add(new Character { name = "Untitled" });
            comboCharacters.Items.Add("Untitled");
            comboCharacters.SelectedIndex = comboCharacters.Items.Count - 1;
        }
        private void butRemoveChar_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this character?", "Character remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.characters.RemoveAt(comboCharacters.SelectedIndex);
                comboCharacters.Items.RemoveAt(comboCharacters.SelectedIndex);
            }
        }
        private void butImportChar_Click(object sender, EventArgs e) {
            Importer i = new Importer();
            if (i.ShowDialog() == DialogResult.OK) {
                loadedNGN.characters.Add(i.ImportedCharacter);
                comboCharacters.Items.Add(i.ImportedCharacter.name);
                comboCharacters.SelectedIndex = comboCharacters.Items.Count - 1;
            }
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

        private void comboAnimID_SelectedIndexChanged(object sender, EventArgs e) {
            //comboNodeID.Items.Clear();
            //if (comboCharacters.SelectedIndex != -1) {
            //    for (int i = 0;i < loadedNGN.characters[comboCharacters.SelectedIndex].Anims[comboAnimID.SelectedIndex].Nodes.Count;i++) {
            //        comboNodeID.Items.Add("Node " + i);
            //    }
            //    comboNodeID.SelectedIndex = 0;
            //}
            fieldChunk.Text = XF.BytesToHex(loadedNGN.characters[comboCharacters.SelectedIndex].Anims[comboAnimID.SelectedIndex].foverride);
        }

        private void comboNodeID_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboNodeID.SelectedIndex != -1) {
                int framecount = loadedNGN.characters[comboCharacters.SelectedIndex].Anims[comboCharacters.SelectedIndex].Nodes[comboNodeID.SelectedIndex].frames.Count;
                dgvAnimationData.Rows.Clear();
                if (framecount > 0) {
                    dgvAnimationData.Rows.Add(framecount);
                    for (int i = 0;i < framecount;i++) {
                        DataGridViewRow DGVRow = dgvAnimationData.Rows[i];
                        AnimationFrame frame = loadedNGN.characters[comboCharacters.SelectedIndex].Anims[comboCharacters.SelectedIndex].Nodes[comboNodeID.SelectedIndex].frames[i];
                        DGVRow.Cells[0].Value = frame.Position.X;
                        DGVRow.Cells[1].Value = frame.Position.Y;
                        DGVRow.Cells[2].Value = frame.Position.Z;
                        DGVRow.Cells[3].Value = frame.Rotation.X;
                        DGVRow.Cells[4].Value = frame.Rotation.Y;
                    }
                }
            }
        }

        #endregion

        #region "Geometry"
        public void LoadGeometryData() {
            //well, i guess we only gotta populate the geom list, the rest is on select basis
            comboGeometry.Items.Clear();
            if (loadedNGN.Geometries.Count > 0) {
                int i = 0;
                foreach (Geometry geom in loadedNGN.Geometries) {
                    comboGeometry.Items.Add(geom.name == "" ? "Geom " + i.ToString().PadLeft(2, '0') : geom.name);
                    i++;
                }
                comboGeometry.SelectedIndex = 0;
            }
        }


        public void LoadGeometry(int id) {
            listGeomShapes.Items.Clear();
            GeomEditor.Visible = true;
            GeomShapeEditor.Clear(true);
            if (loadedNGN.Geometries.Count > 0) {
                if (loadedNGN.Geometries[id].shapes.Count > 0) {
                    for (int i = 0;i < loadedNGN.Geometries[id].shapes.Count;i++) {
                        listGeomShapes.Items.Add((loadedNGN.Geometries[id].shapes[i].name == "") ? "Shape " + i.ToString().PadLeft(2, '0') : loadedNGN.Geometries[id].shapes[i].name);
                    }
                    GeomShapeEditor.Visible = true;
                    listGeomShapes.SelectedIndex = 0;
                }
                if (loadedNGN.GScales.Count > 0) {
                    DynamicScaler ds = loadedNGN.GScales[id];
                    dgvDS.Rows.Clear();
                    if (ds.Translation.Count > 0) {
                        dgvDS.Rows.Add(ds.Translation.Count);
                        for (int i = 0;i < ds.Translation.Count;i++) {
                            DataGridViewRow DGVRow = dgvDS.Rows[i];
                            DGVRow.Cells[0].Value = ds.ShapeID[i];
                            DGVRow.Cells[1].Value = ds.Translation[i].X;
                            DGVRow.Cells[2].Value = ds.Translation[i].Y;
                            DGVRow.Cells[3].Value = ds.Translation[i].Z;
                            DGVRow.Cells[4].Value = ds.Rotation[i].X;
                            DGVRow.Cells[5].Value = ds.Rotation[i].Y;
                            DGVRow.Cells[6].Value = ds.Rotation[i].Z;
                            DGVRow.Cells[7].Value = ds.Scale[i].X;
                            DGVRow.Cells[8].Value = ds.Scale[i].Y;
                            DGVRow.Cells[9].Value = ds.Scale[i].Z;
                            DGVRow.Cells[10].Value = ds.Unknown[i];
                        }
                    }
                }
            }
        }

        private void comboGeometry_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboGeometry.SelectedIndex != -1) {
                LoadGeometry(comboGeometry.SelectedIndex);
            } else {
                GeomEditor.Visible = false;
            }

        }

        private void listGeomShapes_SelectedIndexChanged(object sender, EventArgs e) {
            if (listGeomShapes.SelectedIndex != -1) {
                GeomShapeEditor.Visible = true;
                Shape shp = loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes[listGeomShapes.SelectedIndex];
                GeomShapeEditor.ImportShape(ref shp, ref loadedNGN);
            } else {
                GeomShapeEditor.Visible = false;
            }
        }

        private void butNewGeomShape_Click(object sender, EventArgs e) {
            loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes.Add(new Shape());
            listGeomShapes.Items.Add("<Shape " + (listGeomShapes.Items.Count + 1) + ">");
            listGeomShapes.SelectedIndex = listGeomShapes.Items.Count;
            //LoadGeometry(comboGeometry.SelectedIndex);
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
            comboGeometry.SelectedIndex = comboGeometry.Items.Count - 1;
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
            if (loadedNGN.areaPortals.Count > 0) {
                for (int i = 0;i < loadedNGN.areaPortals.Count;i++) {
                    listAreaPortals.Items.Add("Area Portal " + i);
                }
                listAreaPortals.SelectedIndex = 0;
            }
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
            int si = listAreaPortals.SelectedIndex;
            XF.ListMoveItem(loadedNGN.areaPortals, listAreaPortals.SelectedIndices[0], -1);
            XF.ListMoveItem(listAreaPortals.Items, listAreaPortals.SelectedIndices[0], -1);
            if (listAreaPortals.SelectedIndex > 0) { listAreaPortals.SelectedIndex--; }
        }

        private void butMoveAreaPortalDown_Click(object sender, EventArgs e) {

        }

        private void listAreaPortals_SelectedIndexChanged(object sender, EventArgs e) {
            dvgAP.Rows.Clear();
            if (listAreaPortals.SelectedItems.Count > 0 && loadedNGN.areaPortals[listAreaPortals.SelectedIndex].Vertices.Count > 0) {
                dvgAP.Rows.Add(loadedNGN.areaPortals[listAreaPortals.SelectedIndex].Vertices.Count);
                for (int i = 0;i < loadedNGN.areaPortals[listAreaPortals.SelectedIndex].Vertices.Count;i++) {
                    DataGridViewRow DGVRow = dvgAP.Rows[i];
                    Vector3 v = loadedNGN.areaPortals[listAreaPortals.SelectedIndex].Vertices[i];
                    DGVRow.Cells[0].Value = v.X;
                    DGVRow.Cells[1].Value = v.Y;
                    DGVRow.Cells[2].Value = v.Z;
                }
            }
        }
        #endregion

        #region "Shape Links"

        public void LoadShapeLinks() {
            ShapeLinkEditor.Rows.Clear();
            if (loadedNGN.ObjectLinks.Count > 0) {
                ShapeLinkEditor.Rows.Add(loadedNGN.ObjectLinks.Count);
                for (int i = 0;i < loadedNGN.ObjectLinks.Count;i++) {
                    DataGridViewRow DGVRow = ShapeLinkEditor.Rows[i];
                    Linker l = loadedNGN.ObjectLinks[i];
                    DGVRow.Cells[0].Value = l.ShapeID;
                    DGVRow.Cells[1].Value = l.LinkID;
                }
            }
        }

        private void butNewShapeLink_Click(object sender, EventArgs e) {

        }

        private void butRemoveShapeLink_Click(object sender, EventArgs e) {

        }

        private void butShapeLinkMoveUp_Click(object sender, EventArgs e) {

        }

        private void butShapeLinkMoveDown_Click(object sender, EventArgs e) {
            Importer mp = new Importer();
            mp.Show();
        }

        #endregion

        private void butPasteCharShapeData_Click(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            var window = new Render(800, 600, "Render Preview", loadedNGN.textures[10].image, XF.CompileVirtualOBJ(loadedNGN.characters[comboCharacters.SelectedIndex].model.shapes));
            window.Run(60.0);
        }

        private void GeomShapeEditor_Load(object sender, EventArgs e) {

        }

        private void butSaveChar_Click(object sender, EventArgs e) {
            loadedNGN.ExtractModel(loadedNGN.characters[comboCharacters.SelectedIndex], typeof(Character), true, true, true);
        }

        private void butExportGeomData_Click(object sender, EventArgs e) {

        }
    }
}
