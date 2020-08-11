
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public class MaterialStreamProvider : IMaterialStreamProvider {
			public Stream Open(string materialFilePath) {
				return File.Open(materialFilePath, FileMode.Open, FileAccess.Read);
			}
		}

		public class MaterialNullStreamProvider : IMaterialStreamProvider {
			public Stream Open(string materialFilePath) {
				return null;
			}
		}
	}
}