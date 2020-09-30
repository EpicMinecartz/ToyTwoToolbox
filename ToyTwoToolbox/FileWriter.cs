using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// easily write data to a byte stream for saving <para/>
    /// Note: The functions with _ at the end require implicit pointer references
    /// </summary>
    public class FileWriter {
        /// <summary>
        /// dont modify this manually
        /// </summary>
        public List<byte> fstream = new List<byte>();
        public int foffset = 0;
        /// <summary>
        /// Write string data to the ByteStream
        /// </summary>
        /// <param name="str">the string to append</param>
        /// <param name="padding">how man extra bytes to add onto the end or the start of the str</param>
        public void Add(string str, int padding = 0) {
            FStreamWrite(Encoding.ASCII.GetBytes(str.ToString()), padding);
        }
        /// <summary>
        /// Write int data to the ByteStream
        /// </summary>
        /// <param name="str">the int to append</param>
        /// <param name="padding">how many extra bytes to add onto the end or the start of the int (-1 for prefix, +1 for suffix)</param>
        public void AddScary(int integer, int ByteSize, int padding = 0) {
            FStreamWrite(GetBytes(integer,ByteSize), padding);
        }

        public void AddFloat_(ref int ptr, float flt, int padding = 0) {
            FStreamWrite_(GetBytes(flt), padding, ref ptr);
        }
        public void AddFloat(float flt, int padding = 0, bool c = false) {
            FStreamWrite(GetBytes(flt,c), padding);
        }

        public void AddDouble_(ref int ptr, double dbl, int padding = 0) {
            FStreamWrite_(GetBytes(Convert.ToInt64(dbl), 4), padding, ref ptr);
        }
        public void AddDouble(double dbl, int padding = 0) {
            FStreamWrite(GetBytes(Convert.ToInt64(dbl), 4), padding);
        }

        public void AddInt32_(ref int ptr, int integer, int padding = 0) {
            FStreamWrite_(GetBytes(integer, 4), padding, ref ptr);
        }
        public void AddInt32(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 4), padding);
        }

        public void AddInt16_(ref int ptr, int integer, int padding = 0) {
            FStreamWrite_(GetBytes(integer, 2), padding, ref ptr);
        }
        public void AddInt16(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 2), padding);
        }

        public void AddByte_(ref int ptr, int integer, int padding = 0) {
            FStreamWrite_(GetBytes(integer, 1), padding, ref ptr);
        }
        public void AddByte(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 1), padding);
        }

        public void AddBytes(byte[] Byte, int padding = 0) {
            FStreamWrite(Byte, padding);
        }

        public void AddString(string str, int padding = 0) {
            FStreamWrite(Encoding.ASCII.GetBytes(str), padding);
        }

 

        /// <summary>
        /// this is the actuall append function, ofc we dont want to show this uglyness to the code editor, oh wait, thats you...
        /// use the end user funcs for help, not writing it out again
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padding"></param>
        private void FStreamWrite(Byte[] Bytes, int padding) { FStreamWrite_(Bytes, padding, ref foffset); }
        private void FStreamWrite_(Byte[] Bytes, int padding, ref int ptr ) {
            if (padding > 0) {
                Array.Resize<byte>(ref Bytes, Bytes.Length + padding);
            } else if (padding < 0) {
                Bytes = Enumerable.Repeat<Byte>(0, Math.Abs(padding)).Concat(Bytes).ToArray();
            }
            fstream.AddRange(Bytes);
            ptr += Bytes.Length;
        }


        unsafe Byte[] CopyBytes(float value, int index, bool c) {
            byte[] bytes = new byte[c ? 2 : 4];
            fixed (byte* b = bytes) {
                *(int*)b = *(int*)&value;
            }
            return bytes;
        }

        void CopyBytes(long value, int bytes, byte[] buffer, int index) {
            if (buffer != null && buffer.Length >= index + bytes) {
                for (int i = 0;i < bytes;i++) {
                    buffer[i + index] = unchecked((byte)(value & 0xff));
                    value >>= 8;
                }
            }
        }

        public byte[] GetBytes(long value, int bytes) {
            byte[] buffer = new byte[bytes];
            CopyBytes(value, bytes, buffer, 0);
            return buffer;
        }

        public byte[] GetBytes(float value, bool compress = false) {
            return CopyBytes(value,0, compress);
        }

        public void ReplaceBytes(int ptr, byte[] replacement) {
            for (int i = 0;i < replacement.Length;i++) {
                this.fstream[ptr + i] = replacement[i];
            }
        }

        /// <summary>Write zeros to the byte stream</summary>
        /// <param name="count">How many zeros to write</param>
        /// <returns><seealso cref="int"/> pointing to the offset where the null bytes were written</returns>
        public int Nop(int count = 1) {
            //for (int i = 0;i < count;i++) {
            //    fstream.Add(0);
            //}
            fstream.AddRange(new byte[count]);
            foffset += count;
            return fstream.Count-count;
        }

        public void eof(int ptr) {
            ReplaceBytes(ptr, GetBytes(this.foffset - ptr - 4,4));
        }

        public bool Save(string str) {
            try {
                File.WriteAllBytes(str, fstream.ToArray());
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public void ExternalSave(Byte[] bytes, string path) {
            File.WriteAllBytes(path, bytes);
        }

        public void DumpConsole() {
            Debug.WriteLine(fstream);
        }

    }
}
