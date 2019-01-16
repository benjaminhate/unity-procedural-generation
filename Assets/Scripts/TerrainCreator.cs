using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator : MonoBehaviour {

	public int WIDTH;
	public int HEIGHT;

	public float freq = 0.1f;
	public float amplitude = 0.5f;

	private Mesh mesh;
	private float[,] map;

	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh = new Mesh ();
		GetComponent<MeshCollider> ().sharedMesh = mesh;

		map = new float[WIDTH, HEIGHT];
		GenerateLand (mesh, map);	
	}

	void GenerateLand(Mesh mesh,float[,] map){
		Vector3[] vertices = new Vector3[WIDTH * HEIGHT];
		int triangleSize = 6 * (WIDTH - 1) * (HEIGHT - 1);
		int[] triangles = new int[triangleSize];

		int inTriang = 0;
		for (int j = 0; j < HEIGHT; j++) {
			for (int i = 0; i < WIDTH; i++) {
				map[i, j] = amplitude * Mathf.PerlinNoise (freq * (i + transform.position.x), freq * (j + transform.position.z));
				int index = j * WIDTH + i;
				vertices [index] = new Vector3 (i, map [i, j], j);
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
	}
}
