using UnityEngine;
using System.Collections;

public class RockCube : Cube {
	
//	private Vector3Int nextPosition;
	private CloudCube cl;
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
		if(cl!=null){
			cl.renderer.enabled = true;
			cl = null;
		}
		CloudFall();
		IceBreak();
		PlayMovement();
	}
	
	public void CloudFall(){
		if(transform.position.y > 1 && Level.Singleton.getEntity(transform.position + Vector3.down) is CloudCube){
			CloudCube cl = (CloudCube)Level.Singleton.getEntity(transform.position+Vector3.down);
			if(!CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))||cl.transform.position.y==1){
				Level.Singleton.RemoveEntity(cl.transform.position);
				Level.Singleton.RemoveEntity(cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(cl,cl.transform.position+Vector3.up);
				Level.Singleton.AddEntity(this,cl.transform.position);
				Vector3 aux =cl.transform.position;
				this.cl=cl;
				CubeAnimations.AnimateMove (gameObject, Vector3.down, aux);
				cl.renderer.enabled=false;
				//CubeControllerInput cc = cl.gameObject.GetComponent<CubeControllerInput> ();
				//cc.NotifyMoveTO (new Move (cl, new Vector3Int( cl.transform.position + Vector3.up)));
				//TODO cambiar transform por animacion
				//CubeAnimations.AnimateMove (cl.gameObject, Vector3.down, aux + Vector3.up);
				//this.transform.position = aux;
				cl.transform.position = aux + Vector3.up;
			}else if(CubeHelper.IsFree(new Vector3Int(cl.transform.position+Vector3.down))){
				Level.Singleton.RemoveEntity(transform.position);
				this.cl = cl;
				cl.renderer.enabled=false;
				Debug.Log(transform.position+Vector3.down+Vector3.down);
				//transform.position = cl.transform.position + Vector3.down;
				Level.Singleton.AddEntity(this,transform.position+Vector3.down+Vector3.down);
				//CubeAnimations.AnimateMove (gameObject, Vector3.down, cl.transform.position + Vector3.down);
				Gravity(new Vector3Int(transform.position+Vector3.down+Vector3.down));
				//CloudFall();
			}
		}
	}
	
	public void IceBreak(){
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
	
	public override int GetMoodSequence (Mood mood)
	{
		switch(mood){
		case Mood.Normal:
			return 1;
		case Mood.Proud:
			return 3;
		case Mood.EyesClosed:
			return 2;
		case Mood.Happy:
			return 0;
		case Mood.Angry:
			return 2;
		default: 
			return 0;
		}
	}
}
