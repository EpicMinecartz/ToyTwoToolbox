using System.Windows.Forms;

namespace ToyTwoToolbox {
    partial class SessionManager {
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
            this.Log = new System.Windows.Forms.RichTextBox();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForceExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ForceGCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RestartSession = new System.Windows.Forms.Button();
            this.ObliterateSession = new System.Windows.Forms.Button();
            this.StopSession = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.UseArgs = new System.Windows.Forms.CheckBox();
            this.UseDebug = new System.Windows.Forms.CheckBox();
            this.SessionList = new System.Windows.Forms.ComboBox();
            this.NewSession = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.MenuStrip1.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Log
            // 
            this.Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.Log.HideSelection = false;
            this.Log.Location = new System.Drawing.Point(0, 0);
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.Size = new System.Drawing.Size(348, 433);
            this.Log.TabIndex = 4;
            this.Log.Text = "";
            this.Log.WordWrap = false;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationToolStripMenuItem,
            this.LogToolStripMenuItem,
            this.ErrorDisplay});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MenuStrip1.Size = new System.Drawing.Size(600, 24);
            this.MenuStrip1.TabIndex = 7;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // ApplicationToolStripMenuItem
            // 
            this.ApplicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestartToolStripMenuItem,
            this.ExitToolStripMenuItem,
            this.ForceExitToolStripMenuItem,
            this.ToolStripSeparator1,
            this.ForceGCToolStripMenuItem});
            this.ApplicationToolStripMenuItem.Name = "ApplicationToolStripMenuItem";
            this.ApplicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.ApplicationToolStripMenuItem.Text = "Application";
            // 
            // RestartToolStripMenuItem
            // 
            this.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem";
            this.RestartToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.RestartToolStripMenuItem.Text = "Restart";
            this.RestartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ForceExitToolStripMenuItem
            // 
            this.ForceExitToolStripMenuItem.Name = "ForceExitToolStripMenuItem";
            this.ForceExitToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.ForceExitToolStripMenuItem.Text = "Force Exit";
            this.ForceExitToolStripMenuItem.Click += new System.EventHandler(this.ForceExitToolStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // ForceGCToolStripMenuItem
            // 
            this.ForceGCToolStripMenuItem.Name = "ForceGCToolStripMenuItem";
            this.ForceGCToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.ForceGCToolStripMenuItem.Text = "Force GC";
            this.ForceGCToolStripMenuItem.Click += new System.EventHandler(this.ForceGCToolStripMenuItem_Click);
            // 
            // LogToolStripMenuItem
            // 
            this.LogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportToolStripMenuItem,
            this.ArchiveToolStripMenuItem,
            this.ClearToolStripMenuItem});
            this.LogToolStripMenuItem.Name = "LogToolStripMenuItem";
            this.LogToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.LogToolStripMenuItem.Text = "Log";
            // 
            // ExportToolStripMenuItem
            // 
            this.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            this.ExportToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.ExportToolStripMenuItem.Text = "Export...";
            this.ExportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // ArchiveToolStripMenuItem
            // 
            this.ArchiveToolStripMenuItem.Name = "ArchiveToolStripMenuItem";
            this.ArchiveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.ArchiveToolStripMenuItem.Text = "Archive...";
            this.ArchiveToolStripMenuItem.Click += new System.EventHandler(this.ArchiveToolStripMenuItem_Click);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.ClearToolStripMenuItem.Text = "Clear";
            this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // ErrorDisplay
            // 
            this.ErrorDisplay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ErrorDisplay.BackColor = System.Drawing.Color.Firebrick;
            this.ErrorDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ErrorDisplay.ForeColor = System.Drawing.Color.White;
            this.ErrorDisplay.Name = "ErrorDisplay";
            this.ErrorDisplay.Size = new System.Drawing.Size(58, 20);
            this.ErrorDisplay.Text = "0 Errors";
            this.ErrorDisplay.Click += new System.EventHandler(this.ErrorDisplay_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.SplitContainer1);
            this.Panel1.Controls.Add(this.MenuStrip1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(600, 457);
            this.Panel1.TabIndex = 8;
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.RestartSession);
            this.SplitContainer1.Panel1.Controls.Add(this.ObliterateSession);
            this.SplitContainer1.Panel1.Controls.Add(this.StopSession);
            this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
            this.SplitContainer1.Panel1.Controls.Add(this.UseArgs);
            this.SplitContainer1.Panel1.Controls.Add(this.UseDebug);
            this.SplitContainer1.Panel1.Controls.Add(this.SessionList);
            this.SplitContainer1.Panel1.Controls.Add(this.NewSession);
            this.SplitContainer1.Panel1.Controls.Add(this.Label1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.Log);
            this.SplitContainer1.Size = new System.Drawing.Size(600, 433);
            this.SplitContainer1.SplitterDistance = 248;
            this.SplitContainer1.TabIndex = 8;
            // 
            // RestartSession
            // 
            this.RestartSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RestartSession.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RestartSession.ForeColor = System.Drawing.Color.Black;
            this.RestartSession.Location = new System.Drawing.Point(197, 31);
            this.RestartSession.Name = "RestartSession";
            this.RestartSession.Size = new System.Drawing.Size(23, 23);
            this.RestartSession.TabIndex = 8;
            this.RestartSession.Text = "R";
            this.RestartSession.UseVisualStyleBackColor = true;
            this.RestartSession.Click += new System.EventHandler(this.RestartSession_Click);
            // 
            // ObliterateSession
            // 
            this.ObliterateSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ObliterateSession.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ObliterateSession.ForeColor = System.Drawing.Color.Red;
            this.ObliterateSession.Location = new System.Drawing.Point(221, 31);
            this.ObliterateSession.Name = "ObliterateSession";
            this.ObliterateSession.Size = new System.Drawing.Size(23, 23);
            this.ObliterateSession.TabIndex = 7;
            this.ObliterateSession.Text = "X";
            this.ObliterateSession.UseVisualStyleBackColor = true;
            this.ObliterateSession.Click += new System.EventHandler(this.ObliterateSession_Click);
            // 
            // StopSession
            // 
            this.StopSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopSession.FlatAppearance.BorderSize = 0;
            this.StopSession.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StopSession.Location = new System.Drawing.Point(149, 31);
            this.StopSession.Name = "StopSession";
            this.StopSession.Size = new System.Drawing.Size(46, 23);
            this.StopSession.TabIndex = 4;
            this.StopSession.Text = "Stop";
            this.StopSession.UseVisualStyleBackColor = true;
            this.StopSession.Click += new System.EventHandler(this.StopSession_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Location = new System.Drawing.Point(10, 59);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(234, 368);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            // 
            // UseArgs
            // 
            this.UseArgs.AutoSize = true;
            this.UseArgs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UseArgs.Location = new System.Drawing.Point(152, 7);
            this.UseArgs.Name = "UseArgs";
            this.UseArgs.Size = new System.Drawing.Size(66, 17);
            this.UseArgs.TabIndex = 5;
            this.UseArgs.Text = "Use Args";
            this.UseArgs.UseVisualStyleBackColor = true;
            // 
            // UseDebug
            // 
            this.UseDebug.AutoSize = true;
            this.UseDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UseDebug.Location = new System.Drawing.Point(88, 7);
            this.UseDebug.Name = "UseDebug";
            this.UseDebug.Size = new System.Drawing.Size(55, 17);
            this.UseDebug.TabIndex = 5;
            this.UseDebug.Text = "Debug";
            this.UseDebug.UseVisualStyleBackColor = true;
            // 
            // SessionList
            // 
            this.SessionList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.SessionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SessionList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SessionList.FormattingEnabled = true;
            this.SessionList.Location = new System.Drawing.Point(60, 32);
            this.SessionList.Name = "SessionList";
            this.SessionList.Size = new System.Drawing.Size(86, 21);
            this.SessionList.TabIndex = 2;
            // 
            // NewSession
            // 
            this.NewSession.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.NewSession.Location = new System.Drawing.Point(7, 3);
            this.NewSession.Name = "NewSession";
            this.NewSession.Size = new System.Drawing.Size(75, 23);
            this.NewSession.TabIndex = 1;
            this.NewSession.Text = "New";
            this.NewSession.UseVisualStyleBackColor = true;
            this.NewSession.Click += new System.EventHandler(this.NewSession_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 35);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Session:";
            // 
            // SessionManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(600, 457);
            this.Controls.Add(this.Panel1);
            this.MainMenuStrip = this.MenuStrip1;
            this.MinimumSize = new System.Drawing.Size(475, 457);
            this.Name = "SessionManager";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.Text = "SessionManager";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SessionManager_Closing);
            this.Load += new System.EventHandler(this.SessionManager_Load);
            this.Shown += new System.EventHandler(this.SessionManager_Shown);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel1.PerformLayout();
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		internal RichTextBox Log;
		internal MenuStrip MenuStrip1;
		internal ToolStripMenuItem ApplicationToolStripMenuItem;
		internal ToolStripMenuItem RestartToolStripMenuItem;
		internal ToolStripMenuItem ExitToolStripMenuItem;
		internal Panel Panel1;
		internal ToolStripMenuItem LogToolStripMenuItem;
		internal ToolStripMenuItem ExportToolStripMenuItem;
		internal ToolStripMenuItem ArchiveToolStripMenuItem;
		internal ToolStripMenuItem ClearToolStripMenuItem;
		internal ToolStripMenuItem ErrorDisplay;
		internal SplitContainer SplitContainer1;
		internal Button StopSession;
		internal ComboBox SessionList;
		internal Button NewSession;
		internal Label Label1;
		internal CheckBox UseArgs;
		internal CheckBox UseDebug;
		internal GroupBox GroupBox1;
		internal Button ObliterateSession;
		internal Button RestartSession;
		internal ToolStripSeparator ToolStripSeparator1;
		internal ToolStripMenuItem ForceGCToolStripMenuItem;
		internal ToolStripMenuItem ForceExitToolStripMenuItem;

		#endregion
	}
}