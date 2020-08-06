using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToyTwoToolbox {
    /// <summary>
    /// This is the fileformat definition for NGN files
    /// </summary>
    public class F_NGN : F_Base {
        public FileProcessor.FileTypes FileType { get; } = FileProcessor.FileTypes.NGN;
        private string tempName;
        public string TempName { get => tempName; set => tempName = value; }
        private string filePath;
        public string FilePath { get => filePath; set => filePath = value; }

        public enum NGNFunction {
            Geometry = 256,
            Gscale = 257,
            AreaPortalPosition = 258,
            AreaPortalRotation = 259,
            Textures = 260,
            Linker = 261,
            Creatures = 262
        }

        public F_NGN() {

        }

        //var refs for ngn
        public NGNSchema Schema;
        public List<Texture> textures = new List<Texture>();
        public List<Character> characters = new List<Character>();
        public List<AreaPortal> areaPortals = new List<AreaPortal>();
        public List<Geometry> Geometries = new List<Geometry>();
        public List<DynamicScaler> GScales = new List<DynamicScaler>();
        public List<Linker> ObjectLinks = new List<Linker>();
        FileReader fr;

        public F_NGN ImportNGN(string path, bool multithread = true) {
            //we will be implementing multithreading, but we must change our processes depending on if the user uses it or not
            //FIRST, we do a function scan of the NGN to collect what sections we will be using...
            this.FilePath = path;
            if (multithread == true) {
                Schema = ARSONAnalysis(path);
                ImportViaSchema(Schema, path, this);
            }
            
            return this;
        }

        public F_NGN ImportViaSchema(NGNSchema NSchema, string path, F_NGN File = null) {
            File = (File == null) ? this : File;
            fr = new FileReader(path, true);
            List<Task> Threads = new List<Task>();
            int iteration = 0;
            Task NewThread;
            int FuncAreaPortalRotation = -1;

            foreach (NGNSchema.NGNFunctionDef Func in NSchema.NGNFunctions) {
                switch (Func.FunctionType) {
                    case NGNFunction.Textures:
                        Extract_Textures(Func.FunctionOffset, Func.FunctionLength);
                        break;
                    case NGNFunction.Creatures:
                        Extract_Characters(Func.FunctionOffset);
                        break;
                    case NGNFunction.AreaPortalPosition:
                        Extract_AreaPortal_Position(Func.FunctionOffset);
                        break;
                    case NGNFunction.AreaPortalRotation:
                        //    NewThread = Task.Factory.StartNew(() => { Extract_AreaPortal_Rotation(Func.FunctionOffset); });
                        FuncAreaPortalRotation = iteration;
                        break;
                    case NGNFunction.Geometry:
                        File.Geometries.Add(Extract_Geometry(Func.FunctionOffset));
                        break;
                    case NGNFunction.Gscale:
                        File.GScales.Add(Extract_DynamicScaler(Func.FunctionOffset));
                        break;
                    case NGNFunction.Linker:
                        Extract_ShapeLinks(Func.FunctionOffset);
                        break;
                }
            }




            //foreach (NGNSchema.NGNFunctionDef Func in NSchema.NGNFunctions) {
            //    NewThread = null;
            //    switch (Func.FunctionType) {
            //        case NGNFunction.Textures:
            //            NewThread = Task.Factory.StartNew(() => { File.textures = Extract_Textures(Func.FunctionOffset, Func.FunctionLength); });
            //            //Thread thread = new Thread(() => { File.textures = Extract_Textures(Func.FunctionOffset, Func.FunctionLength); });
            //            //thread.Start();
            //            break;
            //        case NGNFunction.Creatures:
            //            NewThread = Task.Factory.StartNew(() => { File.characters = Extract_Characters(Func.FunctionOffset); });
            //            break;
            //        case NGNFunction.AreaPortalPosition:
            //            NewThread = Task.Factory.StartNew(() => { Extract_AreaPortal_Position(Func.FunctionOffset); });
            //            break;
            //        case NGNFunction.AreaPortalRotation:
            //            //    NewThread = Task.Factory.StartNew(() => { Extract_AreaPortal_Rotation(Func.FunctionOffset); });
            //            FuncAreaPortalRotation = iteration;
            //            break;
            //        case NGNFunction.Geometry:
            //            NewThread = Task.Factory.StartNew(() => { File.Geometries.Add(Extract_Geometry(Func.FunctionOffset)); });
            //            break;
            //        case NGNFunction.Gscale:
            //            NewThread = Task.Factory.StartNew(() => { File.GScales.Add(Extract_DynamicScaler(Func.FunctionOffset)); });
            //            break;
            //        case NGNFunction.Linker:
            //            NewThread = Task.Factory.StartNew(() => { File.Linker = Extract_ShapeLinks(Func.FunctionOffset); });
            //            break;
            //    }
            //    iteration++;
            //    if (NewThread != null) { Threads.Add(NewThread); }
            //}

            //    Task.WaitAll(Threads.ToArray());
            //    if (FuncAreaPortalRotation != -1) {
            //        Threads.Add(Task.Factory.StartNew(() => { Extract_AreaPortal_Rotation(NSchema.NGNFunctions[FuncAreaPortalRotation].FunctionOffset); }));
            //    }
            //Task.WaitAll(Threads.ToArray());


            Console.WriteLine(File.FileType);



            //Task task1 = Task.Factory.StartNew(() => doStuff());
            //Task task2 = Task.Factory.StartNew(() => doStuff());
            //Task task3 = Task.Factory.StartNew(() => doStuff());
            //Task.WaitAll(task1, task2, task3);
            //Console.WriteLine("All threads complete");


            return File;
        }

        public void Extract_Textures(int PTR, int offset) {
            int seekPTR = PTR;
            int textureCount = fr._readint(ref seekPTR, 4);
            for (int i = 0;i < textureCount;i++) {
                Texture Tex = new Texture();
                int TexLength = fr._readint(ref seekPTR, 4);
                int TexNameLength = fr._readint(ref seekPTR, 4)-1;
                string TexName = fr._readstring(ref seekPTR, TexNameLength+1);
                Tex.name = TexName;
                byte[] TexData = fr._readbytes(ref seekPTR, TexLength);
                Tex.image = new System.Drawing.Bitmap(new MemoryStream(TexData));
                this.textures.Add(Tex);
            }
        }

        public void Extract_Characters(int PTR) {
            int seekPTR = PTR;
            List<SlotConfig> CharacterSlots = new List<SlotConfig>();
            int CSCCount = fr._readint(ref seekPTR, 4);
            for (int i = 0;i < CSCCount;i++) {
                int CSCNameLength = fr._readint(ref seekPTR, 1);
                if (CSCNameLength > 0) {
                    CharacterSlots.Add(new SlotConfig {
                        SlotID = i,
                        CharacterName = string.Concat(fr._readstring(ref seekPTR, CSCNameLength).Split(Path.GetInvalidFileNameChars()))
                    });
                }
            }
            if (CharacterSlots.Count > 0) { //at least one valid character
                for (int i = 0;i < CharacterSlots.Count;i++) {
                    Character chr = new Character();
                    chr.name = CharacterSlots[i].CharacterName;
                    Extract_CharacterData(chr, ref seekPTR);
                    this.characters.Add(chr);
                }
            }
        }

        public Character Extract_CharacterData(Character chr, ref int seekPTR) {
            Shape PatchShape = new Shape();
            int PTR = seekPTR;
            int FID = 0;
            int FCL = 0; //function content length
            for (;;){
                FID = fr._readint(ref seekPTR, 4);
                FCL = fr._readint(ref seekPTR, 4);
                if (FID != 0 && FCL < 2) { SessionManager.Report("FCL was invalid for a NGN function state and was skipped [->F_NGN->ARSON->Extract_CharacterData]",SessionManager.RType.WARNING); }
                if (FID <= 1) {
                    return chr;
                }
                if (FID == 512) { //collect node count
                    chr.nodeCount = fr._readint(ref seekPTR, 4);
                    SessionManager.Report("got node id", SessionManager.RType.INFO);
                    continue;
                } else if (FID == 514) {//collect node names
                    List<string> NodeNames = new List<string>();
                    int NodeOffset = 0;
                    int NodeCount = 0;
                    for (;;){
                        if (NodeOffset >= FCL) {
                            chr.nodeCount = NodeCount;
                            break;
                        }
                        int CurrentNodeNameLength = fr._readint(ref seekPTR, 1);
                        if (CurrentNodeNameLength > 0) {
                            NodeNames.Add(fr._readstring(ref seekPTR, CurrentNodeNameLength));
                            NodeOffset += CurrentNodeNameLength + 1;
                            NodeCount++;
                        }
                    }
                    SessionManager.Report("collected " + NodeCount + " node names", SessionManager.RType.INFO);
                    continue;
                } else if (FID == 513) {//null data
                    if (chr.nodeCount > 0) {
                        fr._seek(64 * chr.nodeCount, ref seekPTR);
                    }
                    //SessionManager.Report("bypassed empty data", SessionManager.RType.INFO);
                    continue;
                } else if (FID == 515) {//shape data
                    //SessionManager.Report("begun shape data routine", SessionManager.RType.INFO);
                    for (int i = 0;i < chr.nodeCount;i++) {
                        int ShapeID = fr._readint(ref seekPTR, 2);
                        if((ShapeID & 1) == 1) {
                            Shape shape = new Shape();
                            shape.type = ShapeID;
                            shape.type2 = fr._readint(ref seekPTR, 2);
                            Extract_ShapeData(shape, ref seekPTR);
                            chr.model.shapes.Add(shape);
                        } else { //the shape has an invalid header, this means its likely a nullnode, id like to just skip it but im not sure if i should
                            int Type2ID = fr._readint(ref seekPTR, 2);
                            if (Type2ID == 65535) {//shape is indeed a nullnode, add it anyway
                                chr.model.shapes.Add(new Shape { type2 = Type2ID });
                            } else {
                                SessionManager.Report("The shape ID was invalid or null and was skipped <frs=" + seekPTR + "> [->F_NGN->ARSON->Extract_CharacterData]",SessionManager.RType.ERROR);
                            }
                    }
                    }

                    continue;
                } else if (FID == 516) { //extract animation data for this character
                    //SessionManager.Report("hello sessionmanager, I'm about to read " + FCL + " bytes from " + seekPTR + "!", SessionManager.RType.ERROR);
                    //SessionManager.Report("this animation data will be sent to " + chr.name + " as anim " + chr.Anims.Count, SessionManager.RType.ERROR);
                    Extract_Animations(chr, ref seekPTR, FCL);
                } else if (FID == 517) { //Things to do if we identify that this shape is a Patch shape
                    PatchShape = new Shape();
                    PatchShape.type = 255;
                    Extract_Shape_Textures(ref seekPTR, PatchShape);
                } else if (FID == 518) { //patch materials
                    Extract_Shape_Materials(ref seekPTR, PatchShape);
                } else if (FID == 519) {
                    Extract_Shape_Vertices(ref seekPTR, PatchShape);
                } else if (FID == 520) {
                    Extract_Shape_Patch(ref seekPTR, PatchShape);
                }
            }

            return chr;
        }

        public Shape Extract_ShapeData(Shape shape, ref int PTR) {
            //SessionManager.Report("extracting shape data", SessionManager.RType.INFO);
            int FID = 0;
            int FCL = 0; //function content length
            for (;;){
                FID = fr._readint(ref PTR, 4);
                FCL = fr._readint(ref PTR, 4);
                if (FID == 0) {
                    return shape;
                } else if (FID == 64) { //Get Shape Name
                    Extract_Shape_Name(ref PTR, shape);
                } else if (FID == 65) { //Get Shape Textures
                    Extract_Shape_Textures(ref PTR, shape);
                } else if (FID == 66) { //Get Shape Materials
                    Extract_Shape_Materials(ref PTR, shape);
                } else if (FID == 67) { //Get Shape Vertices
                    Extract_Shape_Vertices(ref PTR, shape);
                } else if (FID == 68) { //Get Shape Primitives
                    Extract_Shape_Primitives(ref PTR, shape);
                } else { //ohno
                    return null;
                }
            }
        }

        public void Extract_Shape_Name(ref int ptr, Shape shape) {
            int ShapeNameLength = fr._readint(ref ptr, 1);
            shape.name = fr._readstring(ref ptr, ShapeNameLength);
        }

        public void Extract_Shape_Textures(ref int ptr, Shape shape) {
            int texture_NametableCount = fr._readint(ref ptr, 2);
            int texture_Count = fr._readint(ref ptr, 2);
            int texture_Unknown = fr._readint(ref ptr, 1);
            int texture_NametableLength = fr._readint(ref ptr, 1);
            for (int i = 0;i < texture_Count;i++) {
                int texture_DataLength = fr._readint(ref ptr, 1);
                shape.textures.Add(string.Concat(fr._readstring(ref ptr, texture_DataLength).Split(Path.GetInvalidFileNameChars())));
            }
            for (int i = 0;i < texture_NametableCount;i++) {//ignore data
                fr._seek(26, ref ptr);
            }
        }

        public void Extract_Shape_Materials(ref int ptr, Shape shape) {
            int material_Count = fr._readint(ref ptr, 4);
            if (material_Count < 1) { return; }
            for (int i = 0;i < material_Count;i++) {
                Material material = new Material();
                material.id = fr._readint(ref ptr, 4);
                int materialLength = fr._readint(ref ptr, 4);
                List<double> RGB = new List<double>();
                for (int j = 1;j < 33;) {
                    if ((material.id & j) == 1) {
                        for (int k = 0;k < 3;k++) {
                            RGB.Add((double)fr._readint(ref ptr, 1) * 1.0 * 0.0039215689);
                        }
                    }
                    j += j;
                }
                if ((material.id & 64) != 0) { material.data = fr._readint(ref ptr, 4); }
                if (((material.id >> 7) & 15) > 0) {
                    material.textureIndex = fr._readint(ref ptr, 2);
                }
                material.RGB = RGB;
                shape.materials.Add(material);
            }
        }

        public void Extract_Shape_Vertices(ref int ptr, Shape shape) {
            //SessionManager.Report("collecting vertices", SessionManager.RType.INFO);
            int V17 = fr._readint(ref ptr, 4);
            int VertexDataLength = fr._readint(ref ptr, 4);
            int vertex_Count = fr._readint(ref ptr, 4);
            int VertexSize = 0;
            if ((V17 & 1) == 1) { VertexSize = VertexDataLength - 12; }
            if ((V17 & 2) == 1) { VertexSize -= 12; }
            if ((V17 & 4) == 1) { VertexSize = 4; }
            if ((V17 & 8) == 1) { VertexSize = 4; }
            if (VertexSize > 0) {
                for (int i = 0;i < vertex_Count;i++) {
                    if ((V17 & 1) != 0) {
                        shape.rawVertices.Add(new Vector3(fr._readflt(ref ptr, 4) * 1.0F, fr._readflt(ref ptr, 4) * 1.0F, fr._readflt(ref ptr, 4) * 1.0F));
                    } else {  }
                    shape.rawVertexData.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                    if ((V17 & 4) != 0) {
                        shape.rawVertexShading.Add(
                            Color.FromArgb(
                                fr._readint(ref ptr, 1),  //VTX A Component
                                fr._readint(ref ptr, 1),  //VTX R Component
                                fr._readint(ref ptr, 1),  //VTX G Component
                                fr._readint(ref ptr, 1)   //VTX B Component
                            )
                        );
                    }
                    if ((V17 & 8) != 0) { fr._seek(4, ref ptr); } //oh no
                    if ((V17 & 240) != 0) {
                        shape.rawVertexTextureCoords.Add(new Vector2(fr._readflt(ref ptr, 4), 1.0 - fr._readflt(ref ptr, 4)));
                    }
                }
                shape.datalength = VertexDataLength;
            } else {
                SessionManager.Report("V17VertexSize was invalid [->F_NGN->ARSON->Extract_Shape_Vertices]", SessionManager.RType.ERROR);
            }
        }

        /// <summary>
        /// Extracts the primitve shape definitions from the NGN. <para/>
        /// See the <seealso cref="Primitive"/> class for info
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="shape"></param>
        public void Extract_Shape_Primitives(ref int ptr, Shape shape) {
            int PrimitiveCount = fr._readint(ref ptr, 4); //the number of prims for this shape
            for (int i = 0;i < PrimitiveCount;i++) {
                Prim prim = new Prim {
                    type = fr._readint(ref ptr, 4), // the id of this primitive
                    materialID = fr._readint(ref ptr, 2), //the id of the material to use for this primitive
                    vertexCount = fr._readint(ref ptr, 2) //how many vertices make up this primitive
                };
                for (int j = 0;j < prim.vertexCount;j++) {
                    prim.vertices.Add(fr._readint(ref ptr, 2)); //collect each Vector based on the prim stucture
                }
                shape.rawPrimitives.Add(prim);
            }
        }


        public void Extract_Animations(Character chr, ref int ptr, int Contract_Length) {
            //int ADRP = ptr;
            //File.AppendAllText(Application.StartupPath + "ADR.dat", "[" + chr.name + "] [" + chr.Anims.Count + "] " + XF.BytesToHex(fr._readbytes(ref ADRP, Contract_Length)) + Environment.NewLine);
            int Contract_External_Length = ptr + Contract_Length;
            int Contract_Internal_Length = ptr + 0; //WARNING ENSURE THIS DOESNT GET COMPILER OPTIMIZED OUT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            int startofanimdata = ptr;
            Animation anim = new Animation {
                id = fr._readint(ref ptr, 4),
                FrameCount = fr._readint(ref ptr, 2),
                UNK1 = fr._readint(ref ptr, 2),
                UNK2 = fr._readint(ref ptr, 2),
                NodeCount = fr._readint(ref ptr, 2),
                UNK3 = fr._readint(ref ptr, 2),
                HPTR = fr._readint(ref ptr, 2)
            };
            int validIncrementor = 0;
            for (int i = 0;i < anim.NodeCount;i++) {
                int NodeOffset = fr._readint(ref ptr, 2);
                anim.Nodes.Add(new Animation.Node {
                    id = (NodeOffset < 0 || NodeOffset > 65532) ? NodeOffset : validIncrementor,
                    offset = NodeOffset
                });
                //Console.WriteLine((NodeOffset < 0 || NodeOffset > 65532) ? NodeOffset : validIncrementor);
                if (NodeOffset > 0 && NodeOffset < 65532) { validIncrementor++; }
            }
            anim.UNK4 = fr._readint(ref ptr, 2);
            anim.UNK5 = fr._readint(ref ptr, 2);
            anim.UNK6 = fr._readint(ref ptr, 2);
            anim.UNK7 = fr._readint(ref ptr, 2);
            startofanimdata = ptr; //actual start of anim data
            Contract_Internal_Length = ptr - Contract_Internal_Length;
            for (int i = 0;i < anim.Nodes.Count;i++) {//for each node
                if (anim.Nodes[i].id > -1 && anim.Nodes[i].id < 65533) {
                    int nodeoffset = i * 10;
                    for (int j = 0;j < anim.FrameCount;j++) {//for each frame of animation
                        int internalFrameOffset = nodeoffset + ((j * validIncrementor) * 10);
                        int frameoffset = startofanimdata + internalFrameOffset; //this is where we are reading from for each frame
                        int frameoffsetstorage = frameoffset;
                        anim.Nodes[i].frames.Add(
                            new AnimationFrame(
                                new Vector3(fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2)),
                                new Vector2(fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2))
                            )
                        );
                        frameoffsetstorage = frameoffset - frameoffsetstorage;
                        Contract_Internal_Length += 10;
                        //SessionManager.Report("reading from: " + internalFrameOffset.ToString() + " out of " + AnimRaw.Length + " | " + " I: " + i + " J: " + j, SessionManager.RType.WARNING);
                    }
                } else {
                    //SessionManager.Report("animation node was skipped", SessionManager.RType.WARNING);
                }
            }
            if (Contract_Internal_Length != Contract_Length || ptr != Contract_Length) { 
                SessionManager.Report("The FCL contract was not correctly fulfilled <" + Contract_Internal_Length + "/" + Contract_Length + ">[->F_NGN->ARSON->Extract_Animations]", SessionManager.RType.WARNING);
                if (Contract_Internal_Length < Contract_Length) {
                    anim.extradata = fr._readbytes(ref Contract_Internal_Length, Contract_Length - Contract_Internal_Length);
                }
                ptr += (Contract_External_Length - ptr) ; //dont ruin the rest of the NGN
            }


            chr.Anims.Add(anim);
        }





        public void Extract_Animations2(Character chr, ref int ptr, int Contract_Length) {
            int Contract_Internal_Length = ptr + 0; //WARNING ENSURE THIS DOESNT GET COMPILER OPTIMIZED OUT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            int startofanimdata = ptr;
            Animation anim = new Animation {
                id = fr._readint(ref ptr, 4),
                FrameCount = fr._readint(ref ptr, 2),
                UNK1 = fr._readint(ref ptr, 2),
                UNK2 = fr._readint(ref ptr, 2),
                NodeCount = fr._readint(ref ptr, 2),
                UNK3 = fr._readint(ref ptr, 2),
                HPTR = fr._readint(ref ptr, 2)
            };
            int validIncrementor = 0;
            for (int i = 0;i < anim.NodeCount;i++) {
                int NodeOffset = fr._readint(ref ptr, 2);
                anim.Nodes.Add(new Animation.Node {
                    id = (NodeOffset < 0 || NodeOffset > 65532) ? NodeOffset : validIncrementor,
                    offset = NodeOffset
                });
                Console.WriteLine((NodeOffset < 0 || NodeOffset > 65532) ? NodeOffset : validIncrementor);
                if (NodeOffset > -1) { validIncrementor++; }
            }
            anim.UNK4 = fr._readint(ref ptr, 2);
            anim.UNK5 = fr._readint(ref ptr, 2);
            anim.UNK6 = fr._readint(ref ptr, 2);
            anim.UNK7 = fr._readint(ref ptr, 2);
            startofanimdata = ptr; //actual start of anim data
            Contract_Internal_Length = ptr - Contract_Internal_Length;
            for (int i = 0;i < anim.Nodes.Count;i++) {//for each node
                if (anim.Nodes[i].id > -1 && anim.Nodes[i].id < 65533) {
                    int nodeoffset = i * 20;
                    for (int j = 0;j < anim.FrameCount;j++) {//for each frame of animation
                        int internalFrameOffset = nodeoffset + ((j * validIncrementor) * 20);
                        int frameoffset = startofanimdata + internalFrameOffset; //this is where we are reading from for each frame
                        int frameoffsetstorage = frameoffset;
                        anim.Nodes[i].frames.Add(
                            new AnimationFrame(
                                new Vector3(fr._readflt(ref frameoffset, 4), fr._readflt(ref frameoffset, 4), fr._readflt(ref frameoffset, 4)),
                                new Vector2(fr._readflt(ref frameoffset, 4), fr._readflt(ref frameoffset, 4))
                            )
                        );
                        frameoffsetstorage = frameoffset - frameoffsetstorage;
                        Contract_Internal_Length +=  20;
                    }
                } else {
                    SessionManager.Report("animation node was skipped", SessionManager.RType.WARNING);
                }
            }
            chr.Anims.Add(anim);
            if (Contract_Internal_Length != Contract_Length) { SessionManager.Report("The FCL contract was not fufilled <" + Contract_Internal_Length + "/" + Contract_Length + ">[->F_NGN->ARSON->Extract_Animations]",SessionManager.RType.WARNING); }
        }

        public void Extract_Shape_Patch(ref int ptr, Shape shape) {
            int patchCount = fr._readint(ref ptr, 4);
            //begin patch loop
            int shapeVertexOffset = 0;
            for (int i = 0;i < patchCount;i++) {
                Patch patch = new Patch {
                    vertexCount = fr._readint(ref ptr, 2),
                    materialID = fr._readint(ref ptr, 2),
                    Unknown1 = fr._readint(ref ptr, 4),
                    Unknown2 = fr._readint(ref ptr, 4),
                    Unknown3 = fr._readint(ref ptr, 4),
                    Unknown4 = fr._readint(ref ptr, 4)
                };
                for (int j = 0;j < patch.vertexCount;j++) {
                    patch.vertices.Add(shapeVertexOffset + j);
                    patch.vertices.Add(shapeVertexOffset + j + patch.vertexCount);
                }
                shapeVertexOffset += patch.vertexCount * 2;
                shape.rawPrimitives.Add(patch);
            }
        }

        public void Extract_AreaPortal_Position(int ptr) {
            int areaPortalCount = fr._readint(ref ptr, 4);
            if (areaPortalCount > 0) {
                for (int i = 0;i < areaPortalCount;i++) {
                    AreaPortal portal = new AreaPortal();
                    int vertexCount = fr._readint(ref ptr, 4);
                    for (int j = 0;j < vertexCount;j++) {
                        portal.Vertices.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                    }
                    this.areaPortals.Add(portal);
                }
            }
        }

        public void Extract_AreaPortal_Rotation(int ptr) {
            int areaPortalCount = fr._readint(ref ptr, 4);
            if (areaPortalCount > 0) {
                for (int i = 0;i < areaPortalCount;i++) {
                    int areaPortalID = fr._readint( ref ptr,4); //skip
                    this.areaPortals[areaPortalID].Rotation = new Vector2(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4));
                }
            }
        }

        public Geometry Extract_Geometry(int ptr) {
            int shapeCount = fr._readint(ref ptr, 4);
            Geometry geom = new Geometry();
            for (int i = 0;i < shapeCount;i++) {
                Shape shape = new Shape {
                    type = 257
                };
                Extract_ShapeData(shape, ref ptr);
                geom.shapes.Add(shape);
            }
            return geom;
        }

        public DynamicScaler Extract_DynamicScaler(int ptr) {
            int shapeCount = fr._readint(ref ptr, 4);
            int shapeDataLength = fr._readint(ref ptr, 4);
            bool unknown = shapeDataLength == 40;
            DynamicScaler DS = new DynamicScaler();
            for (int i = 0;i < shapeCount;i++) {
                DS.Translation.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                DS.Rotation.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                DS.Scale.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                DS.shapeID.Add(fr._readint(ref ptr, 4));
                if (unknown) { DS.Unknown.Add(fr._readint(ref ptr, 4)); }
            }
            return DS;
        }

        public void Extract_ShapeLinks(int ptr) {
            int linkCount = fr._readint(ref ptr, 4);
            fr._seek(4, ref ptr);
            for (int i = 0;i < linkCount;i++) {
                this.ObjectLinks.Add(new Linker {
                    LinkID = fr._readstring(ref ptr, 2),
                    ShapeID = fr._readint(ref ptr, 2)
                });
            }
        }


        public F_NGN ImportViaRaw(NGNSchema NSchema, string path) {
            return new F_NGN();
        }

        private NGNSchema ARSONAnalysis(string path, F_NGN File = null) {
            File = (File == null) ? this : File;
            //so here we go through the file and see if 1. it's valid, and 2 how many threads we can create for each func
            //a nice game of leapfrog
            NGNSchema NS = new NGNSchema(File);
            FileReader fr = new FileReader(path);
            int functionLength;
            int functionID;
            for (;;){
                if (fr.foffset >= fr.fstream.Length) {
                    break;
                }
                
                functionID = fr.readint(4);
                functionLength = fr.readint(4);
                NS.AddFunction((NGNFunction)functionID, fr.foffset, functionLength);
                fr.seek(functionLength);
            }
            return NS;
        }

        public F_Base Import(string path) {
            return ImportNGN(path);
        }

        public bool Export(string path) {
            FileWriter fwngn = new FileWriter();

            //lets just get into it, not in a good mood today
            //textures
            if (textures.Count > 0) {
                int ptr = fwngn.foffset; //get func header ptr
                fwngn.Nop(8); //alloc 8 bytes for header data

            }
            return true;
        }

        /// <summary>
        /// Describes the layout of an associated NGN File
        /// </summary>
        public class NGNSchema {
            public F_NGN LinkedNGN;
            public List<NGNFunctionDef> NGNFunctions;

            public NGNSchema(F_NGN File) {
                NGNFunctions = new List<NGNFunctionDef>();
                LinkedNGN = File;
            }

            public void AddFunction(NGNFunction FunctionID, int NFPTR, int NFL) {
                NGNFunctions.Add(new NGNFunctionDef(FunctionID,NFPTR,NFL));
            }

            public class NGNFunctionDef {
                public NGNFunction FunctionType;
                public int FunctionOffset;
                public int FunctionLength;

                public NGNFunctionDef(NGNFunction FunctionID, int NFPTR, int NFL) {
                    FunctionType = FunctionID;
                    FunctionOffset = NFPTR;
                    FunctionLength = NFL;
                }
            }
        }
    }
}
