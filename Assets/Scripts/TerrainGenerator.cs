using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	public int width;
	public int height;
	public int depth;

	private Terrain terrain;
	private float[,] heights;

	[Range(0.001f,0.025f)]
	public float freq = 0.01f;
	[Range(0.01f,1f)]
	public float amplitude = 0.5f;
	[Range(1,5)]
	public int octave = 5;
	[Range(0.01f,1f)]
	public float frameRate = 0.1f;
	public float pow = 2;

	private float frame = 0;

	// Use this for initialization
	void Start () {
		terrain = GetComponent<Terrain> ();
		heights = new float[width, height];

		terrain.terrainData.size = new Vector3 (width, depth, height);
		terrain.terrainData.heightmapResolution = width + 1;
	}

	void Update(){
		frame += frameRate;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				heights [i, j] = 0;
				for (int k = 0; k < octave; k++) {
					float f = freq * Mathf.Pow (pow, k);
					heights [i, j] += (amplitude / Mathf.Pow (pow, k)) * Mathf.PerlinNoise (f * i + frame, f * j + frame);
				}
			}
		}
		terrain.terrainData.SetHeights (0, 0, heights);
	}
}
