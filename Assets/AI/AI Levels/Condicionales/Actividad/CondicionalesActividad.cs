using UnityEngine;
using System.Collections;

public class CondicionalesActividad : CubeAIBase {
	
	public override void StartCube ()
	{
		if(IsAvailable(Backward)){
			Move(Backward);
		}
		if(IsAvailable(Backward)){
			Move(Backward);
		}
		Jump(Backward);
		Move(Backward);
		Move(Backward);
	}	

}
