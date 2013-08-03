using UnityEngine;
using System.Collections;

public class Move : Command {

    public Move(Cube receiver, Vector3Int endPosition) : base(receiver, endPosition){
    }

    public override void Execute()
    {
		base.Execute();
		Cube.MoveTo(EndPosition);
    }
}
	