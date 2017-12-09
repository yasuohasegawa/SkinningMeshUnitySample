using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone  {

	public Matrix4x4 matrix = Matrix4x4.identity;
	public Matrix4x4 matrixBone = Matrix4x4.identity;
	public Matrix4x4 matrixComb = Matrix4x4.identity;
	public Matrix4x4 matrixInit = Matrix4x4.identity;
	public Matrix4x4 matrixOffset = Matrix4x4.identity;
	public List<Bone> children = new List<Bone>();

	public Bone parentBone = null;

	public Bone (Matrix4x4 mat) {
		matrixInit = mat;
		matrixOffset = Matrix4x4.Inverse (matrixInit);
	}

	public void Add(Bone bone) {
		if(bone.parentBone != null) {
			bone.parentBone.Remove (bone);
		}

		children.Add (bone);
		bone.parentBone = this;
	}

	public void Remove(Bone bone) {
		if (children.Count == 0) {
			return;
		}

		int index = children.IndexOf (bone);
		if (index < 0) {
			return;
		}

		this.children.RemoveAt (index);
		bone.parentBone = null;
	}

	public static void CalcRelativeMat(Bone bone, Matrix4x4 parentOffsteMat) {
		foreach (Bone childBone in bone.children) {
			Bone.CalcRelativeMat(childBone, bone.matrixOffset);
		}

		if(parentOffsteMat != null){
			bone.matrixInit = bone.matrixInit * parentOffsteMat;
		}
	}

	public static void UpdateBone(Bone bone, Matrix4x4 parentWorldMat) {

		bone.matrixBone = parentWorldMat * bone.matrixBone; // parent multiply child
		bone.matrixComb = bone.matrixBone * bone.matrixOffset; // from local to global
		//Debug.Log (">>>>>> UpdateBone:"+bone.children.Count);
		foreach (Bone childBone in bone.children) {
			Bone.UpdateBone(childBone, bone.matrixBone);
		}
	}
}