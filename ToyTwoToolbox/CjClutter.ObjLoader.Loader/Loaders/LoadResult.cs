
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Data;
using ToyTwoToolbox.ObjLoader.Loader.Data.Elements;
using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public class LoadResult {
			public IList<Vertex> Vertices {get; set;}
			public IList<VertexColor> VertexColor {get; set;}
			public IList<TextureCoord> TextureCoords {get; set;}
			public IList<Normal> Normals {get; set;}
			public IList<Group> Groups {get; set;}
			public IList<Data.Material> Materials {get; set;}
		}
	}
}