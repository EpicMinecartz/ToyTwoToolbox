using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                if (!ctrls.Contains(ctrl)) {
                    ctrls.Add(ctrl);
                }
                if (searchContainers && ctrl.Controls.Count > 0) {
                    foreach (System.Windows.Forms.Control ctl in GetControls(ctrl, searchContainers)) {
                        if (!ctrls.Contains(ctl)) {
                            ctrls.Add(ctl);
                        }
                    }
                }
            }
            return ctrls;
        }

        /// <summary>Center a control in a container</summary>
        /// <param name="Obj">The control to modify</param>
        /// <param name="bypasscontainer">Whether to relate to the parent control or the parent form</param>
        /// <param name="CenterX">Whether to center along the X axis</param>
        /// <param name="CenterY">Whether to center along the Y axis</param>
        /// <param name="IsLoaded">Whether to manually specifiy if the form is loaded</param>
        /// <param name="PrintNewCoords">Whether to print the new coords of the control</param>
        /// <returns>The new <seealso cref="Point"/> of the control</returns>
        public static Point CenterObject(Control Obj, bool bypasscontainer = false, bool CenterX = true, bool CenterY = true, bool IsLoaded = true, bool PrintNewCoords = false) {
            Form Owner = Obj.FindForm();
            if (IsLoaded == true || IsFormLoaded(Owner)) { //protect from early open
                int ConWidth, ConHeight, ConX, ConY;
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
                        if (chr == 13) { continue; }
                        if (chr == 10) { textOffset++; textNewLines = 0; continue; }

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
            for (;;){
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
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }

        /// <summary>This function takes any <seealso cref="Image"/> and outputs a standard 24-bit bitmap from it</summary>
        /// <param name="IMG">Input Image</param>
        /// <returns>Converted <seealso cref="Image"/></returns>
        public static Image ProParseImage(Image IMG) {
            try {
                using (Bitmap oldBmp = new Bitmap(IMG)) {
                    using (Bitmap newBmp = new Bitmap(oldBmp)) {
                        return newBmp.Clone(new Rectangle(0, 0, newBmp.Width, newBmp.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    }
                }
            } catch (Exception ex) {
                SessionManager.Report(ex.ToString(), SessionManager.RType.ERROR);
                return null;
            }
        }

        public static void ExportImage(string FilePath, Image Img, System.Drawing.Imaging.ImageFormat BaseFormat, string OverrideFormat = null) {
            if (Img != null) {
                System.Drawing.Imaging.ImageFormat Imgformat = BaseFormat;
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

        /// <summary>Move an item in a list based on it's current position in the list</summary>
        /// <param name="list">The list to enumerate</param>
        /// <param name="item">The index of the affected item</param>
        /// <param name="offset">How much to move the affected item by</param>
        /// <param name="pushToLimit">Whether to move as far as possible in that direction, or just do nothing</param>
        public static int ListMoveItem(System.Collections.IList list, int index, int offset, bool pushToLimit = true) {
            if (index != -1) {
                var item = list[index];
                list.RemoveAt(index);
                if (Math.Sign(offset) == 1) {
                    if (index + offset > list.Count) { if (pushToLimit) { offset = list.Count - index; } else { return 0; } }
                } else {
                    if (index + offset < 0) { if (pushToLimit) { offset =0; } else { return 0; } }
                }
                list.Insert(index + offset, item);
                return index + offset;
            }
            return 0;
        }
        //public static int ListMoveItem(System.Collections.IList list, int index, int offset, bool pushToLimit = true) {
        //    if (index != -1) {
        //        var item = list[index];
        //        list.RemoveAt(index);
        //        if (Math.Sign(offset) == 1) {
        //            if (index + offset > list.Count) { if (pushToLimit) { offset = list.Count - index; } else { return 0; } }
        //        } else {
        //            if (index + offset < 0) { if (pushToLimit) { offset = -(index + (index - offset)); } else { return 0; } }
        //        }
        //        list.Insert(index + offset, item);
        //        return index + offset;
        //    }
        //    return 0;
        //}

        public static Color NGNColToColor(List<double> ARGBL) {
            //if theres an instance of a ngn color having 4 values, ill fix it then
            //if (ARGBL.Count < 4) { ARGBL.Insert(0, 1.0000000695); }
            if (ARGBL.Count > 2) {
                return Color.FromArgb(
                        (int)(ARGBL[0] / 0.0039215689),
                        (int)(ARGBL[1] / 0.0039215689),
                        (int)(ARGBL[2] / 0.0039215689)
                        );
            } else {
                return Color.Black;
            }
        }

        public static List<double> ColorToNGNColor(Color color) {
            return new List<double> {
                color.A * 1.0 * 0.0039215689,
                color.R * 1.0 * 0.0039215689,
                color.G * 1.0 * 0.0039215689,
                color.B * 1.0 * 0.0039215689
            };
        }

        public static float[] CompileVirtualOBJ(List<Shape> shapes) {
            List<float> vobj = new List<float>();
            foreach (Shape shape in shapes) {
                for (int i = 0;i < shape.rawVertices.Count;i++) {
                    vobj.Add(shape.rawVertices[i].X);
                    vobj.Add(shape.rawVertices[i].Y);
                    vobj.Add(shape.rawVertices[i].Z);
                    if (shape.type != 4) { vobj.Add(shape.rawVertices[i].Z); }
                    vobj.Add(1.0f);
                    vobj.Add(1.0f);
                    vobj.Add(1.0f);
                    vobj.Add(shape.rawVertexTextureCoords[i].X);
                    vobj.Add(shape.rawVertexTextureCoords[i].Y);
                }
            }
            return vobj.ToArray();
        }

        private static readonly Random r = new Random();
        /// <summary>Generate a random integer within the specified range. (Or 0 - 2,147,483,647 if no parameters are supplied)</summary>
        /// <param name="min">The lowest value that can be produced</param>
        /// <param name="max">The highest value that can be produced</param>
        /// <returns></returns>
        public static int Random(int min = 0, int max = 2147483647) {
            return r.Next(min, max);
        }

        /// <summary>Returns an array of items based on the selection of items in a listbox.</summary>
        /// <param name="selectedIndexs">The <see cref="ListBox.SelectedIndexCollection"/> to use for collection</param>
        /// <param name="items">The array to collect items from</param>
        /// <returns>Generic auto cast <see cref="T"/>[] of selected items</returns>
        public static T[] ItemsFromListBox<T>(ListBox.SelectedIndexCollection selectedIndexs, T[] items) {
            T[] selecteditems = new T[selectedIndexs.Count];
            for (int i = 0;i < selectedIndexs.Count;i++) {
                selecteditems[i] = items[i];
            }
            return selecteditems;
        }

    }

    public static class Extentions {
        /// <summary>This is an extention method for the List class that enables Deep Copying</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <returns></returns>
        //public static T DeepCopy<T>(this T Obj) {
        //    if (Obj.GetType().IsSerializable == false) {
        //        return default(T);
        //    }

        //    using (System.IO.MemoryStream MStream = new System.IO.MemoryStream()) {
        //        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //        Formatter.Serialize(MStream, Obj);
        //        MStream.Position = 0;
        //        return (T)Formatter.Deserialize(MStream);
        //    }
        //}


        public static void Swap<T>(this List<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
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