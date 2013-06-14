using UnityEngine;
using System.Collections;

public class Actividad2Secuencias : CubeAIBase {
	
	public override void StartCube ()
	{
		SetMood(Mood.Proud);
		//crear funcion para esperar
		SetMood(Mood.Happy);
		SetMood(Mood.EyesClosed);
		SetMood(Mood.Happy);
	}	

}
