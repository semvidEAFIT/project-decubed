using UnityEngine;
using System.Collections;

public class Ejercicio1Secuencias : CubeAIBase {

	public override void StartCube ()
	{
		Move(Backward);
		Move(Left);
		Move(Forward);
		Move(Forward);
		Move(Right);
		Move(Right);
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Left);
		Move(Left);
		Move(Left);
	}
}
