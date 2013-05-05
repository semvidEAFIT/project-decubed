using UnityEngine;
using System.Collections;

public class SensorProfiles : BasicSensor {
	
	public override void NotifyPressed (Vector3Int position)
	{
		Application.LoadLevel("ProfileSelector");
	}
	
}
