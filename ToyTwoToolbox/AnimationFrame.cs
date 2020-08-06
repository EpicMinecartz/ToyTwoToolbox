using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class AnimationFrame {
        public Vector3 Position;
        public Vector2 Rotation;
        public Vector3 Unknown;

        public AnimationFrame(Vector3 position, Vector2 rotation) {
            Position = position;
            Rotation = rotation;
        }
    }
}
