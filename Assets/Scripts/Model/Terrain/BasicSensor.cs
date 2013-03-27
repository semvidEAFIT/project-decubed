using UnityEngine;
using System.Collections;

public class BasicSensor : GameEntity {
	
	public bool pressed;
	public Vector3[] localDirections;
	
	void Awake ()
	{
		pressed = false;
	}
	
	// Update is called once per frame
	public override void Update ()
	{
	
	}
	
	public void NotifyPressed (Vector3Int position)
	{
		this.pressed = true;
		Level.Singleton.SensorActivated();
	}
	
	public void NotifyUnpressed (Vector3Int position)
	{
		this.pressed = false;
		Level.Singleton.SensorDeactivated();
	}
	
	#region Get and Sets	
	public Vector3[] Directions {
		get {
			Vector3[] newV = new Vector3[localDirections.Length];
			for (int i = 0; i < newV.Length; i++) {
				newV [i] = transform.TransformDirection (localDirections [i]);
			}
			return newV;
		}
		set {
			localDirections = value;
		}
	}

	public bool Pressed {
		get {
			return this.pressed;
		}
		set {
			pressed = value;
		}
	}
	#endregion
}
