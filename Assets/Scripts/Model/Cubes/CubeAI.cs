using UnityEngine;
using System.Collections;

public class CubeAI : CubeAIBase {
	
	
	
	public override void main ()
	{
		while(true){
			while (IsAvailable(Right)) {
				Move (Right);
			}
			while (IsAvailable(Foward)) {
				Move (Foward);
			}
		}
	}
	
}
