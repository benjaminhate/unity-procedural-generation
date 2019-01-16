using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public GameObject gameController;

	void Update(){
		int score = gameController.GetComponent<GameController> ().GetScore ();
		GetComponent<Text> ().text = "Score : " + score.ToString ();
	}
}
