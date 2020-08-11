
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.VertexData {
		public struct VertexColor {
			public VertexColor(decimal r, decimal g, decimal b) : this() {
				this.R = r;
				this.G = g;
				this.B = b;
			}

			private decimal privateR;
			public decimal R {
				get {
					return privateR;
				}
				private set {
					privateR = value;
				}
			}
			private decimal privateG;
			public decimal G {
				get {
					return privateG;
				}
				private set {
					privateG = value;
				}
			}
			private decimal privateB;
			public decimal B {
				get {
					return privateB;
				}
				private set {
					privateB = value;
				}
			}
		}
	}
}