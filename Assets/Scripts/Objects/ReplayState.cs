using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReplayState {

	public Vector3 initPos = Vector3.zero;
	public List<ReplayStruct> listAttribute = new List<ReplayStruct> ();
}
