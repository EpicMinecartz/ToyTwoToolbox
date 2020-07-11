using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    class DarkThemeTabControlRender {
        public static void tabControl_DrawItem(object sender, DrawItemEventArgs e) {
            TabControl tabControl = ((TabControl)sender);
            Rectangle tab_rect = tabControl.GetTabRect(e.Index);
            Rectangle lasttabrect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            e.Graphics.FillRectangle(Brushes.DarkOrange, tab_rect);
            e.DrawFocusRectangle();

            

            if (e.State == DrawItemState.Selected) {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), tab_rect);
                e.DrawFocusRectangle();
            } else {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), tab_rect);
            }

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text, tabControl.Font, SystemBrushes.HighlightText, paddedBounds);



            Rectangle background = new Rectangle {

                Size = new Size(tabControl.Right - lasttabrect.Right, lasttabrect.Height + 1),
                Location = new Point(lasttabrect.Right, 0)//pad the rectangle to cover the 1 pixel line between the top of the tabpage and the start of the tabs
            };

            //background.Size = new Size(tabControl.Right - background.Left, lasttabrect.Height + 1);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(15, 15, 15)), background);


            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(15, 15, 15), 3);
            g.DrawRectangle(p, tabControl.Bounds);

        }
    }
}
