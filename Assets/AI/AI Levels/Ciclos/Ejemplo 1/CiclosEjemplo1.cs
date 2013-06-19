using UnityEngine;
using System.Collections;

public class CiclosEjemplo1 : CubeAIBase {
	
	public override void StartCube ()
	{
		int i = 0;
		while (i < 10){
			if (IsAvailable(Right)){
				Move(Right);
			}else {
				Jump(Right);
			}
		}
	}	

}
