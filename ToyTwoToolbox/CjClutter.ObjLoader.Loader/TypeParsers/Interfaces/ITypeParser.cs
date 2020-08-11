
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.TypeParsers.Interfaces {
		public interface ITypeParser {
			bool CanParse(string keyword);
			void Parse(string line);
		}
	}
}