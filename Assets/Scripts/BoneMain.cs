using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
Resources:
https://qiita.com/edo_m18/items/31ee6cbc3b3ff22013ae
http://marupeke296.com/DXG_No61_WhiteBoxSkinMeshAnimation.html
*/
public class BoneMain : MonoBehaviour {
	public List<Bone> bones = new List<Bone> ();
	public List<Matrix4x4> combMatArr = new List<Matrix4x4>();

	private float count = 0f;

	private Matrix4x4 gmat = Matrix4x4.identity;

	private float[] newPosition = new float[45];
	private Vector3[] newVec = new Vector3[15];

	private Vector3[] m_vertices = new Vector3[39];
	private int[] m_indices = new int[39];
	private Color[] m_colors = new Color[39];

	private PlaneData m_planeData;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		Loop ();
	}

	private void Init() {
		m_planeData = new PlaneData ();

		// bone setup from http://marupeke296.com/DXG_No61_WhiteBoxSkinMeshAnimation.html
		Matrix4x4 mat0 = MatrixUtils.RotateZ ((-90.0f * Mathf.PI) / 180);
		Matrix4x4 mat1 = MatrixUtils.RotateZ ((-90.0f * Mathf.PI) / 180);
		Matrix4x4 mat2 = MatrixUtils.RotateZ ((-90.0f * Mathf.PI) / 180);
		Matrix4x4 mat3 = MatrixUtils.RotateZ ((150.0f * Mathf.PI) / 180);
		Matrix4x4 mat4 = MatrixUtils.RotateZ ((150.0f * Mathf.PI) / 180);
		Matrix4x4 mat5 = MatrixUtils.RotateZ ((30.0f * Mathf.PI) / 180);
		Matrix4x4 mat6 = MatrixUtils.RotateZ ((30.0f * Mathf.PI) / 180);
		mat0.m30 = 0.0000f;
		mat0.m31 = 0.0000f;
		mat1.m30 = 0.0000f;
		mat1.m31 = -1.0000f;
		mat2.m30 = 0.0000f;
		mat2.m31 = -2.0000f;
		mat3.m30 = -0.6830f;
		mat3.m31 = 0.3943f;
		mat4.m30 = -1.5490f;
		mat4.m31 = 0.8943f;
		mat5.m30 = 0.6830f;
		mat5.m31 = 0.3943f;
		mat6.m30 = 1.5490f;
		mat6.m31 = 0.8943f;

		Bone rootBone = new Bone(mat0);
		bones.Add (rootBone);
		bones.Add (new Bone(mat1));
		bones.Add (new Bone(mat2));
		bones.Add (new Bone(mat3));
		bones.Add (new Bone(mat4));
		bones.Add (new Bone(mat5));
		bones.Add (new Bone(mat6));

		gmat = Matrix4x4.identity;
		for (int i = 0; i < bones.Count; i++) {
			combMatArr.Add (Matrix4x4.identity);
		}

		// relationship between parent and childs
		bones[0].Add(bones[1]);
		bones[1].Add(bones[2]);

		bones[0].Add(bones[3]);
		bones[3].Add(bones[4]);

		bones[0].Add(bones[5]);
		bones[5].Add(bones[6]);

		Bone.CalcRelativeMat(bones[0], gmat);

		for (int i = 0; i < 39; i++) {
			int idxBase = i * 4;
			int idx0 = idxBase + 0;
			int idx1 = idxBase + 1;
			int idx2 = idxBase + 2;
			int idx3 = idxBase + 3;

			m_colors [i] = new Color (m_planeData.color[idx0], m_planeData.color[idx1], m_planeData.color[idx2], m_planeData.color[idx3]);
		}
	}

	private void Loop() {
		count += 0.03f;
		float s = Mathf.Sin(count);
		float a = 0.3f * s;

		for (int i = 0; i < bones.Count; i++) {
			Matrix4x4 m = MatrixUtils.RotateY (a);
			bones [i].matrixBone = bones [i].matrixInit * m;
		}

		Bone.UpdateBone(bones[0], gmat);

		for (int i = 0; i < bones.Count; i++) {
			combMatArr[i] = bones[i].matrixComb;
		}

		for (int i = 0; i < 15; i++) {
			int idxBase = i * 3;
			int idx0 = idxBase + 0;
			int idx1 = idxBase + 1;
			int idx2 = idxBase + 2;

			Matrix4x4[] comb1 = {
				Matrix4x4.identity,
				Matrix4x4.identity,
				Matrix4x4.identity,
				Matrix4x4.identity
			};
			Matrix4x4 comb2 = Matrix4x4.zero;

			for (var j = 0; j < 3; j++) {
				int boneIdx   = i * 4 + j;
				int weightIdx = i * 3 + j;

				comb1[j] = MatrixUtils.MultiplyScalar(combMatArr[m_planeData.boneIndices[boneIdx]],
					m_planeData.weights[weightIdx],comb1[j]);
			}

			// 1.0 - weight1 - weight2 - weight3
			float weight = 1.0f - (m_planeData.weights[i * 3 + 0] +
				m_planeData.weights[i * 3 + 1] +
				m_planeData.weights[i * 3 + 2]);
			comb1[3] = MatrixUtils.MultiplyScalar(combMatArr[m_planeData.boneIndices[i * 4 + 3]], weight, comb1[3]);

			for (int k = 0; k < 4; k++) {
				comb2 = MatrixUtils.Add (comb2, comb1 [k], comb2);
			}

			Vector3 pos = new Vector3(m_planeData.position[idx0],
				m_planeData.position[idx1],
				m_planeData.position[idx2]);

			pos = comb2.MultiplyVector(pos);
			newPosition[idx0] = pos.x;
			newPosition[idx1] = pos.y;
			newPosition[idx2] = pos.z;
			newVec [i] = pos;
		}

		for(int i = 0; i<m_planeData.index.Length; i++) {
			m_vertices [i] = newVec [m_planeData.index [i]];
			m_indices [i] = i;
		}

		UpdateMesh ();
	}

	private void UpdateMesh() {
		Mesh mesh = gameObject.GetComponent<MeshFilter> ().mesh;
		mesh.Clear ();

		mesh.vertices = m_vertices;
		mesh.triangles = m_indices;
		mesh.colors = m_colors;

		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}

}
