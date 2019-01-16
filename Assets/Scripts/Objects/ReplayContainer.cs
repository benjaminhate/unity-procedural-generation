using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayContainer : MonoBehaviour {

	public ReplayState state;
	public ReplayStruct updateValue;

	void Start(){
		if (state == null) {
			state = new ReplayState ();
			state.initPos = transform.position;
		}
	}

	public virtual void UpdateReplay(){

	}
}
