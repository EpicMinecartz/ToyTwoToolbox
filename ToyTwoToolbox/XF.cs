using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// EXtended WinForms Functionaility
    /// </summary>
    class XF {
		public static System.Collections.IEnumerable GetControls(System.Windows.Forms.Control parent, bool searchContainers) {
			List<System.Windows.Forms.Control> ctrls = new List<System.Windows.Forms.Control>();
			if (!ctrls.Contains(parent)) {
				ctrls.Add(parent);
			}
			foreach (System.Windows.Forms.Control ctrl in parent.Controls) {
				// If TypeOf ctrl Is Control Then
				if (!ctrls.Contains(ctrl)) {
					ctrls.Add((System.Windows.Forms.Control)ctrl);
				}
				// End If
				if (searchContainers && ctrl.Controls.Count > 0) {
					foreach (System.Windows.Forms.Control ctl in GetControls(ctrl, searchContainers)) {
						if (!ctrls.Contains(ctl)) {
							ctrls.Add((System.Windows.Forms.Control)ctl);
						}
					}
				}
			}
			return ctrls;
		}

		public static Point CenterObject(Control Obj, bool bypasscontainer = false, bool CenterX = true, bool CenterY = true, bool IsLoaded = true, bool PrintNewCoords = false) {
			Form Owner = Obj.FindForm();
			if (IsLoaded == true || IsFormLoaded(Owner)) { //protect from early open
				int ConWidth;
				int ConHeight;
				int ConX;
				int ConY;
				if (bypasscontainer == false) {
					ConWidth = Obj.Parent.Width / 2;
					ConHeight = Obj.Parent.Height / 2;
				} else {
					ConWidth = Owner.Width / 2;
					ConHeight = Owner.Height / 2;
				}

                if (CenterX == true) {
                    ConX = ConWidth - Obj.Width / 2;
                } else {
                    ConX = Obj.Location.X;
                }
                
                if (CenterY == true) {
                    ConY = ConHeight - Obj.Height / 2;
                } else {
                    ConY = Obj.Location.Y;
                }
                Obj.Location = new Point(Convert.ToInt32(ConX), Convert.ToInt32(ConY));
			}
			if (PrintNewCoords == true) { Console.WriteLine(Obj.Name + " new position : " + Obj.Location.ToString()); }
			return Obj.Location;
		}

		public static bool IsFormLoaded(Form Form) {
			foreach (Form Frm in Application.OpenForms) {
				if (Frm == Form) { return true; }
			}
			return false;
		}


	}
}
