using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public class MessageFade {
		public FaderStyle CurrentStyle;
		public Form Overlay = new Form();
		public Label OverlayLabel = new Label();
		public Timer Renderer = new Timer();
		public bool FadingIn = true;
		public bool HideOnMinFade = true;
		public Form Client = null;

		public MessageFade(Form Owner = null) {
			Init(DefaultStyle, Owner);
		}

		public MessageFade(FaderStyle Style, Form Owner = null) {
			Init(Style, Owner);
		}

		public object Init(FaderStyle Style, Form Owner = null) {
			//Create label on overlay
			Label OLabel = new Label {
				Font = Style.Font,
				ForeColor = Style.ForeColor,
				Location = new Point(0, 0),
				Name = "OverlayLabel",
				Size = new System.Drawing.Size(362, 37),
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
				Opacity = 0,
				ShowIcon = false,
				ShowInTaskbar = false,
				Text = ""
			};
			Overlay.Controls.Add(OLabel);
			OverlayLabel = OLabel;

			Overlay.Owner = Owner;
			Overlay.Show(Owner);
			Client = Owner;
			Overlay.Hide();

			Renderer.Interval = 25;
			Renderer.Tick += new EventHandler(Render);

			CurrentStyle = Style;
			return null;
		}

		public class OverlayForm : Form {
			protected override CreateParams CreateParams {
				get {
					CreateParams cp = base.CreateParams;
					cp.ExStyle = 0x80000 | 0x20; // WS_EX_TOOLWINDOW | WS_EX_TOPMOST 0x08
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
			if (Client != null) {
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




		public object ApplyStyle(FaderStyle Style) {
			CurrentStyle = Style;
			Overlay.BackColor = Style.BackColor;

			OverlayLabel.Font = Style.Font;
			OverlayLabel.ForeColor = Style.ForeColor;
			//Dim OL As Label = CType(Overlay.Controls("OverlayLabel"), Label)
			//OL.
			return null;
		}

		public object FadeIn(Form ParentForReposition = null) {

			if (Overlay.Visible == true) { Overlay.Show(ParentForReposition); }
			//If ParentForReposition IsNot Nothing Then Reposition(ParentForReposition) : ParentForReposition.Focus() Else Reposition(Overlay.Owner) : Overlay.Owner.Focus()
			if (ParentForReposition != null) {
				Reposition(ParentForReposition);
			} else {
				Reposition(Overlay.Owner);
			}
			//Overlay.BringToFront()
			if (Overlay.Opacity > 0) {
				//MessageBox.Show("opr not ready for inst");
			}
			FadingIn = true;
			Renderer.Start();
			return null;
		}

		public object FadeOut() {
			FadingIn = false;
			Renderer.Start();
			return null;
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

		public object Reposition(Form Parent, bool IgnoreVisibility = false) {
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
			return null;
		}

		public FaderStyle DefaultStyle = new FaderStyle {
			FadeSpeed = 0.1,
			BackColor = Color.Black,
			ForeColor = Color.White,
			Font = new Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)((0))),
			OpacityMax = 0.8,
			OpacityMin = 0.0
		};

		public class FaderStyle {
			public double FadeSpeed;
			public Color BackColor;
			public Color ForeColor;
			public Font Font;
			public double OpacityMax;
			public double OpacityMin;
		}

	}
}
