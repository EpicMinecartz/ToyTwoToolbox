using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ToyTwoToolbox.ObjLoader.Loader.Data.Elements;
using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;
using ToyTwoToolbox.ObjLoader.Loader.Loaders;

namespace ToyTwoToolbox {
    public partial class Importer : Form {
        /// <summary>
        /// This is nowhere near finished and is very broken
        /// </summary>
        public Character ImportedCharacter;
        public Importer() {
            InitializeComponent();
        }

        private void butImportCharShape_Click(object sender, EventArgs e) {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK) {
                ImportedCharacter = ProcessOBJ(OFD.FileName);
            }
        }

        public Character ProcessOBJ(string filepath) {
            ObjLoaderFactory OBJLF = new ObjLoaderFactory();
            var objl = OBJLF.Create();

            FileStream fs = new FileStream(filepath, System.IO.FileMode.Open);

            var Model = objl.Load(fs);
            fs.Close();


            //begin processing
            Character chr = new Character { name = Path.GetFileNameWithoutExtension(filepath) };
            int MajorOffset = 0;
            foreach (Group g in Model.Groups) {
                Shape shape = new Shape();
                shape.rawVertices = ConvertCJCVectorsToVector3((List<Vertex>)Model.Vertices);
                shape.textures.Add(Path.GetFileNameWithoutExtension(g.Material.DiffuseTextureMap));
                shape.name = g.Name;
                shape.type = 1;

                //we have all the vertices
                //now we parse them into primitives aka faces
                int VertexCount = 0;
                foreach (Face face in g.Faces) {
                    Prim prim = new Prim();
                    for (int i = 0;i < face._vertices.Count;i++) {
                        prim.vertices.Add(i);
                        VertexCount++;
                    }
                    shape.rawPrimitives.Add(prim);
                }

                foreach (Face face in g.Faces) {
                    foreach (FaceVertex fv in face._vertices) {
                        int v3df = (VertexCount - Math.Abs(fv.VertexIndex)) + MajorOffset;
                        shape.rawVertices.Add(new Vector3(Model.Vertices[v3df].X, Model.Vertices[v3df].Y, Model.Vertices[v3df].Z));
                        //shape.rawVertexTextureCoords.Add(new Vector2(Model.TextureCoords[v3df].X, Model.TextureCoords[v3df].Y));
                    }
                }


                MajorOffset += VertexCount;
                chr.model.shapes.Add(shape);
                
            }
            button2.Enabled = true;
            return chr;
        }


        public List<Vector3> ConvertCJCVectorsToVector3(List<Vertex> source) {
            List<Vector3> v3d = new List<Vector3>();
            foreach (Vertex v in source) {
                v3d.Add(new Vector3(v.X, v.Y, v.Z));
            }
            return v3d;
        }

        private void Importer_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        //public Color ConvertCJCColorToColor() {

        //}
    }
}
