using UnityEngine;
using System.Collections;

public class Ejercicio2Funciones : CubeAIBase {
	
	public override void StartCube ()
	{
		bool lookingForSensor = true;
		
		while (lookingForSensor){
		
			if (IsPressingSensor()){
				lookingForSensor = false;
			}else {
				Move(Right);
			}
			
		}
	}	
}
