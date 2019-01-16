using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreatorLOD : MonoBehaviour {

	public int WIDTH;
	public int HEIGHT;

	private Mesh mesh;
	private float[,] map;

	// Use this for initialization
	void Start () {
		mesh = new Mesh ();

		map = new float[WIDTH, HEIGHT];
		GenerateLand (mesh, map);

		GetComponent<MeshFilter> ().mesh = mesh;
		GetComponent<MeshCollider> ().sharedMesh = mesh;

		GetComponentInParent<LODGroup> ().RecalculateBounds ();
	}

	void GenerateLand(Mesh mesh,float[,] map){
		Vector3[] vertices = new Vector3[WIDTH * HEIGHT];
		int triangleSize = 6 * (WIDTH - 1) * (HEIGHT - 1);
		int[] triangles = new int[triangleSize];

		Vector2[] uv = new Vector2[WIDTH * HEIGHT];

		int inTriang = 0;
		for (int j = 0; j < HEIGHT; j++) {
			for (int i = 0; i < WIDTH; i++) {
				int iPos = i * ChunkLOD.SIZE_W / (WIDTH - 1);
				int jPos = j * ChunkLOD.SIZE_H / (HEIGHT - 1);
				int index = j * WIDTH + i;

				map [i, j] = ChunkLOD.GetHeight (iPos + transform.position.x, jPos + transform.position.z);

				vertices [index] = new Vector3 (iPos, map [i, j], jPos);

				float uv_i = ((float)i / (WIDTH - 1) + transform.position.x / ChunkLOD.SIZE_W) / ChunkLOD.GLOBAL_WIDTH;
				float uv_j = ((float)j / (HEIGHT - 1) + transform.position.z / ChunkLOD.SIZE_H) / ChunkLOD.GLOBAL_HEIGHT;
				uv [index] = new Vector2 (uv_i, uv_j);

				if (i < WIDTH - 1 && j < HEIGHT - 1) {
					triangles [inTriang] = index;
					triangles [inTriang + 1] = index + WIDTH;
					triangles [inTriang + 2] = index + WIDTH + 1;
					triangles [inTriang + 3] = index;
					triangles [inTriang + 4] = index + WIDTH + 1;
					triangles [inTriang + 5] = index + 1;
					inTriang += 6;
				}
			}
		}
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uv;

		mesh.RecalculateNormals ();
	}
}
