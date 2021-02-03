
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Loaders {
		public abstract class LoaderBase {
			private StreamReader _lineStreamReader;

			protected void StartLoad(Stream lineStream) {
				var fileStream = lineStream as FileStream;

				_lineStreamReader = new StreamReader(lineStream);

				while (!_lineStreamReader.EndOfStream) {
					ParseLine(fileStream.Name);
				}
			}

			private void ParseLine(string streampath = null) {
				var currentLine = _lineStreamReader.ReadLine();

				if (string.IsNullOrWhiteSpace(currentLine) || currentLine[0] == '#') {
					return;
				}

				var fields = currentLine.Trim().Split(null,2);
				var keyword = fields[0].Trim();
				var data = fields[0].Trim();

				if (keyword == "mtllib") {
					if (!data.Contains("\\")) {
						if (streampath != null) {
							data = System.IO.Path.GetDirectoryName(streampath) + "\\" + data;
						}
					}
				}

				ParseLine(keyword, data);
			}

			protected abstract void ParseLine(string keyword, string data);
		}
	}
}