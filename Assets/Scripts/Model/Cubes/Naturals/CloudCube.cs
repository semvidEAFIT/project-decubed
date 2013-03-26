using UnityEngine;
using System.Collections.Generic;

public class CloudCube : Cube {
	
	private bool stuck = false;
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		Vector3Int below = new Vector3Int(nextPosition.x,nextPosition.y,nextPosition.z);
		below.y--;
		if(stuck){
			stuck = false;
		}
		if(CubeHelper.IsFree(below)&& below.y>0){
			stuck = true;
		}
		
		base.MoveTo (nextPosition);
	}
	
	
	public override Command[] GetOptions(){ 
		List<Command> commands = new List<Command>();
		if(!stuck){
			commands = CubeHelper.GetListOptions(base.GetOptions());	
			Vector3Int pos;
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.forward,out pos,GetJumpHeight())){
				if(new Vector3Int(transform.position).y-pos.y>1){
				 	pos = new Vector3Int(transform.position+Vector3.forward);
					pos.y--;
					commands.Add(new Move(this,pos));
				}
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.back,out pos,GetJumpHeight())){
				if(new Vector3Int(transform.position).y-pos.y>1){
				 	pos = new Vector3Int(transform.position+Vector3.back);
					pos.y--;
					commands.Add(new Move(this,pos));
				}
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.right,out pos,GetJumpHeight())){
				if(new Vector3Int(transform.position).y-pos.y>1){
				 	pos = new Vector3Int(transform.position+Vector3.right);
					pos.y--;
					commands.Add(new Move(this,pos));
				}
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.left,out pos,GetJumpHeight())){
				if(new Vector3Int(transform.position).y-pos.y>1){
				 	pos = new Vector3Int(transform.position+Vector3.left);
					pos.y--;
					commands.Add(new Move(this,pos));
				}
			}
			
		}else{
			Vector3Int pos;
			if(CubeHelper.CheckAvailablePosition(transform.position + Vector3.down,out pos,GetJumpHeight())){
				commands.Add(new Move(this,pos));
			}
		}
		return commands.ToArray();
	}
}
