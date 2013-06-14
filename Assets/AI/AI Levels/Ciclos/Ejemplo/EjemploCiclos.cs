using UnityEngine;
using System.Collections;

public class EjemploCiclos : CubeAIBase {
	
	public override void StartCube ()
	{
		while(!IsPressingSensor()){
			Move(Right);
		}
		SetMood(Mood.Proud);
	}	

}
