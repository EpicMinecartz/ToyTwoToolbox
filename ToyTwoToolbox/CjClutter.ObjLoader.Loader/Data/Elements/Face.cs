
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.Elements {
		public class Face {
			public List<FaceVertex> _vertices = new List<FaceVertex>();

			public void AddVertex(FaceVertex vertex) {
				_vertices.Add(vertex);
			}

			public FaceVertex this[int i] {
				get {
					return _vertices[i];
				}
				set {
					_vertices[i] = value;
				}
			}

			public int Count {
				get {
					return _vertices.Count;
				}
				set {

				}
			}
		}

		public struct FaceVertex {
			public FaceVertex(int vertexIndex, int textureIndex, int normalIndex) : this() {
				this.VertexIndex = vertexIndex;
				this.TextureIndex = textureIndex;
				this.NormalIndex = normalIndex;
			}

			public int VertexIndex {get; set;}
			public int TextureIndex {get; set;}
			public int NormalIndex {get; set;}
		}
	}
}