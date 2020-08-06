using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Shape {
        public string name;
        public int type;
        public int type2;
        public int datalength;
        public List<string> textures;
        public List<Material> materials;
        public List<Vector3> rawVertices;
        public List<Vector3> rawVertexData;
        public List<Color> rawVertexShading;
        public List<Vector2> rawVertexTextureCoords;
        public List<IPrimitive> rawPrimitives;

        public Shape() {
            name = "";
            type = 0;
            type2 = 0;
            datalength = 0;
            textures = new List<string>();
            materials = new List<Material>();
            rawVertices = new List<Vector3>();
            rawVertexData = new List<Vector3>();
            rawVertexShading = new List<Color>();
            rawVertexTextureCoords = new List<Vector2>();
            rawPrimitives = new List<IPrimitive>();
        }

        public Prim ReadPrimitive() {
            return new Prim();
        }

        public Patch ReadPatch() {
            return new Patch();
        }

    }
}
