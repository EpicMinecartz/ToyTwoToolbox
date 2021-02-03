
namespace ToyTwoToolbox {
    partial class T2Control_MaterialProperties {
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
            this.listviewTextures = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listviewTextures
            // 
            this.listviewTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewTextures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listviewTextures.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listviewTextures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.listviewTextures.FullRowSelect = true;
            this.listviewTextures.GridLines = true;
            this.listviewTextures.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listviewTextures.HideSelection = false;
            this.listviewTextures.LabelEdit = true;
            this.listviewTextures.Location = new System.Drawing.Point(29, 46);
            this.listviewTextures.MultiSelect = false;
            this.listviewTextures.Name = "listviewTextures";
            this.listviewTextures.Size = new System.Drawing.Size(240, 388);
            this.listviewTextures.TabIndex = 1;
            this.listviewTextures.UseCompatibleStateImageBehavior = false;
            this.listviewTextures.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Level Textures";
            this.columnHeader1.Width = 214;
            // 
            // T2Control_MaterialProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.listviewTextures);
            this.Name = "T2Control_MaterialProperties";
            this.Size = new System.Drawing.Size(766, 505);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listviewTextures;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
