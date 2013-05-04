using UnityEngine;
using System.Collections;

public class SensorStart : BasicSensor {
	
	public override void NotifyPressed (Vector3Int position)
	{
		Application.LoadLevel("PlanetSelector");
	}
	
}
