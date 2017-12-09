using UnityEngine;
using System.Collections;

public class MatrixUtils {
	public static Matrix4x4 Translate(Vector3 pos) {
		Matrix4x4 mat = new Matrix4x4 ();
		mat.m00 = 1f;
		mat.m01 = 0f;
		mat.m02 = 0f;
		mat.m03 = 0f;

		mat.m10 = 0f;
		mat.m11 = 1f;
		mat.m12 = 0f;
		mat.m13 = 0f;

		mat.m20 = 0f;
		mat.m21 = 0f;
		mat.m22 = 1f;
		mat.m23 = 0f;

		mat.m30 = pos.x;
		mat.m31 = pos.y;
		mat.m32 = pos.z;
		mat.m33 = 1f;

		return mat;
	}

	public static Matrix4x4 RotateX(float rad) {
		Matrix4x4 mat = new Matrix4x4 ();
		mat.m00 = 1f;
		mat.m01 = 0f;
		mat.m02 = 0f;
		mat.m03 = 0f;

		mat.m10 = 0f;
		mat.m11 = Mathf.Cos(rad);
		mat.m12 = -Mathf.Sin(rad);
		mat.m13 = 0f;

		mat.m20 = 0f;
		mat.m21 = Mathf.Sin(rad);
		mat.m22 = Mathf.Cos(rad);
		mat.m23 = 0f;

		mat.m30 = 0f;
		mat.m31 = 0f;
		mat.m32 = 0f;
		mat.m33 = 1f;

		return mat;
	}

	public static Matrix4x4 RotateY(float rad) {
		Matrix4x4 mat = new Matrix4x4 ();
		mat.m00 = Mathf.Cos(rad);
		mat.m01 = 0f;
		mat.m02 = -Mathf.Sin(rad);
		mat.m03 = 0f;

		mat.m10 = 0f;
		mat.m11 = 1f;
		mat.m12 = 0f;
		mat.m13 = 0f;

		mat.m20 = Mathf.Sin(rad);
		mat.m21 = 0f;
		mat.m22 = Mathf.Cos(rad);
		mat.m23 = 0f;

		mat.m30 = 0f;
		mat.m31 = 0f;
		mat.m32 = 0f;
		mat.m33 = 1f;

		return mat;
	}

	public static Matrix4x4 RotateZ(float rad) {
		Matrix4x4 mat = new Matrix4x4 ();
		mat.m00 = Mathf.Cos(rad);
		mat.m01 = -Mathf.Sin(rad);
		mat.m02 = 0f;
		mat.m03 = 0f;

		mat.m10 = Mathf.Sin(rad);
		mat.m11 = Mathf.Cos(rad);
		mat.m12 = 0f;
		mat.m13 = 0f;

		mat.m20 = 0f;
		mat.m21 = 0f;
		mat.m22 = 1f;
		mat.m23 = 0f;

		mat.m30 = 0f;
		mat.m31 = 0f;
		mat.m32 = 0f;
		mat.m33 = 1f;

		return mat;
	}

	public static Matrix4x4 Scale(Vector3 scale) {
		Matrix4x4 mat = new Matrix4x4 ();
		mat.m00 = scale.x;
		mat.m01 = 0f;
		mat.m02 = 0f;
		mat.m03 = 0f;

		mat.m10 = 0f;
		mat.m11 = scale.y;
		mat.m12 = 0f;
		mat.m13 = 0f;

		mat.m20 = 0f;
		mat.m21 = 0f;
		mat.m22 = scale.z;
		mat.m23 = 0f;

		mat.m30 = 0f;
		mat.m31 = 0f;
		mat.m32 = 0f;
		mat.m33 = 1f;

		return mat;
	}

