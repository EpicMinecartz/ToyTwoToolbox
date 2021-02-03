using ObjParser;
using ObjParser.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    public partial class Importer : Form {
        /// <summary>
        /// This is nowhere near finished and is very broken
        /// </summary>
        public Character importedCharacter = new Character();
        public Importer() {
            InitializeComponent();
        }

        //public Character ProcessOBJ2(string filepath) {
        //    ObjLoaderFactory OBJLF = new ObjLoaderFactory();
        //    var objl = OBJLF.Create();

        //    FileStream fs = new FileStream(filepath, System.IO.FileMode.Open);

        //    var Model = objl.Load(fs);
        //    fs.Close();


        //    //begin processing
        //    Character chr = new Character { name = Path.GetFileNameWithoutExtension(filepath) };
        //    int MajorOffset = 0;
        //    foreach (Group g in Model.Groups) {
        //        Shape shape = new Shape();
        //        shape.rawVertices = ConvertCJCVectorsToVector3((List<Vertex>)Model.Vertices);
        //        shape.textures.Add(Path.GetFileNameWithoutExtension(g.Material.DiffuseTextureMap));
        //        shape.name = g.Name;
        //        shape.type = 1;

        //        //we have all the vertices
        //        //now we parse them into primitives aka faces
        //        int VertexCount = 0;
        //        foreach (Face face in g.Faces) {
        //            Prim prim = new Prim();
        //            for (int i = 0;i < face._vertices.Count;i++) {
        //                prim.vertices.Add(i);
        //                VertexCount++;
        //            }
        //            shape.rawPrimitives.Add(prim);
        //        }

        //        foreach (Face face in g.Faces) {
        //            foreach (FaceVertex fv in face._vertices) {
        //                int v3df = (VertexCount - Math.Abs(fv.VertexIndex)) + MajorOffset;
        //                shape.rawVertices.Add(new Vector3(Model.Vertices[v3df].X, Model.Vertices[v3df].Y, Model.Vertices[v3df].Z));
        //                //shape.rawVertexTextureCoords.Add(new Vector2(Model.TextureCoords[v3df].X, Model.TextureCoords[v3df].Y));
        //            }
        //        }


        //        MajorOffset += VertexCount;
        //        chr.shapes.Add(shape);

        //    }
        //    return chr;
        //}

        public void ProcessOBJ(string filepath, bool imp_textures, bool imp_materials, bool imp_names, Vector3 imp_translation) {
            Obj obj = new Obj();
            obj.LoadObj(filepath);
            Mtl mat = new Mtl();
            mat.LoadMtl(obj.Mtl);

            //for (int i = 0;i < obj.FaceList.Count;i++) {
            //    Shape shape = new Shape();
            //    //shape.name = obj.GroupNames[i];
            //    shape.rawVertices = objptoshapes(obj, obj.FaceList[i]);
            //    importedCharacter.shapes.Add(new Shape());
            //}


            //a note from the Obj class, when we parse in the obj file, its entirely likely that the obj will have no groups or objects
            //this is a problem as this is so rudimentary that we rely on them existing to build our shape DB
            //the parser takes care of that for us, and regardless of whether or not the .OBJ file has a group or object,
            //a default object and group is made to store all the data in, so...yeah...
            //tl;dr, Objects[0].groups[0] will always exist regardless of whats imported.


            int vOffset = 0;
            int vAccumulator = 0;
            for (int i = 0;i < obj.Objects.Count;i++) {//for each shape...
                Obj.obj o = obj.Objects[i];
                Shape shape = new Shape();
                shape.name = (!imp_names || o.name == null) ? "" : o.name;
                foreach (Vertex v in o.vertexList) {
                    shape.rawVertices.Add(new Vector3((float)v.X, (float)v.Y, (float)v.Z));
                    shape.rawVertexShading.Add((v.color == null) ? System.Drawing.Color.Black : F_NGN.VColorToColor(v.color.ToArrayFloat()));
                }
                Matrix4D GTMF = new Matrix4D();
                imp_translation.SetMatrix4DNegative(GTMF, imp_translation);
                Vector3.TransformPoints(GTMF, ref shape.rawVertices);
                foreach (TextureVertex t in o.textureList) {
                    shape.rawVertexTextureCoords.Add(new Vector3((float)t.X, (float)t.Y, 0.0f));
                }
                for (int j = 0;j < o.groups.Count;j++) {//for each prim...
                    Obj.gf g = o.groups[j];
                    Prim prim = new Prim();
                    if (g.faces[0].VertexIndexList.Count() == 4) {
                        prim.type = 4;
                            } else {
                        prim.type = 1;
                    };
                    if (imp_materials) {
                        ObjParser.Types.Material usemtl = GetUsemtl(g.materialName, mat);
                        Material material = new Material {
                            id = (usemtl.Opacity == 1) ? 129 : 193,
                            RGB = usemtl.DiffuseReflectivity.ToArrayDouble().ToList()
                        };
                        if (imp_textures) { material.textureName = usemtl.DiffuseMap.Split('.')[0]; }
                        shape.AddMaterial(material);
                    }
                    for (int k = 0;k < g.faces.Count;k++) { // for each face... (3/4 points thing this is whats actually the prim, its weird i know)
                        Face f = g.faces[k];
                        for (int l = 0;l < f.VertexIndexList.Length;l++) {//for each vertex, of each face, of each primitive inside this root shape...
                            prim.vertices.Add((f.VertexIndexList[l] - 1) - vOffset);
                            vAccumulator++;
                        }
                    }
                    shape.rawPrimitives.Add(prim);
                }
                shape.rawVertexData = (List<Vector3>)XF.GenerateListData(1, shape.rawVertices.Count, new Vector3(0, 0, 0));
                shape.type = 5;
                shape.type2 = 65535;
                importedCharacter.shapes.Add(shape);
                vOffset += vAccumulator;
                vAccumulator = 0;
            }


            //for (int i = 0;i < obj.GroupFaces.Count;i++) {
            //    Shape shape = new Shape();
            //    shape.name = obj.GroupFaces[i].name;
            //    List<int> vertices = new List<int>();
            //    for (int j = 0;j < obj.GroupFaces[i].faces.Count;j++) {
            //        ObjParser.Types.Face face = obj.GroupFaces[i].faces[j];
            //        Prim prim = new Prim();
            //        prim.type = face.VertexIndexList.Count();
            //        for (int k = 0;k < face.VertexIndexList.Length;k++) {
            //            int v = Math.Abs(face.VertexIndexList[k]) - 1;
            //            prim.vertices.Add(v);
            //            vertices.Add(v);
            //        }
            //        shape.rawPrimitives.Add(prim);
            //    }
            //    for (int j = 0;j < vertices.Count;j++) {
            //        ObjParser.Types.Vertex vertex = obj.VertexList[vertices[j]];
            //        shape.rawVertices.Add(new Vector3((float)vertex.X, (float)vertex.Y, (float)vertex.Z));
            //    }
            //    shape.rawVertexData = (List<Vector3>)XF.GenerateListData(1, shape.rawVertices.Count, new Vector3(0, 0, 0));
            //    shape.rawVertexShading = (List<System.Drawing.Color>)XF.GenerateListData(1, shape.rawVertices.Count, System.Drawing.Color.FromArgb(255, 0, 0, 0));
            //    shape.rawVertexTextureCoords = (List<Vector3>)XF.GenerateListData(1, shape.rawVertices.Count, new Vector3(0, 0, 0));
            //    shape.type = 5;
            //    shape.type2 = 65535;
            //    importedCharacter.shapes.Add(shape);
            //}
        }

        public ObjParser.Types.Material GetUsemtl(string usemtlName, Mtl material) {
            foreach (ObjParser.Types.Material mat in material.MaterialList) {
                if (mat.Name == usemtlName) {
                    return mat;
                }
            }
            return null;
        }

        public List<Vector3> objptoshapes(Obj obj, ObjParser.Types.Face face) {
            List<Vector3> v3d = new List<Vector3>();
            foreach (int fid in face.VertexIndexList) {
                int id = Math.Abs(fid);
                v3d.Add(new Vector3((float)obj.VertexList[id].X, (float)obj.VertexList[id].Y, (float)obj.VertexList[id].Z));
            }
            return v3d;
        }


        //public List<Vector3> ConvertCJCVectorsToVector3(List<Vertex> source) {
        //    List<Vector3> v3d = new List<Vector3>();
        //    foreach (Vertex v in source) {
        //        v3d.Add(new Vector3(v.X, v.Y, v.Z));
        //    }
        //    return v3d;
        //}

        private void Importer_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e) {
            OpenFileDialog OFD = new OpenFileDialog();
            if (OFD.ShowDialog() == DialogResult.OK) {
                textBox1.Text = OFD.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            ProcessOBJ(textBox1.Text, checkTextures.Checked, checkMaterials.Checked, checkName.Checked, new Vector3((checkGX.Checked) ? 1 : 0, (checkGY.Checked) ? 1 : 0, (checkGZ.Checked) ? 1 : 0));
        }
    }
}
