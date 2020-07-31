using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ToyTwoToolbox {
    class DarkThemeMenuRender : ToolStripSystemRenderer {

		public Color DarkTheme_UI_MenuBack = Color.FromArgb(40, 40, 40);
		public Color DarkTheme_UI_Text = Color.FromArgb(240, 240, 240);
		public Color DarkTheme_UI_MenuSel = Color.FromArgb(50, 50, 50);

		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e) {
			e.ToolStrip.BackColor = DarkTheme_UI_MenuBack;//realistically this should only be done once lmao
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
            if (!e.Item.Selected) {
                e.Graphics.FillRectangle(new SolidBrush(DarkTheme_UI_MenuBack), rectangle);
            } else {
                if (e.Item.Enabled) {
                    this.RenderSelectedButtonFill(e.Graphics, rectangle);
                } else {
                    e.Graphics.FillRectangle(new SolidBrush(DarkTheme_UI_MenuBack), rectangle);
                }
            }
        }

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e) {
			if (e.Item is ToolStripMenuItem) { //AndAlso (e.Item.Selected OrElse e.Item.Pressed) Then
				e.TextColor = DarkTheme_UI_Text;
			}
			base.OnRenderItemText(e);

		}

		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e) {
            e.ToolStrip.BackColor = DarkTheme_UI_MenuBack; 
                                                           //AddHandler e.ToolStrip.Items(e.ToolStrip.Items.IndexOf(e.Item)).Paint, AddressOf ExtendedToolStripSeparator_Paint 'dont do this now bcuz of above

            Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);

            e.Graphics.FillRectangle(new SolidBrush(DarkTheme_UI_MenuBack), rectangle);
            this.RenderSeparatorInternal(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size), e.Vertical);
        }



		public void ExtendedToolStripSeparator_Paint(object sender, PaintEventArgs e) {
			ToolStripSeparator toolStripSeparator = (ToolStripSeparator)sender;
			e.Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, toolStripSeparator.Width + 15, toolStripSeparator.Height + 15);
		} 

		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) {
            if (e.Item is ToolStripDropDownItem) {
                e.ArrowColor = DarkTheme_UI_Text;
            }
            base.OnRenderArrow(e);
        }


		private void RenderSeparatorInternal1(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical) {
			VisualStyleElement separator = vertical ? VisualStyleElement.ToolBar.SeparatorHorizontal.Normal : VisualStyleElement.ToolBar.SeparatorVertical.Normal;

			if (ToolStripManager.VisualStylesEnabled && (VisualStyleRenderer.IsElementDefined(separator))) {
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer("ExplorerBar", 0, 0);

				visualStyleRenderer.SetParameters(separator.ClassName, separator.Part, 1); //GetItemState(item))
				visualStyleRenderer.DrawBackground(g, bounds);
			} else {

				Color foreColor = item.ForeColor;
				Color backColor = item.BackColor;

				Pen foreColorPen = SystemPens.ControlDark;
				bool disposeForeColorPen = GetPen(foreColor, ref foreColorPen);

				try {
					if (vertical) {
						if (bounds.Height >= 4) { // scoot down 2PX and start drawing
							bounds.Inflate(0, -2);
						}

						bool rightToLeft = (item.RightToLeft == RightToLeft.Yes);
						Pen leftPen = rightToLeft ? SystemPens.ButtonHighlight : foreColorPen;
						Pen rightPen = rightToLeft ? foreColorPen : SystemPens.ButtonHighlight;

						// Draw dark line
						int startX = bounds.Width / 2;
						g.DrawLine(leftPen, startX, bounds.Top, startX, bounds.Bottom);

						// Draw highlight one pixel to the right
						startX += 1;
						g.DrawLine(rightPen, startX, bounds.Top, startX, bounds.Bottom);

					} else {
						//
						// horizontal separator
						if (bounds.Width >= 4) { // scoot over 2PX and start drawing
							bounds.Inflate(-2, 0);
						}

						// Draw dark line
						int startY = bounds.Height / 2;
						g.DrawLine(foreColorPen, bounds.Left, startY, bounds.Right, startY);

						// Draw highlight one pixel to the right
						startY += 1;
						g.DrawLine(SystemPens.ButtonHighlight, bounds.Left, startY, bounds.Right, startY);
					}

				} finally {
					if (disposeForeColorPen && foreColorPen != null) {
						foreColorPen.Dispose();
					}
				}
			}
		}

		private void RenderSeparatorInternal(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical) {
            Pen foreColorPen = SystemPens.ControlDark;
            Pen highlightColorPen = SystemPens.ButtonHighlight;
            bool isASeparator = item is ToolStripSeparator;
            bool isAHorizontalSeparatorNotOnDropDownMenu = false;

            if (isASeparator) {
                if (vertical) {
                    if (!item.IsOnDropDown) {
                        // center so that it matches office
                        bounds.Y += 3;
                        bounds.Height = Math.Max(0, bounds.Height - 6);
                    }
                } else {
                    // offset after the image margin
                    ToolStripDropDownMenu dropDownMenu = item.GetCurrentParent() as ToolStripDropDownMenu;
                    if (dropDownMenu != null) {
                        if (dropDownMenu.RightToLeft == RightToLeft.No) {
                            // scoot over by the padding (that will line you up with the text - but go two PX before so that it visually looks
                            // like the line meets up with the text).
                            bounds.X += dropDownMenu.Padding.Left - 2;
                            bounds.Width = dropDownMenu.Width - bounds.X;
                        } else {
                            // scoot over by the padding (that will line you up with the text - but go two PX before so that it visually looks
                            // like the line meets up with the text).
                            bounds.X += 2;
                            bounds.Width = dropDownMenu.Width - bounds.X - dropDownMenu.Padding.Right;

                        }
                    } else {
                        isAHorizontalSeparatorNotOnDropDownMenu = true;

                    }
                }
            }
            try {
                if (vertical) {
                    if (bounds.Height >= 4) { // scoot down 2PX and start drawing
                        bounds.Inflate(0, -2);
                    }

                    bool rightToLeft = (item.RightToLeft == RightToLeft.Yes);
                    Pen leftPen = rightToLeft ? highlightColorPen : foreColorPen;
                    Pen rightPen = rightToLeft ? foreColorPen : highlightColorPen;

                    // Draw dark line
                    int startX = bounds.Width / 2;

                    g.DrawLine(leftPen, startX, bounds.Top, startX, bounds.Bottom - 1);

                    // Draw highlight one pixel to the right
                    startX += 1;
                    g.DrawLine(rightPen, startX, bounds.Top + 1, startX, bounds.Bottom);
                } else {
                    //
                    // horizontal separator
                    // Draw dark line

                    if (isAHorizontalSeparatorNotOnDropDownMenu && bounds.Width >= 4) { // scoot down 2PX and start drawing
                        bounds.Inflate(-2, 0);
                    }
                    int startY = bounds.Height / 2;

                    g.DrawLine(foreColorPen, bounds.Left, startY, bounds.Right - 1, startY);

                    if ((!isASeparator) || isAHorizontalSeparatorNotOnDropDownMenu) {
                        // Draw highlight one pixel to the right
                        startY += 1;
                        g.DrawLine(highlightColorPen, bounds.Left + 1, startY, bounds.Right - 1, startY);
                    }
                }
            } finally {
                //If disposeForeColorPen AndAlso foreColorPen IsNot Nothing Then foreColorPen.Dispose()

                //If disposeHighlightColorColorPen AndAlso highlightColorPen IsNot Nothing Then highlightColorPen.Dispose()
            }
        }

		private static bool GetPen(Color color, ref Pen pen) {
			if (color.IsSystemColor) {
				pen = SystemPens.FromSystemColor(color);
				return false;
			} else {
				pen = new Pen(color);
				return true;
			}
		}


		private void RenderSelectedButtonFill(Graphics g, Rectangle bounds) {
			if (bounds.Width == 0 || bounds.Height == 0) {
				return;
			}
			g.FillRectangle(new System.Drawing.SolidBrush(DarkTheme_UI_MenuSel), bounds);
			//g.FillRectangle(New Drawing2D.LinearGradientBrush(
			//                bounds,
			//                Me.ColorTable.ButtonSelectedGradientBegin,
			//                Me.ColorTable.ButtonSelectedGradientEnd,
			//                Drawing2D.LinearGradientMode.Vertical),
			//                bounds)
		}

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e) {
            base.OnRenderSplitButtonBackground(e);
            DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, e.Item as ToolStripSplitButton, ((ToolStripSplitButton)e.Item).DropDownButtonBounds, Color.FromArgb(240,240,240), ArrowDirection.Down));
            //ToolStripSplitButton btn = e.Item as ToolStripSplitButton;
            //Rectangle rc = btn.DropDownButtonBounds;
            ////base.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, e.Item, rc, Color.Black, ArrowDirection.Right));
            //    int x = rc.Left + rc.Width - 8;
            //    int y = rc.Top + rc.Height / 2;
            //    Point[] arrow = new Point[3];
            //    arrow[0] = new Point(x, y - 5);
            //    arrow[1] = new Point(x + 6, y);
            //    arrow[2] = new Point(x, y + 5);
            //    e.Graphics.FillPolygon(Brushes.Black, arrow);
        }

        public void DrawArrow(ToolStripArrowRenderEventArgs e) {
            OnRenderArrow(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {

		}
	}
}
