
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Linq;
using ToyTwoToolbox.ObjLoader.Loader.Common;
using ToyTwoToolbox.ObjLoader.Loader.Data.Elements;
using ToyTwoToolbox.ObjLoader.Loader.Data.VertexData;

namespace ToyTwoToolbox {
	namespace ObjLoader.Loader.Data.DataStore {
		public class DataStore : IDataStore, IGroupDataStore, IVertexDataStore, IVertexColorDataStore, ITextureDataStore, INormalDataStore, IFaceGroup, IMaterialLibrary, IElementGroup {
			private Group _currentGroup;

			private readonly List<Group> _groups = new List<Group>();
			private readonly List<Material> _materials = new List<Material>();

			private readonly List<Vertex> _vertices = new List<Vertex>();
			private readonly List<VertexColor> _vertexcolor = new List<VertexColor>();
			private readonly List<TextureCoord> _textures = new List<TextureCoord>();
			private readonly List<Normal> _normals = new List<Normal>();

			public IList<Vertex> Vertices {
				get {
					return _vertices;
				}
			}

			public IList<VertexColor> VertexColor {
				get {
					return _vertexcolor;
				}
			}

			public IList<TextureCoord> Textures {
				get {
					return _textures;
				}
			}

			public IList<Normal> Normals {
				get {
					return _normals;
				}
			}

			public IList<Material> Materials {
				get {
					return _materials;
				}
			}

			public IList<Group> Groups {
				get {
					return _groups;
				}
			}


			public void AddFace(Face face) {
				PushGroupIfNeeded();

				_currentGroup.AddFace(face);
			}

			public void PushGroup(string groupName) {
				_currentGroup = new Group(groupName);
				_groups.Add(_currentGroup);
			}

			private void PushGroupIfNeeded() {
				if (_currentGroup == null) {
					PushGroup("default");
				}
			}

			public void AddVertex(Vertex vertex) {
				_vertices.Add(vertex);
			}

			void IVertexColorDataStore.AddColor(VertexColor Color) {
				this.AddVertexColor(Color);
			}
			public void AddVertexColor(VertexColor Color) {
				_vertexcolor.Add(Color);
			}

			public void AddTexture(TextureCoord texture) {
				_textures.Add(texture);
			}

			public void AddNormal(Normal normal) {
				_normals.Add(normal);
			}

			public void Push(Material material) {
				_materials.Add(material);
			}

			public void SetMaterial(string materialName) {
				var material = _materials.SingleOrDefault((x) => x.Name.EqualsOrdinalIgnoreCase(materialName));
				PushGroupIfNeeded();
				_currentGroup.Material = material;
			}
		}
	}
}