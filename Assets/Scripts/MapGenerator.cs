using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public GameObject terrainPrefab;

	public int WIDTH;
	public int HEIGHT;
	
	void Start () {
		for (int i = 0; i < WIDTH; i++) {
			for (int j = 0; j < HEIGHT; j++) {
				int x = i * (terrainPrefab.GetComponent<TerrainCreator> ().WIDTH);
				int z = j * (terrainPrefab.GetComponent<TerrainCreator> ().HEIGHT);
				Instantiate (terrainPrefab, transform.position + new Vector3(x,0,z), transform.rotation);
			}
		}
	}
}
