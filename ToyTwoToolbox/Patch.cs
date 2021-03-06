﻿using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
    public class Patch : IPrimitive {
        public Type PrimType { get; } = typeof(Patch);
        private int _type;
        public int type { get => _type; set => _type = value; }
        private int _materialID;
        public int materialID { get => _materialID; set => _materialID = value; }
        private int _vertexCount;
        public int vertexCount { get => _vertexCount; set => _vertexCount = value; }
        private List<int> _vertices;
        public List<int> vertices { get => _vertices; set => _vertices = value; }
        public Shape.PrimFlags flags { get; set; }

        public Int32 Unknown1;
        public Int32 Unknown2;
        public Int32 Unknown3;
        public Int32 Unknown4;

        //the unk vars are still unknown lol 


        public Patch() {
            type = 1;
            vertexCount = 0;
            vertices = new List<int>();
            materialID = 0;
            Unknown1 = 0;
            Unknown2 = 0;
            Unknown3 = 0;
            Unknown4 = 0;
            flags = 0;
        }

        public Patch(int MaterialID, List<int> VertexIDs, int VertexCount = -1, Shape.PrimFlags Flags = 0) {
            type = 1;
            vertexCount = (VertexCount == -1) ? VertexIDs.Count : VertexCount;
            vertices = VertexIDs;
            materialID = MaterialID;
            Unknown1 = 0;
            Unknown2 = 0;
            Unknown3 = 0;
            Unknown4 = 0;
            flags = Flags;
        }

        /// <summary>Convert this patch to a <see cref="Prim"/> type</summary>
        /// <returns><see cref="Prim"/> with converted data</returns>
        public Prim ConvertToPrim() {
            return new Prim(type, materialID, vertices, vertexCount);
        }
    }
}
