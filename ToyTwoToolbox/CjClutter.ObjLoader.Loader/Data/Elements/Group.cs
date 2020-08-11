
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Data.DataStore;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.Elements {
		public class Group : IFaceGroup {
			private readonly List<Face> _faces = new List<Face>();

			public Group(string name) {
				this.Name = name;
			}

			private string privateName;
			public string Name {
				get {
					return privateName;
				}
				private set {
					privateName = value;
				}
			}
			public Material Material {get; set;}

			public IList<Face> Faces {
				get {
					return _faces;
				}
			}

			public void AddFace(Face face) {
				_faces.Add(face);
			}
		}
	}
}