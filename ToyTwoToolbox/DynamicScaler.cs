﻿using System.Collections.Generic;

namespace ToyTwoToolbox {
    public class DynamicScaler {
        public List<int> ShapeID;
        public List<Vector3> Translation;
        public List<Vector3> Rotation;
        public List<Vector3> Scale;
        public List<int> Unknown;

        public DynamicScaler() {
            ShapeID = new List<int>();
            Translation = new List<Vector3>();
            Rotation = new List<Vector3>();
            Scale = new List<Vector3>();
            Unknown = new List<int>();
        }
    }
}