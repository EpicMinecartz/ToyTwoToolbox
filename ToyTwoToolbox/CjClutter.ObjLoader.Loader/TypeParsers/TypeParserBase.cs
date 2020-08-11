
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using ToyTwoToolbox.ObjLoader.Loader.Common;
using ToyTwoToolbox.ObjLoader.Loader.TypeParsers.Interfaces;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers {
		public abstract class TypeParserBase : ITypeParser {
			protected abstract string Keyword {get;}

			public bool CanParse(string keyword) {
				return keyword.EqualsOrdinalIgnoreCase(this.Keyword);
			}

			public abstract void Parse(string line);
		}
	}
}