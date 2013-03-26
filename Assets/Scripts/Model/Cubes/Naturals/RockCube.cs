using UnityEngine;
using System.Collections;

public class RockCube : Cube {
	
	private Vector3Int nextPosition;
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		base.MoveTo (nextPosition);
		Debug.Log ("movetorock");
		this.nextPosition=nextPosition;
	}
	public override int GetJumpHeight ()
	{
		return 0;
	}
	
	public override void OnEndExecution ()
	{
		if(Level.Singleton.getEntity(nextPosition.ToVector3 + Vector3.down) is CloudCube){
			Cube cl = (Cube)Level.Singleton.getEntity(nextPosition.ToVector3+Vector3.down);
			if(!CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))||cl.transform.position.y==1){
				Level.Singleton.RemoveEntity(cl.transform.position);
				Debug.Log(transform.position.x+","+transform.position.y+","+transform.position.z);
				Level.Singleton.RemoveEntity(cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(cl,cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(this,cl.transform.position);
				Vector3 aux =cl.transform.position;//new Vector3(cl.transform.position.x,cl.transform.position.y,cl.transform.position.z);
				//TODO cambiar transform por animacion
				this.transform.position = aux;
				cl.transform.position = aux + Vector3.up;
			}else if(CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))){
				Level.Singleton.RemoveEntity(transform.position);
				transform.position = cl.transform.position+Vector3.down;
				Level.Singleton.AddEntity(this,transform.position);
				Gravity(new Vector3Int(transform.position));
			}
		}
	}

}
