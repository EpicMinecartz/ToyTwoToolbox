using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    class OBJWriter {
        public StringBuilder fstream = new StringBuilder();
        public int foffset = 0;

        public void append(string str) {
            fstream.Append(str);
        }

        public void write(string str) {
            fstream.Append(str);
        }

        public void title(string str) {
            fstream.Append("#\n# " + str + "\n#\n\n");
        }
    }
}
