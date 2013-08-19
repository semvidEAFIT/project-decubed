using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubberCube : Cube {
	
	private List<Vector3Int> positions;
		
	void Start(){
		base.Start();
		positions = new List<Vector3Int>();
	}
	public override void MoveTo (Vector3Int nextPosition)
	{
		//if (!Level.Singleton.ContainsElement (nextPosition)) {
			Level.Singleton.RemoveEntity (new Vector3Int (transform.position));
			Level.Singleton.AddEntity (this, nextPosition);
			//transform.position = nextPosition.ToVector3
		if(positions.Count>1){
			positions.Add(nextPosition);
			CubeAnimations.AnimateMove (gameObject, Vector3.down, positions[0].ToVector3);
			Debug.Log(positions[0]);
			positions.RemoveAt(0);
		}else{
			positions = new List<Vector3Int>();
			CubeAnimations.AnimateMove (gameObject, Vector3.down, nextPosition.ToVector3);
		}
		//	CubeAnimations.AnimateMove (gameObject, Vector3.down, nextPosition.ToVector3);
		//}
	}
	
	
	public override Command[] GetOptions (){
		if(transform.forward != Vector3.down && !Level.Singleton.ContainsSensor(new Vector3Int(transform.position).ToVector3)){
			SetMood(Mood.Happy);
		}
        List<Command> options = new List<Command>();
		Vector3Int pos;
		
		if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.forward,out pos,JumpHeight)){
			options.Add(new Bounce(this, pos, new Vector3Int(Vector3.forward)));
		}
		if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.back,out pos,JumpHeight)){
			options.Add(new Bounce(this, pos, new Vector3Int(Vector3.back)));
		}
		if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.right,out pos,JumpHeight)){
			options.Add(new Bounce(this, pos, new Vector3Int(Vector3.right)));
		}
		if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.left,out pos,JumpHeight)){
			options.Add(new Bounce(this, pos, new Vector3Int(Vector3.left)));
		}
		return options.ToArray();
	}
	
	public override void EndExecution (){
		
		PlayMovement();
		OnEndExecution();
		if(transform.forward == Vector3.down && SpriteSheet.CurrentSequence!=GetMoodSequence( Mood.EyesClosed)){
			SetMood(Mood.EyesClosed);
		}
	}
	
	public override void OnEndExecution (){
		base.OnEndExecution ();
		if(positions.Count>0){
			CubeAnimations.AnimateMove (gameObject, Vector3.down, positions[0].ToVector3);
			positions.RemoveAt(0);
			Debug.Log(positions.Count);
		}else{
			positions = new List<Vector3Int>();
			OrganizeTransform();
			Command.EndExecution();
		}
	}
	
	
	public List<Vector3Int> Positions {
		get {
			return this.positions;
		}
		set {
			positions = value;
		}
	}
	
	public void AddPosition(Vector3Int ve){
		positions.Add(ve);
	}
}
