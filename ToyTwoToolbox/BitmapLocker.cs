using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class BitmapLocker {
        public byte[] pixelData;
        public BitmapData bitmapData;
        public Bitmap image;

        public void AllocateLock(Bitmap img) {
            image = img;
            bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Allocate room for the data.
            int total_size = bitmapData.Stride * bitmapData.Height;
            pixelData = new byte[total_size];

            // Copy the data into the pixelData array.
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, pixelData, 0, total_size);
        }

        public void ReleaseLock() {
            // Copy the data back into the bitmap.
            System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bitmapData.Scan0, bitmapData.Stride * bitmapData.Height);

            // Unlock the bitmap.
            image.UnlockBits(bitmapData);

            // Release resources.
            pixelData = null;
            bitmapData = null;
        }

        public void SetPixel(int X, int Y, Color color) {

        }
    }
}
