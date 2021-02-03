using System;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {

    //This whole thing needs rewriting lmao

    public class MessageFade {
        public FaderStyle CurrentStyle = new FaderStyle();
        public Form Overlay = new Form();
        public Label OverlayLabel = new Label();
        public Timer Renderer = new Timer();
        public bool FadingIn = true;
        public bool HideOnMinFade = true;
        public Form Client = null;

        public MessageFade(Form Owner = null) {
            Init(null,Owner);
        }

        public MessageFade(FaderStyle Style = null, Form Owner = null) {
            Init(Style, Owner);
        }

        public void Init(FaderStyle Style, Form Owner = null) {
            Style = Style ?? CurrentStyle;
            //Create label on overlay
            Label OLabel = new Label {
                Font = Style.Font,
                ForeColor = Style.ForeColor,
                Location = new Point(0, 0),
                Name = "OverlayLabel",
                Size = new System.Drawing.Size(462, 37),
                TabIndex = 0,
                Text = "Drag && Drop files here to open"
            };


            //Create Overlay form
            Overlay = new OverlayForm {
                Owner = Owner,
                BackColor = Style.BackColor,
                ControlBox = false,
                FormBorderStyle = FormBorderStyle.None,
                MinimizeBox = false,
                MaximizeBox = false,
                Name = "ORender",
                Opacity = 0.0,
                ShowIcon = false,
                ShowInTaskbar = false,
                Text = ""
            };

            Overlay.Controls.Add(OLabel);
            OverlayLabel = OLabel;
            Overlay.Owner = Owner;
            Overlay.TopMost = true;                    
            Overlay.Show(Owner);
            Client = Owner;

            //backup code for clickthrough lol
            //[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)] static extern int GetWindowLong(IntPtr hWnd, int nIndex);
            //[System.Runtime.InteropServices.DllImport("user32.dll")] static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
            //int initialStyle = GetWindowLong(Overlay.Handle, -20);
            //SetWindowLong(Overlay.Handle, -20, initialStyle | 0x80000 | 0x20);

            Overlay.Hide();
            Renderer.Interval = 25;
            Renderer.Tick += new EventHandler(Render);
        }

        public class OverlayForm : Form {
            protected override CreateParams CreateParams {
                get {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x80000 | 0x20; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST 0x08
                    return cp;
                }
            }

            protected override bool ShowWithoutActivation {
                get {
                    return true;
                }
            }
        }

        public void Render(object sender, EventArgs e) {
            //assume the hook is valid
            if (Client != null && SessionManager.Debug == true) {
                Client.Text = "FT:" + FadingIn + " CFO:" + Overlay.Opacity;
            }
            if (FadingIn == true) {
                if (Overlay.Opacity <= CurrentStyle.OpacityMax) {
                    Overlay.Opacity += CurrentStyle.FadeSpeed;
                } else {
                    Renderer.Stop();
                }
            } else {
                if (Overlay.Opacity > CurrentStyle.OpacityMin) {
                    Overlay.Opacity -= CurrentStyle.FadeSpeed;
                } else {
                    Renderer.Stop();
                    if (HideOnMinFade == true) {
                        Overlay.Hide();
                    }
                }
            }
        }


        public void ApplyStyle(FaderStyle Style) {
            CurrentStyle = Style;
            Overlay.BackColor = Style.BackColor;
            OverlayLabel.Font = Style.Font;
            OverlayLabel.Text = Style.Text;
            OverlayLabel.ForeColor = Style.ForeColor;
        }

        public void FadeIn(Form ParentForReposition = null) {
            if (Overlay.Visible == true) { Overlay.Show(ParentForReposition); }
            if (ParentForReposition != null) {
                Reposition(ParentForReposition);
            } else {
                Reposition(Overlay.Owner,true);
            }
            Overlay.Opacity = 0.5;
            Overlay.Show();
            Overlay.BringToFront();

            if (Overlay.Opacity > 0) {

            }
            FadingIn = true;
            Renderer.Start();
        }

        public void FadeOut() {
            FadingIn = false;
            Renderer.Start();
        }

        public void Show() {
            Overlay.Opacity = CurrentStyle.OpacityMax;
            Overlay.Show();
            Renderer.Stop();
        }

        public void Hide() {
            Overlay.Opacity = CurrentStyle.OpacityMin;
            Overlay.Hide();
            Renderer.Stop();
        }

        public void Reposition(Form Parent, bool IgnoreVisibility = false) {
            if (Overlay.Visible == true || IgnoreVisibility) {
                Rectangle ScreenRectangle = Parent.RectangleToScreen(Parent.ClientRectangle);
                int PWidth = ScreenRectangle.Right - ScreenRectangle.Left;
                int PHeight = ScreenRectangle.Bottom - ScreenRectangle.Top;

                if (!(Overlay.Location.X == ScreenRectangle.Left) || !(Overlay.Location.Y == ScreenRectangle.Top)) {
                    Overlay.Location = new Point(ScreenRectangle.Left, ScreenRectangle.Top);
                }
                if (!(Overlay.Width == PWidth) || !(Overlay.Height == PHeight)) {
                    Overlay.Size = new Size(PWidth, PHeight);
                }
                OverlayLabel.Location = new Point(Convert.ToInt32((PWidth / 2.0) - (OverlayLabel.Width / 3.0)), Convert.ToInt32((PHeight / 2.0) - (OverlayLabel.Height / 2.0)));
            }
        }

        public FaderStyle WindowOpen = new FaderStyle {
            Text = ""
        };

        public class FaderStyle {
            public double FadeSpeed = 0.1;
            public Color BackColor = Color.Black;
            public Color ForeColor = Color.White;
            public Font Font = new Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (0));
            public string Text = "Drag && Drop files here to open";
            public double OpacityMax = 0.8;
            public double OpacityMin = 0.0;
        }

    }
}
