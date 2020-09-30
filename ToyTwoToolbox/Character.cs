using System.Collections.Generic;

namespace ToyTwoToolbox {
    ///<summary>An individual character, such as Buzz, represented inside the NGN.</summary>
    public class Character {
        ///<summary>The actual name of the character</summary>
        public string name;
        ///<summary>The number of nodes/shapes</summary>
        public int nodeCount;
        ///<summary>Nodes in the character represented by the <seealso cref="SlotConfig"/> format</summary>
        public List<SlotConfig> nodes;
        ///<summary>The character model, containing the <seealso cref="Shape"/> data</summary>
        public Model model;
        ///<summary>The animations this character is able to perform</summary>
        public List<Animation> Anims;

        public Character() {
            name = "";
            nodes = new List<SlotConfig>();
            nodeCount = 0;
            model = new Model();
            Anims = new List<Animation>();
        }
    }
}
