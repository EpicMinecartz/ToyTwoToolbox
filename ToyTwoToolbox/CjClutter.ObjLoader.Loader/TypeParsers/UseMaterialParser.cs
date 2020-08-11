
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Data.DataStore;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers {
		public class UseMaterialParser : TypeParserBase, IUseMaterialParser {
			private readonly IElementGroup _elementGroup;

			public UseMaterialParser(IElementGroup elementGroup) {
				_elementGroup = elementGroup;
			}

			protected override string Keyword {
				get {
					return "usemtl";
				}
			}

			public override void Parse(string line) {
				_elementGroup.SetMaterial(line);
			}
		}
	}
}