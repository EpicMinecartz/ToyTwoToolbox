using System;
using System.Collections;
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

		public static string BytesToHex(byte[] bytes) {
			char[] c = new char[(bytes.Length * 2)];
			int b = 0;
			for (var i = 0;i < bytes.Length;i++) {
				b = (int)bytes[i] >> 4;
				c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
				b = bytes[i] & 0xF;
				c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
			}
			return new string(c);
		}

		/// <summary>
		/// This function takes any <seealso cref="Image"/> and outputs a standard
		/// 24-bit bitmap from it
		/// </summary>
		/// <param name="IMG">Input Image</param>
		/// <returns>Converted <seealso cref="Image"/></returns>
		public static Image ProParseImage(Image IMG) {
			try {
				using (Bitmap oldBmp = new Bitmap(IMG)) {
					using (Bitmap newBmp = new Bitmap(oldBmp)) {
                        return newBmp.Clone(new Rectangle(0, 0, newBmp.Width, newBmp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
					}
				}
			} catch (Exception) {
				return null;
			}
		}

		public static void ExportImage(string FilePath, Image Img, System.Drawing.Imaging.ImageFormat BaseFormat, string OverrideFormat = null) {
            System.Drawing.Imaging.ImageFormat Imgformat = null;
			if (Img != null) {
				Imgformat = BaseFormat;
				if (!(string.IsNullOrEmpty(OverrideFormat))) {
					if (OverrideFormat.ToLower().Contains("bmp")) {
						Imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
					} else if (OverrideFormat.ToLower().Contains("jpeg")) {
						Imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
					} else if (OverrideFormat.ToLower().Contains("png")) {
						Imgformat = System.Drawing.Imaging.ImageFormat.Png;
					} else if (OverrideFormat.ToLower().Contains("gif")) {
						Imgformat = System.Drawing.Imaging.ImageFormat.Gif;
					}
				}
				Img.Save(FilePath, Imgformat);
			}
		}

		public static void GenerateAlphaMap(Bitmap IMG) {
			BitmapLocker bml = new BitmapLocker();
			bml.AllocateLock(IMG);
			byte[] pixels = bml.pixelData;
			Int32 pix = 0;
			for (int X = 0;X < IMG.Height;X++) {
				for (int Y = 0;Y < IMG.Width;Y++) {
					if (pixels[pix] == 0 && pixels[pix + 1] == 255 && pixels[pix + 2] == 0) { //maybe if (pixels[pix] == 0) for extra speeds?
						pixels[pix + 1] = 0;
					} else {
						pixels[pix] = 255;
						pixels[pix + 1] = 255;
						pixels[pix + 2] = 255;
					}
					pix += 3;
				}
			}
			bml.ReleaseLock(); // Unlock the bitmap data.
		}

		/// <summary>
		/// Move an item in a list based on it's current position in the list
		/// </summary>
		/// <param name="list">The list to enumerate</param>
		/// <param name="item">The index of the affected item</param>
		/// <param name="offset">How much to move the affected item by</param>
		/// <param name="pushToLimit">Whether to move as far as possible in that direction, or just do nothing</param>
		public static void ListMoveItem(System.Collections.IList list, int index, int offset, bool pushToLimit = true) {
			var item = list[index];
			list.RemoveAt(index);
			if (Math.Sign(offset) == 1) {
				if (index + offset > list.Count) { if (pushToLimit) { offset = list.Count - index; } else { return; } }
			} else {
				if (index + offset < 0) { if (pushToLimit) { offset = -(index + (index - offset)); } else { return; } }
			}
			list.Insert(index + offset, item);
		}

	}

	public static class Extentions {
		/// <summary>This is an extention method for the List class that enables Deep Copying</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Obj"></param>
		/// <returns></returns>
		public static T DeepCopy<T>(this T Obj) {
			if (Obj.GetType().IsSerializable == false) {
				return default(T);
			}

			using (System.IO.MemoryStream MStream = new System.IO.MemoryStream()) {
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				Formatter.Serialize(MStream, Obj);
				MStream.Position = 0;
				return (T)Formatter.Deserialize(MStream);
			}
		}

		/// <summary>
		/// Move an item in a list based on it's current position in the list
		/// </summary>
		/// <typeparam name="T">the list</typeparam>
		/// <param name="list"></param>
		/// <param name="item"></param>
		/// <param name="offset"></param>
		/// <param name="pushToLimit"></param>
		//public static void MoveItem<T>(this T list, T item, int offset, bool pushToLimit = true) {
		//	var litem = item;
		//	int index = list.IndexOf(item);
		//	list.Remove(item);
		//	if (Math.Sign(offset) == 1) {
		//		if (index + offset > list.Count) { if (pushToLimit) { offset = list.Count - index; } else { return; } }
		//	} else {
		//		if (index + offset < 0) { if(pushToLimit) { offset = -(index + (index - offset)); } else { return; } }
		//	}
		//	list.Insert(index + offset, litem);
		//}
	}
}