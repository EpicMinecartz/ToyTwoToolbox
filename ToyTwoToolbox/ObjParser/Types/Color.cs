namespace ObjParser.Types {
    public class Color : IType {
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }

        public Color() {
            this.r = 1f;
            this.g = 1f;
            this.b = 1f;
        }

        public Color(float R, float G, float B) {
            this.r = R;
            this.g = G;
            this.b = B;
        }

        public void LoadFromStringArray(string[] data) {
            if (data.Length != 4) return;
            r = float.Parse(data[1]);
            g = float.Parse(data[2]);
            b = float.Parse(data[3]);
        }

        public float[] ToArrayFloat() {
            return new float[3] { r, g, b };
        }

        public double[] ToArrayDouble() {
            return new double[3] { r, g, b };
        }

        public override string ToString() {
            return string.Format("{0} {1} {2}", r, g, b);
        }
    }
}
