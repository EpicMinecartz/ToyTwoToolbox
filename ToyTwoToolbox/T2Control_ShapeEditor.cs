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
        Shape loadedShape { //pls fix
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
        ISL materialISL = new ISL(new List<int>(new int[] { -1, 1, 2, 4, 6, 14, 10, 11 }), new List<string>(new string[] { "Disable Metadata", "Solid Texture", "Transparency", "Reflection", "Reflect/Transparent", "Reflect/Transparent", "Transparency", "Transparency" })); //conv to sw-case void?
        bool multiMatEdit = false;

        public T2Control_ShapeEditor() {
            InitializeComponent();
            this.DoubleBuffered = true;
            contextDGV.Renderer = new DarkThemeMenuRender();
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
            dgvShapeData.ignoreCellValueChanged = true; //we arent gonna be saving any changes while loading, and this is a good idea, i think
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
                t2Control_TextureSelector1.SelectedIndex = (mat.textureIndex != 65535 && mat.textureIndexRelative <= loadedNGN.textures.Count) ? mat.textureIndexRelative : -1;
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
            //loadedShape.AddVertex();
        }

        public Shape ParseDGVRow(DataGridViewRow row) {
            Shape shape = new Shape();
            shape.rawVertices.Add(new Vector3((float)row.Cells[0].Value, (float)row.Cells[1].Value, (float)row.Cells[2].Value));
            shape.rawVertexData.Add(new Vector3((float)row.Cells[3].Value, (float)row.Cells[4].Value, (float)row.Cells[5].Value));

            return shape;
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


        private void numericMaterialMetadata_ValueChanged(object sender, EventArgs e) {
            loadedShape.materials[comboMaterial.SelectedIndex].id = (numericMaterialMetadata.Value == -1 ? 129 : 193);
            loadedShape.materials[comboMaterial.SelectedIndex].metadata = (int)numericMaterialMetadata.Value;
            int mmid = materialISL.Index.IndexOf((int)numericMaterialMetadata.Value);
            labelMaterialMetadataDesc.Text = (mmid != -1 ? " - " + materialISL.Data[mmid] : " - Unknown Metadata ID");
        }

        private void dgvShapeData_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            //cell contents updated
            if (dgvShapeData.ignoreCellValueChanged == false) {
                if (e.ColumnIndex == 7) {
                    ColorDialog cd = new ColorDialog {
                        Color = dgvShapeData.Rows[e.RowIndex].Cells["VColor"].Style.BackColor,
                        FullOpen = true
                    };
                    if (cd.ShowDialog() == DialogResult.OK) {
                        dgvShapeData.Rows[e.RowIndex].Cells["VColor"].Style.BackColor = cd.Color;
                    }
                }
                UpdateFromDGV(e.ColumnIndex, e.RowIndex, dgvShapeData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        public void UpdateFromDGV(int ColumnID = -1, int RowID = -1, object Value = null) {
            _UpdateFromDGV(ColumnID, RowID, Value);
        }

        /// <summary>This method updates the appropriate data inside the shape based on the parameters supplied (from a DGV)</summary>
        public void _UpdateFromDGV(int ColumnID = -1, int RowID = -1, object Value = null) {
            if (RowID > -1 && RowID < dgvShapeData.RowCount) {
                int selectedVertex = loadedShape.rawPrimitives[comboPrimitive.SelectedIndex].vertices[RowID];
                if (ColumnID == 0) {                ///use column as the [index]
                    loadedShape.rawVertices[selectedVertex].X = Convert.ToSingle(Value);
                } else if (ColumnID == 1) {
                    loadedShape.rawVertices[selectedVertex].Y = Convert.ToSingle(Value);
                } else if (ColumnID == 2) {
                    loadedShape.rawVertices[selectedVertex].Z = Convert.ToSingle(Value);
                } else if (ColumnID == 3) {
                    loadedShape.rawVertexData[selectedVertex].X = Convert.ToSingle(Value);
                } else if (ColumnID == 4) {
                    loadedShape.rawVertexData[selectedVertex].Y = Convert.ToSingle(Value);
                } else if (ColumnID == 5) {
                    loadedShape.rawVertexData[selectedVertex].Z = Convert.ToSingle(Value);
                } else if (ColumnID == 6) {
                    loadedShape.rawVertexShading[selectedVertex] = System.Drawing.Color.FromArgb(Convert.ToInt32(Value), loadedShape.rawVertexShading[selectedVertex]);
                } else if (ColumnID == 7) {
                    loadedShape.rawVertexShading[selectedVertex] = System.Drawing.Color.FromArgb(loadedShape.rawVertexShading[selectedVertex].A, dgvShapeData.Rows[RowID].Cells[ColumnID].Style.BackColor);
                } else if (ColumnID == 8) {
                    loadedShape.rawVertexTextureCoords[selectedVertex].X = Convert.ToSingle(Value);
                } else if (ColumnID == 9) {
                    loadedShape.rawVertexTextureCoords[selectedVertex].Y = Convert.ToSingle(Value);
                }
            }
            if (ColumnID != 7) { dgvShapeData.Rows[RowID].Cells[ColumnID].Value = Value; }
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

        private void contextDGV_Opening(object sender, CancelEventArgs e) {
            string rts = "Replace selected ";
            if (dgvShapeData.SelectedCells[0].ColumnIndex == 7) {
                rts += "colors";
                contextDGV.Tag = "color";
            } else {
                rts += "values";
                contextDGV.Tag = "";
            }
            replaceSelectedValuesToolStripMenuItem.Text = rts;

        }

        private void replaceSelectedValuesToolStripMenuItem_Click(object sender, EventArgs e) {
            //NOTE when we use dgvShapeData.SelectedCells[0] we are assuming that the column contamination protection has done its job correctly
            //if (contextDGV.Tag == "color") {
            //    if (dgvShapeData.Columns[dgvShapeData.SelectedCells[0].ColumnIndex] is DataGridViewButtonColumn) {
            //        ColorDialog cd = new ColorDialog {
            //            Color = dgvShapeData.SelectedCells[0].Style.BackColor,
            //            FullOpen = true
            //        };
            //        if (cd.ShowDialog() == DialogResult.OK) {
            //            foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
            //                cell.Style.BackColor = cd.Color;
            //                loadedShape.rawVertexShading[loadedShape.rawPrimitives[comboPrimitive.SelectedIndex].vertices[cell.RowIndex]] = cd.Color;
            //            }
            //        }
            //    } else {
            //        using (InputDialog input = new InputDialog()) {
            //            if (input.ShowDialog() == DialogResult.OK) {
            //                foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
            //                    cell.Value = input.input;
            //                    SessionManager.ReportEx("INPUT NOT IMPL!", SessionManager.RType.ERROR);
            //                }
            //            }
            //        }
            //    }
            //}
            System.Drawing.Color CDC;
            string CID = null;
            if (dgvShapeData.SelectedCells[0].ColumnIndex == 7) {
                ColorDialog cd = new ColorDialog {
                    Color = dgvShapeData.SelectedCells[0].Style.BackColor,
                    FullOpen = true
                };
                if (cd.ShowDialog() == DialogResult.OK) {
                    foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
                        cell.Style.BackColor = cd.Color;
                        //loadedShape.rawVertexShading[loadedShape.rawPrimitives[comboPrimitive.SelectedIndex].vertices[cell.RowIndex]] = cd.Color;
                    }
                } else {
                    //id say we just assume nothing else is gonna happen and we bust outta here...
                    return;
                }
            } else {
                using (InputDialog input = new InputDialog()) {
                    if (input.ShowDialog() == DialogResult.OK) {
                        CID = input.input;
                    } else {
                        //id say we just assume nothing else is gonna happen and...feel like i've said this before...
                        return;
                    }
                }
            }




            foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
                UpdateFromDGV(cell.ColumnIndex, cell.RowIndex, CID ?? cell.Value);
            }


        }

        private void fillSelectedWithRandomNumbersToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewCell cell in dgvShapeData.SelectedCells) {
                switch (cell.ColumnIndex) {
                    case 6:
                        cell.Value = XF.Random(0, 255);
                        break;
                    case 8:
                    case 9:
                        cell.Value = XF.Randomf();
                        break;
                    default:
                        cell.Value = XF.Random();
                        break;
                }
            }
        }

        private void selectInvertedSelectionToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dgvShapeData.Rows) {
                foreach (DataGridViewCell cell in row.Cells) {
                    cell.Selected = !cell.Selected;
                }
            }
        }

        private void selectAllCellsToolStripMenuItem_Click(object sender, EventArgs e) {
            dgvShapeData.SelectAll();
        }

        private void selectAllCellsInColumToolStripMenuItem_Click(object sender, EventArgs e) {
            int selcol = dgvShapeData.SelectedCells[dgvShapeData.SelectedCells.Count - 1].ColumnIndex;
            for (int i = 0;i < dgvShapeData.Rows.Count;i++) {
                dgvShapeData.Rows[i].Cells[selcol].Selected = true;
            }
        }

        private void dgvShapeData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            dgvShapeData.ignoreCellValueChanged = false; //pre-empt that stuff gonna start changing, doesnt matter if it doesnt as it'll only be one update
        }

        private void dgvShapeData_CellEnter(object sender, DataGridViewCellEventArgs e) {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            SessionManager.SMptr.ICL.SetCopyType(InternalCopy.ICLFormat.DGV);
            SessionManager.SMptr.ICL.Clear();
            for (int i = 0;i < dgvShapeData.SelectedCells.Count;i++) {
                SessionManager.SMptr.ICL.Copy(dgvShapeData.SelectedCells[i].Value);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e) {
            //get lowest so we dont overflow ouch!
            List<object> ICLP = SessionManager.SMptr.ICL.Paste();
            int lcp = Math.Min(ICLP.Count, dgvShapeData.SelectedCells.Count);
            for (int i = 0;i < lcp;i++) {
                dgvShapeData.SelectedCells[i].Value = ICLP[i];
            }
        }

        private void incrementNumericalItemsByToolStripMenuItem_Click(object sender, EventArgs e) {
            using (InputDialog input = new InputDialog()) {
                if (input.ShowDialogEx(true) == DialogResult.OK) {
                    int rep = 0;
                    if (Int32.TryParse(input.input, out rep)) { //we dont need to verify here, but why not lmao
                        for (int i = 0;i < dgvShapeData.SelectedCells.Count;i++) {
                            int init = 0;
                            if (Int32.TryParse(dgvShapeData.SelectedCells[i].Value.ToString(), out init)) {
                                dgvShapeData.SelectedCells[i].Value = rep + init;
                            }
                        }
                    }
                } else {
                    //id say we just assume nothing else is gonna happen and...feel like i've said this before...
                    return;
                }
            }
        }
    }
}
