using UnityEngine;
using System.Collections;

public class Slide : Command {
	
	private Vector3Int Direction;

	public Slide(Cube receiver, Vector3Int endPosition, Vector3Int Direction) : base(receiver, endPosition){
    	this.Direction = Direction;
	}
	
	public override void Execute()
    {
		if(SlideTo()){
			Cube.MoveTo(EndPosition);
		}
    }
	
	public bool SlideTo(){
		Vector3Int next = new Vector3Int(EndPosition.x+Direction.x,EndPosition.y+Direction.y,EndPosition.z+Direction.z);
		if(CubeHelper.IsFree(next) && next.x <= 10 && next.x >= 0 && next.z <= 10 && next.z >= 0){
			EndPosition = next;
			return SlideTo();
		}else if(next.x > 10 || next.x < 0 || next.z > 10 || next.z < 0){
				Cube.FallOutOfBounds(next.ToVector3);
				return false;
				
		}else{
			return true;
		}
	}
}
