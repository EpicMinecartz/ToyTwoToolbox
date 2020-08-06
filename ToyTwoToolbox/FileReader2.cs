using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class FileReader {
        public byte[] fstream;
        public int foffset;
        //public bool disableDefaultSeek;

        public FileReader(string path, bool disableSeek = false) {
            //disableDefaultSeek = disableSeek;
            fstream = File.ReadAllBytes(path);
        }

        public int length() {
            return fstream.Length;
        }

        public void read(byte[] array, ref int seekPTR, int count = 1, int offset = -1) {
            //Buffer.BlockCopy(fstream, (offset == -1) ? foffset : offset, array, 0, count);
            Buffer.BlockCopy(fstream, (offset != -1) ? offset : seekPTR, array, 0, count);
            seekPTR += count;
        }


        public byte[] readbytes(int count = 1, int offset = -1) { return _readbytes(ref foffset, count, offset); }
        public byte[] _readbytes(ref int seekPTR, int count = 1, int offset = -1) {
            checkInvalidSeek(ref seekPTR, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, ref seekPTR, count, offset);
            return newbytes;
        }

        public string readstring(int count = 1, int offset = -1) { return _readstring(ref foffset, count, offset); }
        public string _readstring(ref int seekPTR, int count = 1, int offset = -1) {
            checkInvalidSeek(ref seekPTR, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, ref seekPTR, count, offset);
            // gsjqFw2  byte[] decBytes4 = HttpServerUtility.UrlTokenDecode(s4);
            //if (BitConverter.IsLittleEndian) { Array.Reverse(newbytes); }
            //return HttpServerUtility.UrlTokenEncode(newbytes);
            return System.Text.Encoding.UTF8.GetString(newbytes).TrimEnd('\0');
        }

        public int readint(int count = 1, int offset = -1) { return _readint(ref foffset, count, offset); }
        public int _readint(ref int seekPTR, int count = 1, int offset = -1) {
            checkInvalidSeek(ref seekPTR, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, ref seekPTR, count, offset);
            //if (swapEndian) { }//if (BitConverter.IsLittleEndian) { Array.Reverse(newbytes); }
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

        public Single readflt(int count = 1, int offset = -1) { return _readflt(ref foffset, count, offset); }
        public Single _readflt(ref int seekPTR, int count = 1, int offset = -1) {
            checkInvalidSeek(ref seekPTR, ref offset);
            byte[] newbytes = new byte[count];
            read(newbytes, ref seekPTR, count, offset);
            //if (BitConverter.IsLittleEndian) { Array.Reverse(newbytes); }
            if (count > 1) {
                if (count < 3) {
                    Array.Resize(ref newbytes, 4);
                    return BitConverter.ToSingle(newbytes, 0);
                } else if (count < 5) {
                    return BitConverter.ToSingle(newbytes, 0);
                } else {
                    return -1; //dont fucking do this lol
                }
            } else {
                return newbytes[0];
            }
        }

        public void checkInvalidSeek(ref int seek, ref int offset) {
            //if (forceNoSeek == false && seek == true && offset != foffset) {
            //    offset = foffset; //offset = (offset == -1) ? foffset : offset;
            //    //if (offset != -1) { SessionManager.SMptr.SM("ARSoN ContractProtection: Prevented an attempt to seek read with an offset"); }
            //}
        }

        public void seek(int amount) { _seek(amount, ref foffset); }
        public void _seek(int amount, ref int seekPTR) {
            seekPTR += amount;
            if (seekPTR < 0) { seekPTR = 0; }
            if (seekPTR > fstream.Length) { seekPTR = fstream.Length; }
        }


    }
}
