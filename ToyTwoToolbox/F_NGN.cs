using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the fileformat definition for NGN files
    /// </summary>
    class F_NGN : F_Base {
        public FileProcessor.FileTypes FileType { get; } = FileProcessor.FileTypes.NGN;
        private string tempName;
        public string TempName { get => tempName; set => tempName = value; }
        private string fileName;
        public string FileName { get => fileName; set => fileName = value; }
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        public F_NGN() {

        }

        public void save(string path) {
            throw new NotImplementedException();
        }
    }
}
