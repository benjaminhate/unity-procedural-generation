using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLOD : MonoBehaviour {
	public static int SIZE_W = 100;
	public static int SIZE_H = 100;

	public static float freq = 0.075f;
	public static float amplitude = 2.5f;

	public static float gaussian_amplitude = 50f;
	public static float gaussian_sigma = 140f;

	public static int GLOBAL_WIDTH = 10;
	public static int GLOBAL_HEIGHT = 10;

	public static Vector3 GetCenter(bool withHeight = true){
		float xMid = (ChunkLOD.GLOBAL_WIDTH / 2) * ChunkLOD.SIZE_W;
		float zMid = (ChunkLOD.GLOBAL_HEIGHT / 2) * ChunkLOD.SIZE_H;
		float yMid = 0;
		if (withHeight) {
			yMid = GetHeight (xMid, zMid);
		}
		return new Vector3 (xMid, yMid, zMid);
	}

	public static float Gaussian(float x, float z, float sigma, float amp){
		return amp * Mathf.Exp (-(Mathf.Pow (x, 2) + Mathf.Pow (z, 2)) / (2 * Mathf.Pow (sigma, 2)));
	}

	public static float GetHeight(float x, float z){
		Vector3 center = GetCenter (false);
		float amp = ChunkLOD.amplitude;
		float g_amp = ChunkLOD.gaussian_amplitude;
		float sigma = ChunkLOD.gaussian_sigma;
		float freq = ChunkLOD.freq;
		float g = Gaussian(x - center.x, z - center.z, sigma, g_amp);
		return amp * Mathf.PerlinNoise (freq * x, freq * z) + g;
	}
}
