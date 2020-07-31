using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ToyTwoToolbox.Properties;

namespace ToyTwoToolbox {
    class T2Control_Checkbox : CheckBox {
        PictureBox Token;
        bool _showToken = true;

        [Description("Whether or not to show the token when checked"),
        Category("Appearance"),
        DefaultValue(true),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ShowToken {
            get { return _showToken; }
            set {
                _showToken = value;
                Invalidate();
            }
        }


        public T2Control_Checkbox() {
            InitializeComponent();
            this.AutoSize = false;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = Color.FromArgb(32, 32, 32);
            this.Appearance = Appearance.Button;
            this.MinimumSize = new Size(99, 95);
            this.Size = new Size(99, 95);
            this.Image = Resources.T2Image_saveTokenHamm;
            this.TextAlign = ContentAlignment.BottomCenter;
            this.TextImageRelation = TextImageRelation.ImageAboveText;
            Token = new PictureBox {
                Image = Resources.T2Image_TokenAnim,
                Size = new Size(48, 32),
                BackColor = Color.Transparent,
                Location = new Point(52, -2),
                Parent = this,
                Visible = false
            };
            this.Controls.Add(Token);
            this.Text = "<Name>";
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // T2Control_Checkbox
            // 
            this.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckedChanged += new System.EventHandler(this.T2Control_Checkbox_CheckedChanged);
            this.ResumeLayout(false);

        }

        private void T2Control_Checkbox_CheckedChanged(object sender, EventArgs e) {
            if (this.Checked == true) {
                //this.BackColor = Color.FromArgb(0, 1, 0);
                //this.ForeColor = Color.Black;
                this.FlatAppearance.BorderSize = 1;
                Token.Visible = _showToken;
            } else {
                //this.BackColor = Color.Transparent;
                //this.ForeColor = Color.FromArgb(240,240,240);
                this.FlatAppearance.BorderSize = 0;
                Token.Visible = false;
            }
        }
    }
}
