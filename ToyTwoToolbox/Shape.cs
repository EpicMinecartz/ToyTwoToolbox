using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ToyTwoToolbox {
    public class Shape {
        /// <summary>DO NOT MANUALLY MODIFY THIS</summary>
        public int _SPType = 0; //this is an internal reference, we set this just to confirm the shape was prim/patch when decompressed 0=prim 1=patch
        public string name;
        public int type;
        public int type2;
        public int datalength;
        public List<string> textures;
        public List<Material> materials;
        public List<Vector3> rawVertices;
        public List<Vector3> rawVertexData;
        public List<Color> rawVertexShading;
        public List<Vector2> rawVertexTextureCoords;
        public List<IPrimitive> rawPrimitives;

        public Shape() {
            name = "";
            type = 0;
            type2 = 0;
            datalength = 0;
            textures = new List<string>();
            materials = new List<Material>();
            rawVertices = new List<Vector3>();
            rawVertexData = new List<Vector3>();
            rawVertexShading = new List<Color>();
            rawVertexTextureCoords = new List<Vector2>();
            rawPrimitives = new List<IPrimitive>(); //sep is done down the pipeline
        }

        public Prim ReadPrimitive() {
            return new Prim();
        }

        public Patch ReadPatch() {
            return new Patch();
        }

        public void ConvertPrimitives(Type conversionType) {
            if (conversionType == typeof(Patch)) {
                for (int i = 0;i < rawPrimitives.Count;i++) {
                    rawPrimitives[i] = ((Prim)rawPrimitives[i]).ConvertToPatch();
                }
                _SPType = 1;
            } else if (conversionType == typeof(Prim)) {
                System.Windows.Forms.DialogResult msg = System.Windows.Forms.MessageBox.Show("Converting this shape format to a prim will cause some patch data to be lost.\n\nAre you sure you want to continue?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
                if (msg == System.Windows.Forms.DialogResult.Yes) {
                    for (int i = 0;i < rawPrimitives.Count;i++) {
                        rawPrimitives[i] = ((Patch)rawPrimitives[i]).ConvertToPrim();
                    }
                    _SPType = 0;
                }
            }
        }
    }
}
