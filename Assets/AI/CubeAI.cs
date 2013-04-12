using UnityEngine;
using System.Collections;

public class CubeAI : CubeAIBase {
	
	public override void StartCube ()
	{
		while(true){
				Move (Right);
				Move (Foward);

		}
	}
	
}
