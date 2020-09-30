using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class AnimationFrame {
        //this needs testing again
        public Vector3 Position;
        public Vector2 Rotation;
        public Byte[] Unknown;

        public AnimationFrame(Vector3 position, Vector2 rotation, Byte[] unknown = null) {
            Position = position;
            Rotation = rotation;
            Unknown = unknown;
        }
    }
}
