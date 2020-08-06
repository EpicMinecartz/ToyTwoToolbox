using System;
using System.Collections.Generic;

namespace ToyTwoToolbox {
	public class Vector3 {
		// data members - X, Y and Z values
		public double X;
		public double Y;
		public double Z;

		/// <summary>Vector3 allows you to store the individual X, Y and Z components that make a 3D point</summary>
		public Vector3() {
			X = 0.0;
			Y = 0.0;
			Z = 0.0;
		}

		// parametrised constructor
		public Vector3(double xx, double yy, double zz) {
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
		public double DotProduct(Vector3 Vec) {
			return (X * Vec.X) + (Y * Vec.Y) + (Z * Vec.Z);
		}

		// length of this vector
		public double Length() {
			return Math.Sqrt(X * X + Y * Y + Z * Z);
		}

		// find the angle between this vector and the parameter vector
		public double AngleTo(Vector3 Vec) {
			double AdotB = DotProduct(Vec);
			double ALstarBL = Length() * Vec.Length();
			if (ALstarBL == 0) {
				return 0.0;
			}
			return Math.Acos(AdotB / ALstarBL);
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
			double Ang = AngleTo(Vec);
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

		// negate this vector
		public void Negate() {
			X = X * -1.0;
			Y = Y * -1.0;
			Z = Z * -1.0;
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

		public double Sum() {
			return X + Y + Z;
		}

		// subtract this vector with the parameter vector
		// and return the result
		public static Vector3 operator *(Vector3 Vec, double Amount) {
			var NewVec = new Vector3();
			NewVec.X = Vec.X * Amount;
			NewVec.Y = Vec.Y * Amount;
			NewVec.Z = Vec.Z * Amount;
			return NewVec;
		}

		public object Scale(double Amount) {
			var NewVec = new Vector3();
			NewVec.X = X + Amount;
			NewVec.Y = Y + Amount;
			NewVec.Z = Z + Amount;
			return NewVec;
		}

		// find the distance between this point and the parameter point
		public double DistanceTo(Vector3 Point) {
			double xval = X - Point.X;
			double yval = Y - Point.Y;
			double zval = Z - Point.Z;
			return Math.Sqrt(xval * xval + yval * yval + zval * zval);
		}

		public Vector3 DistanceToVector(Vector3 Point) {
			double xval = X - Point.X;
			double yval = Y - Point.Y;
			double zval = Z - Point.Z;
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

		public static object Center(ref List<Vector3> Vectors) {
			double IX = 0;
			double IY = 0;
			double IZ = 0;
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

	}
}