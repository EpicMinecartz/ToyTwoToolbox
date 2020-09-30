using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_ShapeEditor : UserControl {
        public delegate void ShapeNameUpdatedEventHandler(Object sender, string text);
        [Description("Occurs when the user updates the shape name.")]
        public event ShapeNameUpdatedEventHandler ReportShapeNameUpdate;


        Shape loadedShape;
        F_NGN loadedNGN;
        IPrimitive selectedPrim;
        public T2Control_ShapeEditor() {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void fieldShapeName_ReportTextUpdate(object sender, string text) {
            if (ReportShapeNameUpdate != null) {
                ReportShapeNameUpdate.Invoke(this, text);
                loadedShape.name = text;
            }
        }

        public void ImportShape(ref Shape shape, ref F_NGN NGN, string altname = "") {
            loadedShape = shape;
            loadedNGN = NGN;

            fieldShapeName.Text = (shape.name == "") ? (altname == "") ? "<Untitled>" : altname : shape.name;
            numericCharShapeID.Value = shape.type;
            numericCharShapeID2.Value = shape.type2;

            groupMaterialProperties.Visible = false;

            comboMaterialTexture.Items.Clear();
            if (loadedNGN.textures.Count > 0) {
                foreach (Texture tex in loadedNGN.textures) {
                    comboMaterialTexture.Items.Add(tex.name);
                }
            }

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
                comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535 && mat.textureIndex <= comboMaterial.Items.Count) ? mat.textureIndex : 0;
                if (mat.textureIndex > comboMaterial.Items.Count) { SessionManager.Report("Material " + comboMaterial.SelectedIndex + " in shape has an invalid textureindex (" + mat.textureIndex + ")", SessionManager.RType.WARN); }
            } else {
                groupMaterialProperties.Visible = false;
            }
        }

        public void UpdateShapeDataUI(bool partialUpdate = false) {
            selectedPrim = loadedShape.rawPrimitives[comboPrimitive.SelectedIndex];
            numericPatchMaterialID.Value = selectedPrim.materialID;
            numericPatchType.Value = selectedPrim.type;
            if (selectedPrim.PrimType == typeof(Prim)) { radioPrim.Checked = true; } else { radioPatch.Checked = true; }
            if (partialUpdate == false) {
                dgvShapeData.Rows.Clear();
                Application.DoEvents();
                foreach (int vertex in selectedPrim.vertices) {
                    dgvShapeData.SuspendLayout();
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
                    dgvShapeData.ResumeLayout();
                }
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
    }
}
