using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HookCube : Cube {
	
	private bool hookMove = false;
	private Vector3 direction;
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		
		
		if(!hookMove){
			base.MoveTo (nextPosition);
		}else{
			if(Level.Singleton.getEntity(nextPosition.ToVector3+direction) is Cube){
				Cube c = (Cube)Level.Singleton.getEntity(nextPosition.ToVector3+direction);
				Level.Singleton.RemoveEntity(new Vector3Int(c.transform.position));
				Level.Singleton.AddEntity(this, nextPosition);
				CubeAnimations.AnimetSwing(gameObject,nextPosition.ToVector3);
				hookMove = false;
			}else{
				Level.Singleton.RemoveEntity(new Vector3Int(transform.position));
	        	Level.Singleton.AddEntity(this, nextPosition);
				CubeAnimations.AnimetSwing(gameObject,nextPosition.ToVector3);
				//transform.position = nextPosition.ToVector3;
				hookMove = false;
			}
		}
	}
	
	public override Command[] GetOptions (){
		Vector3Int upPosition = new Vector3Int (transform.position + Vector3.up);
		List<Command> options = new List<Command>();
		if(CubeHelper.IsFree (upPosition) ){
			options = CubeHelper.GetListOptions(base.GetOptions());
		}
		Vector3Int pos;
		if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.forward),out pos)){
			options.Add(new Swing(this,pos));
			direction = Vector3.forward;
		}
		if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.back),out pos)){
			options.Add(new Swing(this,pos));
			direction = Vector3.back;
		}
		if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.right),out pos)){
			options.Add(new Swing(this,pos));
			direction = Vector3.right;
		}
		if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.left),out pos)){
			options.Add(new Swing(this,pos));
			direction = Vector3.left;
		}
		if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.up),out pos)){
			options.Add(new Swing(this,pos));
			direction = Vector3.up;
		}
		if(transform.position.y>1){
			if(CubeHelper.CheckLastPosition(new Vector3Int(transform.position), new Vector3Int(Vector3.down),out pos)){
				options.Add(new Swing(this,pos));
				direction = Vector3.down;
			}
		}
		
		return options.ToArray();
	}

	public bool HookMove {
		get {
			return this.hookMove;
		}
		set {
			hookMove = value;
		}
	}
	
	public override bool IsSelected {
		get { 
			Vector3Int upPosition = new Vector3Int (transform.position + Vector3.up);
			
			//The cube can't be selected if it have another cube on it
			
			return selected; 
		}
		set { selected = value; }
	}
}
