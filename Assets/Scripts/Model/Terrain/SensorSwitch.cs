using UnityEngine;
using System.Collections;

public class SensorSwitch : BasicSensor {
	
	public Material pressedMaterial;
	
	public override void NotifyPressed (Vector3Int position)
	{
		if(!pressed){
			base.NotifyPressed (position);
		}
	}
	
	public override void NotifyUnpressed (Vector3Int position)
	{
		gameObject.transform.GetChild(0).renderer.material = pressedMaterial;
	}
}
