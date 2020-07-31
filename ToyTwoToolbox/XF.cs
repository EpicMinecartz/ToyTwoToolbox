using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// eXtended winforms Functionaility
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

		public static int CEIO(int Value, int Start, int Target, int correction, int TotalDuration) {
			return (int)(-Target / 2 * (Math.Cos(Math.PI * Value / TotalDuration) - 1) + Start) - correction;
		}


		public static IEnumerable<ctrlType> GetControlsOfType<ctrlType>(Control parent, bool searchContainers = true) where ctrlType : Control {
			List<ctrlType> ctrls = new List<ctrlType>();
			foreach (Control ctrl in parent.Controls) {
				if (ctrl is ctrlType) {
					ctrls.Add((ctrlType)ctrl);
				}
				if (searchContainers && ctrl.Controls.Count > 0) {
					ctrls.AddRange(GetControlsOfType<ctrlType>(ctrl, searchContainers));
				}
			}
			if (parent is ctrlType) { ctrls.Add((ctrlType)parent); }
			return ctrls;
		}

		public static int TextClean(string text, bool opposite = false) {
			if (opposite == true) {
				return text.IndexOf("\\r\\n") / 2;
			} else {
				text = text.Replace("\\r\\n", "");
				return text.Length;
            }

        }

		/// <summary>Allows for a generic list (or List Of List) to be created with duplicated repeated data</summary>
		/// <typeparam name="T">Input the datatype to use in the list: GenerateListData(Of "Type")</typeparam>
		/// <param name="ListCount">The number of lists to return, each the same reference. More than one returns a List(Of List(Of T))</param>
		/// <param name="DataCount">The number of duplicate items in each list</param>
		/// <param name="DataValue">The value of the duplicated data in each list</param>
		/// <returns>Object, make sure to MANUALLY convert the return val to the correct type</returns>
		public static object GenerateListData<T>(int ListCount, int DataCount, T DataValue = default(T)) {
			List<T> L1 = new List<T>();
			L1.AddRange(Enumerable.Repeat(DataValue, DataCount).ToArray());
			if (ListCount > 1) {
				List<List<T>> FL = new List<List<T>>();
				for (var i = 0;i < ListCount;i++) {
					FL.Add(L1);
				}
				return FL;
			} else {
				return L1;
			}
		}

		public static Bitmap CreateTextFromImageFont(string text, Bitmap FontImage, string availableCharacters, Size CharacterBounds, int CharactersPerLine, int CharacterSpacing) {
			Bitmap BBase;

			if (FontImage != null && CharacterBounds.Width != 0 && CharacterBounds.Height != 0 && availableCharacters.Length > 0) {
				int BBWidth = TextClean(text) * (CharacterBounds.Width + CharacterSpacing);
				int BBHeight = TextClean(text, true) * (CharacterBounds.Height + CharacterSpacing);
				BBase = new Bitmap(BBWidth, BBHeight);
				int textOffset = 0;
				int textNewLines = 0;
				//using (SolidBrush brush = new SolidBrush(this.ForeColor)) { graphics.DrawString(this.Text, new Font("Courier New", _CharacterBounds.Height, GraphicsUnit.Pixel), brush, 1, 1); }
				using (Graphics graphics = Graphics.FromImage(BBase)) {
					foreach (char chr in text.ToLower()) {
						if (availableCharacters.IndexOf(chr) == -1) { continue; }
						if ((int)chr == 13) { continue; }
						if ((int)chr == 10) { textOffset++; textNewLines = 0; continue; }

						int FTRow = (int)Math.Ceiling((decimal)(availableCharacters.IndexOf(chr) / CharactersPerLine));
						int FTCol = availableCharacters.IndexOf(chr) % CharactersPerLine;

						int PXOff = CharacterBounds.Width * FTCol;
						int PYOff = CharacterBounds.Height * FTRow;
						Bitmap cloneBitmap = FontImage.Clone(
							new Rectangle {
								Width = CharacterBounds.Width,
								Height = CharacterBounds.Height,
								X = PXOff,
								Y = PYOff
							}, FontImage.PixelFormat);
						graphics.DrawImage(cloneBitmap, new Point((CharacterBounds.Width + CharacterSpacing) * textOffset, (CharacterBounds.Height + CharacterSpacing) * textNewLines));
					}
				}
				return BBase;
			}
			return null;
		}

		public static Color GetTransparentColor(Control CSC) {
			for (;;) {
				if (CSC.BackColor != Color.Transparent) {
					return CSC.BackColor;
                } else {
					if (CSC == CSC.Parent) {
						return Color.Black;
                    } else {
						CSC = CSC.Parent;
                    }
                }
            }
        }

	}
}