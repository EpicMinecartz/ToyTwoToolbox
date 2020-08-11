﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Common;
using ToyTwoToolbox.ObjLoader.Loader.Data.DataStore;
using ToyTwoToolbox.ObjLoader.Loader.Data.Elements;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers {
		public class FaceParser : TypeParserBase, IFaceParser {
			private readonly IFaceGroup _faceGroup;

			public FaceParser(IFaceGroup faceGroup) {
				_faceGroup = faceGroup;
			}

			protected override string Keyword {
				get {
					return "f";
				}
			}

			public override void Parse(string line) {
				var vertices = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

				var face = new Face();

				foreach (var vertexString in vertices) {
					var faceVertex = ParseFaceVertex(vertexString);
					face.AddVertex(faceVertex);
				}

				_faceGroup.AddFace(face);
			}

			private FaceVertex ParseFaceVertex(string vertexString) {
				var fields = vertexString.Split(new[] {'/'}, StringSplitOptions.None);

				var vertexIndex = fields[0].ParseInvariantInt();
				var faceVertex = new FaceVertex(vertexIndex, 0, 0);

				if (fields.Length > 1) {
					var textureIndex = (fields[1].Length == 0) ? 0 : fields[1].ParseInvariantInt();
					faceVertex.TextureIndex = textureIndex;
				}

				if (fields.Length > 2) {
					var normalIndex = (fields.Length > 2 && fields[2].Length == 0) ? 0 : fields[2].ParseInvariantInt();
					faceVertex.NormalIndex = normalIndex;
				}

				return faceVertex;
			}
		}
	}
}