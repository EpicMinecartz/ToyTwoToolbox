namespace ToyTwoToolbox {
    partial class T2Control_MaterialEditor {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.butRemoveShapeMaterial = new System.Windows.Forms.Button();
            this.groupMaterialProperties = new System.Windows.Forms.GroupBox();
            this.labelMaterialMetadataDesc = new System.Windows.Forms.Label();
            this.numericMaterialMetadata = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.comboMaterialTexture = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.butAmbColorPicker = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.listMaterial = new System.Windows.Forms.ListBox();
            this.groupMaterialProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaterialMetadata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butRemoveShapeMaterial
            // 
            this.butRemoveShapeMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemoveShapeMaterial.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.aclui_126;
            this.butRemoveShapeMaterial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butRemoveShapeMaterial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butRemoveShapeMaterial.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butRemoveShapeMaterial.Location = new System.Drawing.Point(90, 4);
            this.butRemoveShapeMaterial.Name = "butRemoveShapeMaterial";
            this.butRemoveShapeMaterial.Size = new System.Drawing.Size(102, 23);
            this.butRemoveShapeMaterial.TabIndex = 19;
            this.butRemoveShapeMaterial.Text = "Remove Selected";
            this.butRemoveShapeMaterial.UseVisualStyleBackColor = true;
            // 
            // groupMaterialProperties
            // 
            this.groupMaterialProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMaterialProperties.Controls.Add(this.labelMaterialMetadataDesc);
            this.groupMaterialProperties.Controls.Add(this.numericMaterialMetadata);
            this.groupMaterialProperties.Controls.Add(this.label9);
            this.groupMaterialProperties.Controls.Add(this.comboMaterialTexture);
            this.groupMaterialProperties.Controls.Add(this.label8);
            this.groupMaterialProperties.Controls.Add(this.butAmbColorPicker);
            this.groupMaterialProperties.Controls.Add(this.label7);
            this.groupMaterialProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupMaterialProperties.Location = new System.Drawing.Point(3, 3);
            this.groupMaterialProperties.Name = "groupMaterialProperties";
            this.groupMaterialProperties.Size = new System.Drawing.Size(380, 176);
            this.groupMaterialProperties.TabIndex = 17;
            this.groupMaterialProperties.TabStop = false;
            this.groupMaterialProperties.Text = "Material properties";
            this.groupMaterialProperties.Visible = false;
            // 
            // labelMaterialMetadataDesc
            // 
            this.labelMaterialMetadataDesc.AutoSize = true;
            this.labelMaterialMetadataDesc.Location = new System.Drawing.Point(161, 136);
            this.labelMaterialMetadataDesc.Name = "labelMaterialMetadataDesc";
            this.labelMaterialMetadataDesc.Size = new System.Drawing.Size(124, 13);
            this.labelMaterialMetadataDesc.TabIndex = 9;
            this.labelMaterialMetadataDesc.Text = " - Unknown Metadata ID";
            // 
            // numericMaterialMetadata
            // 
            this.numericMaterialMetadata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.numericMaterialMetadata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.numericMaterialMetadata.Location = new System.Drawing.Point(70, 133);
            this.numericMaterialMetadata.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numericMaterialMetadata.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericMaterialMetadata.Name = "numericMaterialMetadata";
            this.numericMaterialMetadata.Size = new System.Drawing.Size(85, 20);
            this.numericMaterialMetadata.TabIndex = 8;
            this.numericMaterialMetadata.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericMaterialMetadata.ValueChanged += new System.EventHandler(this.numericMaterialMetadata_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Metadata:";
            // 
            // comboMaterialTexture
            // 
            this.comboMaterialTexture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.comboMaterialTexture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMaterialTexture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMaterialTexture.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.comboMaterialTexture.FormattingEnabled = true;
            this.comboMaterialTexture.Location = new System.Drawing.Point(61, 79);
            this.comboMaterialTexture.Name = "comboMaterialTexture";
            this.comboMaterialTexture.Size = new System.Drawing.Size(128, 21);
            this.comboMaterialTexture.TabIndex = 6;
            this.comboMaterialTexture.SelectedIndexChanged += new System.EventHandler(this.comboMaterialTexture_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Texture:";
            // 
            // butAmbColorPicker
            // 
            this.butAmbColorPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAmbColorPicker.Location = new System.Drawing.Point(61, 28);
            this.butAmbColorPicker.Name = "butAmbColorPicker";
            this.butAmbColorPicker.Size = new System.Drawing.Size(44, 23);
            this.butAmbColorPicker.TabIndex = 4;
            this.butAmbColorPicker.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Color:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.listMaterial);
            this.splitContainer1.Panel1.Controls.Add(this.butRemoveShapeMaterial);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupMaterialProperties);
            this.splitContainer1.Size = new System.Drawing.Size(585, 182);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Materials:";
            // 
            // listMaterial
            // 
            this.listMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listMaterial.ForeColor = System.Drawing.Color.White;
            this.listMaterial.FormattingEnabled = true;
            this.listMaterial.IntegralHeight = false;
            this.listMaterial.Location = new System.Drawing.Point(3, 31);
            this.listMaterial.Name = "listMaterial";
            this.listMaterial.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listMaterial.Size = new System.Drawing.Size(189, 148);
            this.listMaterial.TabIndex = 20;
            this.listMaterial.SelectedIndexChanged += new System.EventHandler(this.listMaterial_SelectedIndexChanged);
            // 
            // T2Control_MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "T2Control_MaterialEditor";
            this.Size = new System.Drawing.Size(585, 182);
            this.groupMaterialProperties.ResumeLayout(false);
            this.groupMaterialProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaterialMetadata)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button butRemoveShapeMaterial;
        private System.Windows.Forms.GroupBox groupMaterialProperties;
        private System.Windows.Forms.Label labelMaterialMetadataDesc;
        private System.Windows.Forms.NumericUpDown numericMaterialMetadata;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboMaterialTexture;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Button butAmbColorPicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listMaterial;
    }
}
