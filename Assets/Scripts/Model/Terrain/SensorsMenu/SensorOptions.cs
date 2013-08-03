using UnityEngine;
using System.Collections;

public class SensorOptions : BasicSensor {
	
	public override void NotifyPressed (Vector3Int position)
	{
		Application.LoadLevel("Options");
	}

}
