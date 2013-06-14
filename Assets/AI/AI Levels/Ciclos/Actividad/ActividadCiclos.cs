using UnityEngine;
using System.Collections;

public class ActividadCiclos : CubeAIBase {
	
	public override void StartCube ()
	{
		Vector3 direction = Right;
		for(int i=0; i<4; i++){
			for(int j=0; j<3; j++){
				Move(direction);
			}
			if(i < 3){
				Jump(direction);	
			}else{
				Move(direction);	
			}
			direction = Vector3.Cross(Down, direction);
		}
		
	}	

}
