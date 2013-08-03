using UnityEngine;
using System.Collections;

public class CubeAI : CubeAIBase {
	
	public override void StartCube ()
	{
		Move (5, Right);
		Move (5, Backward);
		Move (5, Left);
		Move (5, Forward);
	}	
	
	public void Move (int times, Vector3 direction)
	{
		for (int i = 0; i < times; i++) {
			Move (direction);
		}
	}
	
	
}
