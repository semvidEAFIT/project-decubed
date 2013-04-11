using UnityEngine;
using System.Collections;

public class EducationalCube : EducationalCubeHelper{
	
	private int count = 0;
	
	public override void Comandos ()
	{
		switch(count){
		case 0: MoveTo(Foward);
			count++;
			break;
		case 1: MoveTo(Right);
			count++;
			break;
		case 2: MoveTo(Backward);
			count++;
			break;
		case 3: MoveTo(Left);
			count = 0;
			break;
		}
	}
}
