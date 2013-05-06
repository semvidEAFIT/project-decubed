using UnityEngine;
using System.Collections.Generic;

public class IceCube : Cube {
	
//	private Vector3Int nextPosition;
	private bool breakIce = false;
	private Vector3Int endPosition;
	private Vector3Int nextPosition;
	public bool comandEnded = false;
	
	public override void MoveTo (Vector3Int endPosition)
	{
		setMood(Cube.Mood.EyesClosed);
		this.endPosition = endPosition;
		Level.Singleton.RemoveEntity(new Vector3Int(transform.position));
        Level.Singleton.AddEntity(this, endPosition);
		CubeAnimations.AnimateMove (gameObject, Vector3.down, nextPosition.ToVector3);
	}
	
	public override Command[] GetOptions(){ 
		if(transform.forward != Vector3.down && !Level.Singleton.ContainsSensor(new Vector3Int(transform.position).ToVector3)){
			setMood(Mood.Happy);
		}
		List<Command> options = new List<Command>();
			Vector3Int pos;
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.forward,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.forward)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.back,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.back)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.right,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.right)));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.left,out pos,GetJumpHeight())){
				options.Add(new Slide(this,pos,new Vector3Int(Vector3.left)));
			}
            return options.ToArray();
	}
	
	public void Break(){
		breakIce = true;
	}
	
	public override void EndExecution ()
	{
		OrganizeTransform();
		if(Command != null && endPosition.ToVector3 ==  new Vector3Int(transform.position).ToVector3){
			//fix
			Command.EndExecution();
			comandEnded = false;
		}
		PlayMovement();
		OnEndExecution();
	}
	
	public override void OnEndExecution ()
	{
		if(breakIce &&  endPosition.ToVector3 == new Vector3Int(transform.position).ToVector3){
			Level.Singleton.RemoveEntity(endPosition);
			Destroy(this.gameObject);
		}else if( endPosition.z != transform.position.z || endPosition.x != transform.position.x){
			CubeAnimations.AnimateSlide(gameObject, new Vector3Int(endPosition.x,Mathf.RoundToInt(transform.position.y),endPosition.z).ToVector3);
		}else if(  endPosition.y < transform.position.y){
			CubeAnimations.AnimateMove (gameObject, Vector3.down, endPosition.ToVector3);
		}
		Vector3Int next = new Vector3Int(transform.position);
		if(!breakIce && next.x > 10 || next.x < 0 || next.z > 10 || next.z < 0){
			Level.Singleton.RemoveEntity(new Vector3Int(transform.position));
			Destroy(gameObject);
		}
		if(transform.forward == Vector3.down && SpriteSheet.CurrentSequence!=(int)Mood.EyesClosed){
			setMood(Mood.EyesClosed);
		}
	}
	
	public Vector3Int NextPosition {
		get {
			return this.nextPosition;
		}set{
			this.nextPosition = value;
		}
	}
	
	#region Animation Methods
	public override int GetMoodSequence (Mood mood)
	{
		switch(mood){
		case Mood.Normal:
			return 0;
		case Mood.Proud:
			return 3;
		case Mood.EyesClosed:
			return 1;
		case Mood.Happy:
			return 3;
		case Mood.Angry:
			return 2;
		default: 
			return 0;
		}
	}
	#endregion
}
