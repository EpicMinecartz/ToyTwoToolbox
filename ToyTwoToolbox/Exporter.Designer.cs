namespace ToyTwoToolbox {
    partial class Exporter {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.butbrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkMaterials = new System.Windows.Forms.CheckBox();
            this.checkRot = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkGZ = new System.Windows.Forms.CheckBox();
            this.checkGY = new System.Windows.Forms.CheckBox();
            this.checkGX = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkTrans = new System.Windows.Forms.CheckBox();
            this.checkAlpha = new System.Windows.Forms.CheckBox();
            this.checkVCOL = new System.Windows.Forms.CheckBox();
            this.checkTextures = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.butexport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.EditableLabelOutputName = new ToyTwoToolbox.T2Control_EditableLabel();
            this.checkOpacity = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output Folder:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(15, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(284, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // butbrowse
            // 
            this.butbrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butbrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butbrowse.Location = new System.Drawing.Point(305, 23);
            this.butbrowse.Name = "butbrowse";
            this.butbrowse.Size = new System.Drawing.Size(75, 23);
            this.butbrowse.TabIndex = 2;
            this.butbrowse.Text = "Browse...";
            this.butbrowse.UseVisualStyleBackColor = true;
            this.butbrowse.Click += new System.EventHandler(this.butbrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output Format:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 208);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(98, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Wavefront OBJ";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkMaterials);
            this.groupBox1.Controls.Add(this.checkRot);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.checkTextures);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 235);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Settings";
            // 
            // checkMaterials
            // 
            this.checkMaterials.AutoSize = true;
            this.checkMaterials.Checked = true;
            this.checkMaterials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMaterials.Location = new System.Drawing.Point(6, 40);
            this.checkMaterials.Name = "checkMaterials";
            this.checkMaterials.Size = new System.Drawing.Size(101, 17);
            this.checkMaterials.TabIndex = 1;
            this.checkMaterials.Text = "Export Materials";
            this.checkMaterials.UseVisualStyleBackColor = true;
            this.checkMaterials.CheckedChanged += new System.EventHandler(this.checkMaterials_CheckedChanged);
            // 
            // checkRot
            // 
            this.checkRot.AutoSize = true;
            this.checkRot.Checked = true;
            this.checkRot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRot.Location = new System.Drawing.Point(6, 138);
            this.checkRot.Name = "checkRot";
            this.checkRot.Size = new System.Drawing.Size(58, 17);
            this.checkRot.TabIndex = 0;
            this.checkRot.Text = "Rotate";
            this.checkRot.UseVisualStyleBackColor = true;
            this.checkRot.CheckedChanged += new System.EventHandler(this.checkRot_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.checkGZ);
            this.groupBox4.Controls.Add(this.checkGY);
            this.groupBox4.Controls.Add(this.checkGX);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(6, 140);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(353, 49);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // checkGZ
            // 
            this.checkGZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkGZ.AutoSize = true;
            this.checkGZ.Location = new System.Drawing.Point(302, 23);
            this.checkGZ.Name = "checkGZ";
            this.checkGZ.Size = new System.Drawing.Size(33, 17);
            this.checkGZ.TabIndex = 2;
            this.checkGZ.Text = "Z";
            this.checkGZ.UseVisualStyleBackColor = true;
            // 
            // checkGY
            // 
            this.checkGY.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkGY.AutoSize = true;
            this.checkGY.Checked = true;
            this.checkGY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkGY.Location = new System.Drawing.Point(154, 23);
            this.checkGY.Name = "checkGY";
            this.checkGY.Size = new System.Drawing.Size(33, 17);
            this.checkGY.TabIndex = 2;
            this.checkGY.Text = "Y";
            this.checkGY.UseVisualStyleBackColor = true;
            // 
            // checkGX
            // 
            this.checkGX.AutoSize = true;
            this.checkGX.Checked = true;
            this.checkGX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkGX.Location = new System.Drawing.Point(6, 23);
            this.checkGX.Name = "checkGX";
            this.checkGX.Size = new System.Drawing.Size(33, 17);
            this.checkGX.TabIndex = 2;
            this.checkGX.Text = "X";
            this.checkGX.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkOpacity);
            this.groupBox2.Controls.Add(this.checkTrans);
            this.groupBox2.Controls.Add(this.checkAlpha);
            this.groupBox2.Controls.Add(this.checkVCOL);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(6, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 92);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // checkTrans
            // 
            this.checkTrans.AutoSize = true;
            this.checkTrans.Checked = true;
            this.checkTrans.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTrans.Location = new System.Drawing.Point(6, 45);
            this.checkTrans.Name = "checkTrans";
            this.checkTrans.Size = new System.Drawing.Size(182, 17);
            this.checkTrans.TabIndex = 3;
            this.checkTrans.Text = "Replace green with transparency";
            this.checkTrans.UseVisualStyleBackColor = true;
            // 
            // checkAlpha
            // 
            this.checkAlpha.AutoSize = true;
            this.checkAlpha.Location = new System.Drawing.Point(194, 45);
            this.checkAlpha.Name = "checkAlpha";
            this.checkAlpha.Size = new System.Drawing.Size(127, 17);
            this.checkAlpha.TabIndex = 2;
            this.checkAlpha.Text = "Generate alpha maps";
            this.checkAlpha.UseVisualStyleBackColor = true;
            // 
            // checkVCOL
            // 
            this.checkVCOL.AutoSize = true;
            this.checkVCOL.Checked = true;
            this.checkVCOL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkVCOL.Location = new System.Drawing.Point(6, 23);
            this.checkVCOL.Name = "checkVCOL";
            this.checkVCOL.Size = new System.Drawing.Size(184, 17);
            this.checkVCOL.TabIndex = 0;
            this.checkVCOL.Text = "Average vertex color into ambient";
            this.checkVCOL.UseVisualStyleBackColor = true;
            // 
            // checkTextures
            // 
            this.checkTextures.AutoSize = true;
            this.checkTextures.Checked = true;
            this.checkTextures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTextures.Location = new System.Drawing.Point(6, 19);
            this.checkTextures.Name = "checkTextures";
            this.checkTextures.Size = new System.Drawing.Size(96, 17);
            this.checkTextures.TabIndex = 0;
            this.checkTextures.Text = "Export textures";
            this.checkTextures.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 52);
            this.label3.TabIndex = 7;
            this.label3.Text = "Exporting:\r\n<>(IWorldObject.objectType)\r\n0 Shapes\r\nIn <>";
            // 
            // butexport
            // 
            this.butexport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.butexport.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butexport.Enabled = false;
            this.butexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butexport.Location = new System.Drawing.Point(14, 384);
            this.butexport.Name = "butexport";
            this.butexport.Size = new System.Drawing.Size(366, 23);
            this.butexport.TabIndex = 8;
            this.butexport.Text = "Export!";
            this.butexport.UseVisualStyleBackColor = true;
            this.butexport.Click += new System.EventHandler(this.butexport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Output Name:";
            // 
            // EditableLabelOutputName
            // 
            this.EditableLabelOutputName.BackColor = System.Drawing.Color.Transparent;
            this.EditableLabelOutputName.labelColor = System.Drawing.Color.Empty;
            this.EditableLabelOutputName.Location = new System.Drawing.Point(15, 64);
            this.EditableLabelOutputName.MaxWidth = 0;
            this.EditableLabelOutputName.Name = "EditableLabelOutputName";
            this.EditableLabelOutputName.Overflow = false;
            this.EditableLabelOutputName.Size = new System.Drawing.Size(365, 20);
            this.EditableLabelOutputName.TabIndex = 10;
            // 
            // checkOpacity
            // 
            this.checkOpacity.AutoSize = true;
            this.checkOpacity.Location = new System.Drawing.Point(6, 68);
            this.checkOpacity.Name = "checkOpacity";
            this.checkOpacity.Size = new System.Drawing.Size(153, 17);
            this.checkOpacity.TabIndex = 4;
            this.checkOpacity.Text = "Apply ambient opacity data";
            this.checkOpacity.UseVisualStyleBackColor = true;
            // 
            // Exporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(396, 419);
            this.Controls.Add(this.EditableLabelOutputName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butexport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.butbrowse);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Exporter";
            this.ShowIcon = false;
            this.Text = "Exporter";
            this.Load += new System.EventHandler(this.Exporter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button butbrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkGZ;
        private System.Windows.Forms.CheckBox checkGY;
        private System.Windows.Forms.CheckBox checkGX;
        private System.Windows.Forms.CheckBox checkRot;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkAlpha;
        private System.Windows.Forms.CheckBox checkMaterials;
        private System.Windows.Forms.CheckBox checkVCOL;
        private System.Windows.Forms.CheckBox checkTextures;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butexport;
        private System.Windows.Forms.Label label4;
        private T2Control_EditableLabel EditableLabelOutputName;
        private System.Windows.Forms.CheckBox checkTrans;
        private System.Windows.Forms.CheckBox checkOpacity;
    }
}