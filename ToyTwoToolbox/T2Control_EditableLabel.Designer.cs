namespace ToyTwoToolbox {
    partial class T2Control_EditableLabel {
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.butConfirm = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(32, 20);
            this.textBox.TabIndex = 0;
            this.textBox.Text = "text";
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyUp);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel.LinkColor = System.Drawing.Color.Teal;
            this.linkLabel.Location = new System.Drawing.Point(0, 2);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(24, 13);
            this.linkLabel.TabIndex = 1;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "text";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            this.linkLabel.TextChanged += new System.EventHandler(this.linkLabel_TextChanged);
            // 
            // butConfirm
            // 
            this.butConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.butConfirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.butConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.butConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.butConfirm.Location = new System.Drawing.Point(32, 0);
            this.butConfirm.Margin = new System.Windows.Forms.Padding(0);
            this.butConfirm.Name = "butConfirm";
            this.butConfirm.Size = new System.Drawing.Size(20, 20);
            this.butConfirm.TabIndex = 2;
            this.butConfirm.Text = "✓";
            this.butConfirm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butConfirm.UseVisualStyleBackColor = false;
            this.butConfirm.Click += new System.EventHandler(this.butConfirm_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.butCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.butCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.butCancel.Location = new System.Drawing.Point(51, 0);
            this.butCancel.Margin = new System.Windows.Forms.Padding(0);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(20, 20);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "X";
            this.butCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.butCancel.UseVisualStyleBackColor = false;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // T2Control_EditableLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butConfirm);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.textBox);
            this.Name = "T2Control_EditableLabel";
            this.Size = new System.Drawing.Size(71, 20);
            this.Load += new System.EventHandler(this.T2Control_EditableLabel_Load);
            this.Resize += new System.EventHandler(this.T2Control_EditableLabel_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Button butConfirm;
        private System.Windows.Forms.Button butCancel;
    }
}
