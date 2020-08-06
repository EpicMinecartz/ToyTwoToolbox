using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ToyTwoToolbox {
    /// <summary>
    /// Summary description for TabControl.
    /// </summary>
    public class T2TTabControl : System.Windows.Forms.TabControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public T2TTabControl() {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #region Interop

        [StructLayout(LayoutKind.Sequential)]
        private struct NMHDR {
            public IntPtr HWND;
            public uint idFrom;
            public int code;
            public override String ToString() {
                return String.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", HWND, idFrom, code);
            }
        }

        private const int TCN_FIRST = 0 - 550;
        private const int TCN_SELCHANGING = (TCN_FIRST - 2);

        private const int WM_USER = 0x400;
        private const int WM_NOTIFY = 0x4E;
        private const int WM_REFLECT = WM_USER + 0x1C00;

        private const int WM_NCHITTEST = 0x0084;
        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;

        #endregion

                #region BackColor/ForeColor Manipulation

        //As well as exposing the property to the Designer we want it to behave just like any other 
        //controls BackColor property so we need some clever manipulation.

        private Color m_Backcolor = Color.Empty;
        [Browsable(true), Description("The background color used to display text and graphics in a control.")]
        public override Color BackColor {
            get {
                if (m_Backcolor.Equals(Color.Empty)) {
                    if (Parent == null)
                        return Control.DefaultBackColor;
                    else
                        return Parent.BackColor;
                }
                return m_Backcolor;
            }
            set {
                if (m_Backcolor.Equals(value)) return;
                m_Backcolor = value;
                Invalidate();
                //Let the Tabpages know that the backcolor has changed.
                base.OnBackColorChanged(EventArgs.Empty);
            }
        }

        private Color m_Forecolor = Color.Empty;
        [Browsable(true), Description("The foreground color used to display text and graphics in this control.")]
        public override Color ForeColor {
            get {
                if (m_Forecolor.Equals(Color.Empty)) {
                    if (Parent == null)
                        return Control.DefaultBackColor;
                    else
                        return Parent.ForeColor;
                }
                return m_Forecolor;
            }
            set {
                if (m_Forecolor.Equals(value)) return;
                m_Forecolor = value;
                Invalidate();
                //Let the Tabpages know that the backcolor has changed.
                base.OnForeColorChanged(EventArgs.Empty);
            }
        }


        public bool ShouldSerializeBackColor() {
            return !m_Backcolor.Equals(Color.Empty);
        }
        public override void ResetBackColor() {
            m_Backcolor = Color.Empty;
            Invalidate();
        }

        public bool ShouldSerializeForeColor() {
            return !m_Forecolor.Equals(Color.Empty);
        }
        public override void ResetForeColor() {
            m_Forecolor = Color.Empty;
            Invalidate();
        }

        #endregion


        private bool m_ControlBox = false;
        [Browsable(true), Description("Display buttons in the tab such as a close button")]
        public bool ControlBox {
            get {
                return m_ControlBox;
            }
            set {
                if (m_ControlBox.Equals(value)) return;
                m_ControlBox = value;
                Invalidate();
                ResetVars();
            }
        }


        protected override void OnParentBackColorChanged(EventArgs e) {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnParentForeColorChanged(EventArgs e) {
            base.OnParentForeColorChanged(e);
            Invalidate();
        }


        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (m_ControlBox == true) {
                Rectangle r = this.GetTabRect(this.SelectedIndex);
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 11, 11);
                if (closeButton.Contains(e.Location)) {
                    TabRequestDestroy.Invoke(this, this.SelectedIndex);
                    //this.TabPages.Remove(this.SelectedTab);
                }
            }
            base.OnMouseDown(e);
            Invalidate();
        }

        public void DestroyTab(int tabID) {

        }


        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);
            Rectangle r = ClientRectangle;
            if (TabCount <= 0) return;
            //Draw a custom background for Transparent TabPages
            r = SelectedTab.Bounds;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font DrawFont = new Font(Font.FontFamily, 24, FontStyle.Regular, GraphicsUnit.Pixel);
            ControlPaint.DrawStringDisabled(e.Graphics, "Draw Error!", DrawFont, BackColor, (RectangleF)r, sf);
            DrawFont.Dispose();
            //Draw a border around TabPage
            r.Inflate(3, 3);
            TabPage tp = TabPages[SelectedIndex];
            SolidBrush PaintBrush = new SolidBrush(tp.BackColor); //TabControl Border
            Pen PaintPen = new Pen(PaintBrush);
            e.Graphics.FillRectangle(PaintBrush, r);
            ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, ButtonBorderStyle.Outset);
            //Draw the Tabs
            for (int index = 0;index <= TabCount - 1;index++) {
                tp = TabPages[index];
                r = GetTabRect(index);
                ButtonBorderStyle bs = ButtonBorderStyle.Outset;
                PaintBrush.Color = tp.BackColor;
                PaintPen.Color = PaintBrush.Color;
                if (index == SelectedIndex) {
                    //bs = ButtonBorderStyle.Outset;
                    //Console.WriteLine(PaintBrush.Color);
                    e.Graphics.FillRectangle(new SolidBrush(m_Backcolor), r);
                    //e.Graphics.DrawLine(new Pen(PaintBrush.Color,1), new Point(r.Left, r.Top), new Point(r.Right, r.Top));
                    //e.Graphics.DrawLine(new Pen(PaintBrush.Color, 1), new Point(r.Left, r.Bottom), new Point(r.Left, r.Top));


                    //ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, bs,);


                    ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, 1, ButtonBorderStyle.Outset, PaintBrush.Color, 1, ButtonBorderStyle.Outset, PaintBrush.Color, 1, ButtonBorderStyle.Inset, PaintBrush.Color, 0, ButtonBorderStyle.None);
                } else {
                    bs = ButtonBorderStyle.Inset;
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), r);
                    ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, bs);
                }
                //PaintBrush.Color = tp.BackColor;
                //e.Graphics.FillRectangle(PaintBrush, r);
                //ControlPaint.DrawBorder(e.Graphics, r, PaintBrush.Color, bs);
                PaintBrush.Color = tp.ForeColor;

                //Set up rotation for left and right aligned tabs
                if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right) {
                    float RotateAngle = 90;
                    if (Alignment == TabAlignment.Left) RotateAngle = 270;
                    PointF cp = new PointF(r.Left + (r.Width >> 1), r.Top + (r.Height >> 1));
                    e.Graphics.TranslateTransform(cp.X, cp.Y);
                    e.Graphics.RotateTransform(RotateAngle);
                    r = new Rectangle(-(r.Height >> 1), -(r.Width >> 1), r.Height, r.Width);
                }
                //Draw the Tab Text
                Rectangle TabText = new Rectangle(r.X, r.Y, r.Width-((m_ControlBox == true) ? 15 : 0), r.Height);
                //debug - draw text region
                //e.Graphics.DrawRectangle(Pens.Red, TabText);
                if (tp.Enabled) {
                    e.Graphics.DrawString(tp.Text, Font, PaintBrush, TabText, sf);
                } else {
                    ControlPaint.DrawStringDisabled(e.Graphics, tp.Text, Font, tp.BackColor, (RectangleF)r, sf);
                }

                if (m_ControlBox == true) {
                    e.Graphics.DrawString("X", Font, Brushes.Red, r.Right - 15, r.Top + 4);
                    e.Graphics.DrawRectangle(Pens.DarkGray, new Rectangle(r.Right - 15, r.Top + 4, 11, 11));
                }



                //e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
                //e.DrawFocusRectangle();

                e.Graphics.ResetTransform();
            }
            PaintBrush.Dispose();
        }


        [Description("Occurs as a tab is being changed.")]
        public event SelectedTabPageChangeEventHandler SelectedIndexChanging;

        [Description("Occurs as a tab is being closed.")]
        public event TabRequestDestroyEventHandler TabRequestDestroy;


        //The default hit-test for a TabControl's
        //background is HTTRANSPARENT, preventing
        //me from receiving mouse and drag events
        //over the background.  I catch this and 
        //replace HTTRANSPARENT with HTCLIENT to 
        //allow the user to drag over us when we 
        //have no TabPages.
        //protected override void WndProc(ref Message m) {
        //    //if (m.Msg == (WM_REFLECT + WM_NOTIFY)) {
        //    //    NMHDR hdr = (NMHDR)(Marshal.PtrToStructure(m.LParam, typeof(NMHDR)));
        //    //    if (hdr.code == TCN_SELCHANGING) {
        //    //        TabPage tp = TestTab(PointToClient(Cursor.Position));
        //    //        if (tp != null) {
        //    //            TabPageChangeEventArgs e = new TabPageChangeEventArgs(SelectedTab, tp);
        //    //            if (SelectedIndexChanging != null)
        //    //                SelectedIndexChanging(this, e);
        //    //            if (e.Cancel || tp.Enabled == false) {
        //    //                m.Result = new IntPtr(1);
        //    //                return;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    if (m.Msg == WM_NCHITTEST) {
        //        if (m.Result.ToInt32() == HTTRANSPARENT) { m.Result = new IntPtr(HTCLIENT); }
        //    }
        //    base.WndProc(ref m);
        //}


        private TabPage TestTab(Point pt) {
            for (int index = 0;index <= TabCount - 1;index++) {
                if (GetTabRect(index).Contains(pt.X, pt.Y))
                    return TabPages[index];
            }
            return null;
        }

        private void ResetVars() {

        }

    }


    public class TabPageChangeEventArgs : EventArgs {
        private TabPage _Selected = null;
        private TabPage _PreSelected = null;
        public bool Cancel = false;

        public TabPage CurrentTab {
            get {
                return _Selected;
            }
        }


        public TabPage NextTab {
            get {
                return _PreSelected;
            }
        }


        public TabPageChangeEventArgs(TabPage CurrentTab, TabPage NextTab) {
            _Selected = CurrentTab;
            _PreSelected = NextTab;
        }


    }


    public delegate void SelectedTabPageChangeEventHandler(Object sender, TabPageChangeEventArgs e);
    public delegate void TabRequestDestroyEventHandler(Object sender, int tabID);

}
