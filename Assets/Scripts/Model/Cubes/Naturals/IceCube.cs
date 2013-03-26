using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		Level.Singleton.RemoveEntity(new Vector3Int(transform.position));
		//TODO:Fix Animation
		transform.position = nextPosition.ToVector3;
        Level.Singleton.AddEntity(this, nextPosition);
		Gravity(nextPosition);
		EndExecution();
	}
	
	public override Command[] GetOptions(){ 
		List<Command> options = new List<Command>();
			Vector3Int pos;
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.forward,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.forward)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.back,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.back)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.right,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.right)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.left,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.left)));
			}
            return options.ToArray();
	}
	
}
