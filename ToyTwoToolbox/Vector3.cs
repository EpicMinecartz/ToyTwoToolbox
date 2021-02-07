using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
	/// <summary>
	/// Vector 3D definition
	/// </summary>
	public class Vector3 {
		/// <summary>
		/// Note, this class originally supported double, but now uses float
		/// </summary>

		// data members - X, Y and Z values
		public float X;
		public float Y;
		public float Z { get; set; }

		/// <summary>Vector3 allows you to store the individual X, Y and Z components that make a 3D point</summary>
		public Vector3() {
			X = 0.0f;
			Y = 0.0f;
			Z = 0.0f;
		}

		// parametrised constructor
		public Vector3(float xx, float yy, float zz) {
			X = xx;
			Y = yy;
			Z = zz;
		}

		// copy constructor
		public Vector3(Vector3 Vec) {
			X = Vec.X;
			Y = Vec.Y;
			Z = Vec.Z;
		}

        // dot product of this vector and the parameter vector
        public float DotProduct(Vector3 Vec) {
			return (X * Vec.X) + (Y * Vec.Y) + (Z * Vec.Z);
		}

		// length of this vector
		public float Length() {
			return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		// find the angle between this vector and the parameter vector
		public float AngleTo(Vector3 Vec) {
			float AdotB = DotProduct(Vec);
			float ALstarBL = Length() * Vec.Length();
			if (ALstarBL == 0) {
				return 0.0f;
			}
			return (float)Math.Acos(AdotB / ALstarBL);
		}

		// checks whether this vector is equal to the parameter vector
		public bool IsEqualTo(Vector3 Vec) {
			if (X == Vec.X && Y == Vec.Y && Z == Vec.Z) {
				return true;
			}
			return false;
		}

		// checks whether this vector is perpendicular to the parameter vector
		public bool IsPerpendicularTo(Vector3 Vec) {
			float Ang = AngleTo(Vec);
			if (Ang == (90 * Math.PI / 180.0)) {
				return true;
			}
			return false;
		}

		// checks whether this vector is X axis
		public object IsXAxis() {
			if (X != 0.0 && Y == 0.0 && Z == 0.0) {
				return true;
			}
			return false;
		}

		// checks whether this vector is Y axis
		public object IsYAxis() {
			if (X == 0.0 && Y != 0.0 && Z == 0.0) {
				return true;
			}
			return false;
		}

		// checks whether this vector is Z axis
		public object IsZAxis() {
			if (X == 0.0 && Y == 0.0 && Z != 0.0) {
				return true;
			}
			return false;
		}


		// add this vector with the parameter vector
		// and return the result
		public Vector3 Add(Vector3 Vec) {
			var NewVec = new Vector3();
			NewVec.X = X + Vec.X;
			NewVec.Y = Y + Vec.Y;
			NewVec.Z = Z + Vec.Z;
			return NewVec;
		}

		// subtract this vector with the parameter vector
		// and return the result
		public Vector3 Subtract(Vector3 Vec) {
			var NewVec = new Vector3();
			NewVec.X = X - Vec.X;
			NewVec.Y = Y - Vec.Y;
			NewVec.Z = Z - Vec.Z;
			return NewVec;
		}

		public float Sum() {
			return X + Y + Z;
		}

		// subtract this vector with the parameter vector
		// and return the result
		public static Vector3 operator *(Vector3 Vec, float Amount) {
			var NewVec = new Vector3();
			NewVec.X = Vec.X * Amount;
			NewVec.Y = Vec.Y * Amount;
			NewVec.Z = Vec.Z * Amount;
			return NewVec;
		}

		public object Scale(float Amount) {
			var NewVec = new Vector3();
			NewVec.X = X + Amount;
			NewVec.Y = Y + Amount;
			NewVec.Z = Z + Amount;
			return NewVec;
		}

		// find the distance between this point and the parameter point
		public float DistanceTo(Vector3 Point) {
			float xval = X - Point.X;
			float yval = Y - Point.Y;
			float zval = Z - Point.Z;
			return (float)Math.Sqrt(xval * xval + yval * yval + zval * zval);
		}

		public Vector3 DistanceToVector(Vector3 Point) {
			float xval = X - Point.X;
			float yval = Y - Point.Y;
			float zval = Z - Point.Z;
			return new Vector3(xval, yval, zval);
		}

		// translate this point by the parameter vector
		public void TranslateBy(Vector3 Vec) {
			if (Vec.Length() > 1.0) {
				X = X + Vec.X;
				Y = Y + Vec.Y;
				Z = Z + Vec.Z;
			}
		}


		// negate this vector
		public void Negate() {
			X = -Math.Abs(X);
			Y = -Math.Abs(Y);
			Z = -Math.Abs(Z);
		}

		// negate this vector
		public void FullNegate() {
			X = X * -1.0f;
			Y = Y * -1.0f;
			Z = Z * -1.0f;
		}

		public void SetMatrix4DNegative(Matrix4D matrix, Vector3 NegateVector) {
			if (NegateVector.X != 0) { matrix._matrix[2, 2] = -Math.Abs(X); }
			if (NegateVector.Y != 0) { matrix._matrix[1, 1] = -Math.Abs(Y); }
			if (NegateVector.Z != 0) { matrix._matrix[0, 0] = -Math.Abs(Z); }
			matrix._matrix[3, 3] = -1;
		}

		public void TransformBy(Matrix4D Mat) {
			float xx = 0;
			float yy = 0;
			float zz = 0;

			xx = (X * Convert.ToSingle(Mat._matrix.GetValue(0, 0))) +
				(Y * Convert.ToSingle(Mat._matrix.GetValue(1, 0))) + 
				(Z * Convert.ToSingle(Mat._matrix.GetValue(2, 0))) + 
				(Convert.ToSingle(Mat._matrix.GetValue(0, 3)));
			yy = (X * Convert.ToSingle(Mat._matrix.GetValue(0, 1))) + 
				(Y * Convert.ToSingle(Mat._matrix.GetValue(1, 1))) + 
				(Z * Convert.ToSingle(Mat._matrix.GetValue(2, 1))) + 
				(Convert.ToSingle(Mat._matrix.GetValue(1, 3)));
			zz = (X * Convert.ToSingle(Mat._matrix.GetValue(0, 2))) + 
				(Y * Convert.ToSingle(Mat._matrix.GetValue(1, 2))) + 
				(Z * Convert.ToSingle(Mat._matrix.GetValue(2, 2))) + 
				(Convert.ToSingle(Mat._matrix.GetValue(2, 3)));

			X = xx;
			Y = yy;
			Z = zz;
		}

		public static void TransformPoints(Matrix4D Mat, ref List<Vector3> Points) {
			foreach (Vector3 Pt in Points) {
				Pt.TransformBy(Mat);
			}
		}

		public static object Center(ref List<Vector3> Vectors) {
			float IX = 0;
			float IY = 0;
			float IZ = 0;
			foreach (Vector3 VTX in Vectors) {
				IX += VTX.X;
				IY += VTX.Y;
				IZ += VTX.Z;
			}
			return new Vector3(IX / Vectors.Count, IY / Vectors.Count, IZ / Vectors.Count);
		}

		public override string ToString() {
			return "{" + X.ToString() + "," + Y.ToString() + "," + Z.ToString() + "}";
		}

        public string ToOBJ() {
			return X + " " + Y  +" " + Z;
        }
    }
}