
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
		public class GroupParser : TypeParserBase, IGroupParser {
			private readonly IGroupDataStore _groupDataStore;

			public GroupParser(IGroupDataStore groupDataStore) {
				_groupDataStore = groupDataStore;
			}

			protected override string Keyword {
				get {
					return "g";
				}
			}

			public override void Parse(string line) {
				_groupDataStore.PushGroup(line);
			}
		}
	}
}