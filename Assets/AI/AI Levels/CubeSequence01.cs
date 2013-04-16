using UnityEngine;
using System.Collections;

public class CubeSequence01 : CubeAIBase {
	
	public override void StartCube ()
	{
		Move(Foward);
		Move(Right);
		Move(Backward);
	}	

}
