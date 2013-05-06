using UnityEngine;
using System.Collections;

public class SensorSwitch : BasicSensor {
	
	public Material unpressedMaterial;
	public Material pressedMaterial;
	public bool initPressed;
	
	public override void Start(){
		base.Start();
		this.pressed = initPressed;
		Renderer r = gameObject.transform.GetChild(0).renderer;
		Material[] m = new Material[2];
		if(pressed){
			m[0] = r.material;
			m[1] = pressedMaterial;
			Level.Singleton.SensorActivated();
		}else{
			m[0] = r.material;
			m[1] = unpressedMaterial;
			//Level.Singleton.SensorDeactivated();
		}
		r.materials = m;
	}
	
	public override void NotifyPressed (Vector3Int position)
	{
		
		Pressed = !Pressed;
		Renderer r = gameObject.transform.GetChild(0).renderer;
		Material[] m = new Material[2];
		if(pressed){
			m[0] = r.material;
			m[1] = pressedMaterial;
			Level.Singleton.SensorActivated();
		}else{
			m[0] = r.material;
			m[1] = unpressedMaterial;
			Level.Singleton.SensorDeactivated();
		}
		r.materials = m;
	}
	
	
	public override void NotifyUnpressed (Vector3Int position)
	{
	}
}
