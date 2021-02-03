
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.IO;
using ToyTwoToolbox.ObjLoader.Loader.Data.DataStore;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public class ObjLoader : LoaderBase, IObjLoader {
			private readonly IDataStore _dataStore;
			private readonly List<ITypeParser> _typeParsers = new List<ITypeParser>();

			private readonly List<string> _unrecognizedLines = new List<string>();

			public ObjLoader(IDataStore dataStore, IFaceParser faceParser, IGroupParser groupParser, INormalParser normalParser, ITextureParser textureParser, IVertexParser vertexParser, IMaterialLibraryParser materialLibraryParser, IUseMaterialParser useMaterialParser) {
				_dataStore = dataStore;
				SetupTypeParsers(vertexParser, faceParser, normalParser, textureParser, groupParser, materialLibraryParser, useMaterialParser);
			}

			private void SetupTypeParsers(params ITypeParser[] parsers) {
				foreach (var parser in parsers) {
					_typeParsers.Add(parser);
				}
			}

			protected override void ParseLine(string keyword, string data) {
				foreach (var typeParser in _typeParsers) {
					if (typeParser.CanParse(keyword) && keyword != "mtllib") { //remove the  && keyword != "mtllib" at a later date, somethings rip and i cant be arsed rn
						typeParser.Parse(data);
						return;
					}
				}

				_unrecognizedLines.Add(keyword + " " + data);
			}

			public LoadResult Load(Stream lineStream) {
				StartLoad(lineStream);

				return CreateResult();
			}

			private LoadResult CreateResult() {
				var result = new LoadResult {
					Vertices = _dataStore.Vertices,
					VertexColor = _dataStore.VertexColor,
					TextureCoords = _dataStore.Textures,
					Normals = _dataStore.Normals,
					Groups = _dataStore.Groups,
					Materials = _dataStore.Materials
				};
				return result;
			}

            internal class Data {
            }
        }
	}
}