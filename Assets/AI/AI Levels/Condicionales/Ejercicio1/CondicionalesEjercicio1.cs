using UnityEngine;
using System.Collections;

public class CondicionalesEjercicio1: CubeAIBase {
	
	public override void StartCube ()
	{
		if(!IsAvailable(Forward)){
			Move(Backward);	
		}else{
			if(!IsAvailable(Backward)){
				Move(Forward);	
			}else{
				if(!IsAvailable(Left)){
					Move(Right);	
				}else{
					Move(Left);
				}
			}
		}
	}	

}
