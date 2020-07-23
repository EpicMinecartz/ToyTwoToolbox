using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    class F_Save : F_Base {
        public FileProcessor.FileTypes FileType { get; } = FileProcessor.FileTypes.Save;
        /// <summary>
        /// Used when a file name isnt provided
        /// </summary>
        /// <remarks> this probably shouldnt be modified by anyone unless you know wtf your doing </remarks>
        private string tempName;
        public string TempName { get => tempName; set => tempName = value; }
        private string fileName;
        public string FileName { get => fileName; set => fileName = value; }
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        public void save(string path) {
            throw new NotImplementedException();
        }
    }
}
