
namespace ToyTwoToolbox {
    partial class T2Control_TextureSelector {
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
            this.components = new System.ComponentModel.Container();
            this.UIA = new System.Windows.Forms.Timer(this.components);
            this.BasePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // BasePanel
            // 
            this.BasePanel.AutoScroll = true;
            this.BasePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BasePanel.Location = new System.Drawing.Point(0, 0);
            this.BasePanel.Name = "BasePanel";
            this.BasePanel.Size = new System.Drawing.Size(201, 108);
            this.BasePanel.TabIndex = 0;
            this.BasePanel.MouseEnter += new System.EventHandler(this.BasePanel_MouseEnter);
            this.BasePanel.MouseLeave += new System.EventHandler(this.BasePanel_MouseLeave);
            this.BasePanel.Resize += new System.EventHandler(this.BasePanel_Resize);
            // 
            // T2Control_TextureSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.BasePanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "T2Control_TextureSelector";
            this.Size = new System.Drawing.Size(201, 108);
            this.Load += new System.EventHandler(this.T2Control_TextureSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer UIA;
        private System.Windows.Forms.Panel BasePanel;
    }
}
