using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public interface F_Base {
        //Type FileType { get; }
        FileProcessor.FileTypes FileType { get; }
        string FileName { get; set; }
        string TempName { get; set; }
        string FilePath { get; set; }

        void save(string path);

    }
}
