using UnityEngine;
using System.Collections;

public class Ejercicio3Secuencias : CubeAIBase {

	public override void StartCube ()
	{
		for(int i=0; i<4; i++){
			Move(Backward);
		}
	}
}
