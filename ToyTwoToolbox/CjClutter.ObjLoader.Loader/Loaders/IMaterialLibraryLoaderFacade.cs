
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public interface IMaterialLibraryLoaderFacade {
			void Load(string materialFileName);
		}
	}
}