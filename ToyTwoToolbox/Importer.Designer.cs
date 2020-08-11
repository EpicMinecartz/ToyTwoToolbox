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
            this.butImportCharShape = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butImportCharShape
            // 
            this.butImportCharShape.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.AddMark_10580_inverse;
            this.butImportCharShape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.butImportCharShape.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butImportCharShape.Location = new System.Drawing.Point(12, 12);
            this.butImportCharShape.Name = "butImportCharShape";
            this.butImportCharShape.Size = new System.Drawing.Size(23, 23);
            this.butImportCharShape.TabIndex = 6;
            this.butImportCharShape.UseVisualStyleBackColor = true;
            this.butImportCharShape.Click += new System.EventHandler(this.butImportCharShape_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(41, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(119, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Import";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(202, 47);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.butImportCharShape);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Importer";
            this.Text = "Importer";
            this.Load += new System.EventHandler(this.Importer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butImportCharShape;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}