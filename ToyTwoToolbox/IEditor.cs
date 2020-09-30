using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public interface IEditor {
        string TempName { get; set; }
        string FilePath { get; set; }

        bool SaveChanges(bool MemorySave = false, string path = "");
    }
}
