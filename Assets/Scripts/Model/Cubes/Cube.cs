using UnityEngine;
using System.Collections.Generic;

public class Cube : Entity{
	
//	/// <summary>
//	/// The cube that is selected in the whole world.
//	/// </summary>
//	private static Cube selectedCube;
	
	#region Variables
	/// <summary>
	/// The cube is selected.
	/// </summary>
    protected bool selected = false;
	
    /// <summary>
    /// The current command that is executing, if its null then there.
    /// </summary>
	private Command command;
	private int jumpHeight = 1;
	#endregion
	
//	/// <summary>
//	/// Moves Cube to the direction .
//	/// </summary>
//	/// <param name='nextPosition'>
//	/// Next position.
//	/// </param>
//    public virtual void MoveTo(Vector3 nextPosition) {
//        Level.Singleton.Entities.Remove(Vector3Int.getVector(transform.position));
//		CubeAnimations.AnimateMove(gameObject, Vector3.down, nextPosition);
//        Level.Singleton.Entities.Add(Vector3Int.getVector(nextPosition), this);
//    }
	
	#region Command Management
	/// <summary>
	/// Gets the options of the normal cube.
	/// </summary>
	/// <value>
	/// The options of commands of the chosen cube.
	/// </value>
    public virtual Command[] Options{ 
        get {
            List<Command> options = new List<Command>();
			Vector3Int pos;
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.forward,out pos,jumpHeight)){
				options.Add(new Move(this,pos));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.back,out pos,jumpHeight)){
				options.Add(new Move(this,pos));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.right,out pos,jumpHeight)){
				options.Add(new Move(this,pos));
			}
			if (CubeHelper.CheckAvailablePosition(transform.position + Vector3.left,out pos,jumpHeight)){
				options.Add(new Move(this,pos));
			}
            return options.ToArray();
        }
    }
	
	public void EndExecution(){
		OrganizeTransform();
		if(command != null){
			command.EndExecution();
		}
	}
	
	public void OrganizeTransform(){
		Transform obj = gameObject.transform.parent;
		transform.parent = null;
		if (obj != null){
			MonoBehaviour.Destroy(obj.gameObject);
		}
		transform.position = Vector3Round(transform.position);
		transform.rotation = Quaternion.Euler(Vector3Round(transform.rotation.eulerAngles));
	}
	
    public void FallOutOfBounds(Vector3 outOfBouncePosition)
    {
		//TODO Arreglar Esto
//		CubeAnimations.AnimateSlide(gameObject,outOfBouncePosition + new Vector3(0,-10,0), "KillCube", null);
    }
	
	public void KillCube(){
		Destroy(gameObject);
	}
	
	#endregion
	
//    public void NotifyClick()
//    {
//        if (selectedCube != this)
//        {
//            if(selectedCube != null){
//                selectedCube.IsSelected = false;
//            }
//            selected = true;
//            selectedCube = this;
//        }
//    }
	
	public override void Update(){
	}
	
	#region Gets and Sets
	public Command Command {
		get {
			return this.command;
		}
		set {
			command = value;
		}
	}
	
	public virtual bool IsSelected
    {
        get { return selected && CubeHelper.IsFree(transform.position + Vector3.up); }
        set { selected = value; }
    }

	public int JumpHeight {
		get {
			return this.jumpHeight;
		}
		set {
			jumpHeight = value;
		}
	}
	#endregion
	
	#region Helper Methods
	
	private Vector3 Vector3Round(Vector3 v){
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}
	#endregion
}
