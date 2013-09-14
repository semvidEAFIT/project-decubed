using UnityEngine;
using System.Collections.Generic;
using System;
public class MetalCube : RockCube {
	
	private Vector3 direction;
	private bool magneticMove;
	public bool rockFall;
	
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		if (!Level.Singleton.ContainsElement (nextPosition)) {
			Level.Singleton.RemoveEntity (new Vector3Int (transform.position));
			Level.Singleton.AddEntity (this, nextPosition);
			CubeAnimations.AnimateMove (gameObject, direction, nextPosition.ToVector3);
		}
		direction = Vector3.down;
	}
	
	private bool FindTopPosition(float x, float z,out Vector3Int finalpos){
		if(x < 0 || x > 10 || z < 0 || z > 10 ){
			finalpos = null;
			return false;
		}
		
		float y = transform.position.y;
		if(!CubeHelper.IsFree(new Vector3Int(new Vector3(x,y,z)))){
			finalpos = null;
			return false;
		}
		Level instance = Level.Singleton;
		if(!CubeHelper.IsFree(new Vector3Int(new Vector3(x,y,z)+Vector3.up))&&instance.getEntity(new Vector3(x,y,z)+Vector3.up).GetComponent<MagneticTerrain>()!=null){
			finalpos = new Vector3Int(new Vector3(x,y,z));
			magneticMove = true;
			return true;
		}
		while (y > 0){
			Vector3 position = new Vector3(x,y,z);
			if((!CubeHelper.IsFree(new Vector3Int(position+Vector3.forward)) && instance.getEntity(position+Vector3.forward).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.back)) && instance.getEntity(position+Vector3.back).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.right)) && instance.getEntity(position+Vector3.right).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.left)) && instance.getEntity(position+Vector3.left).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.down)))||
				(y==1)){
				if(CubeHelper.IsFree(new Vector3Int(position+Vector3.down))&&y>1){
					magneticMove = true;
				}
				finalpos = new Vector3Int(position);
				return true;
			}
			y--;
		}
		finalpos = null;
		return false;
	}
	
	public override Command[] GetOptions ()
	{
		magneticMove = false;			
		direction = Vector3.down;
		List<Command> options = new List<Command>();
		Vector3Int final;
		
		if(FindTopPosition(transform.position.x,transform.position.z+1,out final)){
			if(magneticMove){
				options.Add(new MagneticMove(this,final, Vector3.forward));
			}else{
				options.Add(new Move(this,final));
			}
		}
		if(FindTopPosition(transform.position.x,transform.position.z-1,out final)){
			if(magneticMove){
				options.Add(new MagneticMove(this,final, Vector3.back));
			}else{
				options.Add(new Move(this,final));
			}			
		}
		if(FindTopPosition(transform.position.x+1,transform.position.z,out final)){
			if(magneticMove){
				options.Add(new MagneticMove(this,final, Vector3.right));
			}else{
				options.Add(new Move(this,final));
			}
		}
		if(FindTopPosition(transform.position.x-1,transform.position.z,out final)){
			if(magneticMove){
				options.Add(new MagneticMove(this,final, Vector3.left));
			}else{
				options.Add(new Move(this,final));
			}
		}
		Level instance = Level.Singleton;
		if(CubeHelper.IsFree(new Vector3Int((transform.position+Vector3.up)))){
			if((!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.up+Vector3.forward))&&instance.getEntity(transform.position+Vector3.up+Vector3.forward).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.up+Vector3.back))&&instance.getEntity(transform.position+Vector3.up+Vector3.back).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.up+Vector3.right))&&instance.getEntity(transform.position+Vector3.up+Vector3.right).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.up+Vector3.left))&&instance.getEntity(transform.position+Vector3.up+Vector3.left).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.up+Vector3.up))&&instance.getEntity(transform.position+Vector3.up+Vector3.up).GetComponent<MagneticTerrain>()!=null)){
				options.Add(new MagneticMove(this,new Vector3Int(transform.position+Vector3.up),Vector3.up));
			}
		}
		if(CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.down))&&CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down))){
			if((!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.forward))&&instance.getEntity(transform.position+Vector3.down+Vector3.forward).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.back))&&instance.getEntity(transform.position+Vector3.down+Vector3.back).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.right))&&instance.getEntity(transform.position+Vector3.down+Vector3.right).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.left))&&instance.getEntity(transform.position+Vector3.down+Vector3.left).GetComponent<MagneticTerrain>()!=null)){
				options.Add(new MagneticMove(this,new Vector3Int(transform.position+Vector3.down),Vector3.down));
			}
		}
		/*if(CubeHelper.IsFree(new Vector3Int(transform.position + Vector3.down))&&transform.position.y > 1){
			bool canFall = true;
			foreach(Command c in options){
				if(c.EndPosition.ToVector3 == transform.position+Vector3.down){
					canFall = false;
				}
			}
			if(canFall){
				
				options.Add(new Move(this,CubeHelper.GetTopPosition(transform.position+Vector3.down)));
			}
		}
		*/
		return options.ToArray();
	}
	
	public override bool IsSelected {
		get { 
			Vector3Int upPosition = new Vector3Int (transform.position + Vector3.up);
			
			//The cube can't be selected if it have another cube on it
			
			return selected && (CubeHelper.IsFree (upPosition)||CubeHelper.IsFree (new Vector3Int(transform.position+Vector3.down))); 
		}
		set { selected = value; }
	}
	
	public override void OnEndExecution ()
	{
		if(rockFall){
			base.OnEndExecution ();
			rockFall = true;
		}
	}
	
	public Vector3 Direction {
		get {
			return this.direction;
		}
		set {
			direction = value;
		}
	}
}
