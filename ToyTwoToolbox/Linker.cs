using System.Security.Policy;

namespace ToyTwoToolbox {
    public class Linker {
        public int LinkID;
        public int ShapeID;

        public Linker() {}

        public Linker(int linkID, int shapeID) {
            LinkID = linkID;
            ShapeID = shapeID;
        }
    }
}