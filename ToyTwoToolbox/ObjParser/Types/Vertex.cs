using System;
using System.Globalization;

namespace ObjParser.Types {
    public class Vertex : IType {
        public const int MinimumDataLength = 4;
        public const string Prefix = "v";
        public Color color = null;

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public int Index { get; set; }

        public void LoadFromStringArray(string[] data) {
            if (data.Length < MinimumDataLength)
                throw new ArgumentException("Input array must be of minimum length " + MinimumDataLength, "data");

            if (!data[0].ToLower().Equals(Prefix))
                throw new ArgumentException("Data prefix must be '" + Prefix + "'", "data");

            bool success;

            double x, y, z;

            success = double.TryParse(data[1], NumberStyles.Any, CultureInfo.InvariantCulture, out x);
            if (!success) throw new ArgumentException("Could not parse X parameter as double");

            success = double.TryParse(data[2], NumberStyles.Any, CultureInfo.InvariantCulture, out y);
            if (!success) throw new ArgumentException("Could not parse Y parameter as double");

            success = double.TryParse(data[3], NumberStyles.Any, CultureInfo.InvariantCulture, out z);
            if (!success) throw new ArgumentException("Could not parse Z parameter as double");

            X = x;
            Y = y;
            Z = z;

            if (data.Length == 7) {
                float.TryParse(data[4], NumberStyles.Any, CultureInfo.InvariantCulture, out float r);
                float.TryParse(data[5], NumberStyles.Any, CultureInfo.InvariantCulture, out float g);
                float.TryParse(data[6], NumberStyles.Any, CultureInfo.InvariantCulture, out float b);
                color = new Color(r, g, b);
            }
        }

        public override string ToString() {
            return string.Format("v {0} {1} {2}", X, Y, Z);
        }
    }
}
