using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Texture {
        public string name;
        public int type;
        public Bitmap image;

        public void New() {
            image = new Bitmap(1, 1);

        }
    }
}
