using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the fileformat definition for BIN files
    /// </summary>
    public class F_BIN : F_Base {
        public FileProcessor.FileTypes FileType { get; } = FileProcessor.FileTypes.BIN;
        private string tempName;
        public string TempName { get => tempName; set => tempName = value; }
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        public F_BIN(string filePath) {
            this.filePath = filePath;
        }

        public static F_Base ImportBin(string path) {
            return new F_BIN(path);
        }

        public F_Base Import(string path) {
            return ImportBin(path);
        }

        public void Process() {
            this.RawData = File.ReadAllBytes(this.filePath);
            //WATCH THE ENDIAN 

            //First we process the colour table to get a list of available colors to poll.
            ColorTables.Clear();
            short CTLength = (short)(RawData[9] << 8 | RawData[8]);
            int TableCount = CTLength / 3;
            ColorTableTotal = Convert.ToInt16(TableCount);
            int TextureCount = TableCount / 16;
            for (var i = 0;i < TextureCount;i++) {
                List<Color> CTable = new List<Color>();
                for (var h = 0;h <= TextureCount * 3 - 3;h += 3) {
                    Int32 off = 12 + i * 48 + h; //index ctbl offset
                    Color CColor = Color.FromArgb(RawData[off], RawData[off + 1], RawData[off + 2]);
                    CTable.Add(CColor);
                }
                ColorTables.Add(CTable);
            }

            //and now the fun stuff
            short offset = (short)((RawData[0x9] << 0x8 | RawData[0x8]) + 0xC);
            int bmw = (short)(RawData[0x5] << 0x8 | RawData[0x4]);
            int bmh = (short)(RawData[0x7] << 0x8 | RawData[0x6]);
            #region "c"
            /*each byte stores 2 pixels, thus one line of 64px is actually 128px therefore 64px=128px = half the vertical line
            //so 2 64px chunks Is one entire vertical line
            // the way this is done is by getting the upper and lower parts of the bytes, make sure to wear your mask!
            //FF = 11111111 == (F/1111) & (F/1111)
            */

            if(bmw < 1 || bmh < 1) {
                MessageBox.Show("Something seems wrong with this file, are you sure it contains texture data? \n\n [Width or Height were 0]", "BIN load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.RawBitmap = new Bitmap(1,1);
                return;
            }

            #endregion
            this.RawBitmap = new Bitmap(bmw, bmh);

            int rc = bmw / 64;
            int cc = bmh / 64;
            for (int row = 0;row < rc;row++) { //the current texture row we are on 0-4, each texture row is 64px down, but first we need to know what colum of texture we are in
                for (int column = 0;column < cc;column++) { //#ref 1
                    for (int pixelrow = 0;pixelrow <= 63;pixelrow++) { //for each row of 32px in this square
                        for (int pixel = 0;pixel <= 62;pixel += 2) {   //for each pixel on this line
                                                                        //had to make it 63 in here so that the points can be set properly, do pixel /2 you'll be alright
                            int i128 = (bmw / 64) * 32; //block length
                            int X = pixel + (column * 64);
                            int y = pixelrow + (row * 64);
                            int c1 = column * 64;
                            int c2 = c1 + pixel / 2;
                            int r1 = row * 64;
                            int r2 = (r1 + (pixelrow * i128));
                            int fo = (offset + ((c1 + c2 + r1 + r2 + (column * 32) + (row * (i128 * 64))) - ((column + row) * i128)));

                            if (fo + 1 > RawData.Length - 1) return; //this is awful

                            Int16[] UncompressedPoints = ExpandPointData((short)(RawData[fo + 1] << 8 | RawData[fo]));
                            this.RawBitmap.SetPixel(X, y, ColorTables[(column) + (row * 4)][UncompressedPoints[1]]);
                            this.RawBitmap.SetPixel(X + 1, y, ColorTables[(column) + (row * 4)][UncompressedPoints[0]]);
                        }
                    }
                }
            }
        }

        public Int16[] ExpandPointData(short b1) {
            short[] TPGD = new short[3];
            TPGD[0] = (byte)(b1 >> 4 & 0xF);
            TPGD[1] = (byte)(b1 & 0xF);
            return TPGD;
        }

        public bool Export(string path = "") {
            throw new NotImplementedException();
        }

        public List<List<Color>> ColorTables = new List<List<Color>>();
        public Int16 ColorTableTotal;
        public byte[] RawData;
        public Bitmap RawBitmap;

    }
}


/*#refs
 
  1:
    ok, so row0 col0 is the top left square etc...
    now we know what sqaure we are in, we need to convert that to a pointer in the raw file

    find out what VERTICAL square we are in, hence the colum, then multiply by 32 (thus 4*32 = a full row)
    as this is how the data is stored in the file, vertical lines of 128px total length
    then we multiply that by the row number, except not entirely
    we actually need to do 128*row as each row is worth 128

 */
