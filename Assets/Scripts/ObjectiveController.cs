using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
			gameController.GetComponent<GameController> ().AddScore (1);
			gameController.GetComponent<GameController> ().finish = true;
		}
	}
}
