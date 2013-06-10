using UnityEngine;
using System.Collections;

public class CubeConditional01 : CubeAIBase {
	
	public override void StartCube ()
	{
		while (!IsPressingSensor()){
			Move(Forward);
		}
	}	

}
