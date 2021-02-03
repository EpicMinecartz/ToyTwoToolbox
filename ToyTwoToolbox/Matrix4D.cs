using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
	public class Matrix4D {

		public double[,] _matrix = new double[4, 4];

		public Matrix4D() {
			this.MakeIdentity();
		}

		public Matrix4D(double xRadians, double yRadians, double zRadians) {
			this.MakeIdentity();
			this.SetBy(NewRotate(xRadians, yRadians, zRadians));
		}

		public Matrix4D MakeIdentity() {

			this._matrix[0, 0] = 1;
			this._matrix[0, 1] = 0;
			this._matrix[0, 2] = 0;
			this._matrix[0, 3] = 0;

			this._matrix[1, 0] = 0;
			this._matrix[1, 1] = 1;
			this._matrix[1, 2] = 0;
			this._matrix[1, 3] = 0;

			this._matrix[2, 0] = 0;
			this._matrix[2, 1] = 0;
			this._matrix[2, 2] = 1;
			this._matrix[2, 3] = 0;

			this._matrix[3, 0] = 0;
			this._matrix[3, 1] = 0;
			this._matrix[3, 2] = 0;
			this._matrix[3, 3] = 1;

			return this;
		}

		public void SetBy(Matrix4D matrix) {
			for (var i = 0;i <= 3;i++) {
				for (var j = 0;j <= 3;j++) {
					this._matrix[i, j] = matrix._matrix[i, j];
				}
			}
		}

		public static Matrix4D NewRotateAroundX(double radians) {
			var matrix = new Matrix4D();
			matrix._matrix[1, 1] = Math.Cos(radians);
			matrix._matrix[1, 2] = Math.Sin(radians);
			matrix._matrix[2, 1] = -(Math.Sin(radians));
			matrix._matrix[2, 2] = Math.Cos(radians);
			return matrix;
		}
		public static Matrix4D NewRotateAroundY(double radians) {
			var matrix = new Matrix4D();
			matrix._matrix[0, 0] = Math.Cos(radians);
			matrix._matrix[0, 2] = -(Math.Sin(radians));
			matrix._matrix[2, 0] = Math.Sin(radians);
			matrix._matrix[2, 2] = Math.Cos(radians);
			return matrix;
		}
		public static Matrix4D NewRotateAroundZ(double radians) {
			var matrix = new Matrix4D();
			matrix._matrix[0, 0] = Math.Cos(radians);
			matrix._matrix[0, 1] = Math.Sin(radians);
			matrix._matrix[1, 0] = -(Math.Sin(radians));
			matrix._matrix[1, 1] = Math.Cos(radians);
			return matrix;
		}

		//// multiply matrix 1 and 2 and return the resultant matrix
		//private Matrix4D Multiply(Matrix4D Mat1, Matrix4D Mat2) {
		//	var Mat = new Matrix4D();
		//	int ii = 0;
		//	int jj = 0;
		//	int kk = 0;
		//	double sum = 0;
		//	for (ii = 0;ii <= 3;ii++) {
		//		for (jj = 0;jj <= 3;jj++) {
		//			sum = 0;
		//			for (kk = 0;kk <= 3;kk++) {
		//				sum = sum + (Mat1._matrix.GetValue(ii, kk) * Mat2._matrix.GetValue(kk, jj));
		//			}
		//			Mat._matrix.SetValue(sum, ii, jj);
		//		}
		//	}
		//	return Mat;
		//}


		// multiply the currnet matrix with the parameter
		//public void PostMultiplyBY(Matrix4D Mat) {
		//	var Thismat = new Matrix4D();
		//	var CMat = new Matrix4D();
		//	Thismat._matrix = _matrix;
		//	CMat = Multiply(Thismat, Mat);
		//	_matrix = CMat._matrix;
		//	Thismat = null;
		//	CMat = null;
		//}

		// find the cofactor matrix and return the same
		//public Matrix4D CoFactor() {
		//	int i = 0;
		//	int j = 0;
		//	int ii = 0;
		//	int jj = 0;
		//	int i1 = 0;
		//	int j1 = 0;
		//	double det = 0;
		//	var m = new Matrix4D();
		//	var CMat = new Matrix4D();

		//	for (j = 0;j <= 3;j++) {
		//		for (i = 0;i <= 3;i++) {
		//			i1 = 0;
		//			for (ii = 0;ii <= 3;ii++) {
		//				if (ii != i) {
		//					j1 = 0;
		//					for (jj = 0;jj <= 3;jj++) {
		//						if (jj != j) {
		//							m._matrix.SetValue(_matrix.GetValue(ii, jj), i1, j1);
		//							j1 = +j1 + 1;
		//						}
		//					}
		//					i1 = i1 + 1;
		//				}
		//			}
		//			det = m.Determinant();
		//			CMat._matrix.SetValue(Math.Pow(-1.0, i + j + 2.0) * det, i, j);
		//		}
		//	}
		//	m = null;
		//	return CMat;
		//}


		// find the determinant of the current matrix
		//public double Determinant() { //this might be fucked now
		//	double det = 0.0;
		//	var m = new Matrix2D();

		//	for (int j1 = 0;j1 <= 3;j1++) {
		//		for (int i = 1;i <= 3;i++) {
		//			int j2 = 0;
		//			for (int j = 0;j <= 3;j++) {
		//				if (j != j1) {
		//					m.matrix.SetValue(_matrix.GetValue(i, j), i - 1, j2);
		//					j2 = j2 + 1;
		//				}
		//			}
		//		}
		//		double d = Math.Pow(-1.0, j1 + 2);
		//		d *= _matrix.GetValue(0, j1) * m.Determinant();
		//		det += d;
		//	}
		//	m = null;
		//	return det;
		//}

		// find the transpose
		public void Transpose() {
			int i = 0;
			int j = 0;
			double tmp = 0.0;

			for (i = 1;i <= 3;i++) {
				int tempVar = i;
				for (j = 0;j <= tempVar;j++) {
					tmp = Convert.ToDouble(_matrix.GetValue(i, j));
					_matrix.SetValue(_matrix.GetValue(j, i), i, j);
					_matrix.SetValue(tmp, j, i);
				}
			}
		}

		//public Matrix4D GetInverse() {
		//	Matrix4D NewMatrix = null;
		//	double det = Determinant();
		//	det = 1 / det;
		//	NewMatrix = CoFactor();
		//	NewMatrix.Transpose();

		//	for (int i = 0;i <= 3;i++) {
		//		for (int j = 0;j <= 3;j++) {
		//			NewMatrix._matrix.SetValue(NewMatrix._matrix.GetValue(i, j) * det, i, j);
		//		}
		//	}
		//	return NewMatrix;
		//}

		// set the current matrix to identity matrix
		public void SetToIdentity() {
			int i = 0;
			int tempVar = _matrix.GetUpperBound(0);
			for (i = _matrix.GetLowerBound(0);i <= tempVar;i++) {
				int j = 0;
				int tempVar2 = _matrix.GetUpperBound(1);
				for (j = _matrix.GetLowerBound(1);j <= tempVar2;j++) {
					_matrix.SetValue(0, i, j);
				}
			}
			_matrix.SetValue(1, 0, 0);
			_matrix.SetValue(1, 1, 1);
			_matrix.SetValue(1, 2, 2);
			_matrix.SetValue(1, 3, 3);
		}

		public static Matrix4D NewRotate(double radiansX, double radiansY, double radiansZ) {
			var matrix = NewRotateAroundX(radiansX);
			matrix = matrix * NewRotateAroundY(radiansY);
			matrix = matrix * NewRotateAroundZ(radiansZ);
			return matrix;
		}

		public static Matrix4D NewRotateByDegrees(double degreesX, double degreesY, double degreesZ) {
			return NewRotate(Angle.DegreesToRadians(degreesX), Angle.DegreesToRadians(degreesY), Angle.DegreesToRadians(degreesZ));
		}

		public static Matrix4D NewRotateFromDegreesAroundX(double degrees) {
			return NewRotateAroundX(Angle.DegreesToRadians(degrees));
		}
		public static Matrix4D NewRotateFromDegreesAroundY(double degrees) {
			return NewRotateAroundY(Angle.DegreesToRadians(degrees));
		}
		public static Matrix4D NewRotateFromDegreesAroundZ(double degrees) {
			return NewRotateAroundZ(Angle.DegreesToRadians(degrees));
		}

		public static Matrix4D operator *(Matrix4D matrix1, Matrix4D matrix2) {
			var matrix = new Matrix4D();
			for (var i = 0;i <= 3;i++) {
				for (var j = 0;j <= 3;j++) {
					matrix._matrix[i, j] = (matrix2._matrix[i, 0] * matrix1._matrix[0, j]) + (matrix2._matrix[i, 1] * matrix1._matrix[1, j]) + (matrix2._matrix[i, 2] * matrix1._matrix[2, j]);
				}
			}
			return matrix;
		}

		public static Point3D operator *(Matrix4D matrix1, Point3D point3D) {
			var x = point3D.InitialX * matrix1._matrix[0, 0] + point3D.InitialY * matrix1._matrix[0, 1] + point3D.InitialZ * matrix1._matrix[0, 2];
			var y = point3D.InitialX * matrix1._matrix[1, 0] + point3D.InitialY * matrix1._matrix[1, 1] + point3D.InitialZ * matrix1._matrix[1, 2];
			var z = point3D.InitialX * matrix1._matrix[2, 0] + point3D.InitialY * matrix1._matrix[2, 1] + point3D.InitialZ * matrix1._matrix[2, 2];
			point3D.X = x;
			point3D.Y = y;
			point3D.Z = z;
			return point3D;
		}

	}

	public sealed class Angle {

		private Angle() {
		}

		public static double DegreesToRadians(double degrees) {
			return (Math.PI / 180) * degrees;
		}

		public static double RadiansToDegrees(double radians) {
			return (180 / Math.PI) * radians;
		}

		public static int As180(int angle_Renamed) {
			angle_Renamed = angle_Renamed % 360;

			angle_Renamed = (angle_Renamed < -180 ? 180 - Math.Abs(angle_Renamed % 180) : (angle_Renamed > 180) ? Math.Abs(angle_Renamed % 180) - 180 : angle_Renamed);
			return angle_Renamed;
		}
	}

	public class Point3D {
		private double _x;
		private double _y;
		private double _z;
		private bool _xIsDefined;
		private bool _yIsDefined;
		private bool _zIsDefined;

		public Point3D() {
		}
		public Point3D(double x, double y, double z) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		private double privateInitialX;
		public double InitialX {
			get {
				return privateInitialX;
			}
			private set {
				privateInitialX = value;
			}
		}
		private double privateInitialY;
		public double InitialY {
			get {
				return privateInitialY;
			}
			private set {
				privateInitialY = value;
			}
		}
		private double privateInitialZ;
		public double InitialZ {
			get {
				return privateInitialZ;
			}
			private set {
				privateInitialZ = value;
			}
		}

		public double X {
			get {
				return this._x;
			}
			set {
				if (!this._xIsDefined) {
					this._xIsDefined = true;
					this.InitialX = value;
				}
				this._x = value;
			}
		}
		public double Y {
			get {
				return this._y;
			}
			set {
				if (!this._yIsDefined) {
					this._yIsDefined = true;
					this.InitialY = value;
				}
				this._y = value;
			}
		}
		public double Z {
			get {
				return this._z;
			}
			set {
				if (!this._zIsDefined) {
					this._zIsDefined = true;
					this.InitialZ = value;
				}
				this._z = value;
			}
		}
		public Point3D SetBy(Point3D point) {
			this.X = point.X;
			this.Y = point.Y;
			this.Z = point.Z;
			return this;
		}

		public void ResetToInitial() {
			this.X = this.InitialX;
			this.Y = this.InitialY;
			this.Z = this.InitialZ;
		}
	}

}
