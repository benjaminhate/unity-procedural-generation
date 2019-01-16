using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {
    public const float DILATE = 0.1f;
    public const float WIDTH = 4;
    public Mesh SetMesh(Vector2 u)
    {
        int i,j;
        Mesh m = new Mesh();
        float len = u.magnitude;
        int L = Mathf.FloorToInt(len);
        Vector3[] vertices = new Vector3[2 * L + 4];
        int[] triangles = new int[3 * (vertices.Length-2)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector2 v = u.normalized;
        Vector2 orth = 4*new Vector2(-v.y, v.x); // orthogonal vector, for road width
        float x = this.transform.position.x;
        float y = this.transform.position.z;
        for (i = 0; i < len; i++)
        {
			vertices[2*i] = new Vector3(v.x*i - orth.x, ChunkLOD.GetHeight(x+v.x*i - orth.x, y+v.y*i - orth.y), v.y*i - orth.y);
			vertices[2*i + 1] = new Vector3(v.x*i + orth.x, ChunkLOD.GetHeight(x+v.x*i + orth.x, y+v.y*i + orth.y), v.y*i + orth.y);
            uv[2 * i] = new Vector2(0, i*DILATE);
            uv[2 * i + 1] = new Vector2(1, i*DILATE);
        }
		vertices[2 * i] = new Vector3(u.x  - orth.x, ChunkLOD.GetHeight(x + u.x - orth.x, y + u.y - orth.y),  u.y - orth.y);
		vertices[2 * i + 1] = new Vector3(u.x + orth.x, ChunkLOD.GetHeight(x + u.x + orth.x, y + u.y + orth.y), u.y + orth.y);
        uv[2 * i] = new Vector2(0, i*DILATE);
        uv[2 * i + 1] = new Vector2(1, i*DILATE);
        for (i = 0, j = 2; i <=L; i++,j+=2) {
            triangles[6 * i] = j - 1;
            triangles[6 * i + 1] = j+1;
            triangles[6 * i + 2] = j;
            triangles[6 * i + 3] = j - 2;
            triangles[6 * i + 4] = j-1;
            triangles[6 * i + 5] = j;
        }
  
        m.vertices = vertices;
        m.triangles = triangles;
        m.uv = uv;
        m.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = m;
        return m;
    }
}
