using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ToyTwoToolbox {
    public class Shape {
        /// <summary>DO NOT MANUALLY MODIFY THIS</summary>
        private int _id;
        public int _SPType = 0; //this is an internal reference, we set this just to confirm the shape was prim/patch when decompressed 0=prim 1=patch
        public IWorldObject owner = null;
        public string name;
        public int type;
        public int type2;
        public int datalength;
        /// <summary>Holds the names of textures that are used by materials</summary>
        public List<string> textures;
        /// <summary>Holds the ID of the relative texture in the <seealso cref="textures"/> list. This ID is the ID of the texture in the NGN.</summary>
        public List<int> texturesGlobal;
        public List<Material> materials;
        public List<Vector3> rawVertices;
        public List<Vector3> rawVertexData;
        public List<Color> rawVertexShading;
        public List<Vector3> rawVertexTextureCoords;
        public List<IPrimitive> rawPrimitives;

        public Shape() {
            _id = XF.Random(0, 32000);
            name = "";
            type = 0;
            type2 = 0;
            datalength = 0;
            textures = new List<string>();
            texturesGlobal = new List<int>();
            materials = new List<Material>();
            rawVertices = new List<Vector3>();
            rawVertexData = new List<Vector3>();
            rawVertexShading = new List<Color>();
            rawVertexTextureCoords = new List<Vector3>();
            rawPrimitives = new List<IPrimitive>(); //sep is done down the pipeline
        }
        /// <summary>Automatically registeres the material with this shape as the owner context</summary>
        /// <param name="material">The material to register into this shape</param>
        public void AddMaterial(Material material) {
            material.owner = this;
            materials.Add(material);
        }

        /// <summary>check if the shape already references a texture, and if not, add it to the list</summary>
        /// <param name="textureName">The name to validate</param>
        /// <returns>True if it didnt exist and was added</returns>
        public bool AddTexture(string textureName) {
            foreach (string tex in textures) {
                if(textureName==tex) {
                    return false;
                }
            }
            textures.Add(textureName);
            return true;
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
