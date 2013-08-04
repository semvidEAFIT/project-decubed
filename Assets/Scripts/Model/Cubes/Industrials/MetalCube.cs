using UnityEngine;
using System.Collections.Generic;
using System;
public class MetalCube : RockCube {
	
	private Vector3 direction;
	private bool magneticMove;
	
	public override void MoveTo (Vector3Int nextPosition)
	{
		if (!Level.Singleton.ContainsElement (nextPosition)) {
			Level.Singleton.RemoveEntity (new Vector3Int (transform.position));
			Level.Singleton.AddEntity (this, nextPosition);
			CubeAnimations.AnimateMove (gameObject, direction, nextPosition.ToVector3);
		}
	}
	
	private bool FindTopPosition(float x, float z,out Vector3Int finalpos){
		float i = transform.position.y;
		
		if(!CubeHelper.IsFree(new Vector3Int(new Vector3(x,i,z)))){
			finalpos = null;
			return false;
		}
		Level instance = Level.Singleton;
		while (i > 0){
			Vector3 position = new Vector3(x,i,z);
			if((!CubeHelper.IsFree(new Vector3Int(position+Vector3.forward)) && instance.getEntity(position+Vector3.forward).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.back)) && instance.getEntity(position+Vector3.back).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.right)) && instance.getEntity(position+Vector3.right).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.left)) && instance.getEntity(position+Vector3.left).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(position+Vector3.down)))||
				(i==1)){
				if(CubeHelper.IsFree(new Vector3Int(position+Vector3.down)) && CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down))){
					magneticMove = true;
				}
				finalpos = new Vector3Int(position);
				return true;
			}
			i--;
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
			if(!magneticMove){
				options.Add(new Move(this,final));
			}else{
				options.Add(new MagneticMove(this,final,Vector3.forward));
			}
		}
		if(FindTopPosition(transform.position.x,transform.position.z-1,out final)){
			if(!magneticMove){
				options.Add(new Move(this,final));
			}else{
				options.Add(new MagneticMove(this,final,Vector3.back));
			}			
		}
		if(FindTopPosition(transform.position.x+1,transform.position.z,out final)){
			if(!magneticMove){
				options.Add(new Move(this,final));
			}else {
				options.Add(new MagneticMove(this,final,Vector3.right));
			}
		}
		if(FindTopPosition(transform.position.x-1,transform.position.z,out final)){
			if(!magneticMove){
				options.Add(new Move(this,final));
			}else{
				options.Add(new MagneticMove(this,final,Vector3.left));
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
				magneticMove = true;
			}
		}
		if(CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.down))&&CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down))){
			if((!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.forward))&&instance.getEntity(transform.position+Vector3.down+Vector3.forward).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.back))&&instance.getEntity(transform.position+Vector3.down+Vector3.back).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.right))&&instance.getEntity(transform.position+Vector3.down+Vector3.right).GetComponent<MagneticTerrain>()!=null)||
				(!CubeHelper.IsFree(new Vector3Int(transform.position+Vector3.down+Vector3.left))&&instance.getEntity(transform.position+Vector3.down+Vector3.left).GetComponent<MagneticTerrain>()!=null)){
				options.Add(new MagneticMove(this,new Vector3Int(transform.position+Vector3.down),Vector3.down));
				magneticMove = true;
			}
		}
		if(CubeHelper.IsFree(new Vector3Int(transform.position + Vector3.down))&&transform.position.y > 1){
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
		if(!magneticMove){
			base.OnEndExecution ();
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
