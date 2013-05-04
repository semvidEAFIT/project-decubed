using UnityEngine;
using System.Collections;

public class SensorSwitch : BasicSensor {
	
	public Material unpressedMaterial;
	public Material pressedMaterial;
	public bool initPressed;
	
	public override void Start(){
		base.Start();
		this.pressed = initPressed;
		if(pressed){
			gameObject.transform.GetChild(0).renderer.material = pressedMaterial;
			Level.Singleton.SensorActivated();
		}else{
			gameObject.transform.GetChild(0).renderer.material = unpressedMaterial;
			//Level.Singleton.SensorDeactivated();
		}
	}
	
	public override void NotifyPressed (Vector3Int position)
	{
		
		Pressed = !Pressed;
		if(pressed){
			gameObject.transform.GetChild(0).renderer.material = pressedMaterial;
			Level.Singleton.SensorActivated();
		}else{
			gameObject.transform.GetChild(0).renderer.material = unpressedMaterial;
			Level.Singleton.SensorDeactivated();
		}
	}
	
	
	public override void NotifyUnpressed (Vector3Int position)
	{
	}
}
