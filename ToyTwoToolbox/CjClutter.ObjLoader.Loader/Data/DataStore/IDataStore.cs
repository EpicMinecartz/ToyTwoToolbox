
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Data.Elements;
using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.DataStore {
		public interface IDataStore {
			IList<Vertex> Vertices {get;}
			IList<VertexColor> VertexColor {get;}
			IList<TextureCoord> Textures {get;}
			IList<Normal> Normals {get;}
			IList<Material> Materials {get;}
			IList<Group> Groups {get;}
		}
	}
}