using UnityEngine;
using System.Collections;

public class Slide : Command {
	
	private Vector3Int Direction;
	private bool finished = false;

	public Slide(Cube receiver, Vector3Int endPosition, Vector3Int Direction) : base(receiver, endPosition){
    	this.Direction = Direction;
	}
	
	public override void Execute()
    {
		base.Execute();
		IceCube ic = (IceCube)Cube;
		ic.NextPosition = EndPosition;
		SlideTo(true);
		Cube.MoveTo(EndPosition);
		
    }
	
	public bool SlideTo(bool first){
		Vector3Int next = new Vector3Int(EndPosition.x+Direction.x,EndPosition.y+Direction.y,EndPosition.z+Direction.z);
		if(CubeHelper.IsFree(next) && Cube.transform.position+Direction.ToVector3+Vector3.up!=  EndPosition.ToVector3){
			Level.Singleton.notifySwitches(EndPosition);
		}
		if (EndPosition.y == new Vector3Int (Cube.transform.position).y+1){
			return true;
		}
		
		if(!finished && CubeHelper.IsFree(next) && next.x <= 10 && next.x >= 0 && next.z <= 10 && next.z >= 0){
			EndPosition = next;
			return SlideTo(false);
		}else if(next.x > 10 || next.x < 0 || next.z > 10 || next.z < 0){
				//Cube.FallOutOfBounds(next.ToVector3);
				EndPosition = next;
				return true;
				
		}
		if(Level.Singleton.getEntity(next.ToVector3) is RockCube ){
			IceCube ic = (IceCube)Cube;
			ic.Break();
			return true;
		}
		while(CubeHelper.IsFree(new Vector3Int(EndPosition.ToVector3 + Vector3.down)) && EndPosition.y > 1){
			Level.Singleton.notifySwitches(EndPosition);
			EndPosition = new Vector3Int(EndPosition.ToVector3 + Vector3.down);
		}
		return true;
		
	}
}
