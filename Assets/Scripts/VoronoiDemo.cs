using UnityEngine;
using System.Collections.Generic;
using Delaunay;
using Delaunay.Geo;

public class VoronoiDemo : MonoBehaviour
{

    public Material land;
    public Texture2D tx;
	public GameObject roadPrefab;
	public GameObject[] buildingPrefabs;

	[Range(0,1)]
	public float probA = 0.8f;
	[Range(0,1)]
	public float probB = 0.5f;

    public const int NPOINTS = 10;
    public const int WIDTH = 200;
    public const int HEIGHT = 200;

    private List<Vector2> m_points;
	private List<LineSegment> m_edges = null;
	private List<LineSegment> m_spanningTree;
	private List<LineSegment> m_delaunayTriangulation;

    private float [,] createMap() 
    {
        float [,] map = new float[WIDTH, HEIGHT];
		for (int i = 0; i < WIDTH; i++)
			for (int j = 0; j < HEIGHT; j++)
				//map [i, j] = (i > 10 && i < 100 && j > 10 && j < 100) ? 1 : 0;
                map[i, j] = Mathf.PerlinNoise(0.02f * i + 0.43f, 0.018f * j + 0.22f);
        return map;
    }

	private Vector2 randomPoint(float[,] map){
		int max_recur = 200;
		Vector2 vec;
		float r;
		do {
			max_recur--;
			r = Random.value;
			vec = new Vector2 (Random.Range (0, WIDTH - 1), Random.Range (0, HEIGHT - 1));
		} while(r > map[(int)vec.x,(int)vec.y] && max_recur > 0);
		return vec;
	}

	private void GenerateRandomPoints(float[,] map,List<Vector2> points,int NPOINTS){
		for (int i = 0; i < NPOINTS; i++) {
			Vector2 vec = randomPoint (map);
			points.Add (vec);
		}
	}

	private void GenerateGridPoints(List<Vector2> points,Vector2 center,int N,int M){
		for (int i = -N/2; i < N/2; i++) {
			for (int j = -M/2; j < M/2; j++) {
				int x = (int) i*N/2;
				int y = (int) j*M/2;
				Vector2 vec = center + new Vector2 (x,y);
				points.Add (vec);
			}
		}
	}

	private void GenerateCirclePoints(List<Vector2> points,Vector2 center,int N,int M,int R){
		for (int j = 0; j < M; j++) {
			for (int i = 0; i < N; i++) {
				float angle = 2 * Mathf.PI * i/N;
				int x = (int)(j*R*Mathf.Cos(angle));
				int y = (int)(j*R*Mathf.Sin(angle));
				Vector2 vec = center + new Vector2 (x,y);
				points.Add (vec);
			}
		}
	}

	private void GenerateCity(float[,] map, List<Vector2> points){
		GenerateRandomPoints (map, points, NPOINTS);
		int N = (int)(0.05 * WIDTH);
		int M = (int)(0.05 * HEIGHT);
		int R = (int)(0.01 * (WIDTH + HEIGHT));
		foreach (Vector2 point in points.ToArray()) {
			float prob = map [(int)point.x, (int)point.y];
			if (prob > probA) {
				GenerateCirclePoints (points, point, N, M, R);
			}
			if (prob > probB && prob <= probA) {
				GenerateGridPoints (points, point, N, M);
			}
		}
	}

	private void Edges(float[,] map, Color[] pixels){
		m_points = new List<Vector2> ();
		List<uint> colors = new List<uint> ();
		GenerateCity(map,m_points);
		for (int i = 0; i < m_points.Count; i++) {
			colors.Add ((uint)0);
		}
		Delaunay.Voronoi v = new Delaunay.Voronoi (m_points, colors, new Rect (0, 0, WIDTH, HEIGHT));
		m_edges = v.VoronoiDiagram ();
		m_spanningTree = v.SpanningTree (KruskalType.MINIMUM);
		m_delaunayTriangulation = v.DelaunayTriangulation ();
		Color color = Color.red;
		/* Shows Voronoi diagram */
		/*for (int i = 0; i < m_edges.Count; i++) {
			LineSegment seg = m_edges [i];				
			Vector2 left = (Vector2)seg.p0;
			Vector2 right = (Vector2)seg.p1;
			DrawLine (pixels,left, right,color);
		}*/
		/* Shows Delaunay triangulation */
		for (int i = 0; i < m_edges.Count; i++) {
			LineSegment seg = m_edges [i];				
			Vector2 left = (Vector2)seg.p0;
			Vector2 right = (Vector2)seg.p1;
			//DrawLine (pixels,left, right,color);

			/*ChunkLOD prefabLOD = mapGen.terrainPrefab.GetComponent<ChunkLOD> ();
			float chunkSize = prefabLOD.SIZE_W;
			float terrainSize = mapGen.WIDTH;
			float SIZE = WIDTH;

			float length = (right - left).magnitude  * (terrainSize * chunkSize) / (float) SIZE;
			float angle = Vector2.Angle (right - left, Vector2.right);*/
			float scaleFactor = ChunkLOD.GLOBAL_WIDTH * ChunkLOD.SIZE_W / WIDTH;

			GameObject road = Instantiate (roadPrefab, new Vector3 (scaleFactor * left.x, 0.2f, scaleFactor * left.y), Quaternion.identity);
			Road r = road.GetComponent<Road> ();
			r.SetMesh (scaleFactor * (right - left));
		}
	}

