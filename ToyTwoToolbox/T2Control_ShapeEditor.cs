using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
            comboMaterial.Items.Clear();
            if (shape.materials.Count > 0) {
                for (int i = 0;i < shape.materials.Count;i++) {
                    comboMaterial.Items.Add("Material " + i);
                }
            }


            comboMaterialTexture.Items.Clear();
            if (loadedNGN.textures.Count > 0) {
                foreach (Texture tex in loadedNGN.textures) {
                    comboMaterialTexture.Items.Add(tex.name);
                }
                comboMaterialTexture.SelectedIndex = 0;
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
            selectedPrim = loadedShape.rawPrimitives[comboPrimitive.SelectedIndex];
            numericPatchMaterialID.Value = selectedPrim.materialID;
            numericPatchType.Value = selectedPrim.type;
            if (selectedPrim.PrimType == typeof(Prim)) { radioPrim.Checked = true; } else { radioPatch.Checked = true; }
            dgvShapeData.Rows.Clear();
            Application.DoEvents();
            foreach (int vertex in selectedPrim.vertices) {
                dgvShapeData.SuspendLayout();
                dgvShapeData.Rows.Add();
                DataGridViewRow DGVRow = (DataGridViewRow)dgvShapeData.Rows[dgvShapeData.Rows.Count-2]; //2 because -1 == the new line block which doesnt persist its data lmao
                DGVRow.Cells[0].Value = loadedShape.rawVertices[vertex].X;
                DGVRow.Cells[1].Value = loadedShape.rawVertices[vertex].Y;
                DGVRow.Cells[2].Value = loadedShape.rawVertices[vertex].Z;

                if (loadedShape.rawVertexData.Count >0) {
                    DGVRow.Cells[3].Value = loadedShape.rawVertexData[vertex].X;
                    DGVRow.Cells[4].Value = loadedShape.rawVertexData[vertex].Y;
                    DGVRow.Cells[5].Value = loadedShape.rawVertexData[vertex].Y;
                }

                if (loadedShape.rawVertexShading.Count > 0) {
                    DGVRow.Cells[6].Value = loadedShape.rawVertexShading[vertex].A;
                    DGVRow.Cells[7].Style.BackColor = System.Drawing.Color.FromArgb(
                        (int)loadedShape.rawVertexShading[vertex].R,
                        (int)loadedShape.rawVertexShading[vertex].G,
                        (int)loadedShape.rawVertexShading[vertex].B);
                }

                    if (loadedShape.rawVertexTextureCoords.Count > 0) {
                        DGVRow.Cells[8].Value = loadedShape.rawVertexTextureCoords[vertex].Y;
                        DGVRow.Cells[9].Value = loadedShape.rawVertexTextureCoords[vertex].Y;
                    }
                dgvShapeData.ResumeLayout();
            }
        }

        private void radioPrim_CheckedChanged(object sender, EventArgs e) {

        }

        private void comboMaterial_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboMaterial.SelectedIndex != -1) {
                groupMaterialProperties.Visible = true;
                Material mat = loadedShape.materials[comboMaterial.SelectedIndex];
                butAmbColorPicker.BackColor = XF.NGNColToColor(mat.RGB);
                comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535) ? mat.textureIndex : 0;
            } else {
                groupMaterialProperties.Visible = false;
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
                loadedShape.rawPrimitives.RemoveAt(comboPrimitive.SelectedIndex);
                comboPrimitive.Items.RemoveAt(comboPrimitive.SelectedIndex);
            }
        }

        private void numericPatchMaterialID_ValueChanged(object sender, EventArgs e) {

        }

        private void numericPatchType_ValueChanged(object sender, EventArgs e) {

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
            comboMaterial.SelectedIndex = comboMaterial.Items.Count-1;
        }
    }
}
