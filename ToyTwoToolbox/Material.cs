using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Material {
        public int id;
        public List<double> RGB;
        public int textureIndex;
        public int data;

        public Material() {
            id = 0;
            RGB = new List<double>();
            textureIndex = 0;
            data = 0;
        }

    }
}
