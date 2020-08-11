
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
		public class TextureParser : TypeParserBase, ITextureParser {
			private readonly ITextureDataStore _textureDataStore;

			public TextureParser(ITextureDataStore textureDataStore) {
				_textureDataStore = textureDataStore;
			}

			protected override string Keyword {
				get {
					return "vt";
				}
			}

			public override void Parse(string line) {
				string[] parts = line.Split(' ');

				float x = parts[0].ParseInvariantFloat();
				float y = parts[1].ParseInvariantFloat();

				var texture = new TextureCoord(x, y);
				_textureDataStore.AddTexture(texture);
			}
		}
	}
}