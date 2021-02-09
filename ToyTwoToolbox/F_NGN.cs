using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
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
        public List<int> SlotValidity = new List<int>();
        public List<SlotConfig> characterSlots = new List<SlotConfig>();
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
            SessionManager.Report("Importing NGN Data via ARSON Schema...");
            File = File ?? this;
            fr = new FileReader(path, true);
            List<Task> Threads = new List<Task>();

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
                        Extract_AreaPortal_Rotation(Func.FunctionOffset);
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


            //Thread thread = new Thread(() => { File.textures = Extract_Textures(Func.FunctionOffset, Func.FunctionLength); });
            //thread.Start();


            //foreach (NGNSchema.NGNFunctionDef Func in NSchema.NGNFunctions) {
            //    NewThread = null;
            //    switch (Func.FunctionType) {
            //        case NGNFunction.Textures:
            //            NewThread = Task.Factory.StartNew(() => { Extract_Textures(Func.FunctionOffset, Func.FunctionLength); });
            //            break;
            //        case NGNFunction.Creatures:
            //            NewThread = Task.Factory.StartNew(() => { Extract_Characters(Func.FunctionOffset); });
            //            break;
            //        case NGNFunction.AreaPortalPosition:
            //            NewThread = Task.Factory.StartNew(() => { Extract_AreaPortal_Position(Func.FunctionOffset); });
            //            break;
            //        case NGNFunction.AreaPortalRotation:
            //            //NewThread = Task.Factory.StartNew(() => { Extract_AreaPortal_Rotation(Func.FunctionOffset); });
            //            FuncAreaPortalRotation = iteration;
            //            break;
            //        case NGNFunction.Geometry:
            //            NewThread = Task.Factory.StartNew(() => { File.Geometries.Add(Extract_Geometry(Func.FunctionOffset)); });
            //            break;
            //        case NGNFunction.Gscale:
            //            NewThread = Task.Factory.StartNew(() => { File.GScales.Add(Extract_DynamicScaler(Func.FunctionOffset)); });
            //            break;
            //        case NGNFunction.Linker:
            //            NewThread = Task.Factory.StartNew(() => { Extract_ShapeLinks(Func.FunctionOffset); });
            //            break;
            //    }
            //    iteration++;
            //    if (NewThread != null) { Threads.Add(NewThread); }
            //}

            //Task.WaitAll(Threads.ToArray());
            //if (FuncAreaPortalRotation != -1) {
            //    Threads.Add(Task.Factory.StartNew(() => { Extract_AreaPortal_Rotation(NSchema.NGNFunctions[FuncAreaPortalRotation].FunctionOffset); }));
            //}
            //Task.WaitAll(Threads.ToArray());


            //Console.WriteLine(File.FileType);



            //Task task1 = Task.Factory.StartNew(() => doStuff());
            //Task task2 = Task.Factory.StartNew(() => doStuff());
            //Task task3 = Task.Factory.StartNew(() => doStuff());
            //Task.WaitAll(task1, task2, task3);
            //Console.WriteLine("All threads complete");

            SessionManager.Report("Completed NGN Import!");
            return File;
        }

        public void Extract_Textures(int PTR, int offset) {
            SessionManager.Report("Extracting NGN Textures...");
            int seekPTR = PTR;
            int textureCount = fr._readint(ref seekPTR, 4);
            for (int i = 0;i < textureCount;i++) {
                Texture Tex = new Texture();
                int TexLength = fr._readint(ref seekPTR, 4);
                int TexNameLength = fr._readint(ref seekPTR, 4) - 1;
                string TexName = fr._readstring(ref seekPTR, TexNameLength + 1);
                Tex.name = TexName;
                byte[] TexData = fr._readbytes(ref seekPTR, TexLength);
                Tex.image = new System.Drawing.Bitmap(new MemoryStream(TexData));
                this.textures.Add(Tex);
            }
        }

        public void Extract_Characters(int PTR) {
            SessionManager.Report("Extracting NGN Characters...");
            int seekPTR = PTR;
            List<SlotConfig> CharacterSlots = new List<SlotConfig>();
            List<int> _SlotValidity = new List<int>();
            int CSCCount = fr._readint(ref seekPTR, 4);
            int validslots = 0;
            for (int i = 0;i < CSCCount;i++) {
                int CSCNameLength = fr._readint(ref seekPTR, 1);
                if (CSCNameLength > 0) {
                    CharacterSlots.Add(new SlotConfig {
                        SlotID = i,
                        Name = string.Concat(fr._readstring(ref seekPTR, CSCNameLength).Split(Path.GetInvalidFileNameChars()))
                    });
                    _SlotValidity.Add(validslots);
                    validslots++;
                } else {
                    _SlotValidity.Add(-1);
                }
            }
            if (CharacterSlots.Count > 0) { //at least one valid character
                for (int i = 0;i < CharacterSlots.Count;i++) {
                    Character chr = new Character {
                        owner = this,
                        name = CharacterSlots[i].Name
                    };
                    SessionManager.Report("Character found: " + chr.name + " - extracting data...");
                    Extract_CharacterData(chr, ref seekPTR);
                    this.characters.Add(chr);
                }
            }
            SlotValidity = _SlotValidity;
            characterSlots = CharacterSlots;
        }

        public Character Extract_CharacterData(Character chr, ref int seekPTR) {
            Shape PatchShape = new Shape();
            int FID;
            int FCL; //function content length
            for (;;){
                FID = fr._readint(ref seekPTR, 4);
                FCL = fr._readint(ref seekPTR, 4);
                if (FID != 0 && FCL < 2) { SessionManager.Report("FCL was invalid for a NGN function state and was skipped [->F_NGN->ARSON->Extract_CharacterData]", SessionManager.RType.WARN); }
                if (FID <= 1) {
                    return chr;
                }
                if (FID == 512) { //collect node count
                    chr.nodeCount = fr._readint(ref seekPTR, 4);
                    continue;
                } else if (FID == 514) {//collect node names
                    List<SlotConfig> Nodes = new List<SlotConfig>();
                    int NodeOffset = 0;
                    int NodeCount = 0;
                    for (int i = 0;;i++) {
                        if (NodeOffset >= FCL) {
                            if(chr.nodeCount!=0 && chr.nodeCount < NodeCount) {
                                SessionManager.Report("Node count in Character <" + chr.name + "> was invalid!", SessionManager.RType.WARN);
                            }
                            chr.nodeCount = NodeCount;
                            break;
                        }
                        int CurrentNodeNameLength = fr._readint(ref seekPTR, 1);
                        if (CurrentNodeNameLength > 0) {
                            Nodes.Add(new SlotConfig { SlotID = i, Name = fr._readstring(ref seekPTR, CurrentNodeNameLength) });
                            NodeOffset += CurrentNodeNameLength + 1;
                            NodeCount++;
                        }
                    }
                    chr.nodes = Nodes;
                    continue;
                } else if (FID == 513) {//null data
                    if (chr.nodeCount > 0) {
                        fr._seek(64 * chr.nodeCount, ref seekPTR);
                    }
                    //SessionManager.Report("bypassed empty data", SessionManager.RType.INFO);
                    continue;
                } else if (FID == 515) {//shape data
                    SessionManager.Report("Decompressing shape data...");
                    for (int i = 0;i < chr.nodeCount;i++) {
                        int ShapeID = fr._readint(ref seekPTR, 2);
                        if ((ShapeID & 1) == 1) {
                            Shape shape = new Shape();
                            shape.type = ShapeID;
                            shape.type2 = fr._readint(ref seekPTR, 2);
                            Extract_ShapeData(shape, ref seekPTR);
                            chr.RegisterShape(shape);
                        } else { //the shape has an invalid header, this means its likely a nullnode, id like to just skip it but im not sure if i should
                            int Type2ID = fr._readint(ref seekPTR, 2);
                            if (Type2ID == 65535) {//shape is indeed a nullnode, add it anyway
                                chr.RegisterShape(new Shape { type = ShapeID, type2 = Type2ID });
                            } else {
                                SessionManager.Report("The shape ID was invalid or null and was skipped <frs=" + seekPTR + "> [->F_NGN->ARSON->Extract_CharacterData]", SessionManager.RType.ERROR);
                            }
                        }
                        //SessionManager.Report("Context Owner:" + chr.name + " ShapeID:" + chr.model.shapes[chr.model.shapes.Count - 1].type + " ShapeID2:" + chr.model.shapes[chr.model.shapes.Count - 1].type2);
                    }

                    continue;
                } else if (FID == 516) { //extract animation data for this character
                    Extract_Animations(chr, ref seekPTR, FCL);
                } else if (FID == 517) { //Things to do if we identify that this shape is a Patch shape
                    PatchShape = new Shape {
                        _SPType = 1
                    };
                    Extract_Shape_Textures(ref seekPTR, PatchShape);
                } else if (FID == 518) { //patch materials
                    Extract_Shape_Materials(ref seekPTR, PatchShape, FCL);
                } else if (FID == 519) {
                    Extract_Shape_Vertices(ref seekPTR, PatchShape);
                } else if (FID == 520) {
                    Extract_Shape_Patch(ref seekPTR, PatchShape);
                    chr.RegisterShape(PatchShape);
                }
            }

            return chr;
        }

        public Shape Extract_ShapeData(Shape shape, ref int PTR) {
            for (;;){
                //SessionManager.Report("extracting shape data", SessionManager.RType.INFO);
                int FID = fr._readint(ref PTR, 4);
                int FCL = fr._readint(ref PTR, 4);
                if (FID == 0) {
                    return shape;
                } else if (FID == 64) { //Get Shape Name
                    Extract_Shape_Name(ref PTR, shape);
                } else if (FID == 65) { //Get Shape Textures
                    Extract_Shape_Textures(ref PTR, shape);
                } else if (FID == 66) { //Get Shape Materials
                    Extract_Shape_Materials(ref PTR, shape, FCL);
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
            SessionManager.Report("Extracting shape textures...", SessionManager.RType.DEBUG);
            int texture_NametableCount = fr._readint(ref ptr, 2);
            int texture_Count = fr._readint(ref ptr, 2);
            int texture_Unknown = fr._readint(ref ptr, 1);
            int texture_NametableLength = fr._readint(ref ptr, 1);
            for (int i = 0;i < texture_Count;i++) {
                int texture_DataLength = fr._readint(ref ptr, 1);
                string texturename = string.Concat(fr._readstring(ref ptr, texture_DataLength).Split(Path.GetInvalidFileNameChars()));
                shape.textures.Add(texturename);
                shape.texturesGlobal.Add(TexNameToGlobalID(texturename));
            }
            for (int i = 0;i < texture_NametableCount;i++) {//ignore data
                fr._seek(26, ref ptr);
            }
        }

        public void Extract_Shape_Materials(ref int ptr, Shape shape, int ContractLength = -1) {
            SessionManager.Report("Extracting shape materials...", SessionManager.RType.DEBUG);
            int InternalContractPTR = ptr;
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
                            RGB.Add(fr._readint(ref ptr, 1) * 1.0 * 0.0039215689);
                        }
                    }
                    j += j;
                }
                if ((material.id & 64) != 0) { material.metadata = fr._readint(ref ptr, 4); }
                if (((material.id >> 7) & 15) > 0) {
                    material.textureIndex = fr._readint(ref ptr, 2);
                    material.textureIndexRelative = (material.textureIndex == 65535) ? 65535 : shape.texturesGlobal[material.textureIndex];
                }
                material.RGB = RGB;
                //Contract checks
                if (materialLength > 5 && (material.id & 64) == 0) {
                    SessionManager.Report("Invalid material properties for shape @" + (ptr - materialLength), SessionManager.RType.WARN);
                }
                shape.AddMaterial(material);
            }
            ptr += SessionManager.ValidateContract(ptr - InternalContractPTR, ContractLength);
        }

        public void Extract_Shape_Vertices(ref int ptr, Shape shape) {
            SessionManager.Report("Extracting shape AV3D...", SessionManager.RType.DEBUG);
            int V17 = fr._readint(ref ptr, 4);
            int VertexDataLength = fr._readint(ref ptr, 4); //36?
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
                    } else { }
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
                        shape.rawVertexTextureCoords.Add(new Vector3(fr._readflt(ref ptr, 4), 1.0f - fr._readflt(ref ptr, 4), 0.0f));
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
            SessionManager.Report("Extracting shape primitives...", SessionManager.RType.DEBUG);
            int PrimitiveCount = fr._readint(ref ptr, 4); //the number of prims for this shape
            for (int i = 0;i < PrimitiveCount;i++) {
                Prim prim = new Prim {
                    type = fr._readint(ref ptr, 4), // the id of this primitive
                    materialID = fr._readint(ref ptr, 2), //the id of the material to use for this primitive
                    vertexCount = fr._readint(ref ptr, 2) //how many vertices make up this primitive
                };
                if (prim.type == 5 || prim.type == 6) {
                    prim.flags |= Shape.PrimFlags.LookAt;
                }
                for (int j = 0;j < prim.vertexCount;j++) {
                    prim.vertices.Add(fr._readint(ref ptr, 2)); //collect each Vector based on the prim stucture
                }
                shape.rawPrimitives.Add(prim);
            }
        }


        public void Extract_Animations(Character chr, ref int ptr, int Contract_Length) {
            SessionManager.Report("Extracting character animations...");
            Animation anim = new Animation();
            anim.foverride = fr._readbytes(ref ptr, Contract_Length);


            //int Contract_External_Length = ptr + Contract_Length;
            //int Contract_Internal_Length = ptr + 0;
            //int startofanimdata = ptr;

            //fr.SetDebugSlot(0);

            //Animation anim = new Animation {
            //    id = fr._readint(ref ptr, 4),
            //    FrameCount = fr._readint(ref ptr, 2),
            //    UNK1 = fr._readint(ref ptr, 2),
            //    UNK2 = fr._readint(ref ptr, 2),
            //    NodeCount = fr._readint(ref ptr, 2),
            //    UNK3 = fr._readint(ref ptr, 2),
            //    HPTR = fr._readint(ref ptr, 2)
            //};
            //int validIncrementor = 0;
            //for (int i = 0;i < anim.NodeCount;i++) {
            //    int NodeOffset = fr._readint(ref ptr, 2);
            //    anim.Nodes.Add(new Animation.Node {
            //        id = (NodeOffset < 0 || NodeOffset > 65532) ? NodeOffset : validIncrementor,
            //        offset = NodeOffset
            //    });
            //    if (NodeOffset > 0 && NodeOffset < 65532) { anim.firstGoodNode = anim.firstGoodNode ?? i-1; validIncrementor++; }
            //}
            //anim.firstGoodNode = anim.firstGoodNode ?? 0;

            //anim.UNK4 = fr._readint(ref ptr, 2);
            //anim.UNK5 = fr._readint(ref ptr, 2);
            //anim.UNK6 = fr._readint(ref ptr, 2);
            //anim.UNK7 = fr._readint(ref ptr, 2);
            ////the old method to read the anim data was terrible
            ////im not saying this one is god tier or anything, but its better
            ////essentially we just collect based on the node offsets
            ////was trying to do some fancy loop optimizations but its not worth it
            ////so what we do is this
            ////look at the offset provided by the node
            ////now look at the node count
            ////lets look at the second node, which has an offset of 5 (which means 5vertices*4bytes for each == 20 bytes total)
            ////so if we do the current frame (0) * (5*2) we get 20
            ////which if you look at the first node makes sense as the first node offset points to 0
            ////and 0 + 5*2 == 20, the start of the second node

            ////oh shit, i forgot to mention, we are in the context of the whole NGN here, so we need to offset the framepointer too
            ////this is done by doing frameoffset * (nodecount*20)

            ////tl;dr, do the framecount*(nodeoffset*4) to get the offset within the anim data
            ////as i said, not perfect, but it will do as you shall see

            ////to pull the extra data we do a neat little trick,
            ////use the index position of the current node, lets say 4
            ////multiply that by 5 to figure out our legitimate byte offset (4*5)=40
            ////the compare it to our actual offset (anim[4].offset = 52)
            ////this means we are over our XYZRR budget, so there are 12 more bytes of data to collect
            ////12 / 2? so we collect 6 more items of data

            //startofanimdata = ptr; //actual start of anim data
            //Contract_Internal_Length = ptr - Contract_Internal_Length;
            //int dataoffset = startofanimdata;
            //dataoffset = startofanimdata; //+ (i * (anim.NodeCount * 10));// * (anim.Nodes[j].offset * 4);
            //for (int i = 0;i < anim.FrameCount;i++) {
            //    int fsPTR = dataoffset;
            //    for (int j = 0;j < anim.Nodes.Count;j++) {
            //        if (anim.Nodes[j].id > -1 && anim.Nodes[j].id < 65533) { //lmao
            //            anim.Nodes[j].frames.Add(
            //                new AnimationFrame(
            //                    new Vector3(fr._readflt(ref dataoffset, 2), fr._readflt(ref dataoffset, 2), fr._readflt(ref dataoffset, 2)),
            //                    new Vector2(fr._readflt(ref dataoffset, 2), fr._readflt(ref dataoffset, 2)),
            //                    fr._readbytes(ref dataoffset, Math.Max((anim.Nodes[j].offset - (int)(((j - anim.firstGoodNode) * 5)))*2, 0))
            //                //fr._readbytes(ref dataoffset, (anim.Nodes.Count < j + 1) ? Math.Max(anim.Nodes[j].offset - ((j * 5) * 2), 0) : ((anim.Nodes[j + 1].offset - anim.Nodes[j + 1].offset) - 10))
            //                )
            //            );
            //            int dbl = 10 + Math.Max((anim.Nodes[j].offset - (int)(((j - anim.firstGoodNode) * 5))) * 2, 0);
            //            Contract_Internal_Length += dbl;
            //        }
            //    }
            //    Console.WriteLine("For frame " + i + " we collected " + (dataoffset - fsPTR) + " bytes of data");
            //}

            ////for (int i = 0;i < anim.Nodes.Count;i++) {//for each node
            ////    if (anim.Nodes[i].id > -1 && anim.Nodes[i].id < 65533) {
            ////        int nodeoffset = i * 10;
            ////        for (int j = 0;j < anim.FrameCount;j++) {//for each frame of animation
            ////            int internalFrameOffset = nodeoffset + ((j * validIncrementor) * 10);
            ////            int frameoffset = startofanimdata + internalFrameOffset; //this is where we are reading from for each frame
            ////            int frameoffsetstorage = frameoffset;
            ////            anim.Nodes[i].frames.Add(
            ////                new AnimationFrame(
            ////                    new Vector3(fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2)),
            ////                    new Vector2(fr._readflt(ref frameoffset, 2), fr._readflt(ref frameoffset, 2))
            ////                )
            ////            );
            ////            frameoffsetstorage = frameoffset - frameoffsetstorage;
            ////            Contract_Internal_Length += 10;
            ////        }
            ////    } else {
            ////        //SessionManager.Report("animation node was skipped", SessionManager.RType.WARNING);
            ////    }
            ////}
            //if (Contract_Internal_Length != Contract_Length || ptr != Contract_External_Length) {
            //    SessionManager.Report(" [ARSON CP]The FCL contract was not correctly fulfilled <" + Contract_Internal_Length + "/" + Contract_Length + ">[->F_NGN->ARSON->Extract_Animations]", SessionManager.RType.WARNING);
            //    if (Contract_Internal_Length < Contract_Length) {
            //        anim.extradata = fr._readbytes(ref Contract_Internal_Length, Contract_Length - Contract_Internal_Length);
            //    }
            //    ptr += (Contract_External_Length - ptr); //dont ruin the rest of the NGN collection
            //}

            //fr.dbgPTR = -1;
            //Console.WriteLine("Exporting debug bytes for animdata");
            //Console.WriteLine(XF.BytesToHex(fr.dstr[0].ToArray()));
            //fr.dstr[0].Clear();

            chr.Anims.Add(anim);
        }

        public void Extract_Shape_Patch(ref int ptr, Shape shape) {
            SessionManager.Report("Extracting shape patch...");
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
                    int areaPortalID = fr._readint(ref ptr, 4); //skip
                    this.areaPortals[areaPortalID].Rotation = new Vector2(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4));
                }
            }
        }

        public Geometry Extract_Geometry(int ptr) {
            int shapeCount = fr._readint(ref ptr, 4);
            Geometry geom = new Geometry {
                owner = this
            };
            for (int i = 0;i < shapeCount;i++) {
                Shape shape = new Shape {
                    type = 257
                };
                Extract_ShapeData(shape, ref ptr);
                geom.RegisterShape(shape);
            }
            return geom;
        }

        public DynamicScaler Extract_DynamicScaler(int ptr) {
            SessionManager.Report("Extracting dynamic scaler...");
            int shapeCount = fr._readint(ref ptr, 4);
            int shapeDataLength = fr._readint(ref ptr, 4);
            bool unknown = shapeDataLength == 40;
            DynamicScaler DS = new DynamicScaler();
            for (int i = 0;i < shapeCount;i++) {
                DS.Translation.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                DS.Rotation.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                ptr -= 12;
                DS.RotationMatrix.Add(new Vector3(fr._readint(ref ptr, 4), fr._readint(ref ptr, 4), fr._readint(ref ptr, 4)));
                DS.Scale.Add(new Vector3(fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4), fr._readflt(ref ptr, 4)));
                DS.ShapeID.Add(fr._readint(ref ptr, 4));
                if (!unknown) { DS.Unknown.Add(fr._readint(ref ptr, 4)); }
            }
            return DS;
        }

        public void Extract_ShapeLinks(int ptr) {
            int linkCount = fr._readint(ref ptr, 4);
            fr._seek(4, ref ptr);
            for (int i = 0;i < linkCount;i++) {
                this.ObjectLinks.Add(new Linker {
                    LinkID = fr._readint(ref ptr, 2),
                    ShapeID = fr._readint(ref ptr, 2)
                });
            }
        }


        public F_NGN ImportViaRaw(NGNSchema NSchema, string path) {
            return new F_NGN();
        }

        private NGNSchema ARSONAnalysis(string path, F_NGN File = null) {
            SessionManager.Report("Beginning ARSON Analysis...");
            File = (File == null) ? this : File;
            //so here we go through the file and see if 1. it's valid, and 2 how many threads we can create for each func
            //threads not used atm cant be arsed to sort it out again yet
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
                if (functionID == 0) {
                    if (functionLength != 0) {
                        SessionManager.Report("An invalid function was detected, and will be skipped", SessionManager.RType.WARN);
                    }
                } else {
                    NS.AddFunction((NGNFunction)functionID, fr.foffset, functionLength);
                }
                fr.seek(functionLength);
            }
            SessionManager.Report("Completed ARSON Analysis");
            return NS;
        }

        public F_Base Import(string path) {
            return ImportNGN(path);
        }

        public bool Export(string path) {
            FileWriter fw = new FileWriter();
            int EGC = 0; //this is a wide scope var that tracks the number of geometries written to the NGN
                         //this is useful for both geom and gscale, due to the way we track the function usage
            foreach (NGNSchema.NGNFunctionDef def in Schema.NGNFunctions) {
                fw.AddInt32((int)def.FunctionType); //add func id
                int initfunclength = fw.foffset;
                //lets just get into it, not in a good mood today
                // for every part of the ngn that is valid
                int funclengthptr = fw.Nop(4);
                switch (def.FunctionType) {
                    case NGNFunction.Textures:
                        fw.AddInt32(textures.Count);
                        for (int i = 0;i < textures.Count;i++) {
                            using (MemoryStream ms = new MemoryStream()) {
                                textures[i].image.Save(ms, ImageFormat.Bmp);
                                byte[] imgbytes = ms.ToArray();
                                fw.AddInt32(imgbytes.Length);
                                fw.AddInt32(textures[i].name.Length);
                                fw.AddString(textures[i].name);
                                fw.AddBytes(imgbytes);
                            }
                        }
                        break;
                    case NGNFunction.Creatures:
                        //its probably a good idea to break this out into separate functions...
                        fw.AddInt32(SlotValidity.Count);
                        //fill the character slots
                        if (characters.Count == -1) {
                            fw.Nop(62);
                        } else {
                            for (int i = 0;i < SlotValidity.Count;i++) {
                                if (SlotValidity[i] != -1) {
                                    string name = characters[SlotValidity[i]].name;
                                    fw.AddByte(name.Length + 1);
                                    fw.AddString(name, 1);
                                } else {
                                    fw.Nop(1);
                                }
                                //characterSlots[i].SlotID;
                            }
                        }

                        //write all character data,  remeber: local function ids are required in here
                        for (int i = 0;i < characters.Count;i++) {
                            Character chr = characters[i];
                            //NODES & CHARACTERS
                            fw.AddInt32(512); //characters Function ID
                            fw.AddInt32(4); //?
                            /*  BUG 019 - 09/02/2021
                                    We need to check if the last shape inside the character is of type Patch.
                                    If not, we do not do the subtraction. This needs looking into more.
                                    For now, we will check if the last shape is a patch as well as the initial count == 1 check
                                    In the future, it should be verified that both checks are required, as the patch check might do the ==1 check's job
                            */
                            fw.AddInt32((chr.shapes.Count == 1) ? 1 : chr.shapes.Count - chr.shapes[chr.shapes.Count - 1]._SPType);

                            //NODE STRUCT
                            fw.AddInt32(514);
                            int fNodeLengthPTR = fw.Nop(4);
                            for (int j = 0;j < chr.nodes.Count;j++) {
                                fw.AddByte(chr.nodes[j].Name.Length + 1);
                                fw.AddString(chr.nodes[j].Name, 1);
                            }
                            fw.eof(fNodeLengthPTR);

                            //NULL NODE BYTES (no func length forwarding)
                            fw.AddInt32(513);
                            fw.AddInt32(64 * chr.nodes.Count);
                            fw.Nop(64 * chr.nodes.Count);

                            //SHAPE DATA
                            fw.AddInt32(515);
                            int fShapeLength = fw.Nop(4);
                            for (int k = 0;k < chr.shapes.Count;k++) {
                                Shape shape = chr.shapes[k];
                                if (shape._SPType == 0) { //prim
                                    if (fShapeLength == 0) {
                                        fShapeLength = fw.Nop(4);
                                    }
                                } else {
                                    if (fShapeLength != 0) {
                                        fw.eof(fShapeLength);
                                        fShapeLength = 0;
                                    }
                                }
                                WriteShapeData(ref fw, ref shape);
                            }
                            if (fShapeLength != 0) {
                                fw.eof(fShapeLength);
                            }

                            //ANIMATION DATA
                            for (int l = 0;l < chr.Anims.Count;l++) {
                                Animation anim = chr.Anims[l];
                                fw.AddInt32(516);
                                int fAnimDataLength = fw.Nop(4);
                                WriteAnimationData(ref fw, ref anim);
                                fw.eof(fAnimDataLength);
                            }
                            fw.Nop(8);
                        }
                        break;
                    case NGNFunction.AreaPortalPosition:
                        fw.AddInt32(areaPortals.Count);
                        for (int i = 0;i < areaPortals.Count;i++) {
                            AreaPortal ap = areaPortals[i];
                            fw.AddInt32(ap.Vertices.Count);
                            for (int j = 0;j < ap.Vertices.Count;j++) {
                                fw.AddFloat(ap.Vertices[j].X);
                                fw.AddFloat(ap.Vertices[j].Y);
                                fw.AddFloat(ap.Vertices[j].Z);
                            }
                        }
                        break;
                    case NGNFunction.AreaPortalRotation:
                        fw.AddInt32(areaPortals.Count);
                        for (int i = 0;i < areaPortals.Count;i++) {
                            AreaPortal ap = areaPortals[i];
                            fw.AddInt32(i);
                            fw.AddFloat(ap.Rotation.X);
                            fw.AddFloat(ap.Rotation.Y);
                        }
                        break;
                    case NGNFunction.Geometry:
                        Geometry geo = Geometries[EGC];
                        fw.AddInt32(geo.shapes.Count);
                        for (int i = 0;i < geo.shapes.Count;i++) {
                            Shape shape = geo.shapes[i];
                            WriteShapeData(ref fw, ref shape);
                        }
                        EGC++;
                        break;
                    case NGNFunction.Gscale: //257
                        DynamicScaler ds = GScales[EGC - 1]; //we assume there cant be a gscale without a geometry object backer
                        fw.AddInt32(Geometries[EGC - 1].shapes.Count);
                        fw.AddInt32(44); //lmao - this is the length of the data

                        for (int i = 0;i < Geometries[EGC - 1].shapes.Count;i++) {
                            fw.AddFloat(ds.Translation[i].X);
                            fw.AddFloat(ds.Translation[i].Y);
                            fw.AddFloat(ds.Translation[i].Z);
                            fw.AddFloat(ds.Rotation[i].X);
                            fw.AddFloat(ds.Rotation[i].Y);
                            fw.AddFloat(ds.Rotation[i].Z);
                            fw.AddFloat(ds.Scale[i].X);
                            fw.AddFloat(ds.Scale[i].Y);
                            fw.AddFloat(ds.Scale[i].Z);
                            fw.AddInt32(i);
                            if (ds.Unknown.Count != 0 && ds.Unknown.Count >= i) { fw.AddInt32(ds.Unknown[i]); }
                        }
                        break;
                    case NGNFunction.Linker:
                        fw.AddInt32(ObjectLinks.Count);
                        fw.AddInt32(ObjectLinks[ObjectLinks.Count - 1].LinkID);
                        for (int i = 0;i < ObjectLinks.Count;i++) {
                            fw.AddInt16(ObjectLinks[i].LinkID);
                            fw.AddInt16(ObjectLinks[i].ShapeID);
                        }
                        break;
                }
                fw.eof(funclengthptr); //lptr implementation
            }
            fw.Nop(8);
            fw.Save(path);
            filePath = path;
            SessionManager.Report("Successfully saved NGN to: " + path + "[Main->F_NGN->Export]");
            return true;
        }

        public void WriteShapeData(ref FileWriter fw, ref Shape shape) {
            if (shape.type2 == 65535) {
                fw.AddInt16(shape.type);
                fw.AddInt16(shape.type2);
            }
            if (shape._SPType == 1 || (shape.type & 1) == 1) { //begin export
                //SHAPE NAME
                if (shape.name != "") {
                    fw.AddInt32(64);
                    string shapename = shape.name;
                    fw.AddInt32(shapename.Length + 2);
                    fw.AddByte(shapename.Length + 1);
                    fw.AddString(shapename, 1);
                }

                //SHAPE TEXTURES
                fw.AddInt32(shape._SPType == 1 ? 517 : 65); //tex header function id based on prim/patch
                int fShapeDataLength = fw.Nop(4);
                int texcount = shape.textures.Count;
                fw.AddInt16(texcount);
                fw.AddInt16(texcount);
                fw.AddByte(texcount > 0 ? (shape.type2 == 65535 ? 6 : 5) : 0); //if shape is char shape and not patch (!65535) -> texture name, the if is for ngn fileformat accuracy, but is probably not required
                fw.AddByte(26);
                for (int i = 0;i < texcount;i++) {
                    fw.AddByte(shape.textures[i].Length + (shape.type2 == 65535 ? 1 : 0));//assume we cleaned the string
                    fw.AddString(shape.textures[i], (shape.type2 == 65535 ? 1 : 0));
                }
                for (int i = 0;i < texcount;i++) {
                    fw.AddByte(i);
                    fw.Nop(25);
                }
                fw.eof(fShapeDataLength);

                //SHAPE MATERIALS
                if (shape.materials.Count > 0) {
                    fw.AddInt32(shape._SPType == 1 ? 518 : 66); //tex header function id based on prim/patch
                    int fMaterialDataLength = fw.Nop(4);
                    fw.AddInt32(shape.materials.Count);
                    for (int i = 0;i < shape.materials.Count;i++) {
                        Material mat = shape.materials[i];
                        fw.AddInt32(mat.id);
                        int fiMaterialDataLength = fw.Nop(4);
                        for (int j = 0;j < mat.RGB.Count;j++) {
                            fw.AddByte((int)(mat.RGB[j] / 0.0039215689));
                        }
                        if (mat.id == 193) { fw.AddInt32(mat.metadata); }
                        fw.AddInt16(mat.textureIndex);
                        fw.eof(fiMaterialDataLength);
                    }
                    fw.eof(fMaterialDataLength);
                }

                //VERTICES
                fw.AddInt32(shape._SPType == 1 ? 519 : 67); //func header
                /*  BUG 017 - 29/01/2021
                        dataLength is usually polled from the file but in this instance it is unknown
                        we could calculate it based on the contents of the vertices, but it is also unknown how the game will respond to a changing length
                        for now we assume 36
                */
                fw.AddInt32(36 /* shape.dataLength */ * shape.rawVertices.Count + 12);//pre calculated func length
                fw.AddInt32(23 /* shape.VertexLineLength */);
                fw.AddInt32(36 /* shape.dataLength */);
                fw.AddInt32(shape.rawVertices.Count);

                for (int i = 0;i < shape.rawVertices.Count;i++) {
                    fw.AddFloat(shape.rawVertices[i].X);
                    fw.AddFloat(shape.rawVertices[i].Y);
                    fw.AddFloat(shape.rawVertices[i].Z);
                    fw.AddFloat(shape.rawVertexData[i].X);
                    fw.AddFloat(shape.rawVertexData[i].Y);
                    fw.AddFloat(shape.rawVertexData[i].Z);
                    fw.AddByte(shape.rawVertexShading[i].A);
                    fw.AddByte(shape.rawVertexShading[i].R);
                    fw.AddByte(shape.rawVertexShading[i].G);
                    fw.AddByte(shape.rawVertexShading[i].B);
                    fw.AddFloat(shape.rawVertexTextureCoords[i].X);
                    fw.AddFloat(1.0f - shape.rawVertexTextureCoords[i].Y);
                }

                //PATCH HANDLER
                if (shape._SPType == 0) {
                    fw.AddInt32(68);
                    int fPrimDataLength = fw.Nop(4);
                    fw.AddInt32(shape.rawPrimitives.Count);
                    for (int i = 0;i < shape.rawPrimitives.Count;i++) {
                        Prim prim = (Prim)shape.rawPrimitives[i];
                        fw.AddInt32(prim.type);
                        fw.AddInt16(prim.materialID);
                        fw.AddInt16(prim.vertices.Count);
                        for (int j = 0;j < prim.vertices.Count;j++) {
                            fw.AddInt16(prim.vertices[j]);
                        }
                    }
                    fw.eof(fPrimDataLength);
                    fw.Nop(8);
                } else {
                    fw.AddInt32(520);
                    int fPatchDataLength = fw.Nop(4);
                    fw.AddInt32(shape.rawPrimitives.Count);
                    for (int i = 0;i < shape.rawPrimitives.Count;i++) {
                        Patch patch = (Patch)shape.rawPrimitives[i];
                        fw.AddInt16(patch.vertices.Count / 2);
                        fw.AddInt16(patch.materialID);
                        fw.AddInt32(patch.Unknown1);
                        fw.AddInt32(patch.Unknown2);
                        fw.AddInt32(patch.Unknown3);
                        fw.AddInt32(patch.Unknown4);
                    }
                    fw.eof(fPatchDataLength);
                }
            }
        }

        public void WriteAnimationData(ref FileWriter fw, ref Animation anim) {
            //we only use chunk data for now so we redirect flow via _.foverride
            fw.AddBytes(anim.foverride);

            //DO NOT REMOVE
            //fw.AddInt32(anim.id);
            //fw.AddInt16(anim.FrameCount);
            //fw.AddInt16(anim.UNK1);
            //fw.AddInt16(anim.UNK2);
            //fw.AddInt16(anim.NodeCount);
            //fw.AddInt16(anim.UNK3);
            //fw.AddInt16(anim.HPTR);
            //foreach (Animation.Node node in anim.Nodes) {
            //    fw.AddInt16(node.offset);
            //}
            //fw.AddInt16(anim.UNK4);
            //fw.AddInt16(anim.UNK5);
            //fw.AddInt16(anim.UNK6);
            //fw.AddInt16(anim.UNK7);
            //// cant do it the old way i tried and its gonna be mad
            ////for each node get the same frame 
            ////okay i explained it badly, e.g. node1 frame1, node2 frame1...node1 frame2, node2 frame2 etc...
            ////first get the highest frame count
            //int UpperFrame = 0;
            //foreach (Animation.Node node in anim.Nodes) {
            //    if (node.frames.Count > UpperFrame) { UpperFrame = node.frames.Count; }
            //}
            ////now, for i = 0 to UpperFrame, write data at frame pos in node
            //for (int i = 0;i < UpperFrame;i++) {
            //    for (int j = 0;j < anim.Nodes.Count;j++) {
            //        if (anim.Nodes[j].frames.Count - 1 != -1 && anim.Nodes[j].frames.Count >= i) {
            //            AnimationFrame frame = anim.Nodes[j].frames[i];
            //            fw.AddByte(124);
            //            fw.AddByte(i);
            //            fw.AddByte(j);
            //            fw.AddByte(124);
            //            fw.AddFloat(frame.Position.X, c: true);
            //            fw.AddFloat(frame.Position.Y, c: true);
            //            fw.AddFloat(frame.Position.Z, c: true);
            //            fw.AddFloat(frame.Rotation.X, c: true);
            //            fw.AddFloat(frame.Rotation.Y, c: true);
            //        }
            //    }
            //}
            ////oh also we need to append any extra data we forgot
            //if (anim.extradata != null) { fw.AddBytes(anim.extradata); }
            //fw.Nop(8);
        }


        /// <summary>
        /// Old Patch Fixer, impl ref do not use
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SplitIndexer"></param>
        /// <returns></returns>
        public static List<int> PatchFix(List<int> data, List<int> SplitIndexer) {
            List<int> lnp = new List<int>();
            List<int> unp = new List<int>();
            for (var j = 0;j < SplitIndexer.Count;j++) {
                int incinc = 0;
                sbyte h = 0;
                incinc += (j > 0) ? SplitIndexer[j - 1] : 0;
                unp.Clear();
                for (var i = 0;i < SplitIndexer[j];i++) {
                    if (h == 0) {
                        if (i % 2 == 0) { lnp.Add(i + ((j > 0) ? incinc : 0)); }
                    } else {
                        if (!((i % 2) == 0)) { unp.Add(i + ((j > 0) ? incinc : 0)); }
                    }
                    if (i == SplitIndexer[j] - 1 && h == 0) { i = 0; h = 1; }
                }
                lnp.AddRange(unp);
            }
            return lnp;
        }

        public void Extract(IWorldObject worldObject) {
            Exporter ex = new Exporter();
            if (ex.Prompt(worldObject) == DialogResult.OK) {
                ExtractModel(worldObject, ex.ex_path, Format3D.obj, ex.ex_name, ex.ex_tex, ex.ex_mat, true, ex.ex_alpha, ex.ex_vcol, ex.ex_rot);
            }
        }

        /// <summary>
        /// Global model extraction for NGN data
        /// </summary>
        /// <param name="objdata">The <seealso cref="Character"/> or <seealso cref="Geometry"/> data to extract</param>
        /// <param name="VertexColorOBJ">Whether to append the vertex data into the OBJ spec</param>
        /// <param name="CopyMaps">Whether to copy the used textures into the output directory</param>
        /// <param name="GenerateApha">Whether to generate and copy alpha maps for materials and store them in the output directory</param>
        /// <param name="SimVColor">Whether to average out the vertex color data and use that rather than global material color</param>
        /// <param name="GeomTrans">What axis to apply translation to</param>
        public void ExtractModel(IWorldObject objdata, string path, Format3D format, string OutputName, bool CopyMaps, bool CreateMats, bool VertexColorOBJ, bool GenerateApha, bool SimVColor, Vector3 GeomTrans) {
            SessionManager.Report("Begining object extraction");
            string name = objdata.name ?? "test";
            SessionManager.Report("Target: " + name);
            List<Shape> shapes = objdata.shapes;
            List<MtlM> materials = new List<MtlM>();
            List<bool> texselection = (List<bool>)XF.GenerateListData(1, textures.Count, false);
            List<bool> alphaselection = (List<bool>)XF.GenerateListData(1, textures.Count, false);
            SessionManager.Report("Export location: " + path);
            OBJWriter obj = new OBJWriter();
            StringBuilder mtlsb = new StringBuilder();
            obj.append("# OBJ Export - Toy Two Toolbox\n");
            obj.append("# File created " + DateTime.Now + "\n");
            obj.append("\nmtllib " + name + ".mtl\n");
            int i = 0;
            SessionManager.Report("Detected " + shapes.Count + " shapes", SessionManager.RType.DEBUG);
            int vOffset = 1;
            int vAccumulator = 0;
            foreach (Shape shape in shapes) {
                int j = 0;
                if (shape.rawPrimitives.Count > 0) {
                    SessionManager.Report("Shape " + j + " - Detected " + shape.rawPrimitives.Count + " primitives", SessionManager.RType.DEBUG);
                    List<Vector3> transformv3d = shape.rawVertices;

                    List<IPrimitive> modifiedPrimities = shape.rawPrimitives.Copy();
                    if (objdata.objectType == typeof(Geometry)) {
                        Matrix4D GTMF = new Matrix4D();
                        GeomTrans.SetMatrix4DNegative(GTMF, GeomTrans);
                        transformv3d = shape.rawVertices.Copy();
                        modifiedPrimities.Clear();
                        foreach (IPrimitive rawprim in shape.rawPrimitives) {
                            IPrimitive prim = rawprim.Copy();

                            if (prim.type == 5 || prim.type == 6) {//correct for issues with the poles and such, quads with 5/6 verts
                                                                   //shape lookat fix
                                List<Vector3> las = new List<Vector3>();
                                for (int h = prim.vertices.Count - 4;h < prim.vertices.Count;h++) {
                                    las.Add(transformv3d[prim.vertices[h]]);
                                }
                                Matrix4D GTMLA = new Matrix4D();
                                Matrix4D_Transform(GTMLA, shape.rawVertices[prim.vertices[0]]);
                                Vector3.TransformPoints(GTMLA, ref las);
                                prim.vertices.Swap(3, 4);
                                //prim.vertices.RemoveAt(0);
                                prim.vertexCount--;
                            }
                            if (prim.type == 5 || prim.type == 6) { prim.vertices.RemoveAt(0); prim.type = 4; }
                            modifiedPrimities.Add(prim);
                        }

                        if (j == 0) {
                            if (GScales != null) {
                                Matrix4D GTM = new Matrix4D();
                                Matrix4D_Scale(GTM, new Vector3(GScales[0].Scale[i].X - 0.0f, GScales[0].Scale[i].Y - 0.0f, GScales[0].Scale[i].Z - 0.0f));
                                Matrix4D_Translate3D(GTM, GScales[0].RotationMatrix[i]);
                                Matrix4D_Transform(GTM, GScales[0].Translation[i]);
                                Vector3.TransformPoints(GTM, ref transformv3d);
                                Vector3.TransformPoints(GTMF, ref transformv3d);
                            }
                        }

                    } else {//chr
                        transformv3d = shape.rawVertices.Copy();
                        Matrix4D GTMF = new Matrix4D();
                        GeomTrans.SetMatrix4DNegative(GTMF, GeomTrans);
                        Vector3.TransformPoints(GTMF, ref transformv3d);
                    }

                    obj.append("o " + name + ((shape.name == "") ? "shape" + i.ToString().PadLeft(2, '0') : shape.name) + "\n");


                    for (int k = 0;k < shape.rawVertices.Count;k++) {
                        obj.append("v  " + transformv3d[k].ToOBJ() + ((VertexColorOBJ) ? " " + ColorToVColor(shape.rawVertexShading[k]) : "") + "\n");
                    }
                    obj.append("# " + shape.rawVertices.Count + " vertices\n\n");

                    for (int k = 0;k < shape.rawVertexTextureCoords.Count;k++) {
                        obj.append("vt  " + shape.rawVertexTextureCoords[k].ToOBJ() + "\n");
                    }

                    obj.append("# " + shape.rawVertexTextureCoords.Count + " texture coords\n\n");

                    foreach (IPrimitive prim in modifiedPrimities) {//shape.rawPrimitives) {IPrimitive prim = rawprim.Copy();
                        if (shape._SPType == 1) {
                            List<int> pvtxf = new List<int>();
                            prim.type = 4;
                            for (int k = 0;k < prim.vertices.Count;k += 4) {
                                pvtxf.Add(prim.vertices[k + 1]);
                                pvtxf.Add(prim.vertices[k]);
                                pvtxf.Add(prim.vertices[k + 2]);
                                pvtxf.Add(prim.vertices[k + 3]);
                            }
                            prim.vertices = pvtxf;
                        }

                        string objname = $"{name}_{((shape.name == "") ? "shape" + i.ToString().PadLeft(2, '0') : shape.name)}_face{j.ToString().PadLeft(2, '0')}";
                        //obj.title("object " + objname);
                        OBJP oPrim = new OBJP();


                        int primtype = (prim.type == 1) ? 3 : prim.type;

                        obj.append("g " + objname + "\n");
                        string mn = "s" + i + "p" + j;
                        if (CreateMats) {
                            string mt = "";
                            Material selmat = shape.materials[prim.materialID];
                            if (selmat.textureIndex != 65535) {
                                mt = textures[selmat.textureIndexRelative].name;
                                texselection[selmat.textureIndexRelative] = true;
                            }
                            if (selmat.metadata == 2 || selmat.metadata == 11) {
                                alphaselection[selmat.textureIndexRelative] = true;
                            }
                            bool mtla = true;
                            foreach (MtlM mat in materials) {
                                if (mat.name == mn && mat.texcname == mt + ".bmp" && mat.texaname == ((GenerateApha) ? mt + "_a" : "")) {
                                    mtla = false;
                                }
                            }
                            if (mtla) {
                                SessionManager.Report("Adding material...", SessionManager.RType.DEBUG);
                                materials.Add(new MtlM {
                                    name = mn,
                                    mrgb = (SimVColor) ? AverageVC(shape.rawVertexShading, prim.vertices) : shape.materials[prim.materialID].RGB,
                                    opacity = AverageAlpha(shape.rawVertexShading, prim.vertices), //this needs properly implementing, along with the import
                                    texcname = mt + ".bmp",
                                    texaname = (GenerateApha) ? mt + "_a" : ""
                                });
                            }
                            obj.append("usemtl " + mn);
                        }
                        for (int k = 0;k < prim.vertices.Count;k += primtype) {
                            obj.append("\nf ");
                            for (int l = 0;l < primtype;l++) {
                                obj.face(prim.vertices[k + l] + vOffset);
                            }
                        }
                        if ((prim.flags & Shape.PrimFlags.LookAt) == Shape.PrimFlags.LookAt) {
                            //vAccumulator += shape.rawPrimitives[j].type * (shape.rawPrimitives[j].vertices.Count / shape.rawPrimitives[j].type);
                            vAccumulator += (primtype * (prim.vertices.Count / primtype)) + (shape.rawPrimitives[j].vertices.Count - prim.vertices.Count);
                        } else {
                            vAccumulator += primtype * (prim.vertices.Count / primtype);
                        }
                        obj.append("\n# " + prim.vertices.Count / primtype + " polygons\n\n\n");
                        j++;
                    }
                    vOffset += vAccumulator;
                    vAccumulator = 0;
                }
                i++;
            }
            if (CreateMats) {
                SessionManager.Report("Compiling Material Component...", SessionManager.RType.DEBUG);
                foreach (MtlM mtl in materials) {
                    mtlsb.Append("newmtl " + mtl.name + "\nillum2\n");
                    mtlsb.Append("d " + mtl.opacity + " \n");
                    mtlsb.Append("Kd " + mtl.mrgb[0] + " " + mtl.mrgb[1] + " " + mtl.mrgb[2] + " \n");
                    mtlsb.Append("Ka " + 0 + " " + 0 + " " + 0 + " \n");
                    if (mtl.texcname != ".bmp") { mtlsb.Append("map_Kd " + mtl.texcname + "\n"); }
                    if (mtl.texaname != ".bmp") { mtlsb.Append("map_Ka " + mtl.texaname + "\n"); }
                }
                for (int j = 0;j < texselection.Count;j++) {
                    if (texselection[j] == true) {
                        XF.ExportImage(path + "\\" + textures[j].name + ".bmp", textures[j].image, ImageFormat.Bmp);
                        if (alphaselection[j] == true) {
                            Bitmap CLIPIMG = (Bitmap)textures[j].image.Clone();
                            XF.GenerateAlphaMap(CLIPIMG);
                            XF.ExportImage(path + "\\" + textures[j].name + "_a.bmp", CLIPIMG, ImageFormat.Bmp);
                            CLIPIMG.Dispose();
                        }
                    }
                }
                File.WriteAllText(path + "\\" + OutputName + ".mtl", mtlsb.ToString());
            }
            File.WriteAllText(path + "\\" + OutputName + ".obj", obj.fstream.ToString());
            SessionManager.Report("Successfully written OBJ and MTL files!", SessionManager.RType.TEXT, Color.Green);
        }

        public List<double> AverageVC(List<Color> vc, List<int> selection = null) {
            int r = 0;
            int g = 0;
            int b = 0;
            int vcs = vc.Count;
            if (selection != null) {
                vcs = selection.Count;
                foreach (int vtx in selection) {
                    r += vc[vtx].R;
                    g += vc[vtx].G;
                    b += vc[vtx].B;
                }
            } else {
                foreach (Color col in vc) {
                    r += col.R;
                    g += col.G;
                    b += col.B;
                }
            }
            r /= vcs;
            g /= vcs;
            b /= vcs;
            List<double> vca = XF.ColorToNGNColor(Color.FromArgb(r, g, b));
            vca.RemoveAt(0);
            return vca;
        }

        public decimal AverageAlpha(List<Color> cols, List<int> selection = null) {
            decimal a = 0;
            if (selection != null) {
                foreach (int vtx in selection) {
                    a += cols[vtx].A;
                }
            } else {
                foreach (Color col in cols) {
                    a += col.A;
                }
            }
            return (a / (selection != null ? selection.Count : cols.Count)) / 255;
        }

        public void ProcessMaterials(List<MtlM> mats) {

        }

        public static string ColorToVColor(Color color) {
            return (double)color.R / 255 + " " + (double)color.G / 255 + " " + (double)color.B / 255;
        }

        public static Color VColorToColor(float[] color) {
            return Color.FromArgb(Convert.ToInt32(color[0] * 255), Convert.ToInt32(color[1] * 255), Convert.ToInt32(color[2] * 255));
        }

        public void GTransformation(Matrix4D gtm, float sine, float cosine) {

        }

        public static Matrix4D Matrix4D_RotateZ(Matrix4D GTM, float Sine, float Cosine) {
            double GTM0 = GTM._matrix[0, 0];
            double GTM4 = GTM._matrix[1, 0];
            double GTM8 = GTM._matrix[2, 0];
            double GTM12 = GTM._matrix[3, 0];
            GTM._matrix[0, 0] = GTM0 * Sine - Cosine * GTM._matrix[0, 1]; // [00]
            GTM._matrix[0, 1] = GTM0 * Cosine + Sine * GTM._matrix[0, 1]; // [01]
            GTM._matrix[1, 0] = GTM4 * Sine - Cosine * GTM._matrix[1, 1]; // [04]
            GTM._matrix[1, 1] = Sine * GTM._matrix[1, 1] + GTM4 * Cosine; // [05]
            GTM._matrix[2, 0] = GTM8 * Sine - Cosine * GTM._matrix[2, 1]; // [08]
            GTM._matrix[2, 1] = Sine * GTM._matrix[2, 1] + GTM8 * Cosine; // [09]
            GTM._matrix[3, 0] = GTM12 * Sine - Cosine * GTM._matrix[3, 1]; // [12]
            GTM._matrix[3, 1] = Sine * GTM._matrix[3, 1] + GTM12 * Cosine; // [13]
            return GTM;
        }

        public static Matrix4D Matrix4D_RotateY(Matrix4D GTM, float Sine, float Cosine) {
            double GTM0 = GTM._matrix[0, 0];
            double GTM4 = GTM._matrix[1, 0];
            double GTM8 = GTM._matrix[2, 0];
            double GTM12 = GTM._matrix[3, 0];
            GTM._matrix[0, 0] = Cosine * GTM._matrix[0, 2] + GTM0 * Sine; // [00]
            GTM._matrix[0, 2] = Sine * GTM._matrix[0, 2] - GTM0 * Cosine; // [02]
            GTM._matrix[1, 0] = Cosine * GTM._matrix[1, 2] + GTM4 * Sine; // [04]
            GTM._matrix[1, 2] = Sine * GTM._matrix[1, 2] - GTM4 * Cosine; // [06]
            GTM._matrix[2, 0] = GTM8 * Sine + Cosine * GTM._matrix[2, 2]; // [08]
            GTM._matrix[2, 2] = Sine * GTM._matrix[2, 2] - GTM8 * Cosine; // [10]
            GTM._matrix[3, 0] = Cosine * GTM._matrix[3, 2] + GTM12 * Sine; // [12]
            GTM._matrix[3, 2] = Sine * GTM._matrix[3, 2] - GTM12 * Cosine; // [14]
            return GTM;
        }


        public static Matrix4D Matrix4D_RotateX(Matrix4D GTM, float Sine, float Cosine) {
            double GTM1 = GTM._matrix[0, 1];
            double GTM5 = GTM._matrix[1, 1];
            double GTM9 = GTM._matrix[2, 1];
            double GTM13 = GTM._matrix[3, 1];
            GTM._matrix[0, 1] = GTM1 * Sine - Cosine * GTM._matrix[0, 2]; // [01]
            GTM._matrix[0, 2] = Sine * GTM._matrix[0, 2] + GTM1 * Cosine; // [02]
            GTM._matrix[1, 1] = GTM5 * Sine - Cosine * GTM._matrix[1, 2]; // [05]
            GTM._matrix[1, 2] = Sine * GTM._matrix[1, 2] + GTM5 * Cosine; // [06]
            GTM._matrix[2, 1] = GTM9 * Sine - Cosine * GTM._matrix[2, 2]; // [09]
            GTM._matrix[2, 2] = Sine * GTM._matrix[2, 2] + GTM9 * Cosine; // [10]
            GTM._matrix[3, 1] = GTM13 * Sine - Cosine * GTM._matrix[3, 2]; // [13]
            GTM._matrix[3, 2] = GTM13 * Cosine + Sine * GTM._matrix[3, 2]; // [14]
            return GTM;
        }

        public static object Matrix4D_Scale(Matrix4D GTM, Vector3 ScaleVertex) {
            GTM._matrix[0, 0] = GTM._matrix[0, 0] * ScaleVertex.X;
            GTM._matrix[0, 1] = GTM._matrix[0, 1] * ScaleVertex.Y;
            GTM._matrix[0, 2] = GTM._matrix[0, 2] * ScaleVertex.Z;

            GTM._matrix[1, 0] = GTM._matrix[1, 0] * ScaleVertex.X;
            GTM._matrix[1, 1] = GTM._matrix[1, 1] * ScaleVertex.Y;
            GTM._matrix[1, 2] = GTM._matrix[1, 2] * ScaleVertex.Z;

            GTM._matrix[2, 0] = GTM._matrix[2, 0] * ScaleVertex.X;
            GTM._matrix[2, 1] = GTM._matrix[2, 1] * ScaleVertex.Y;
            GTM._matrix[2, 2] = GTM._matrix[2, 2] * ScaleVertex.Z;

            GTM._matrix[3, 0] = GTM._matrix[3, 0] * ScaleVertex.X;
            GTM._matrix[3, 1] = GTM._matrix[3, 1] * ScaleVertex.Y;
            GTM._matrix[3, 2] = GTM._matrix[3, 2] * ScaleVertex.Z;
            return true;
        }

        public static object Matrix4D_Transform(Matrix4D GTM, Vector3 TranslationVertex) {
            GTM._matrix[0, 3] = GTM._matrix[0, 3] + TranslationVertex.X; //GTM._matrix[3, 0] = GTM._matrix[3, 0] + TranslationVertex.X
            GTM._matrix[1, 3] = GTM._matrix[1, 3] + TranslationVertex.Y; //GTM._matrix[3, 1] = GTM._matrix[3, 1] + TranslationVertex.Y
            GTM._matrix[2, 3] = GTM._matrix[2, 3] + TranslationVertex.Z; //GTM._matrix[3, 2] = GTM._matrix[3, 2] + TranslationVertex.Z
            return true;
        }

        public void Matrix4D_Translate3D(Matrix4D GTM, Vector3 RotationMatrix) {
            //Vector3 sine = new Vector3(SessionManager.GenerateV3DSine(RotationMatrix,false));
            //Vector3 cosine = new Vector3(SessionManager.GenerateV3DSine(RotationMatrix,true));
            float sz = SessionManager.GenerateSine((int)(RotationMatrix.Z) + 16384 & 0xFFFF);
            float cz = SessionManager.GenerateSine((int)(RotationMatrix.Z) & 0xFFFF);
            double GTM0 = GTM._matrix[0, 0];
            double GTM4 = GTM._matrix[1, 0];
            double GTM8 = GTM._matrix[2, 0];
            double GTM12 = GTM._matrix[3, 0];
            GTM._matrix[0, 0] = GTM0 * sz - cz * GTM._matrix[0, 1]; // [00]
            GTM._matrix[0, 1] = GTM0 * cz + sz * GTM._matrix[0, 1]; // [01]
            GTM._matrix[1, 0] = GTM4 * sz - cz * GTM._matrix[1, 1]; // [04]
            GTM._matrix[1, 1] = sz * GTM._matrix[1, 1] + GTM4 * cz; // [05]
            GTM._matrix[2, 0] = GTM8 * sz - cz * GTM._matrix[2, 1]; // [08]
            GTM._matrix[2, 1] = sz * GTM._matrix[2, 1] + GTM8 * cz; // [09]
            GTM._matrix[3, 0] = GTM12 * sz - cz * GTM._matrix[3, 1]; // [12]
            GTM._matrix[3, 1] = sz * GTM._matrix[3, 1] + GTM12 * cz; // [13]


            float sy = SessionManager.GenerateSine((int)(RotationMatrix.Y) + 16384 & 0xFFFF);
            float cy = SessionManager.GenerateSine((int)(RotationMatrix.Y) & 0xFFFF);
            GTM0 = GTM._matrix[0, 0];
            GTM4 = GTM._matrix[1, 0];
            GTM8 = GTM._matrix[2, 0];
            GTM12 = GTM._matrix[3, 0];
            GTM._matrix[0, 0] = cy * GTM._matrix[0, 2] + GTM0 * sy; // [00]
            GTM._matrix[0, 2] = sy * GTM._matrix[0, 2] - GTM0 * cy; // [02]
            GTM._matrix[1, 0] = cy * GTM._matrix[1, 2] + GTM4 * sy; // [04]
            GTM._matrix[1, 2] = sy * GTM._matrix[1, 2] - GTM4 * cy; // [06]
            GTM._matrix[2, 0] = GTM8 * sy + cy * GTM._matrix[2, 2]; // [08]
            GTM._matrix[2, 2] = sy * GTM._matrix[2, 2] - GTM8 * cy; // [10]
            GTM._matrix[3, 0] = cy * GTM._matrix[3, 2] + GTM12 * sy; // [12]
            GTM._matrix[3, 2] = sy * GTM._matrix[3, 2] - GTM12 * cy; // [14]

            float sx = SessionManager.GenerateSine((int)(RotationMatrix.X) + 16384 & 0xFFFF);
            float cx = SessionManager.GenerateSine((int)(RotationMatrix.X) & 0xFFFF);
            double GTM1 = GTM._matrix[0, 1];
            double GTM5 = GTM._matrix[1, 1];
            double GTM9 = GTM._matrix[2, 1];
            double GTM13 = GTM._matrix[3, 1];
            GTM._matrix[0, 1] = GTM1 * sx - cx * GTM._matrix[0, 2]; // [01]
            GTM._matrix[0, 2] = sx * GTM._matrix[0, 2] + GTM1 * cx; // [02]
            GTM._matrix[1, 1] = GTM5 * sx - cx * GTM._matrix[1, 2]; // [05]
            GTM._matrix[1, 2] = sx * GTM._matrix[1, 2] + GTM5 * cx; // [06]
            GTM._matrix[2, 1] = GTM9 * sx - cx * GTM._matrix[2, 2]; // [09]
            GTM._matrix[2, 2] = sx * GTM._matrix[2, 2] + GTM9 * cx; // [10]
            GTM._matrix[3, 1] = GTM13 * sx - cx * GTM._matrix[3, 2]; // [13]
            GTM._matrix[3, 2] = GTM13 * cx + sx * GTM._matrix[3, 2]; // [14]
        }

        [Obsolete("Not required as of yet", false)]
        public static object Matrix4D_Compute(Vector3 Out, Matrix4D Unknown, Matrix4D GTM) {
            return new NotImplementedException();
        }

        public enum Format3D {
            obj = 0
        }

        public int TexNameToGlobalID(string texname) {
            for (int i = 0;i < textures.Count;i++) {
                if (textures[i].name == texname) {
                    return i;
                }
            }
            return -1;
        }


        /// <summary>Retrieves a <seealso cref="List{"/> of <seealso cref="Material"/> from a <seealso cref="List{"/> of <seealso cref="F_NGN"/></summary>
        /// <param name="levels">The levels to pull materials from</param>
        /// <param name="filter">0 = Every Material<para/>1 = Character Materials only<para/>2 = Level Materials only</param>
        /// <returns></returns>
        public static List<Material> GetMaterialsFromLevels(List<F_NGN> levels, int filter = 0) {
            List<Material> mats = new List<Material>();

            foreach (F_NGN lev in levels) {
                if (filter == -1 || filter == 0) {//Everything or just Chars
                    foreach (Character chr in lev.characters) {
                        mats.AddRange(chr.GetMaterials());
                    }
                }
                if (filter == -1 || filter == 1) {//Everything or just Chars
                    foreach (Geometry geom in lev.Geometries) {
                        mats.AddRange(geom.GetMaterials());
                    }
                }
            }
            return mats;
        }

        /// <summary>Describes the layout of an associated NGN File</summary>
        public class NGNSchema {
            public F_NGN LinkedNGN;
            public List<NGNFunctionDef> NGNFunctions;

            public NGNSchema(F_NGN File) {
                NGNFunctions = new List<NGNFunctionDef>();
                LinkedNGN = File;
            }

            public void AddFunction(NGNFunction FunctionID, int NFPTR, int NFL) {
                NGNFunctions.Add(new NGNFunctionDef(FunctionID, NFPTR, NFL));
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
