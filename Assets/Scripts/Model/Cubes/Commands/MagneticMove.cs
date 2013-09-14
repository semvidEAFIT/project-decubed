using UnityEngine;
using System.Collections;

public class MagneticMove : Command {
	
	private Vector3 direction;

	public MagneticMove(Cube receiver, Vector3Int endPosition, Vector3 Direction) : base(receiver, endPosition){
    	this.direction = Direction;
	}
	
	public override void Execute()
    {
		base.Execute();
		MetalCube cube = (MetalCube)Cube;
		cube.Direction = direction;
		cube.rockFall = false;
		Cube.MoveTo(EndPosition);
    }
}
