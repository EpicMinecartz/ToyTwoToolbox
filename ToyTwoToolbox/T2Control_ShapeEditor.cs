using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_ShapeEditor : UserControl {
        public delegate void ShapeNameUpdatedEventHandler(Object sender, string text);
        [Description("Occurs when the user updates the shape name.")]
        public event ShapeNameUpdatedEventHandler ReportShapeNameUpdate;

        private Shape[] loadedShapes;
        public T2Control_NGNEditor owner;
        Shape loadedShape {
            get {
                return loadedShapes[0]; //For reference, it doesnt really matter which we return here, as when the user changes something it will modify update all shapes. So we just pick the first.
            }
            set {
                if (multiMatEdit) {
                    for (int i = 0;i < loadedShapes.Length;i++) {
                        loadedShapes[i] = value;
                    }
                }
            }
        }
        F_NGN loadedNGN;
        IPrimitive selectedPrim;
        ISL materialISL = new ISL(new List<int>(new int[] { -1, 1, 2, 10, 11 }), new List<string>(new string[] { "Disable Metadata", "Solid Texture", "Transparency", "Transparency", "Transparency" }));
        bool multiMatEdit = false;

        public T2Control_ShapeEditor() {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        /// <summary>Internally toggle the state of the multi-material editor</summary>
        /// <param name="state">The state to set</param>
        /// <param name="selectedShapes">An array of <seealso cref="Shape"/> to modify when the user makes changes</param>
        public void ToggleMultiMatEdit(bool? state = null, Shape[] selectedShapes = null) {
            multiMatEdit = state ?? !multiMatEdit;
            if (selectedShapes != null) { loadedShapes = selectedShapes; }
            if (multiMatEdit == true) {
                t2TTabControl4.TabPages[0].Text = "Selected Shapes";
            } else {
                t2TTabControl4.TabPages[0].Text = "Selected Shape";
            }
        }

        private void fieldShapeName_ReportTextUpdate(object sender, string text) {
            if (ReportShapeNameUpdate != null) {
                ReportShapeNameUpdate.Invoke(this, text);
                loadedShape.name = text;
            }
        }

        public void ImportShape(ref Shape shape, ref F_NGN NGN, string altname = "", bool isMultiMat = false) {
            groupMaterialProperties.SuspendLayout();
            t2Control_TextureSelector1.SuspendLayout();
            if (!isMultiMat) {
                Shape[] sp = new Shape[1];
                sp[0] = shape;
                loadedShapes = sp;
                loadedShape = shape;
            }
            loadedNGN = NGN;

            fieldShapeName.Text = (shape.name == "") ? (altname == "") ? "<Untitled>" : altname : shape.name;
            numericCharShapeID.Value = shape.type;
            numericCharShapeID2.Value = shape.type2;

            if (loadedNGN.textures.Count > 0) {
                t2Control_TextureSelector1.Init(loadedNGN.textures);
            }
            groupMaterialProperties.Visible = false;
            comboMaterial.Items.Clear();
            if (shape.materials.Count > 0) {
                for (int i = 0;i < shape.materials.Count;i++) {
                    comboMaterial.Items.Add("Material " + i);
                }
                comboMaterial.SelectedIndex = 0;
            }

            dgvShapeData.Rows.Clear();
            comboPrimitive.Items.Clear();
            if (shape.rawPrimitives.Count > 0) {
                int i = 0;
                foreach (IPrimitive prim in shape.rawPrimitives) {
                    comboPrimitive.Items.Add((prim.PrimType == typeof(Prim) ? "Prim " : "Patch ") + i);
                    i++;
                }
                comboPrimitive.SelectedIndex = -1;
                comboPrimitive.SelectedIndex = 0;
            }
            groupMaterialProperties.ResumeLayout();
            t2Control_TextureSelector1.ResumeLayout();
            Application.DoEvents();
            UpdateShapeDataUI(true);
        }

        public void Clear(bool Hide = false) {
            dgvShapeData.Rows.Clear();
            comboPrimitive.Items.Clear();
            fieldShapeName.Text = "";
            this.Visible = !Hide;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateShapeDataUI();
        }

        private void comboMaterial_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboMaterial.SelectedIndex != -1) {
                groupMaterialProperties.Visible = true;
                Material mat = loadedShape.materials[comboMaterial.SelectedIndex];
                butAmbColorPicker.BackColor = XF.NGNColToColor(mat.RGB);
                t2Control_TextureSelector1.SelectedIndex = (mat.textureIndex != 65535 && mat.textureIndexRelative <= loadedNGN.textures.Count) ? mat.textureIndexRelative : 0;
                if (mat.textureIndex > loadedShape.textures.Count) { SessionManager.Report("Material " + comboMaterial.SelectedIndex + " in shape has an invalid textureindex (" + mat.textureIndex + ")", SessionManager.RType.WARN); }
                numericMaterialMetadata.Value = (mat.id == 129 ? -1 : mat.metadata);
            } else {
                groupMaterialProperties.Visible = false;
            }
        }

        public void UpdateShapeDataUI(bool partialUpdate = false) {
            if (comboPrimitive.SelectedIndex != -1) {
                selectedPrim = loadedShape.rawPrimitives[comboPrimitive.SelectedIndex];
                dgvShapeData.Enabled = true;
                numericPatchMaterialID.Enabled = true;
                numericPatchType.Enabled = true;

                numericPatchMaterialID.Value = selectedPrim.materialID;
                numericPatchType.Value = selectedPrim.type;
                if (selectedPrim.PrimType == typeof(Prim)) { radioPrim.Checked = true; } else { radioPatch.Checked = true; }
                if (partialUpdate == false) {
                    dgvShapeData.Rows.Clear();
                    dgvShapeData.SuspendLayout();
                    foreach (int vertex in selectedPrim.vertices) {
                        if (vertex < loadedShape.rawVertices.Count) {
                            dgvShapeData.Rows.Add();
                            DataGridViewRow DGVRow = dgvShapeData.Rows[dgvShapeData.Rows.Count - 2]; //2 because -1 == the new line block which doesnt persist its data lmao
                            DGVRow.Cells[0].Value = loadedShape.rawVertices[vertex].X;
                            DGVRow.Cells[1].Value = loadedShape.rawVertices[vertex].Y;
                            DGVRow.Cells[2].Value = loadedShape.rawVertices[vertex].Z;

                            if (loadedShape.rawVertexData.Count > 0) {
                                DGVRow.Cells[3].Value = loadedShape.rawVertexData[vertex].X;
                                DGVRow.Cells[4].Value = loadedShape.rawVertexData[vertex].Y;
                                DGVRow.Cells[5].Value = loadedShape.rawVertexData[vertex].Z;
                            }

                            if (loadedShape.rawVertexShading.Count > 0) {
                                DGVRow.Cells[6].Value = loadedShape.rawVertexShading[vertex].A;
                                DGVRow.Cells[7].Style.BackColor = System.Drawing.Color.FromArgb(
                                    loadedShape.rawVertexShading[vertex].R,
                                    loadedShape.rawVertexShading[vertex].G,
                                    loadedShape.rawVertexShading[vertex].B);
                            }

                            if (loadedShape.rawVertexTextureCoords.Count > 0) {
                                DGVRow.Cells[8].Value = loadedShape.rawVertexTextureCoords[vertex].X;
                                DGVRow.Cells[9].Value = loadedShape.rawVertexTextureCoords[vertex].Y;
                            }
                        } else {
                            SessionManager.Report("PrimVTX count exceeded shape raw vert cout", SessionManager.RType.WARN);
                        }
                    }
                    dgvShapeData.ResumeLayout();
                }
                dgvShapeData.Visible = true;
            } else {
                dgvShapeData.Visible = false;
                dgvShapeData.Rows.Clear();
                dgvShapeData.Enabled = false;
                numericPatchMaterialID.Enabled = false;
                numericPatchType.Enabled = false;
            }
        }

        private void fieldShapeName_TextChanged(object sender, EventArgs e) {
            loadedShape.name = fieldShapeName.Text;
            //
        }

        private void numericCharShapeID_ValueChanged(object sender, EventArgs e) {
            loadedShape.type = (int)numericCharShapeID.Value;
        }

        private void numericCharShapeID2_ValueChanged(object sender, EventArgs e) {
            loadedShape.type2 = (int)numericCharShapeID2.Value;
        }

        private void butAddPrim_Click(object sender, EventArgs e) {
            loadedShape.rawPrimitives.Add(new Prim());
            comboPrimitive.Items.Add("Prim " + comboPrimitive.Items.Count);
            comboPrimitive.SelectedIndex = comboPrimitive.Items.Count - 1;
        }

        private void butRemovePrim_Click(object sender, EventArgs e) {
            DialogResult msg = MessageBox.Show("Are you sure you want to remove this primitive?", "Primitive remove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes) {
                int li = comboPrimitive.SelectedIndex;
                loadedShape.rawPrimitives.RemoveAt(comboPrimitive.SelectedIndex);
                comboPrimitive.Items.RemoveAt(comboPrimitive.SelectedIndex);
                if (comboPrimitive.Items.Count > 0) {
                    comboPrimitive.SelectedIndex = --li;
                } else {
                    dgvShapeData.Rows.Clear();
                }
                UpdateShapeDataUI();
            }
        }

        private void numericPatchMaterialID_ValueChanged(object sender, EventArgs e) {
            loadedShape.rawPrimitives[comboPrimitive.SelectedIndex].materialID = (int)numericPatchMaterialID.Value;
        }

        private void numericPatchType_ValueChanged(object sender, EventArgs e) {
            loadedShape.rawPrimitives[comboPrimitive.SelectedIndex].type = (int)numericPatchType.Value;
        }

        private void dgvShapeData_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void dgvShapeData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            //selectedPrim = loadedShape.rawPrimitives[comboPrimitive.SelectedIndex];
            //we MUST ensure we keep track of the vertices we add

        }

        private void dgvShapeData_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) {

        }

        private void dgvShapeData_CellEndEdit(object sender, DataGridViewCellEventArgs e) {

        }

        private void butNewShapeMaterial_Click(object sender, EventArgs e) {
            loadedShape.materials.Add(new Material());
            comboMaterial.Items.Add("Material" + comboMaterial.Items.Count);
            comboMaterial.SelectedIndex = comboMaterial.Items.Count - 1;
        }

        private void radioPatch_Click(object sender, EventArgs e) {
            if (loadedShape._SPType == 0) {
                loadedShape.ConvertPrimitives(typeof(Patch));
                UpdateShapeDataUI(true);
            }
        }

        private void radioPrim_Click(object sender, EventArgs e) {
            if (loadedShape._SPType == 1) {
                //there is data stored in a patch that is non transferable as idk what its used for yet so idk if it can be
                //either way, we currently prompt to ensure the user knows this
                loadedShape.ConvertPrimitives(typeof(Prim));
                UpdateShapeDataUI(true);
            }
        }

        private void comboPrimitive_ParentChanged(object sender, EventArgs e) {

        }

        private void radioPatch_CheckedChanged(object sender, EventArgs e) {

        }

        private void numericMaterialMetadata_ValueChanged(object sender, EventArgs e) {
            loadedShape.materials[comboMaterial.SelectedIndex].id = (numericMaterialMetadata.Value == -1 ? 129 : 193);
            loadedShape.materials[comboMaterial.SelectedIndex].metadata = (int)numericMaterialMetadata.Value;
            int mmid = materialISL.Index.IndexOf((int)numericMaterialMetadata.Value);
            labelMaterialMetadataDesc.Text = (mmid != -1 ? " - " + materialISL.Data[mmid] : " - Unknown Metadata ID");
        }

        private void dgvShapeData_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            //cell contents updated
            if (dgvShapeData.ignoreCellValueChanged == false) {
                DataGridView dgv = (DataGridView)sender;
                if (e.RowIndex > -1 && e.RowIndex < dgvShapeData.RowCount) {
                    int selectedVertex = selectedPrim.vertices[e.RowIndex];
                    ///use column as the [index]
                    DataGridViewCellCollection selectedCells = dgvShapeData.Rows[e.RowIndex].Cells;
                    if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn) {
                        ColorDialog cd = new ColorDialog {
                            Color = dgvShapeData.Rows[e.RowIndex].Cells["VColor"].Style.BackColor,
                            FullOpen = true
                        };
                        if (cd.ShowDialog() == DialogResult.OK) {
                            selectedCells["VColor"].Style.BackColor = cd.Color;
                            loadedShape.rawVertexShading[selectedVertex] = cd.Color;
                        }
                    }
                    if (e.ColumnIndex == 0) {
                        loadedShape.rawVertices[selectedVertex].X = Convert.ToSingle(selectedCells[0].Value);
                    } else if (e.ColumnIndex == 1) {
                        loadedShape.rawVertices[selectedVertex].Y = Convert.ToSingle(selectedCells[1].Value);
                    } else if (e.ColumnIndex == 2) {
                        loadedShape.rawVertices[selectedVertex].Z = Convert.ToSingle(selectedCells[2].Value);
                    } else if (e.ColumnIndex == 3) {
                        loadedShape.rawVertexData[selectedVertex].Z = Convert.ToSingle(selectedCells[3].Value);
                    } else if (e.ColumnIndex == 4) {
                        loadedShape.rawVertexData[selectedVertex].Y = Convert.ToSingle(selectedCells[4].Value);
                    } else if (e.ColumnIndex == 5) {
                        loadedShape.rawVertexData[selectedVertex].Z = Convert.ToSingle(selectedCells[5].Value);
                    } else if (e.ColumnIndex == 6) {
                        loadedShape.rawVertexShading[selectedVertex] = System.Drawing.Color.FromArgb((int)selectedCells[6].Value, loadedShape.rawVertexShading[selectedVertex]);
                    } else if (e.ColumnIndex == 7) {
                        //handled above
                    } else if (e.ColumnIndex == 8) {
                        loadedShape.rawVertexTextureCoords[selectedVertex].X = Convert.ToSingle(selectedCells[8].Value);
                    } else if (e.ColumnIndex == 9) {
                        loadedShape.rawVertexTextureCoords[selectedVertex].Y = Convert.ToSingle(selectedCells[9].Value);
                    }
                }
            }
        }

        private void dgvShapeData_MouseUp(object sender, MouseEventArgs e) {
            if (dgvShapeData.SelectedCells.Count > 1) {//potential cross contamination
                if (dgvShapeData.SelectedCells[dgvShapeData.SelectedCells.Count - 1].ColumnIndex == 7) {
                    foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
                        if (cell.ColumnIndex != 7) {
                            cell.Selected = false;
                        }
                    }
                } else {
                    foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
                        if (cell.ColumnIndex == 7) {
                            cell.Selected = false;
                        }
                    }
                }
            }
        }

        //what the f is this method lol
        private void butSendToMultiMat_Click(object sender, EventArgs e) {
            ((Main)(owner.owner.owner.TabContainer.Parent)).RegisterMultiMaterial(loadedShape.materials[comboMaterial.SelectedIndex]);
        }

        //BUG - Initializing the TextureSelector causes this to be invoked, via ..\T2Control_TextureSelector.cs@26
        private void t2Control_TextureSelector1_SelectedIndexChanged(int Index) {
            Material mat = loadedShape.materials[comboMaterial.SelectedIndex];
            mat.textureIndexRelative = t2Control_TextureSelector1.SelectedIndex;

            int texOffset = loadedShape.textures.IndexOf(t2Control_TextureSelector1.SelectedPanel._texture.name);
            if (texOffset == -1) {
                loadedShape.AddTexture(t2Control_TextureSelector1.SelectedPanel._texture.name);
                mat.textureIndex = loadedShape.textures.Count - 1;
            } else {
                mat.textureIndex = texOffset;
            }
        }
    }
}
