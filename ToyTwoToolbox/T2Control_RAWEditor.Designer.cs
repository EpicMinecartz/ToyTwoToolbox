namespace ToyTwoToolbox {
    partial class T2Control_RAWEditor {
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
            this.tabControl1 = new ToyTwoToolbox.T2TTabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.butTextureSaveAll = new System.Windows.Forms.Button();
            this.pictureTexture = new System.Windows.Forms.PictureBox();
            this.contextTexture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateAlphaMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTextureInfo = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTexture)).BeginInit();
            this.contextTexture.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabControl1.ControlBox = false;
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1036, 460);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tabPage4.Controls.Add(this.splitContainer1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1028, 431);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Texture";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.butTextureSaveAll);
            this.splitContainer1.Panel2.Controls.Add(this.pictureTexture);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1022, 425);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer2.Size = new System.Drawing.Size(238, 425);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 2;
            // 
            // butTextureSaveAll
            // 
            this.butTextureSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butTextureSaveAll.BackgroundImage = global::ToyTwoToolbox.Properties.Resources.Save;
            this.butTextureSaveAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butTextureSaveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butTextureSaveAll.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butTextureSaveAll.Location = new System.Drawing.Point(7, 387);
            this.butTextureSaveAll.Name = "butTextureSaveAll";
            this.butTextureSaveAll.Size = new System.Drawing.Size(23, 23);
            this.butTextureSaveAll.TabIndex = 18;
            this.butTextureSaveAll.UseVisualStyleBackColor = true;
            // 
            // pictureTexture
            // 
            this.pictureTexture.ContextMenuStrip = this.contextTexture;
            this.pictureTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureTexture.Location = new System.Drawing.Point(0, 20);
            this.pictureTexture.Name = "pictureTexture";
            this.pictureTexture.Size = new System.Drawing.Size(778, 403);
            this.pictureTexture.TabIndex = 0;
            this.pictureTexture.TabStop = false;
            // 
            // contextTexture
            // 
            this.contextTexture.BackColor = System.Drawing.SystemColors.Control;
            this.contextTexture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem1,
            this.CopyToolStripMenuItem,
            this.GenerateAlphaMapToolStripMenuItem});
            this.contextTexture.Name = "TextureRootContext";
            this.contextTexture.Size = new System.Drawing.Size(183, 70);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Image = global::ToyTwoToolbox.Properties.Resources.Save;
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.ToolStripMenuItem1.Text = "&Export...";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.CopyHS;
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.CopyToolStripMenuItem.Text = "&Copy";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // GenerateAlphaMapToolStripMenuItem
            // 
            this.GenerateAlphaMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToFileToolStripMenuItem,
            this.ToClipboardToolStripMenuItem});
            this.GenerateAlphaMapToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.toy2alpham;
            this.GenerateAlphaMapToolStripMenuItem.Name = "GenerateAlphaMapToolStripMenuItem";
            this.GenerateAlphaMapToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.GenerateAlphaMapToolStripMenuItem.Text = "&Generate Alpha Map";
            // 
            // ToFileToolStripMenuItem
            // 
            this.ToFileToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.BuzzFolder;
            this.ToFileToolStripMenuItem.Name = "ToFileToolStripMenuItem";
            this.ToFileToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ToFileToolStripMenuItem.Text = "To &File...";
            this.ToFileToolStripMenuItem.Click += new System.EventHandler(this.ToFileToolStripMenuItem_Click);
            // 
            // ToClipboardToolStripMenuItem
            // 
            this.ToClipboardToolStripMenuItem.Image = global::ToyTwoToolbox.Properties.Resources.Save;
            this.ToClipboardToolStripMenuItem.Name = "ToClipboardToolStripMenuItem";
            this.ToClipboardToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.ToClipboardToolStripMenuItem.Text = "To &Clipboard";
            this.ToClipboardToolStripMenuItem.Click += new System.EventHandler(this.ToClipboardToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelTextureInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 20);
            this.panel1.TabIndex = 1;
            // 
            // labelTextureInfo
            // 
            this.labelTextureInfo.AutoSize = true;
            this.labelTextureInfo.Location = new System.Drawing.Point(3, 3);
            this.labelTextureInfo.Name = "labelTextureInfo";
            this.labelTextureInfo.Size = new System.Drawing.Size(202, 13);
            this.labelTextureInfo.TabIndex = 2;
            this.labelTextureInfo.Text = "Texture: <none> Resolution: 0x0 Index: 0";
            // 
            // T2Control_RAWEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Controls.Add(this.tabControl1);
            this.Name = "T2Control_RAWEditor";
            this.Size = new System.Drawing.Size(1036, 460);
            this.Resize += new System.EventHandler(this.T2Control_RAWEditor_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureTexture)).EndInit();
            this.contextTexture.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private T2TTabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        internal System.Windows.Forms.ContextMenuStrip contextTexture;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem GenerateAlphaMapToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToFileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToClipboardToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button butTextureSaveAll;
        private System.Windows.Forms.PictureBox pictureTexture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTextureInfo;
    }
}
