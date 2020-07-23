using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    class Texture {
        string name;
        int type;
        Bitmap image;

        public void New() {
            image = new Bitmap(1, 1);

        }
    }
}
