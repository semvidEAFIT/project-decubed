using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bounce : Command
{
	private Vector3Int position;
	private List<Vector3Int> positions;
		
	public Bounce (Cube receiver, Vector3Int endPosition, Vector3Int direction): base(receiver, endPosition)
	{
		positions = new List<Vector3Int>(); 
		position = new Vector3Int (Cube.transform.position);
		Cube = receiver;
		if (endPosition.y > 1 && Level.Singleton.getEntity (endPosition.ToVector3 + Vector3.down) is RubberCube) {
			FindEndPosition (endPosition, direction, 2);
		} else {
			FindEndPosition (endPosition, direction, 1);
		}
	}
	
	public void FindEndPosition (Vector3Int endPosition, Vector3Int direction, int multiplier)
	{	
		positions.Add(endPosition);
		//Debug.Log(endPosition);
		int jumpHeight = ((position.y - endPosition.y)*multiplier)+1;
		if (jumpHeight > 0) {
			Vector3Int aux = new Vector3Int (endPosition.ToVector3 + direction.ToVector3);
			aux.y += jumpHeight;
			aux = CubeHelper.GetTopPosition (aux.ToVector3);
			if (aux.y > endPosition.y) {
				if (aux.y < (endPosition.y + jumpHeight)) {
					EndPosition = aux;
				}
			} else if (aux.y < endPosition.y) {
				position=endPosition;
				EndPosition = aux;
				if (endPosition.y > 1 && Level.Singleton.getEntity (aux.ToVector3 + Vector3.down) is RubberCube) {
					FindEndPosition (aux, direction, 2);
				} else {
					FindEndPosition (aux, direction, 1);
				}	
			}else{	
				EndPosition = endPosition;
			}
		}else{
			EndPosition = endPosition;
		}
		
	}
	public override void Execute()
    {
		base.Execute();
		((RubberCube)Cube).Positions = positions;
		Cube.MoveTo(EndPosition);
			
    }
}
