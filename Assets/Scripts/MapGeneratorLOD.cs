using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorLOD : MonoBehaviour {

	public GameObject terrainPrefab;
	public GameObject enseirbPrefab;

	public int WIDTH;
	public int HEIGHT;

	void Start () {
		ChunkLOD.GLOBAL_HEIGHT = HEIGHT;
		ChunkLOD.GLOBAL_WIDTH = WIDTH;
		for (int i = 0; i < WIDTH; i++) {
			for (int j = 0; j < HEIGHT; j++) {
				int x = i * (ChunkLOD.SIZE_W);
				int z = j * (ChunkLOD.SIZE_H);
				Instantiate (terrainPrefab, transform.position + new Vector3(x,0,z), Quaternion.identity);
			}
		}
		Vector3 center = ChunkLOD.GetCenter ();
		center.y += enseirbPrefab.transform.localScale.y / 2;
		Instantiate (enseirbPrefab, center, Quaternion.identity);
	}
}