	private float[,] map;
	private Color[] pixels;

	void Start ()
	{
        map = createMap();
       	pixels = createPixelMap(map);

        /* Create random points points */
		//m_points = new List<Vector2> ();
		//List<uint> colors = new List<uint> ();
		/* Randomly pick vertices */
		//GenerateRandomPoints (map, m_points, NPOINTS);
		//GenerateGridPoints (m_points, N, M);
		//GenerateCirclePoints(m_points,N,M,R);
		/* Generate Graphs */
		

		//Color color = Color.blue;
		/* Shows Voronoi diagram */
		/*for (int i = 0; i < m_edges.Count; i++) {
			LineSegment seg = m_edges [i];				
			Vector2 left = (Vector2)seg.p0;
			Vector2 right = (Vector2)seg.p1;
			DrawLine (pixels,left, right,color);
		}*/
		Edges (map, pixels);
		for (int i = 0; i < m_points.Count; i++) {
			float scaleFactor = ChunkLOD.GLOBAL_WIDTH * ChunkLOD.SIZE_W / WIDTH;
			float maxX = ChunkLOD.GLOBAL_WIDTH * ChunkLOD.SIZE_W;
			float maxY = ChunkLOD.GLOBAL_HEIGHT * ChunkLOD.SIZE_H;
			Vector2 point = scaleFactor * m_points [i];
			if(point.x <= maxX && point.x >= 0 && point.y <= maxY && point.y >= 0){
				float value = map [(int)m_points [i].x - 1, (int)m_points [i].y - 1];
				if (value >= probA) {
					GameObject building = Instantiate (buildingPrefabs [0], new Vector3 (point.x, ChunkLOD.GetHeight (point.x, point.y), point.y), Quaternion.identity);
					building.transform.localScale *= 600;
				}
				if (value < probA && value >= probB) {
					GameObject building = Instantiate (buildingPrefabs [1], new Vector3 (point.x, ChunkLOD.GetHeight (point.x, point.y), point.y), Quaternion.identity);
					building.transform.localScale *= 600;
				}
				if (value < probB) {
					GameObject building = Instantiate (buildingPrefabs [2], new Vector3 (point.x, ChunkLOD.GetHeight (point.x, point.y), point.y), Quaternion.identity);
					building.transform.localScale *= 400;
				}
			}
		}

		Color color = Color.red;

        /* Shows spanning tree */
        
		color = Color.black;
		/*if (m_spanningTree != null) {
			for (int i = 0; i< m_spanningTree.Count; i++) {
				LineSegment seg = m_spanningTree [i];				
				Vector2 left = (Vector2)seg.p0;
				Vector2 right = (Vector2)seg.p1;
				DrawLine (pixels,left, right,color);
			}
		}*/
        /* Apply pixels to texture */
        tx = new Texture2D(WIDTH, HEIGHT);
        land.SetTexture ("_SplatTex", tx);
		tx.SetPixels (pixels);
		tx.Apply ();

	}



    /* Functions to create and draw on a pixel array */
    private Color[] createPixelMap(float[,] map)
    {
        Color[] pixels = new Color[WIDTH * HEIGHT];
        for (int i = 0; i < WIDTH; i++)
            for (int j = 0; j < HEIGHT; j++)
            {
				pixels [i * HEIGHT + j] = Color.green;//Color.Lerp(Color.black, Color.white, map[i, j]);
            }
        return pixels;
    }
    private void DrawPoint (Color [] pixels, Vector2 p, Color c) {
		if (p.x<WIDTH&&p.x>=0&&p.y<HEIGHT&&p.y>=0) 
		    pixels[(int)p.x*HEIGHT+(int)p.y]=c;
	}
	// Bresenham line algorithm
	private void DrawLine(Color [] pixels, Vector2 p0, Vector2 p1, Color c) {
		int x0 = (int)p0.x;
		int y0 = (int)p0.y;
		int x1 = (int)p1.x;
		int y1 = (int)p1.y;

		int dx = Mathf.Abs(x1-x0);
		int dy = Mathf.Abs(y1-y0);
		int sx = x0 < x1 ? 1 : -1;
		int sy = y0 < y1 ? 1 : -1;
		int err = dx-dy;
		while (true) {
            if (x0>=0&&x0<WIDTH&&y0>=0&&y0<HEIGHT)
    			pixels[x0*HEIGHT+y0]=c;

			if (x0 == x1 && y0 == y1) break;
			int e2 = 2*err;
			if (e2 > -dy) {
				err -= dy;
				x0 += sx;
			}
			if (e2 < dx) {
				err += dx;
				y0 += sy;
			}
		}
	}
}