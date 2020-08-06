using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ToyTwoToolbox {
    ///Primitives
    ///==========
    ///A primitive is a physical shape, made of vertices connected together, the primitive part is the connection between those vertices
    ///Within a TOY2 Shape, you will see a list of vertices, sometimes in a seemingly random order.
    ///The primitive section of the TOY2 Shape, has a header, defining what kind of primitive is about to be made, then a list of numbers.
    ///Those number actually reference the vertices in a top down order for example (4, 0) 0,1,2,3 to make a square, where:
    ///(4, = type
    /// 0) = materialID
    /// 0,1,2,3 = vertexIDs
    ///


    /// <summary>
    /// The primitive shape class
    /// </summary>
    public class Prim : IPrimitive {
        public Type PrimType { get; } = typeof(Prim);
        private int _type;
        public int type { get => _type; set => _type = value; }
        private int _materialID;
        public int materialID { get => _materialID; set => _materialID = value; }
        private int _vertexCount;
        public int vertexCount { get => _vertexCount; set => _vertexCount = value; }
        private List<int> _vertices;
        public List<int> vertices { get => _vertices; set => _vertices = value; }
        

        public Prim() {
            type = 0;
            materialID = 0;
            vertexCount = 0;
            vertices = new List<int>();
        }

        public Prim(int Type, int MaterialID, List<int> VertexIDs, int VertexCount = -1) {
            type = Type;
            materialID = MaterialID;
            vertexCount = (VertexCount == -1) ? VertexIDs.Count : VertexCount;
            vertices = VertexIDs;
        }

        public Patch ConvertToPatch() {
            return new Patch(materialID, vertices, vertexCount);
        }
    }
}
