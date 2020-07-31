namespace ToyTwoToolbox {
    partial class FirstOpenPanel {
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
            this.label1 = new System.Windows.Forms.Label();
            this.LLNewFile = new System.Windows.Forms.LinkLabel();
            this.LLopenFile = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.LLNewNGN = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label1.Location = new System.Drawing.Point(51, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to the Toy Two Toolbox!";
            // 
            // LLNewFile
            // 
            this.LLNewFile.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.LLNewFile.AutoSize = true;
            this.LLNewFile.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LLNewFile.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.LLNewFile.Location = new System.Drawing.Point(4, 56);
            this.LLNewFile.Name = "LLNewFile";
            this.LLNewFile.Size = new System.Drawing.Size(245, 45);
            this.LLNewFile.TabIndex = 1;
            this.LLNewFile.TabStop = true;
            this.LLNewFile.Text = "Create a new file";
            this.LLNewFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // LLopenFile
            // 
            this.LLopenFile.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.LLopenFile.AutoSize = true;
            this.LLopenFile.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LLopenFile.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.LLopenFile.Location = new System.Drawing.Point(278, 56);
            this.LLopenFile.Name = "LLopenFile";
            this.LLopenFile.Size = new System.Drawing.Size(294, 45);
            this.LLopenFile.TabIndex = 2;
            this.LLopenFile.TabStop = true;
            this.LLopenFile.Text = "Open an existing file";
            this.LLopenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LLopenFile_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.label2.Location = new System.Drawing.Point(238, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 45);
            this.label2.TabIndex = 3;
            this.label2.Text = "or";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel5);
            this.panel1.Controls.Add(this.linkLabel4);
            this.panel1.Controls.Add(this.LLNewNGN);
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(580, 59);
            this.panel1.TabIndex = 4;
            // 
            // linkLabel5
            // 
            this.linkLabel5.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel5.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.linkLabel5.Location = new System.Drawing.Point(4, -4);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(52, 60);
            this.linkLabel5.TabIndex = 4;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = " <<\r\nBack";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel5_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.linkLabel4.Location = new System.Drawing.Point(166, 1);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(83, 45);
            this.linkLabel4.TabIndex = 3;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Save";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // LLNewNGN
            // 
            this.LLNewNGN.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.LLNewNGN.AutoSize = true;
            this.LLNewNGN.Font = new System.Drawing.Font("Segoe UI Light", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LLNewNGN.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.LLNewNGN.Location = new System.Drawing.Point(72, 1);
            this.LLNewNGN.Name = "LLNewNGN";
            this.LLNewNGN.Size = new System.Drawing.Size(87, 45);
            this.LLNewNGN.TabIndex = 2;
            this.LLNewNGN.TabStop = true;
            this.LLNewNGN.Text = "NGN";
            this.LLNewNGN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FirstOpenPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LLopenFile);
            this.Controls.Add(this.LLNewFile);
            this.Controls.Add(this.label1);
            this.Name = "FirstOpenPanel";
            this.Size = new System.Drawing.Size(580, 115);
            this.Load += new System.EventHandler(this.FirstOpenPanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel LLNewFile;
        private System.Windows.Forms.LinkLabel LLopenFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel LLNewNGN;
    }
}
