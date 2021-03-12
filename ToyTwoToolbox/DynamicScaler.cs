using System.Collections.Generic;

namespace ToyTwoToolbox {
    //Note, it would probably be better to utilise custom objects for this, but eh idk
    /// <summary>
    /// Stores the data for the dynamic scaler, properties that affect shapes generally, rather than their internal properties.
    /// </summary>
    public class DynamicScaler {
        public List<int> ShapeID;
        public List<Vector3> Translation;
        public List<Vector3> Rotation;
        public List<Vector3> RotationMatrix;
        public List<Vector3> Scale;
        public List<int> Unknown;

        public DynamicScaler() {
            ShapeID = new List<int>();
            Translation = new List<Vector3>();
            Rotation = new List<Vector3>();
            RotationMatrix = new List<Vector3>();
            Scale = new List<Vector3>();
            Unknown = new List<int>();
        }

        public void Append(int shapeid) {
            Append(shapeid, new Vector3(), new Vector3(), new Vector3(), new Vector3(), 0);
        }

        public void Append(int shapeid, Vector3 translation, Vector3 rotation, Vector3 rotationmatrix, Vector3 scale, int unknown) {
            ShapeID.Add(shapeid);
            Translation.Add(translation);
            Rotation.Add(rotation);
            RotationMatrix.Add(rotationmatrix);
            Scale.Add(scale);
            Unknown.Add(unknown);
        }

        public void Remove(int index) {
            ShapeID.RemoveAt(index);
            Translation.RemoveAt(index);
            Rotation.RemoveAt(index);
            RotationMatrix.RemoveAt(index);
            Scale.RemoveAt(index);
            Unknown.RemoveAt(index);
        }
    }
}