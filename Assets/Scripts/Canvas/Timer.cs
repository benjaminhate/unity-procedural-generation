using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameObject gameController;

	void Update(){
		float time = gameController.GetComponent<GameController> ().GetTime ();
		GetComponent<Text> ().text = time.ToString ();
	}
}
