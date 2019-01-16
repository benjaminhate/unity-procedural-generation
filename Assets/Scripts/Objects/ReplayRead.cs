using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayRead : ReplayContainer {

	int i = 0;

	public override void UpdateReplay ()
	{
		if (i >= state.listAttribute.Count) {
			updateValue = ReplayStruct.zero;
		} else {
			updateValue = state.listAttribute [i++];
		}
	}
}
