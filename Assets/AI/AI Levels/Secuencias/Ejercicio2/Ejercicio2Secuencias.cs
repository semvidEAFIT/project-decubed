using UnityEngine;
using System.Collections;

public class Ejercicio2Secuencias : CubeAIBase {

	public override void StartCube ()
	{
		Move(Backward);
		Move(Backward);
		Jump(Backward);
		Move(Right);
		Jump(Right);
		Move(Forward);
		Jump(Forward);
		Move(Left);
		Move(Backward);
	}
}
