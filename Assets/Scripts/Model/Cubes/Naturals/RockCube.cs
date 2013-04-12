using UnityEngine;
using System.Collections;

public class RockCube : Cube {
	
//	private Vector3Int nextPosition;
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		base.MoveTo (nextPosition);
	//	this.nextPosition=nextPosition;
	}
	
	public override int GetJumpHeight ()
	{
		return 0;
	}
	
	public override void OnEndExecution ()
	{
		CloudFall();
		IceBreak();
	}
	
	public void CloudFall(){
		if(transform.position.y > 1 && Level.Singleton.getEntity(transform.position + Vector3.down) is CloudCube){
			Cube cl = (Cube)Level.Singleton.getEntity(transform.position+Vector3.down);
			if(!CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))||cl.transform.position.y==1){
				Level.Singleton.RemoveEntity(cl.transform.position);
				Level.Singleton.RemoveEntity(cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(cl,cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(this,cl.transform.position);
				Vector3 aux =cl.transform.position;
				//TODO cambiar transform por animacion
				this.transform.position = aux;
				cl.transform.position = aux + Vector3.up;
				CloudFall();
			}else if(CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))){
				Level.Singleton.RemoveEntity(transform.position);
				transform.position = cl.transform.position+Vector3.down;
				Level.Singleton.AddEntity(this,transform.position);
				Gravity(new Vector3Int(transform.position));
				CloudFall();
			}
		}
	}
	
	public void IceBreak(){
		Debug.Log(transform.position);
		if(transform.position.y > 1 && Level.Singleton.getEntity(transform.position + Vector3.down) is IceCube){	
			Cube ic = (Cube)Level.Singleton.getEntity(transform.position+Vector3.down);
			Level.Singleton.RemoveEntity(ic.transform.position);
			Level.Singleton.RemoveEntity(transform.position);
			Level.Singleton.AddEntity(this,ic.transform.position);
			Destroy(ic.gameObject);
			transform.position = transform.position+Vector3.down; 
			OnEndExecution();
		}
	}
}
