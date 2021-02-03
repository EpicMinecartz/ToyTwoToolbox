namespace ToyTwoToolbox {
    partial class Importer {
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
            this.t2TTabControl1 = new ToyTwoToolbox.T2TTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkMaterials = new System.Windows.Forms.CheckBox();
            this.checkRot = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkGZ = new System.Windows.Forms.CheckBox();
            this.checkGY = new System.Windows.Forms.CheckBox();
            this.checkGX = new System.Windows.Forms.CheckBox();
            this.checkTextures = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkName = new System.Windows.Forms.CheckBox();
            this.t2TTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // t2TTabControl1
            // 
            this.t2TTabControl1.ControlBox = false;
            this.t2TTabControl1.Controls.Add(this.tabPage1);
            this.t2TTabControl1.Controls.Add(this.tabPage2);
            this.t2TTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t2TTabControl1.Location = new System.Drawing.Point(0, 0);
            this.t2TTabControl1.Name = "t2TTabControl1";
            this.t2TTabControl1.SelectedIndex = 0;
            this.t2TTabControl1.Size = new System.Drawing.Size(396, 243);
            this.t2TTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(388, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "OBJ Import";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkName);
            this.groupBox2.Controls.Add(this.checkMaterials);
            this.groupBox2.Controls.Add(this.checkRot);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.checkTextures);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(11, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 136);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import Settings";
            // 
            // checkMaterials
            // 
            this.checkMaterials.AutoSize = true;
            this.checkMaterials.Checked = true;
            this.checkMaterials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMaterials.Location = new System.Drawing.Point(6, 40);
            this.checkMaterials.Name = "checkMaterials";
            this.checkMaterials.Size = new System.Drawing.Size(100, 17);
            this.checkMaterials.TabIndex = 1;
            this.checkMaterials.Text = "Import Materials";
            this.checkMaterials.UseVisualStyleBackColor = true;
            // 
            // checkRot
            // 
            this.checkRot.AutoSize = true;
            this.checkRot.Checked = true;
            this.checkRot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRot.Location = new System.Drawing.Point(6, 78);
            this.checkRot.Name = "checkRot";
            this.checkRot.Size = new System.Drawing.Size(58, 17);
            this.checkRot.TabIndex = 0;
            this.checkRot.Text = "Rotate";
            this.checkRot.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.checkGZ);
            this.groupBox4.Controls.Add(this.checkGY);
            this.groupBox4.Controls.Add(this.checkGX);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(6, 80);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 49);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // checkGZ
            // 
            this.checkGZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkGZ.AutoSize = true;
            this.checkGZ.Location = new System.Drawing.Point(303, 23);
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
            this.checkGY.Location = new System.Drawing.Point(155, 23);
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
            // checkTextures
            // 
            this.checkTextures.AutoSize = true;
            this.checkTextures.Checked = true;
            this.checkTextures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTextures.Location = new System.Drawing.Point(6, 19);
            this.checkTextures.Name = "checkTextures";
            this.checkTextures.Size = new System.Drawing.Size(95, 17);
            this.checkTextures.TabIndex = 0;
            this.checkTextures.Text = "Import textures";
            this.checkTextures.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(11, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(366, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Import OBJ Data...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.textBox1.Location = new System.Drawing.Point(63, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(232, 20);
            this.textBox1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "OBJ File:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(301, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(388, 197);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Raw Shape data import";
            // 
            // checkName
            // 
            this.checkName.AutoSize = true;
            this.checkName.Location = new System.Drawing.Point(6, 59);
            this.checkName.Name = "checkName";
            this.checkName.Size = new System.Drawing.Size(91, 17);
            this.checkName.TabIndex = 5;
            this.checkName.Text = "Import Names";
            this.checkName.UseVisualStyleBackColor = true;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(396, 243);
            this.Controls.Add(this.t2TTabControl1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Importer";
            this.Text = "Importer";
            this.Load += new System.EventHandler(this.Importer_Load);
            this.t2TTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private T2TTabControl t2TTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkMaterials;
        private System.Windows.Forms.CheckBox checkRot;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkGZ;
        private System.Windows.Forms.CheckBox checkGY;
        private System.Windows.Forms.CheckBox checkGX;
        private System.Windows.Forms.CheckBox checkTextures;
        private System.Windows.Forms.CheckBox checkName;
    }
}