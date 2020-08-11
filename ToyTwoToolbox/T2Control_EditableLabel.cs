using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_EditableLabel : UserControl {
        //this needs some serious reworking
        //particularly because its not supposed to scale with the window size
        public delegate void TextUpdatedEventHandler(Object sender, string text);
        [Description("Occurs when the user updates the field")]
        public event TextUpdatedEventHandler ReportTextUpdate;

        public delegate void CancelEventHandler(Object sender);
        [Description("Occurs when the user updates the field")]
        public event CancelEventHandler ReportCancelUpdate;

        public string Text { get => textBox.Text; set => linkLabel.Text = value; }

        public bool isEditing = false;
        private Size defaultSize;
        private bool maxdisabled = false;
        private bool build = false;

        private Color _labelColor = Color.Empty;
        [Browsable(true), Description("The foreground color used to display the label")]
        public Color labelColor {
            get {
                return _labelColor;
            }
            set {
                if (_labelColor.Equals(value)) return;
                _labelColor = value;
                Invalidate();
            }
        }

        private bool _overflow = false;
        [Browsable(true), Description("Whether to ignore the maximum size when scaling")]
        public bool Overflow {
            get {
                return _overflow;
            }
            set {
                if (_overflow.Equals(value)) return;
                _overflow = value;
                Invalidate();
            }
        }

        private int _maximimwidth = 0;
        [Browsable(true), Description("The maximum width this control can scale to")]
        public int MaxWidth {
            get {
                return _maximimwidth;
            }
            set {
                if (_maximimwidth.Equals(value)) return;
                _maximimwidth = value;
                Invalidate();
            }
        }

        public T2Control_EditableLabel() {
            InitializeComponent();
            linkLabel.ForeColor = _labelColor;
            build = (LicenseManager.UsageMode != LicenseUsageMode.Designtime);
        }

        private void textBox_TextChanged(object sender, EventArgs e) {
            //ResizeControl();
        }

        public void ResizeControl() {
            //I HAVE DISABLED ALL THIS AND IT NOW RELIES ON RIGHT ANCHORS, GO MAKE THEM GO AWAY AND UN COMMENT THIS IF YOU WANT
            //Size TextSize = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            //Size size = TextSize;
            //if (_overflow == true || size.Width < _maximimwidth) {
            //    //if (size.Width + butConfirm.Width * 2 < _maximimwidth) {
            //    //    size = new Size(_maximimwidth - butConfirm.Width * 2, 0);
            //    //} else
            //        //if (size.Width < 33 || size.Width + butConfirm.Width * 2 < 72) {
            //        //    size = new Size(32, 0);
            //        //}
            //        textBox.Multiline = true;
            //    textBox.Width = size.Width;
            //    textBox.Multiline = false;
            //    butConfirm.Location = new Point(textBox.Location.X + size.Width, butConfirm.Location.Y);
            //    butCancel.Location = new Point(butConfirm.Location.X + butConfirm.Width, butConfirm.Location.Y);
            //}
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ToggleEdit(true);
        }

        public void ToggleEdit(bool editing) {
            isEditing = editing;
            linkLabel.Visible = !editing;
            textBox.Visible = editing;
            butConfirm.Visible = editing;
            butCancel.Visible = editing;
        }

        private void butConfirm_Click(object sender, EventArgs e) {
            if (ReportTextUpdate != null) { ReportTextUpdate.Invoke(this, textBox.Text); }
            linkLabel.Text = textBox.Text;
            ToggleEdit(false);
        }

        private void butCancel_Click(object sender, EventArgs e) {
            if (ReportCancelUpdate != null) { ReportCancelUpdate.Invoke(this); }
            ToggleEdit(false);
        }

        private void T2Control_EditableLabel_Load(object sender, EventArgs e) {
            ToggleEdit(false);
            if (build) {
                if (_maximimwidth == 0) { _maximimwidth = this.Size.Width; maxdisabled = true; }
                ResizeControl();
            }
        }

        private void T2Control_EditableLabel_Resize(object sender, EventArgs e) {
            if (build) {
                if (maxdisabled == true) {
                    _maximimwidth = this.Size.Width;
                    ResizeControl();
                }
            }
        }

        private void linkLabel_TextChanged(object sender, EventArgs e) {
            textBox.Text = linkLabel.Text;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                butConfirm.PerformClick();
            } else if (e.KeyCode == Keys.Escape) {
                butCancel.PerformClick();
            }
        }
    }
}
