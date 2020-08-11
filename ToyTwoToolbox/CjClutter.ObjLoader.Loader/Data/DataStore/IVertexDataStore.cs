
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.DataStore {
		public interface IVertexDataStore {
			void AddVertex(Vertex vertex);
		}
	}
}