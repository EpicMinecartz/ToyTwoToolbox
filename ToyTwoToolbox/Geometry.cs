using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
    public class Geometry : IWorldObject {
        /// <summary>The type of IWorldObject</summary>
        public Type objectType { get; } = typeof(Geometry);
        /// <summary>The NGN that has this geometry registered</summary>
        public F_NGN owner { get; set; }
        ///<summary>The actual name of the geometry</summary>
        public string name { get; set; }
        ///<summary>The geometry data, containing the <seealso cref="Shape"/> data</summary>
        public List<Shape> shapes { get; set; }
        ///<summary>The realtime transformation data</summary>
        public DynamicScaler dynamicScaler;

        public Geometry() {
            owner = null;
            name = "";
            shapes = new List<Shape>();
            dynamicScaler = new DynamicScaler();
        }

        public List<Material> GetMaterials() {
            List<Material> mats = new List<Material>();
            foreach (Shape shape in shapes) {
                mats.AddRange(shape.materials.ToArray());
            }
            return mats;
        }

        public void RegisterShape(Shape shape) {
            shape.owner = this;
            shapes.Add(shape);
        }
    }
}
