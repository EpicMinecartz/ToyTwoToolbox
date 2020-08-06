using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ToyTwoToolbox {
    /// <summary>
    /// As we now import files as bytes, we defo need a good way to read them
    /// these functions can be called randomly, but its highly advised to just create an inst
    /// </summary>
    class FileReader2 {
        /// <summary>
        /// dont modify this manually
        /// </summary>
        public byte[] fstream;
        public int foffset;
        public bool forceNoSeek;

        public FileReader2(string path, bool disableSeek = false) {
            forceNoSeek = disableSeek;
            fstream = File.ReadAllBytes(path);
        }

        public int length() {
            return fstream.Length;
        }

        public void read(byte[] array, int count = 1, int offset = -1, bool seek = true) {
            Buffer.BlockCopy(fstream, (offset==-1) ? foffset : offset, array, 0, count);
            foffset += count;
        }

        public byte[] readbytes(int count = 1, int offset = -1, bool seek = true) {
            checkInvalidSeek(seek, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, count, offset, seek);
            return newbytes;
        }


        public string readstring(int count = 1, int offset = -1, bool seek = true) {
            checkInvalidSeek(seek, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, count, offset, seek);
            // gsjqFw2  byte[] decBytes4 = HttpServerUtility.UrlTokenDecode(s4);
            //if (BitConverter.IsLittleEndian) { Array.Reverse(newbytes); }
            //return HttpServerUtility.UrlTokenEncode(newbytes);
            return System.Text.Encoding.UTF8.GetString(newbytes).TrimEnd('\0');
        }

        public int readint(int count = 1, int offset = -1, bool seek = true) {
            checkInvalidSeek(seek, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, count, offset, seek);
            //if (BitConverter.IsLittleEndian) { Array.Reverse(newbytes); }
            if (count > 1) {
                if (count < 3) {
                    Array.Resize(ref newbytes, 4);
                    return BitConverter.ToInt32(newbytes, 0);
                } else if (count < 5) {
                    return BitConverter.ToInt32(newbytes, 0);
                } else {
                    return -1; //dont fucking do this lol
                }
            } else {
                return newbytes[0];
            }
        }

        public void checkInvalidSeek(bool seek, ref int offset) {
            if (forceNoSeek == false && seek == true && offset != foffset) {
                offset = foffset; //offset = (offset == -1) ? foffset : offset;
                //if (offset != -1) { SessionManager.SMptr.SM("ARSoN ContractProtection: Prevented an attempt to seek read with an offset"); }
            }
        }

        public void seek(int amount) {
            foffset += amount;
            if (foffset < 0) { foffset = 0; }
            if (foffset > fstream.Length) { foffset = fstream.Length; }
        }
    }
}
