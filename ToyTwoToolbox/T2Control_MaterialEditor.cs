using System.Collections.Generic;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_MaterialEditor : UserControl, IEditor {
        public string tempName { get; set; }
        public string filePath { get; set; }
        public UserControl main { get; set; }

        public TabController.TCTab owner { get; set; }
        private List<Material> materials = new List<Material>();
        public T2Control_MaterialEditor() {
            InitializeComponent();
            this.main = this;
            this.Dock = DockStyle.Fill;
            Init();
        }

        /// <summary>
        /// Late init
        /// </summary>
        /// <param name="file">If a file is needed, then import it here</param>
        public void Init(F_Base file = null) {
            LoadCachedMaterials();
        }

        public void LoadCachedMaterials() {
            materials = Main.Globals.gMMaterials;
            comboMaterialTexture.Items.Clear();
            List<Texture> texs = materials[0].owner.owner.owner.textures;
            if (texs.Count > 0) {
                foreach (Texture tex in texs) {
                    comboMaterialTexture.Items.Add(tex.name);
                }
            }
            foreach (Material mat in materials) {
                listMaterial.Items.Add(mat.owner.owner.name + " -> " + mat.owner.name + " -> mat");
            }
        }

        public bool SaveChanges(bool JustMemory = false, string path = "") {
            return false;
        }

        private void listMaterial_SelectedIndexChanged(object sender, System.EventArgs e) {
            groupMaterialProperties.Visible = true;
            Material mat = materials[listMaterial.SelectedIndex];
            butAmbColorPicker.BackColor = XF.NGNColToColor(mat.RGB);
            comboMaterialTexture.SelectedIndex = (mat.textureIndex != 65535 && mat.textureIndex <= listMaterial.Items.Count) ? mat.textureIndex : 0;
            if (mat.textureIndex > listMaterial.Items.Count) { SessionManager.Report("Material " + listMaterial.SelectedIndex + " in shape has an invalid textureindex (" + mat.textureIndex + ")", SessionManager.RType.WARN); }
            numericMaterialMetadata.Value = (mat.id == 129 ? -1 : mat.metadata);
        }

        private void comboMaterialTexture_SelectedIndexChanged(object sender, System.EventArgs e) {

        }

        private void numericMaterialMetadata_ValueChanged(object sender, System.EventArgs e) {

        }
    }
}
