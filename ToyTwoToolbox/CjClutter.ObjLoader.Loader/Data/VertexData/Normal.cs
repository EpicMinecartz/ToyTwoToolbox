
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.VertexData {
		public struct Normal {
			public Normal(float x, float y, float z) : this() {
				this.X = x;
				this.Y = y;
				this.Z = z;
			}

			private float privateX;
			public float X {
				get {
					return privateX;
				}
				private set {
					privateX = value;
				}
			}
			private float privateY;
			public float Y {
				get {
					return privateY;
				}
				private set {
					privateY = value;
				}
			}
			private float privateZ;
			public float Z {
				get {
					return privateZ;
				}
				private set {
					privateZ = value;
				}
			}
		}
	}
}