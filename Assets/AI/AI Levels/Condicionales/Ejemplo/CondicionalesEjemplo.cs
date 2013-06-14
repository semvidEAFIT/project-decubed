using UnityEngine;
using System.Collections;

public class CondicionalesEjemplo : CubeAIBase {
	
	public override void StartCube ()
	{
		Move(Backward);
		Move(Backward);
		Move(Backward);
		Move(Backward);
		if(IsPressingSensor()){
			SetMood(Mood.Happy);
		}else{
			SetMood(Mood.Angry);
		}
	}	

}
