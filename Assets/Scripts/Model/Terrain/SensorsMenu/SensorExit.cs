using UnityEngine;
using System.Collections;

public class SensorExit : BasicSensor {
	
	public override void NotifyPressed (Vector3Int position)
	{
		Application.Quit();
	}
	
}
