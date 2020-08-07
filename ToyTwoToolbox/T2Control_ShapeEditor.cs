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
        Shape loadedShape;
        F_NGN loadedNGN;
        IPrimitive selectedPrim;
        public T2Control_ShapeEditor() {
            InitializeComponent();
            //dgvShapeData.DefaultCellStyle = SessionManager.DarkThemeCellDGV;
            //dgvShapeData.ColumnHeadersDefaultCellStyle = SessionManager.DarkThemeCellDGV;
            //dgvShapeData.RowsDefaultCellStyle = SessionManager.DarkThemeCellDGV;
        }

        public void ImportShape(ref Shape shape, ref F_NGN NGN) {
            loadedShape = shape;
            loadedNGN = NGN;

            fieldShapeName.Text = shape.name;
            numericCharShapeID.Value = shape.type;
            numericCharShapeID2.Value = shape.type2;
            comboMaterialTexture.Items.Clear();
            foreach (Texture tex in loadedNGN.textures) {
                comboMaterialTexture.Items.Add(tex.name);
            }
            comboMaterialTexture.SelectedIndex = 0;
            numericMaterialID.Value = (shape.materials.Count > 0) ? 0 : -1;
            numericMaterialID.Maximum = shape.materials.Count;
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
            fieldShapeName.Clear();
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

                DGVRow.Cells[3].Value = loadedShape.rawVertexData[vertex].X;
                DGVRow.Cells[4].Value = loadedShape.rawVertexData[vertex].Y;
                DGVRow.Cells[5].Value = loadedShape.rawVertexData[vertex].Y;

                DGVRow.Cells[6].Value = loadedShape.rawVertexShading[vertex].A;
                DGVRow.Cells[7].Style.BackColor = System.Drawing.Color.FromArgb(
                    (int)loadedShape.rawVertexShading[vertex].R, 
                    (int)loadedShape.rawVertexShading[vertex].G, 
                    (int)loadedShape.rawVertexShading[vertex].B);

                DGVRow.Cells[8].Value = loadedShape.rawVertexTextureCoords[vertex].Y;
                DGVRow.Cells[9].Value = loadedShape.rawVertexTextureCoords[vertex].Y;
                dgvShapeData.ResumeLayout();
            }
        }

        private void radioPrim_CheckedChanged(object sender, EventArgs e) {

        }

        private void numericMaterialID_ValueChanged(object sender, EventArgs e) {
            groupMaterialProperties.Enabled = (numericMaterialID.Value != -1);
            if (numericMaterialID.Value != -1) {
                Material mat = loadedShape.materials[(int)numericMaterialID.Value];
                List<double> AmbColor = mat.RGB;
                butAmbColorPicker.BackColor = Color.FromArgb((int)AmbColor[0], (int)AmbColor[1], (int)AmbColor[2]);
                comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535) ? mat.textureIndex : 0;

            }
        }
        public void Hello() {
            int HelloCount = 14;
            for (int i = 0;i <= HelloCount;i++) {
                Console.WriteLine("Hello");
            }
        }
        private void butNewShapeMaterial_Click(object sender, EventArgs e) {
            Hello();
        }

        private void fieldShapeName_TextChanged(object sender, EventArgs e) {
            loadedShape.name = fieldShapeName.Text;
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
    }
}
