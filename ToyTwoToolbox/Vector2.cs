using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
    public class Vector2 {
        // data members - X, Y and Z values
        public float X;
        public float Y;

        /// <summary>Vector2 allows you to store the individual X, Y components that make a 2D point</summary>
        public Vector2() {
            X = 0.0f;
            Y = 0.0f;
        }

        // parametrised constructor
        public Vector2(float xx, float yy) {
            X = xx;
            Y = yy;
        }

        // copy constructor
        public Vector2(Vector2 Vec) {
            X = Vec.X;
            Y = Vec.Y;
        }

        // dot product of this vector and the parameter vector
        public float DotProduct(Vector2 Vec) {
            return (X * Vec.X) + (Y * Vec.Y);
        }

        // length of this vector
        public float Length() {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        // find the angle between this vector and the parameter vector
        public float AngleTo(Vector2 Vec) {
            float AdotB = DotProduct(Vec);
            float ALstarBL = Length() * Vec.Length();
            if (ALstarBL == 0) {
                return 0.0f;
            }
            return (float)Math.Acos(AdotB / ALstarBL);
        }

        // checks whether this vector is equal to the parameter vector
        public bool IsEqualTo(Vector2 Vec) {
            if (X == Vec.X && Y == Vec.Y) {
                return true;
            }
            return false;
        }

        // checks whether this vector is perpendicular to the parameter vector
        public bool IsPerpendicularTo(Vector2 Vec) {
            float Ang = AngleTo(Vec);
            if (Ang == (90 * Math.PI / 180.0)) {
                return true;
            }
            return false;
        }

        // checks whether this vector is X axis
        public object IsXAxis() {
            if (X != 0.0 && Y == 0.0) {
                return true;
            }
            return false;
        }

        // checks whether this vector is Y axis
        public object IsYAxis() {
            if (X == 0.0 && Y != 0.0) {
                return true;
            }
            return false;
        }

        // checks whether this vector is Z axis
        public object IsZAxis() {
            if (X == 0.0 && Y == 0.0) {
                return true;
            }
            return false;
        }

        // negate this vector
        public void Negate() {
            X = X * -1.0f;
            Y = Y * -1.0f;
        }

        // add this vector with the parameter vector
        // and return the result
        public Vector2 Add(Vector2 Vec) {
            var NewVec = new Vector2();
            NewVec.X = X + Vec.X;
            NewVec.Y = Y + Vec.Y;
            return NewVec;
        }

        // subtract this vector with the parameter vector
        // and return the result
        public Vector2 Subtract(Vector2 Vec) {
            var NewVec = new Vector2();
            NewVec.X = X - Vec.X;
            NewVec.Y = Y - Vec.Y;
            return NewVec;
        }

        public float Sum() {
            return X + Y;
        }

        // subtract this vector with the parameter vector
        // and return the result
        public static Vector2 operator *(Vector2 Vec, float Amount) {
            var NewVec = new Vector2();
            NewVec.X = Vec.X * Amount;
            NewVec.Y = Vec.Y * Amount;
            return NewVec;
        }

        public object Scale(float Amount) {
            var NewVec = new Vector2();
            NewVec.X = X + Amount;
            NewVec.Y = Y + Amount;
            return NewVec;
        }


        public Vector2 DistanceToVector(Vector2 Point) {
            float xval = X - Point.X;
            float yval = Y - Point.Y;
            return new Vector2(xval, yval);
        }


        public override string ToString() {
            return "{" + X.ToString() + "," + Y.ToString() + "}";
        }

        internal string ToOBJ() {
            return X + " " + Y;
        }
    }
}