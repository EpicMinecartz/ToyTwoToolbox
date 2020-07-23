using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    class Shape {
        string name;
        int type;
        int type2;
        int datalength;
        List<int> textures;
        List<Material> materials;
        List<Vector3> rawVertices;
        List<Vector3> rawVertexData;
        List<Color> rawVertexShading;
        List<Vector2> rawVertexTextureCoords;
        RawPrimContainer rawPrimitives;

        public Primitive ReadPrimitive() {
            return new Primitive();
        }

        public Patch ReadPatch() {
            return new Patch();
        }

    }
}
