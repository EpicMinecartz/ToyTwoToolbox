
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System;
using System.Globalization;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Common {
		public static class StringExtensions {
			public static float ParseInvariantFloat(this string floatString) {
				return float.Parse(floatString, CultureInfo.InvariantCulture.NumberFormat);
			}

			public static decimal ParseInvariantDec(this string floatString) {
				return decimal.Parse(floatString, CultureInfo.InvariantCulture.NumberFormat);
			}

			public static int ParseInvariantInt(this string intString) {
				return int.Parse(intString, CultureInfo.InvariantCulture.NumberFormat);
			}

			public static bool EqualsOrdinalIgnoreCase(this string str, string s) {
				return str.Equals(s, StringComparison.OrdinalIgnoreCase);
			}

			public static bool IsNullOrEmpty(this string str) {
				return string.IsNullOrEmpty(str);
			}
		}
	}
}