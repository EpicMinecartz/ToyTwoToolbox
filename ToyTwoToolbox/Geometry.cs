using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Geometry {
        public string name;
        public List<Shape> shapes;
        public List<string> textures;
        public DynamicScaler dynamicScaler;

        public Geometry() {
            name = "";
            shapes = new List<Shape>();
            textures = new List<string>();
            dynamicScaler = new DynamicScaler();
        }
    }
}
