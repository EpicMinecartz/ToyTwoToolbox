using System.Collections.Generic;

namespace ToyTwoToolbox {
    public class Material {
        /// <summary>The type of <seealso cref="Material"/></summary>
        public int id; //193 or 129 - go figure why those
        /// <summary>The <seealso cref="Shape"/> that this <seealso cref="Material"/> is registered within</summary>
        public Shape owner = null;
        /// <summary>Global <seealso cref="Material"/> color</summary>
        public List<double> RGB; //ARGB actually
        /// <summary>Refers to the <seealso cref="Shape.textures"/> cache for this specific shape</summary>
        public int textureIndex;
        /// <summary>Refers to the list of <seealso cref="F_NGN.textures"/> within the NGN</summary>
        public int textureIndexRelative;
        /// <summary>An integer that specifies how this <seealso cref="Material"/> should be rendered in-game</summary>
        public int metadata;
        /// <summary>A value to temporarily store the name of the texture that this material relates to</summary>
        public string textureName;

        public Material() {
            id = 0;
            RGB = new List<double>();
            textureIndex = 0;
            textureIndexRelative = 0;
            metadata = 0;
        }

        //NOTE: I have found a way to avoid this for now, when we collect the shape texture names,
        //      I fill the associated texturesGlobal list with the global IDs
        //      now we can do <Shape>.texturesGlobal[<MaterialTextureIndex>]
        /// <summary>Finds the relative global tex index</summary>
        /// <param name="textureindex"></param>
        public void GetRelativeTexIndex(int textureindex) {
            List<Texture> globaltex = owner.owner.owner.textures;
            for (int i = 0;i < globaltex.Count;i++) {
                if (globaltex[i].name == owner.textures[textureindex]) {
                    textureIndexRelative = i;
                }
            }
        }

    }
}
