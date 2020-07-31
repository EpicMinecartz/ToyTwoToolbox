using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class T2Control_TextBox : TextBox {
        private Bitmap _FontImage;
        private Size _CharacterBounds;
        private int _CharactersPerLine;
        private List<char> _AvailableCharacters = new List<char>();
        private int _CharacterSpacing;



        public const int WM_NCPAINT = 0x85;

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public extern static IntPtr GetWindowDC(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            if (m.Msg == 0x0200/*WM_MOUSEMOVE */) {
                DrawRealFakeText();

                m.Result = new IntPtr(-1);
            }

            if (m.Msg == WM_NCPAINT) {
                IntPtr hDC = GetWindowDC(m.HWnd);
                using (Graphics g = Graphics.FromHdc(hDC)) {
                    //g.DrawString(this.Text, new Font("Courier New", _CharacterBounds.Height, GraphicsUnit.Pixel), new SolidBrush(this.ForeColor), this.ClientRectangle);
                }
                ReleaseDC(m.HWnd, hDC);
            }
        }



            public T2Control_TextBox() : base() {
            if (IsHandleCreated) {
                SetStyle(ControlStyles.UserPaint, true);
            }
            Multiline = true;
            //Width = 130;
            //Height = 119;
        }


        [ReadOnly(true)]
        public override Font Font {
            get {
                return new Font("Courier New", 12F, FontStyle.Regular, GraphicsUnit.Point);
            }
        }



        [Description("The image used to simulate a font"),
        Category("Appearance"),
        DefaultValue(typeof(Bitmap), "null"),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Bitmap FontImage {
            get { return _FontImage; }
            set {
                _FontImage = value;
                InitializeNewFont();
                Invalidate();
            }
        }

        [Description("The size of each character in the font image"),
        Category("Appearance"),
        DefaultValue(typeof(Size), "10, 10"),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public new Size CharacterBounds {
            get { return _CharacterBounds; }
            set {
                _CharacterBounds = value;
                RecalculateFontBounds();
                Invalidate();
            }
        }

        [Description("The amount of space in pixels between each rendered character"),
        Category("Appearance"),
        DefaultValue(30),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int CharacterSpacing {
            get { return _CharacterSpacing; }
            set {
                _CharacterSpacing = value;
                Invalidate();
            }
        }

        [Description("The amount of characters per line in the font sheet"),
        Category("Appearance"),
        DefaultValue(8),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int CharactersPerLine {
            get { return _CharactersPerLine; }
            set {
                _CharactersPerLine = value;
                Invalidate();
            }
        }

        [Description("The characters (in order) that are available in this font sheet"),
        Category("Appearance"),
        DefaultValue(typeof(string), "abcdefghijklmnopqrstuvwxyz123456789.,;:\\/'!?()\"><"),
        Browsable(true), EditorBrowsable(EditorBrowsableState.Never)]
        public string AvailableCharacters {
            get {
                if (_AvailableCharacters.Count > 0) {
                    return String.Join("", _AvailableCharacters);
                } else { 
                    return ""; 
                }
            }
            set {
                _AvailableCharacters.Clear();
                foreach (char chr in value) {
                    _AvailableCharacters.Add(chr);
                }
                Invalidate();
            }
        }

        public bool InitializeNewFont() {
            return true;
        }

        public bool RecalculateFontBounds() {

            return true;
        }

        public override sealed bool Multiline {
            get { return base.Multiline; }
            set { base.Multiline = value; }
        }

        //protected override void OnPaintBackground(PaintEventArgs e) {
        //    //var buttonPath = new System.Drawing.Drawing2D.GraphicsPath();
        //    //var newRectangle = ClientRectangle;

        //    //newRectangle.Inflate(-10, -10);
        //    //e.Graphics.DrawEllipse(System.Drawing.Pens.Black, newRectangle);
        //    //newRectangle.Inflate(1, 1);
        //    //buttonPath.AddEllipse(newRectangle);
        //    //Region = new System.Drawing.Region(buttonPath);

        //    base.OnPaintBackground(e);
        //}

        protected override void OnPaint(PaintEventArgs e) {
            //TextRenderer.MeasureText(this.Text, Font);
            //TextRenderer.DrawText(e.Graphics, this.Text, Font, new Point(0, 0), Color.Pink);
            base.OnPaint(e);
            DrawRealFakeText();
        }

        protected override void OnTextChanged(EventArgs e) {
            DrawRealFakeText();
        }

        protected void OnMouseMove(EventArgs e) {
            DrawRealFakeText();
        }

        private void DrawRealFakeText() {
            //this.Font = new Font("Arial", _CharacterBounds.Height*2, GraphicsUnit.Pixel);
            if (CharactersPerLine == 0) { CharactersPerLine = 8; }
            if (_FontImage != null && _CharacterBounds.Width != 0 && _CharacterBounds.Height != 0 && _AvailableCharacters.Count > 0) {
                using (Graphics graphics = this.CreateGraphics()) {
                    int stroff = 0;
                    int strrtn = 0;
                    //using (SolidBrush brush = new SolidBrush(this.ForeColor)) { graphics.DrawString(this.Text, new Font("Courier New", _CharacterBounds.Height, GraphicsUnit.Pixel), brush, 1, 1); }
                    foreach (char chr in this.Text.ToLower()) {
                        if ((int)chr == 13) { continue; }
                        if ((int)chr == 10) { strrtn++; stroff = 0; continue; }
                        //we gotta figure out how the current char relates to the img char on the texture
                        //ofc we dont have colum or row index so we have to augment it
                        int FTRow = (int)Math.Ceiling((decimal)(_AvailableCharacters.IndexOf(chr) / _CharactersPerLine));
                        int FTCol = _AvailableCharacters.IndexOf(chr) % _CharactersPerLine;
                        //hahaha you thought that was it, you dont know the power of the dark side
                        int PXOff = _CharacterBounds.Width * FTCol;
                        int PYOff = _CharacterBounds.Height * FTRow;
                        Bitmap cloneBitmap = _FontImage.Clone(
                            new Rectangle {
                                Width = _CharacterBounds.Width,
                                Height = _CharacterBounds.Height,
                                X = PXOff,
                                Y = PYOff
                            }, _FontImage.PixelFormat);
                        graphics.DrawImage(cloneBitmap, new Point((_CharacterBounds.Width + _CharacterSpacing) * stroff, (_CharacterBounds.Height + _CharacterSpacing) * strrtn));
                        stroff++;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
