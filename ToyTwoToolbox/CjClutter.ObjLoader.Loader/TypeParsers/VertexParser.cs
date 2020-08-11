
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Common;
using ToyTwoToolbox.ObjLoader.Loader.Data.DataStore;
using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers {
		public class VertexParser : TypeParserBase, IVertexParser {
			private readonly IVertexDataStore _vertexDataStore;
			private readonly IVertexColorDataStore _vertexColorDataStore;

			public VertexParser(IVertexDataStore vertexDataStore) { //, vertexColorDataStore As IVertexColorDataStore)
				_vertexDataStore = vertexDataStore;
				_vertexColorDataStore = (ToyTwoToolbox.ObjLoader.Loader.Data.DataStore.IVertexColorDataStore)vertexDataStore;
			}

			protected override string Keyword {
				get {
					return "v";
				}
			}

			public override void Parse(string line) {
				string[] parts = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

				var x = parts[0].ParseInvariantFloat();
				var y = parts[1].ParseInvariantFloat();
				var z = parts[2].ParseInvariantFloat();

				var r = parts[3].ParseInvariantDec();
				var g = parts[4].ParseInvariantDec();
				var b = parts[5].ParseInvariantDec();

				var vertex = new Vertex(x, y, z);
				_vertexDataStore.AddVertex(vertex);

				var vcol = new VertexColor(r, g, b);
				_vertexColorDataStore.AddColor(vcol);
			}
		}
	}
}