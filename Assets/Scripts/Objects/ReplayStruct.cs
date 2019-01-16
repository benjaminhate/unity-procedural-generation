using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayStruct {

	public Vector2 axis;
	public bool sprint;

	public ReplayStruct(float vertical, float horizontal, bool sprint){
		this.axis = new Vector2 (vertical, horizontal);
		this.sprint = sprint;
	}

	public static ReplayStruct zero = new ReplayStruct(0,0,false);
}
