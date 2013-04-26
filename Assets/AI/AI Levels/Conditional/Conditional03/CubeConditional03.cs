using UnityEngine;
using System.Collections;

public class CubeConditional03 : CubeAIBase {
	
	public override void StartCube ()
	{
		Vector3 direction = Forward;
		while(!IsPressingSensor()){
			if(!IsAvailable(direction)){
				direction = RotateRight(direction);
			}
			Move(direction);
		}/*
		if (IsAvailable(Forward)){
			direction = Forward;
		}else if (IsAvailable(Right)){
			direction = Right;
		}else if (IsAvailable(Left)){
		
		}else {
		
		}*/
	}	

}
