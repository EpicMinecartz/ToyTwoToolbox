
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Loaders;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers {
		public class MaterialLibraryParser : TypeParserBase, IMaterialLibraryParser {
			private readonly IMaterialLibraryLoaderFacade _libraryLoaderFacade;

			public MaterialLibraryParser(IMaterialLibraryLoaderFacade libraryLoaderFacade) {
				_libraryLoaderFacade = libraryLoaderFacade;
			}

			protected override string Keyword {
				get {
					return "mtllib";
				}
			}

			public override void Parse(string line) {
				_libraryLoaderFacade.Load(line);
			}
		}
	}
}