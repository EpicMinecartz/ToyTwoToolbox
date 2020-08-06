using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    class FileWriter {
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
        /// <param name="padding">how man extra bytes to add onto the end or the start of the int (-1 for prefix, +1 for suffix)</param>
        public void AddScary(int integer, int ByteSize, int padding = 0) {
            FStreamWrite(GetBytes(integer,ByteSize), padding);
        }

        public void AddInt32(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 4), padding);
        }

        public void AddInt16(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 2), padding);
        }

        public void AddByte(int integer, int padding = 0) {
            FStreamWrite(GetBytes(integer, 1), padding);
        }

        public void AddByte(byte Byte, int padding = 0) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// this is the actuall append function, ofc we dont want to show this uglyness to the code editor, oh wait, thats you...
        /// use the end user funcs for help, not writing it out again
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padding"></param>
        private void FStreamWrite(Byte[] Bytes, int padding) {
            if (padding > 0) {
                Array.Resize<byte>(ref Bytes, Bytes.Length + padding);
            } else if (padding < 0) {
                Bytes = Enumerable.Repeat<Byte>(0, Math.Abs(padding)).Concat(Bytes).ToArray();
            }
            fstream.AddRange(Bytes);
        }

        protected void CopyBytesImpl(long value, int bytes, byte[] buffer, int index) {
            for (int i = 0;i < bytes;i++) {
                buffer[i + index] = unchecked((byte)(value & 0xff));
                value = value >> 8;
            }
        }

        void CopyBytes(long value, int bytes, byte[] buffer, int index) {
            if (buffer == null) {
                throw new ArgumentNullException("buffer", "Byte array must not be null");
            }
            if (buffer.Length < index + bytes) {
                throw new ArgumentOutOfRangeException("Buffer not big enough for value");
            }
            CopyBytesImpl(value, bytes, buffer, index);
        }

        byte[] GetBytes(long value, int bytes) {
            byte[] buffer = new byte[bytes];
            CopyBytes(value, bytes, buffer, 0);
            return buffer;
        }

        public void ReplaceBytes(int ptr, byte[] replacement) {

        }


        public void Nop(int count = 1) {
            byte n = 0;
            for (int i = 0;i < count;i++) {
                fstream.Add(n);
            }
        }

        public void ExternalNop(Byte[] bytes, int count) {
            for (int i = 0;i < count;i++) {
                //bytes.add
            }
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
