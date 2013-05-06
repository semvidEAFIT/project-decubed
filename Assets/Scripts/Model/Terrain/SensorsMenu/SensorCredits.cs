using UnityEngine;
using System.Collections;

public class SensorCredits : BasicSensor {
	
	public override void NotifyPressed (Vector3Int position)
	{
		Application.LoadLevel("Credits");
	}
}
