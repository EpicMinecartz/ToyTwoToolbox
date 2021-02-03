using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
    ///<summary>An individual character, such as Buzz, represented inside the NGN.</summary>
    public class Character : IWorldObject {
        /// <summary>The type of IWorldObject</summary>
        public Type objectType { get; } = typeof(Character);
        /// <summary>The NGN that has this character registered</summary>
        public F_NGN owner { get; set; }
        ///<summary>The actual name of the character</summary>
        public string name { get; set; }
        ///<summary>The character model, containing the <seealso cref="Shape"/> data</summary>
        public List<Shape> shapes { get; set; }
        ///<summary>The number of nodes/shapes</summary>
        public int nodeCount;
        ///<summary>Nodes in the character represented by the <seealso cref="SlotConfig"/> format</summary>
        public List<SlotConfig> nodes;
        ///<summary>The animations this character is able to perform</summary>
        public List<Animation> Anims;

        public Character() {
            owner = null;
            name = "";
            nodes = new List<SlotConfig>();
            nodeCount = 0;
            shapes = new List<Shape>();
            Anims = new List<Animation>();
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
