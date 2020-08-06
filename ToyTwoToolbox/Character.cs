using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Character {
        public string name;
        public int nodeCount;
        public Model model;
        public List<Animation> Anims;

        public Character() {
            name = "";
            nodeCount = 0;
            model = new Model();
            Anims = new List<Animation>();
        }
    }
}
