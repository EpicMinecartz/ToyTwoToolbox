using FolderDialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_NGNEditor : UserControl, IEditor {
        Shape[] ui_selectedCharShapes;
        public string tempName { get; set; }
        public string filePath { get; set; }
        public UserControl main { get; set; }
        public TabController.TCTab owner { get; set; }

        public T2Control_NGNEditor(F_NGN file = null) {
            InitializeComponent();
            this.DoubleBuffered = true;
            tabControl1.DrawItem += new DrawItemEventHandler(DarkThemeTabControlRender.tabControl_DrawItem);
            contextTexture.Renderer = new DarkThemeMenuRender();
            contextCharShapes.Renderer = new DarkThemeMenuRender();
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listviewTextures.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            dvgAP.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            dvgAP.GetType().InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, dvgAP, new object[] { true });
            CharShapeEditor.owner = this;
            GeomShapeEditor.owner = this;
            this.Dock = DockStyle.Fill;
            this.main = this;
            if (file != null) { LoadFile(file); } //if this doesnt load in constructor, you will have to do it yourself later
            checkBox1.Visible = SessionManager.Debug;
        }

        /// <summary>
        /// Late init
        /// </summary>
        /// <param name="file">If a file is needed, then import it here</param>
        public void Init(F_Base file = null) {
            LoadFile((F_NGN)file);
        }


        F_NGN loadedNGN = null;
        public void LoadFile(F_NGN ngn) {
            loadedNGN = ngn;
            SessionManager.Report("Importing NGN contents into editor...");
            //seperate functions handle each tab, that way they can be called in the future to "reset" a tab
            LoadTextureData();
            LoadCharacterData();
            LoadGeometryData();
            LoadAreaPortals();
            LoadShapeLinks();
            SessionManager.Report("Successfully Imported NGN contents into editor!");
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
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadTextureData] Processing NGN textures");
            //first generate the image list for the texture viewer list and populate the imagelist with textures
            if (loadedNGN.textures.Count > 0) {
                t2Control_TextureSelector1.Init(loadedNGN.textures);
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
            }
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadTextureData] Processed " + loadedNGN.textures.Count + " textures");
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
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadCharacterData] Processing NGN characters");
            //well, i guess we only gotta populate the character list, the rest is on select basis
            comboCharacters.Items.Clear();
            if (loadedNGN.characters.Count > 0) {
                foreach (Character chr in loadedNGN.characters) {
                    comboCharacters.Items.Add(chr.name);
                }
                comboCharacters.SelectedIndex = 0;
            }
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadCharacterData] Procesed " + loadedNGN.characters.Count + " characters");
        }

        public void LoadCharacter(int id) {
            SessionManager.Report("Collecting Character information for char" + id + "...");
            listCharShapes.Items.Clear();
            CharEditor.Visible = true;
            comboAnimID.Items.Clear();
            if (loadedNGN.characters[comboCharacters.SelectedIndex].Anims.Count > 0) {
                for (int i = 0;i < loadedNGN.characters[comboCharacters.SelectedIndex].Anims.Count;i++) {
                    comboAnimID.Items.Add("Animation " + i);
                }
                comboAnimID.SelectedIndex = 0;
            }


            if (loadedNGN.characters[id].shapes.Count > 0) {
                for (int i = 0;i < loadedNGN.characters[id].shapes.Count;i++) {
                    listCharShapes.Items.Add((loadedNGN.characters[id].shapes[i].name == "") ? "<Shape " + i.ToString().PadLeft(2, '0') + ">" : loadedNGN.characters[id].shapes[i].name);
                }
                listCharShapes.SelectedIndex = 0;
            } else {
                CharShapeEditor.Visible = false;
            }
            SessionManager.Report("Successfully Collected character data!");
        }

        private void comboCharacters_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboCharacters.SelectedIndex != -1) {
                LoadCharacter(comboCharacters.SelectedIndex);
            } else {
                CharEditor.Visible = false;
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
            loadedNGN.characters.Add(new Character { name = "Untitled" + comboCharacters.Items.Count });
            comboCharacters.Items.Add("Untitled" + comboCharacters.Items.Count);
            comboCharacters.SelectedIndex = comboCharacters.Items.Count - 1;
        }
        private void butRemoveChar_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this character?", "Character remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                int AS = -1;
                if (comboCharacters.SelectedIndex + 1 < comboCharacters.Items.Count) {
                    AS = comboCharacters.SelectedIndex;
                } else if (comboCharacters.SelectedIndex - 1 > -1) {
                    AS = comboCharacters.SelectedIndex - 1;
                }
                loadedNGN.characters.RemoveAt(comboCharacters.SelectedIndex);
                comboCharacters.Items.RemoveAt(comboCharacters.SelectedIndex);
                if (AS == -1) {
                    comboCharacters_SelectedIndexChanged(this, EventArgs.Empty);
                } else {
                    comboCharacters.SelectedIndex = AS;
                }
            }
        }
        private void butImportChar_Click(object sender, EventArgs e) {

        }
        private void butNewShape_Click(object sender, EventArgs e) {
            loadedNGN.characters[comboCharacters.SelectedIndex].shapes.Add(new Shape());
            LoadCharacter(comboCharacters.SelectedIndex);
        }
        private void butMoveShapeDown_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.characters[comboCharacters.SelectedIndex].shapes, listCharShapes.SelectedIndex, 1);
            listCharShapes.SelectedIndex = XF.ListMoveItem(listCharShapes.Items, listCharShapes.SelectedIndex, 1);
        }
        private void butMoveShapeUp_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.characters[comboCharacters.SelectedIndex].shapes, listCharShapes.SelectedIndex, -1);
            listCharShapes.SelectedIndex = XF.ListMoveItem(listCharShapes.Items, listCharShapes.SelectedIndex, -1);
        }
        private void butRemoveShape_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this shape?", "Character remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.characters[comboCharacters.SelectedIndex].shapes.RemoveAt(listCharShapes.SelectedIndex);
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

        private void butPasteCharShapeData_Click(object sender, EventArgs e) {

        }

        private void button2_Click(object sender, EventArgs e) {
            var window = new Render(800, 600, "Render Preview", loadedNGN.textures[10].image, XF.CompileVirtualOBJ(loadedNGN.characters[comboCharacters.SelectedIndex].shapes));
            window.Run(60.0);
        }

        private void butSaveChar_Click(object sender, EventArgs e) {
            loadedNGN.Extract(loadedNGN.characters[comboCharacters.SelectedIndex]);
        }

        private void aboveToolStripMenuItem_Click(object sender, EventArgs e) {
            InjectNewShape(listCharShapes, loadedNGN.characters[comboCharacters.SelectedIndex].shapes, listCharShapes.SelectedIndex, -1);
        }

        private void replaceToolStripMenuItem1_Click(object sender, EventArgs e) {
            InjectNewShape(listCharShapes, loadedNGN.characters[comboCharacters.SelectedIndex].shapes, listCharShapes.SelectedIndex, 0);
        }

        private void belowToolStripMenuItem_Click(object sender, EventArgs e) {
            InjectNewShape(listCharShapes, loadedNGN.characters[comboCharacters.SelectedIndex].shapes, listCharShapes.SelectedIndex, 1);
        }

        private void listCharShapes_DrawItem(object sender, DrawItemEventArgs e) {
            Color backcolor = e.BackColor;
            if (e.Index != -1) {
                if (listCharShapes_dragging) {
                    if (e.Index == this.listCharShapes.IndexFromPoint(listCharShapes.PointToClient(MousePosition))) {
                        backcolor = Color.DarkRed;
                    } else if (e.Index == this.listCharShapes.SelectedIndex) {
                        backcolor = Color.DarkGreen;
                    }
                }
                e.DrawBackground();
                using (SolidBrush backColorBrush = new SolidBrush(backcolor))
                using (SolidBrush foreColorBrush = new SolidBrush(Color.White)) {
                    e.Graphics.FillRectangle(backColorBrush, e.Bounds);
                    e.Graphics.DrawString(e.Index + " : " + listCharShapes.Items[e.Index].ToString(), listCharShapes.Font, foreColorBrush, e.Bounds, StringFormat.GenericTypographic);
                }
            }

        }

        private void listCharShapes_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
            listCharShapes_dragging = true;
            int LastIndex = listCharShapes.IndexFromPoint(listCharShapes.PointToClient(MousePosition));
            if (listCharShapes_dragging && LCSLastIndex != LastIndex) {
                LCSLastIndex = LastIndex;
                listCharShapes.Invalidate();
            }
        }

        private void listCharShapes_DragDrop(object sender, DragEventArgs e) {
            int index = this.listCharShapes.IndexFromPoint(listCharShapes.PointToClient(new Point(e.X, e.Y)));
            if (index < 0) index = this.listCharShapes.Items.Count - 1;
            Shape shape = loadedNGN.characters[comboCharacters.SelectedIndex].shapes[this.listCharShapes.SelectedIndex];
            loadedNGN.characters[comboCharacters.SelectedIndex].shapes.RemoveAt(this.listCharShapes.SelectedIndex);
            loadedNGN.characters[comboCharacters.SelectedIndex].shapes.Insert(index, shape);
            object data = listCharShapes.SelectedItem;
            this.listCharShapes.Items.Remove(data);
            this.listCharShapes.Items.Insert(index, data);
            this.listCharShapes.SelectedIndex = index;
            listCharShapes_dragging = false;
            //listCharShapes.Invalidate();
        }

        bool listCharShapes_dragging = false;
        int LCSLastIndex;
        private void listCharShapes_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (listCharShapes.SelectedItems.Count > 1) {//im sure optimization can be done here if required, for now no
                    ui_selectedCharShapes = loadedNGN.characters[comboCharacters.SelectedIndex].shapes.ToArray();
                    //CharShapeEditor.ToggleMultiMatEdit(true, XF.ItemsFromListBox(listCharShapes.SelectedIndices, ui_selectedCharShapes));
                } else {
                    //CharShapeEditor.ToggleMultiMatEdit(false);
                    ui_selectedCharShapes = null;
                    //Normal 
                    if (listCharShapes.SelectedIndex != -1 && listCharShapes.SelectedItems.Count < 2) {
                        CharShapeEditor.Visible = true;
                        Shape shp = loadedNGN.characters[comboCharacters.SelectedIndex].shapes[listCharShapes.SelectedIndex];
                        CharShapeEditor.ImportShape(ref shp, ref loadedNGN, listCharShapes.Items[listCharShapes.SelectedIndex].ToString(), false);
                    } else {
                        CharShapeEditor.Visible = false;
                    }
                }
                if (this.listCharShapes.SelectedItem != null) {
                    base.OnMouseDown(e);
                    this.listCharShapes.DoDragDrop(this.listCharShapes.SelectedItem, DragDropEffects.Move);
                }
            } else {
            //    listCharShapes.SelectedItems.Clear();
            //    listCharShapes.SelectedIndex = this.listCharShapes.IndexFromPoint(e.Location);
            }
            base.OnMouseDown(e);
        }

        private void listShapes_SelectedIndexChanged(object sender, EventArgs e) {
            //sadly we cant change the selection mode in runtime as it will recreate the control and do other not nice things
            //instead, we can simply use MultiExtended from the getgo, and check for more than one item selected, lol

            //TODO: Move everything below into the ToggleMultiMatEdit function, and just check in there how many items are in the array and toggle based on that

            //NOTE
            //MULTI MATERIAL EDITING IS NOW HANDLED EXTERNALLY
            //SEE T2Control_MaterialEditor



            if (listCharShapes.SelectedItems.Count > 1) {//im sure optimization can be done here if required, for now no
                ui_selectedCharShapes = loadedNGN.characters[comboCharacters.SelectedIndex].shapes.ToArray();
                //CharShapeEditor.ToggleMultiMatEdit(true, XF.ItemsFromListBox(listCharShapes.SelectedIndices, ui_selectedCharShapes));
            } else {
                //CharShapeEditor.ToggleMultiMatEdit(false);
                ui_selectedCharShapes = null;
                //Normal 
                if (listCharShapes.SelectedIndex != -1 && listCharShapes.SelectedItems.Count < 2) {
                    CharShapeEditor.Visible = true;
                    Shape shp = loadedNGN.characters[comboCharacters.SelectedIndex].shapes[listCharShapes.SelectedIndex];
                    CharShapeEditor.ImportShape(ref shp, ref loadedNGN, listCharShapes.Items[listCharShapes.SelectedIndex].ToString(), false);
                } else {
                    CharShapeEditor.Visible = false;
                }
            }



        }

        private void listCharShapes_MouseUp(object sender, MouseEventArgs e) {
            listCharShapes_dragging = false;
        }

        private void listCharShapes_MouseDoubleClick(object sender, MouseEventArgs e) {
            int index = this.listCharShapes.IndexFromPoint(listCharShapes.PointToClient(MousePosition));
            Shape shape = loadedNGN.characters[comboCharacters.SelectedIndex].shapes[index];
            if (index > 0) {
                InputDialog ID = new InputDialog(shape.name);
                if (ID.ShowDialog() == DialogResult.OK) {
                    shape.name = ID.input;
                    listCharShapes.Items[index] = ID.input;
                }
            }
            base.OnMouseDoubleClick(e);
        }

        #endregion

        #region "Geometry"
        public void LoadGeometryData() {
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadGeometryData] Processing NGN geometries");
            comboGeometry.Items.Clear();
            if (loadedNGN.Geometries.Count > 0) {
                int i = 0;
                foreach (Geometry geom in loadedNGN.Geometries) {
                    comboGeometry.Items.Add(geom.name == "" ? "Geom " + i.ToString().PadLeft(2, '0') : geom.name);
                    i++;
                }
                comboGeometry.SelectedIndex = 0;
            }
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadGeometryData] Procesed " + loadedNGN.Geometries.Count + " geometries");
        }


        public void LoadGeometry(int id) {
            SessionManager.Report("Collecting Geometry information for geom" + id + "...");
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
            SessionManager.Report("Successfully loaded geometry data!");
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
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butRemoveGeomShape_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this shape?", "Geometry shape remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes.RemoveAt(listGeomShapes.SelectedIndex);
                listGeomShapes.Items.RemoveAt(listGeomShapes.SelectedIndex);
            }
        }

        private void butMoveGeomShapeUp_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes, listGeomShapes.SelectedIndex, 1);
            listGeomShapes.SelectedIndex = XF.ListMoveItem(listGeomShapes.Items, listGeomShapes.SelectedIndex, 1);
        }

        private void butMoveGeomShapeDown_Click(object sender, EventArgs e) {
            XF.ListMoveItem(loadedNGN.Geometries[comboGeometry.SelectedIndex].shapes, listGeomShapes.SelectedIndex, -1);
            listGeomShapes.SelectedIndex = XF.ListMoveItem(listGeomShapes.Items, listGeomShapes.SelectedIndex, -1);
        }

        private void butNewGeometry_Click(object sender, EventArgs e) {
            loadedNGN.Geometries.Add(new Geometry { name = "Untitled" });
            comboGeometry.Items.Add("Untitled");
            comboGeometry.SelectedIndex = comboGeometry.Items.Count - 1;
        }

        private void butImportGeometry_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butRemoveGeometry_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this geometry?", "Geometry remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                int AS = -1;
                int selGeom = comboGeometry.SelectedIndex;
                if (selGeom + 1 < comboGeometry.Items.Count) {
                    AS = selGeom;
                } else if (selGeom - 1 > -1) {
                    AS = selGeom - 1;
                }
                loadedNGN.Geometries.RemoveAt(selGeom);
                comboGeometry.Items.RemoveAt(selGeom);
                if (AS == -1) {
                    comboGeometry_SelectedIndexChanged(this, EventArgs.Empty);
                } else {
                    comboGeometry.SelectedIndex = AS;
                }
            }
        }

        private void butNewGeomMaterial_Click(object sender, EventArgs e) {

        }

        private void butRemoveGeomMaterial_Click(object sender, EventArgs e) {

        }

        private void butExportGeomData_Click(object sender, EventArgs e) {
            butExportGeomData.Visible = false;
            loadedNGN.Extract(loadedNGN.Geometries[comboGeometry.SelectedIndex]);
            butExportGeomData.Visible = true;
        }

        #endregion

        #region "Area Portals"
        public void LoadAreaPortals() {
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadAreaPortals] Processing NGN AreaPortals ");
            listAreaPortals.Items.Clear();
            if (loadedNGN.areaPortals.Count > 0) {
                for (int i = 0;i < loadedNGN.areaPortals.Count;i++) {
                    listAreaPortals.Items.Add("Area Portal " + i);
                }
                listAreaPortals.SelectedIndex = 0;
            }
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadAreaPortals] Procesed " + loadedNGN.areaPortals.Count + " AreaPortals");
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
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadShapeLinks] Processing NGN ShapeLinks");
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
            SessionManager.Report("[FDR@T2Control_NGNEditor -> LoadShapeLinks] Procesed " + loadedNGN.ObjectLinks.Count + " ShapeLinks");
        }

        private void butNewShapeLink_Click(object sender, EventArgs e) {
            Linker l = new Linker(0, 0);
            loadedNGN.ObjectLinks.Add(l);
            ShapeLinkEditor.Rows.Add();
            DataGridViewRow DGVRow = ShapeLinkEditor.Rows[ShapeLinkEditor.Rows.Count-2];
            DGVRow.Cells[0].Value = l.ShapeID;
            DGVRow.Cells[1].Value = l.LinkID;
        }

        private void butRemoveShapeLink_Click(object sender, EventArgs e) {
            int selectedRowCount = ShapeLinkEditor.SelectedRows.Count;
            for (int i = 0;i < selectedRowCount;i++) {
                loadedNGN.ObjectLinks.RemoveAt(ShapeLinkEditor.SelectedRows[i].Index);
                ShapeLinkEditor.Rows.Remove(ShapeLinkEditor.SelectedRows[i]);
            }         
        }

        private void butShapeLinkMoveUp_Click(object sender, EventArgs e) {
            //XF.ListMoveItem(loadedNGN.ObjectLinks, ShapeLinkEditor.SelectedRows[0].Index, -1);
            //DataGridViewRow DGVRow  = ShapeLinkEditor.Rows[ShapeLinkEditor.SelectedRows[0].Index];
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butShapeLinkMoveDown_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        #endregion


        private void CharShapeEditor_Load(object sender, EventArgs e) {

        }


        private void contextCharShapes_Opening(object sender, CancelEventArgs e) {

        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e) {
            using (InputDialog ID = new InputDialog()) {
                if (ID.ShowDialog() == DialogResult.OK) {
                    Character chr = loadedNGN.characters[comboCharacters.SelectedIndex];
                    foreach (int item in listCharShapes.SelectedIndices) {
                        chr.shapes[item].name = ID.input;
                    }
                    listCharShapes.Items.Clear();
                    if (chr.shapes.Count > 0) {
                        for (int i = 0;i < chr.shapes.Count;i++) {
                            listCharShapes.Items.Add((chr.shapes[i].name == "") ? "<Shape " + i.ToString().PadLeft(2, '0') + ">" : chr.shapes[i].name);
                        }
                        listCharShapes.SelectedIndex = 0;
                    }
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e) {
            DialogResult msr = MessageBox.Show("Are you sure you want to delete these shapes?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (msr == DialogResult.Yes) {
                if (ui_selectedCharShapes != null) {
                    for (int i = 0;i < ui_selectedCharShapes.Length;i++) {
                        loadedNGN.characters[comboCharacters.SelectedIndex].shapes.Remove(ui_selectedCharShapes[i]);
                        listCharShapes.Items.Remove(ui_selectedCharShapes[i]);
                    }
                } else {
                    loadedNGN.characters[comboCharacters.SelectedIndex].shapes.RemoveAt(listCharShapes.SelectedIndex);
                    listCharShapes.Items.Remove(listCharShapes.SelectedIndex);
                }

            }
        }

        /// <summary>
        /// This can be used on both the character and geometry tabs. Either adding or replacing with a new blank shape.
        /// </summary>
        /// <param name="listbox">The listbox collection to modify</param>
        /// <param name="shapeList">The List collection to modify</param>
        /// <param name="index">The position in both the listbox and list to orient at</param>
        /// <param name="pos">Whether to place before index (-1), After index (1) or replace the current item (0)</param>
        public void InjectNewShape(ListBox listbox, List<Shape> shapeList, int index, int pos, bool isGeom = false) {
            Shape shp = new Shape();
            shp.type2 = 65535;
            if (pos == 0) {
                listbox.Items[index] = shp;
                shapeList[index] = shp;
            } else {
                if (pos == -1 && index < 1) { index = 1; }
                if (pos == 1 && index > listbox.Items.Count - 1) { index = listbox.Items.Count - 1; }
                listbox.Items.Insert(index + pos, shp);
                shapeList.Insert(index + pos, shp);
            }
            //LoadCharacter(comboCharacters.SelectedIndex);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e) {
            butMoveCharShapeUp.PerformClick();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e) {
            butMoveCharShapeDown.PerformClick();
        }

        private void button2_Click_1(object sender, EventArgs e) {
            using (Importer i = new Importer()) {
                if (i.ShowDialog() == DialogResult.OK) {
                    List<Shape> importedShapes = i.importedCharacter.shapes;
                    int j = comboCharacters.SelectedIndex;
                    loadedNGN.characters[comboCharacters.SelectedIndex].shapes.Clear();
                    foreach (Shape shape in importedShapes) {
                        loadedNGN.characters[comboCharacters.SelectedIndex].RegisterShape(shape);
                        foreach (Material mat in shape.materials) {
                            shape.AddTexture(mat.textureName);
                            mat.textureIndex = shape.textures.Count - 1;
                            shape.texturesGlobal.Add(loadedNGN.TexNameToGlobalID(mat.textureName));
                            //mat.GetRelativeTexIndex(mat.textureIndex);
                        }
                    }


                    comboCharacters.SelectedIndex = comboCharacters.Items.Count - 1;
                    comboCharacters.SelectedIndex = j;
                    //loadedNGN.characters.Add(i.importedCharacter);
                    //comboCharacters.Items.Add(i.importedCharacter.name);
                    //comboCharacters.SelectedIndex = comboCharacters.Items.Count - 1;
                }
            }
        }

        private void butRenameChar_Click(object sender, EventArgs e) {
            InputDialog ID = new InputDialog(loadedNGN.characters[comboCharacters.SelectedIndex].name);
            if (ID.ShowDialog() == DialogResult.OK) {
                loadedNGN.characters[comboCharacters.SelectedIndex].name = ID.input;
                comboCharacters.Items[comboCharacters.SelectedIndex] = ID.input;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            t2Control_TextureSelector1.Visible = checkBox1.Checked;
        }

        private void T2Control_NGNEditor_Resize(object sender, EventArgs e) {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            //this breaks some of the tabs so dont do it yet
            //foreach (TabPage t in tabControl1.TabPages) {
            //    if(t!=tabControl1.SelectedTab) {
            //        t.SuspendLayout();
            //    } else {
            //        t.ResumeLayout();
            //    }
            //}
        }

        private void butDSRemove_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butDSAdd_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butDSMoveDown_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butDSMoveUp_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butPreviewGeomShapeData_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butCopyGeomData_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }

        private void butPasteGeomData_Click(object sender, EventArgs e) {
            SessionManager.Report(SessionManager.Errors.NotImplemented);
        }
    }
}
