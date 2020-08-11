
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public interface IMaterialLibraryLoader {
			void Load(Stream lineStream);
		}
	}
}