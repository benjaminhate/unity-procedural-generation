using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public float timeGame = 60f;
	public GameObject playerPrefab;

	public bool finish = false;

	private float timeBegin;
	[SerializeField]
	private int score = 0;

	[SerializeField]
	List<ReplayState> states = new List<ReplayState> ();
	List<GameObject> players = new List<GameObject> ();
	int playerAI = 0;

	// Use this for initialization
	void Start () {
		BeginGame ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GetTime() < 0) {
			FinishGame ();
		}
		if (finish) {
			finish = false;
			SaveGame ();
			EndGame ();
			BeginGame ();
		}
	}

	public float GetTime(){
		return timeGame - (Time.time - timeBegin);
	}

	void BeginGame(){
		for (int i = 0; i < playerAI; i++) {
			CreatePlayer (states[i].initPos, false, states[i]);
		}
		CreatePlayer (RandomPosition (), true, null);
		timeBegin = Time.time;
	}

	void SaveGame(){
		states.Add (players [players.Count - 1].GetComponent<ReplayInput> ().state);
		playerAI += 1;
	}

	void EndGame(){
		for (int i = 0; i < players.Count; i++) {
			Destroy (players [i]);
		}
	}

	void FinishGame(){
		Debug.Log ("END");
		EndGame ();
	}

	void CreatePlayer(Vector3 pos, bool attachCamera, ReplayState state){
		Vector3 center = ChunkLOD.GetCenter ();
		Quaternion rotation = Quaternion.LookRotation (center - pos, Vector3.up);
		GameObject player = Instantiate (playerPrefab, pos, rotation);
		Destroy (player.GetComponent<ReplayContainer> ());
		if (attachCamera) {
			player.AddComponent<ReplayInput> ();
			player.GetComponent<ReplayInput> ().state = state;
			player.GetComponentInChildren<Camera> ().gameObject.SetActive (true);
			player.tag = "Player";
		} else {
			player.AddComponent<ReplayRead> ();
			player.GetComponent<ReplayRead> ().state = state;
			player.GetComponentInChildren<Camera> ().gameObject.SetActive (false);
		}
		players.Add (player);
	}

	Vector3 RandomPosition(){
		Vector3 pos = Vector3.zero;

		pos.x = Random.Range (10, ChunkLOD.GLOBAL_WIDTH * ChunkLOD.SIZE_W - 10);
		pos.z = Random.Range (10, ChunkLOD.GLOBAL_HEIGHT * ChunkLOD.SIZE_H - 10);
		pos.y = ChunkLOD.GetHeight (pos.x, pos.z) + 2;

		return pos;
	}

	public void AddScore(int _score){
		score += _score;
	}
	public int GetScore(){
		return score;
	}
}
