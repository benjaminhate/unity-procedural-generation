using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReplayContainer))]
public class PlayerController : MonoBehaviour {

	public float speed;
	public float rotSpeed;
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<ReplayContainer> ().UpdateReplay ();
		Move ();
	}

	void Move () {
		ReplayStruct value = GetComponent<ReplayContainer> ().updateValue;
		float vertical = value.axis.x;
		float horizontal = value.axis.y;

		float cur_speed = speed;
		if (value.sprint) {
			cur_speed *= 2;
		}

		transform.Translate (0, 0, vertical * cur_speed * Time.deltaTime);
		transform.Rotate (0, horizontal * rotSpeed * Time.deltaTime, 0);
	}
}
