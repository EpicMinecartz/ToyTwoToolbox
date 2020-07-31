using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public interface F_Base {
        //Type FileType { get; }
        FileProcessor.FileTypes FileType { get; }
        string TempName { get; set; }
        string FilePath { get; set; }

        F_Base Import(string path);
        bool Export(string path = "");

    }
}
