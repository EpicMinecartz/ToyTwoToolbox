using FolderDialog;
using System;
using System.IO;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    //Note, this thing is a bit of a mess.
    /// <summary>A dialog based window to configure NGN export settings</summary>
    public partial class Exporter : Form {
        public string ex_path;
        public bool ex_tex;
        public bool ex_mat;
        public bool ex_vcol;
        public bool ex_alpha;
        public Vector3 ex_rot = new Vector3();
        public bool ex_gx;
        public bool ex_gy;
        public bool ex_gz;
        public string ex_name;
        public bool ex_trn;
        public bool ex_opa;
        public IWorldObject obj;

        public Exporter() {
            InitializeComponent();
        }

        private void Exporter_Load(object sender, EventArgs e) {
            label3.Text = "Exporting:\n" + obj.name + "(" + obj.objectType + ")\n" + obj.shapes.Count + " Shapes\nIn:" + System.IO.Path.GetFileName(obj.owner.FilePath);
            EditableLabelOutputName.Text = "Export";
        }

        public DialogResult Prompt(IWorldObject worldObject) {
            obj = worldObject;
            return this.ShowDialog();
        }

        private void butbrowse_Click(object sender, EventArgs e) {
            FolderSelectDialog fbd = new FolderSelectDialog();
            if (fbd.ShowDialog() == true) {
                textBox1.Text = fbd.FileName;
            }
        }

        private void butexport_Click(object sender, EventArgs e) {
            ex_path = textBox1.Text;
            ex_tex = checkTextures.Checked;
            ex_mat = checkMaterials.Checked;
            ex_vcol = checkMaterials.Checked && checkVCOL.Checked;
            ex_alpha = checkMaterials.Checked && checkAlpha.Checked;
            ex_gx = checkRot.Checked && checkGX.Checked;
            ex_gy = checkRot.Checked && checkGY.Checked;
            ex_gz = checkRot.Checked && checkGZ.Checked;
            ex_rot = new Vector3((ex_gx) ? 1 : 0, (ex_gy) ? 1 : 0, (ex_gz) ? 1 : 0);
            ex_name = EditableLabelOutputName.Text;
            ex_trn = checkMaterials.Checked && checkTrans.Checked;
            ex_opa = checkMaterials.Checked && checkOpacity.Checked;
        }

        private void checkRot_CheckedChanged(object sender, EventArgs e) {
            groupBox4.Enabled = checkRot.Checked;
        }

        private void checkMaterials_CheckedChanged(object sender, EventArgs e) {
            groupBox2.Enabled = checkMaterials.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            butexport.Enabled = Directory.Exists(textBox1.Text);
        }
    }
}