	public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2) {
		Matrix4x4 result;

		// First row
		result.m00 = value1.m00 * value2.m00 + value1.m01 * value2.m10 + value1.m02 * value2.m20 + value1.m03 * value2.m30;
		result.m01 = value1.m00 * value2.m01 + value1.m01 * value2.m11 + value1.m02 * value2.m21 + value1.m03 * value2.m31;
		result.m02 = value1.m00 * value2.m02 + value1.m01 * value2.m12 + value1.m02 * value2.m22 + value1.m03 * value2.m32;
		result.m03 = value1.m00 * value2.m03 + value1.m01 * value2.m13 + value1.m02 * value2.m23 + value1.m03 * value2.m33;

		// Second row
		result.m10 = value1.m10 * value2.m10 + value1.m11 * value2.m10 + value1.m12 * value2.m20 + value1.m13 * value2.m30;
		result.m11 = value1.m10 * value2.m11 + value1.m11 * value2.m11 + value1.m12 * value2.m21 + value1.m13 * value2.m31;
		result.m12 = value1.m10 * value2.m12 + value1.m11 * value2.m12 + value1.m12 * value2.m22 + value1.m13 * value2.m32;
		result.m13 = value1.m10 * value2.m13 + value1.m11 * value2.m13 + value1.m12 * value2.m23 + value1.m13 * value2.m33;

		// Third row
		result.m20 = value1.m20 * value2.m10 + value1.m21 * value2.m10 + value1.m22 * value2.m20 + value1.m23 * value2.m30;
		result.m21 = value1.m20 * value2.m11 + value1.m21 * value2.m11 + value1.m22 * value2.m21 + value1.m23 * value2.m31;
		result.m22 = value1.m20 * value2.m12 + value1.m21 * value2.m12 + value1.m22 * value2.m22 + value1.m23 * value2.m32;
		result.m23 = value1.m20 * value2.m13 + value1.m21 * value2.m13 + value1.m22 * value2.m23 + value1.m23 * value2.m33;

		// Fourth row
		result.m30 = value1.m30 * value2.m10 + value1.m31 * value2.m10 + value1.m33 * value2.m20 + value1.m33 * value2.m30;
		result.m31 = value1.m30 * value2.m11 + value1.m31 * value2.m11 + value1.m33 * value2.m21 + value1.m33 * value2.m31;
		result.m32 = value1.m30 * value2.m12 + value1.m31 * value2.m12 + value1.m33 * value2.m22 + value1.m33 * value2.m32;
		result.m33 = value1.m30 * value2.m13 + value1.m31 * value2.m13 + value1.m33 * value2.m23 + value1.m33 * value2.m33;

		return result;
	}

	public static Matrix4x4 Inverse(Matrix4x4 m) {
		float A2323 = m.m22 * m.m33 - m.m23 * m.m32 ;
		float A1323 = m.m21 * m.m33 - m.m23 * m.m31 ;
		float A1223 = m.m21 * m.m32 - m.m22 * m.m31 ;
		float A0323 = m.m20 * m.m33 - m.m23 * m.m30 ;
		float A0223 = m.m20 * m.m32 - m.m22 * m.m30 ;
		float A0123 = m.m20 * m.m31 - m.m21 * m.m30 ;
		float A2313 = m.m12 * m.m33 - m.m13 * m.m32 ;
		float A1313 = m.m11 * m.m33 - m.m13 * m.m31 ;
		float A1213 = m.m11 * m.m32 - m.m12 * m.m31 ;
		float A2312 = m.m12 * m.m23 - m.m13 * m.m22 ;
		float A1312 = m.m11 * m.m23 - m.m13 * m.m21 ;
		float A1212 = m.m11 * m.m22 - m.m12 * m.m21 ;
		float A0313 = m.m10 * m.m33 - m.m13 * m.m30 ;
		float A0213 = m.m10 * m.m32 - m.m12 * m.m30 ;
		float A0312 = m.m10 * m.m23 - m.m13 * m.m20 ;
		float A0212 = m.m10 * m.m22 - m.m12 * m.m20 ;
		float A0113 = m.m10 * m.m31 - m.m11 * m.m30 ;
		float A0112 = m.m10 * m.m21 - m.m11 * m.m20 ;

		float det = m.m00 * ( m.m11 * A2323 - m.m12 * A1323 + m.m13 * A1223 ) 
			- m.m01 * ( m.m10 * A2323 - m.m12 * A0323 + m.m13 * A0223 ) 
			+ m.m02 * ( m.m10 * A1323 - m.m11 * A0323 + m.m13 * A0123 ) 
			- m.m03 * ( m.m10 * A1223 - m.m11 * A0223 + m.m12 * A0123 ) ;
		det = 1 / det;

		return new Matrix4x4() {
			m00 = det *   ( m.m11 * A2323 - m.m12 * A1323 + m.m13 * A1223 ),
			m01 = det * - ( m.m01 * A2323 - m.m02 * A1323 + m.m03 * A1223 ),
			m02 = det *   ( m.m01 * A2313 - m.m02 * A1313 + m.m03 * A1213 ),
			m03 = det * - ( m.m01 * A2312 - m.m02 * A1312 + m.m03 * A1212 ),
			m10 = det * - ( m.m10 * A2323 - m.m12 * A0323 + m.m13 * A0223 ),
			m11 = det *   ( m.m00 * A2323 - m.m02 * A0323 + m.m03 * A0223 ),
			m12 = det * - ( m.m00 * A2313 - m.m02 * A0313 + m.m03 * A0213 ),
			m13 = det *   ( m.m00 * A2312 - m.m02 * A0312 + m.m03 * A0212 ),
			m20 = det *   ( m.m10 * A1323 - m.m11 * A0323 + m.m13 * A0123 ),
			m21 = det * - ( m.m00 * A1323 - m.m01 * A0323 + m.m03 * A0123 ),
			m22 = det *   ( m.m00 * A1313 - m.m01 * A0313 + m.m03 * A0113 ),
			m23 = det * - ( m.m00 * A1312 - m.m01 * A0312 + m.m03 * A0112 ),
			m30 = det * - ( m.m10 * A1223 - m.m11 * A0223 + m.m12 * A0123 ),
			m31 = det *   ( m.m00 * A1223 - m.m01 * A0223 + m.m02 * A0123 ),
			m32 = det * - ( m.m00 * A1213 - m.m01 * A0213 + m.m02 * A0113 ),
			m33 = det *   ( m.m00 * A1212 - m.m01 * A0212 + m.m02 * A0112 ),
		};
	}

	public static Vector3 ExtractTranslationFromMatrix(Matrix4x4 matrix) {
		Vector3 translate;
		translate.x = matrix.m03;
		translate.y = matrix.m13;
		translate.z = matrix.m23;
		return translate;
	}

	public static Matrix4x4 MultiplyScalar(Matrix4x4 matrix, float scalar, Matrix4x4 dest) {
		dest.m00 = matrix.m00 * scalar;
		dest.m01 = matrix.m01 * scalar;
		dest.m02 = matrix.m02 * scalar;
		dest.m03 = matrix.m03 * scalar;

		dest.m10 = matrix.m10 * scalar;
		dest.m11 = matrix.m11 * scalar;
		dest.m12 = matrix.m12 * scalar;
		dest.m13 = matrix.m13 * scalar;

		dest.m20 = matrix.m20 * scalar;
		dest.m21 = matrix.m21 * scalar;
		dest.m22 = matrix.m22 * scalar;
		dest.m23 = matrix.m23 * scalar;

		dest.m30 = matrix.m30 * scalar;
		dest.m31 = matrix.m31 * scalar;
		dest.m32 = matrix.m32 * scalar;
		dest.m33 = matrix.m33 * scalar;

		return dest;
	}

	public static Matrix4x4 Add(Matrix4x4 A, Matrix4x4 B, Matrix4x4 dest) {
		dest.m00 = A.m00 + B.m00;
		dest.m01 = A.m01 + B.m01;
		dest.m02 = A.m02 + B.m02;
		dest.m03 = A.m03 + B.m03;

		dest.m10 = A.m10 + B.m10;
		dest.m11 = A.m11 + B.m11;
		dest.m12 = A.m12 + B.m12;
		dest.m13 = A.m13 + B.m13;

		dest.m20 = A.m20 + B.m20;
		dest.m21 = A.m21 + B.m21;
		dest.m22 = A.m22 + B.m22;
		dest.m23 = A.m23 + B.m23;

		dest.m30 = A.m30 + B.m30;
		dest.m31 = A.m31 + B.m31;
		dest.m32 = A.m32 + B.m32;
		dest.m33 = A.m33 + B.m33;

		return dest;
	}

}