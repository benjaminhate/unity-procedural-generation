using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayInput : ReplayContainer {

	public override void UpdateReplay ()
	{
		updateValue = new ReplayStruct (Input.GetAxis ("Vertical"), Input.GetAxis ("Horizontal"), Input.GetKey (KeyCode.LeftShift));
		state.listAttribute.Add (updateValue);
	} 
}
