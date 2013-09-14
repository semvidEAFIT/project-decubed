using UnityEngine;
using System.Collections;

public class Swing : Command {
	
	public Swing(Cube receiver, Vector3Int endPosition) : base(receiver, endPosition){
    
	}
	
	public override void Execute ()
	{
		HookCube hc = (HookCube)Cube;
		hc.HookMove = true;
		base.Execute ();
		hc.MoveTo(EndPosition);
	}
}
