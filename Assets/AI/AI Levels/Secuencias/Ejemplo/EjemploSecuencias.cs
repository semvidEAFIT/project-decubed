using UnityEngine;
using System.Collections;

public class EjemploSecuencias: CubeAIBase {
	
	public override void StartCube ()
	{
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Forward);
		Move(Forward);
		Move(Left);
		Move(Left);
	}	

}
