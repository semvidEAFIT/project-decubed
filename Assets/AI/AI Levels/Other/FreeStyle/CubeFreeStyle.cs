using UnityEngine;
using System.Collections;

public class CubeFreeStyle : CubeAIBase {
	
	public override void StartCube ()
	{
		Vector3 a = Backward;
		while (true){
			Jump(a);
			a = RotateRight(a);
			a = RotateRight(a);
			a = RotateRight(a);
		}
	}	

}
