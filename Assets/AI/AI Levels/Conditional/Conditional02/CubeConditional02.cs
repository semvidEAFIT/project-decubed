using UnityEngine;
using System.Collections;

public class CubeConditional02 : CubeAIBase {
	
	public override void StartCube ()
	{
		Vector3 a = Forward;
		while (!IsGameOver()){
			Move(a);
			if(IsPressingSensor()){
				a = RotateRight(a);
			}
		}
	}	

}
