
namespace ToyTwoToolbox {
    partial class TexturePreview {
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
            this.ITexture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ITexture)).BeginInit();
            this.SuspendLayout();
            // 
            // ITexture
            // 
            this.ITexture.Location = new System.Drawing.Point(3, 3);
            this.ITexture.Name = "ITexture";
            this.ITexture.Size = new System.Drawing.Size(144, 144);
            this.ITexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ITexture.TabIndex = 1;
            this.ITexture.TabStop = false;
            // 
            // TexturePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ITexture);
            this.Name = "TexturePreview";
            this.Load += new System.EventHandler(this.TexturePreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ITexture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox ITexture;
    }
}
