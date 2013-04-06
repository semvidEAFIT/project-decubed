using UnityEngine;
using System.Collections.Generic;

public class EducationalCubeHelper : Cube {
	
	private CubeControllerInput cc;
	private int id;
	private bool finished;	
	// Use this for initialization
	void Start () {
		cc = this.gameObject.GetComponent<CubeControllerInput>();
		id = 0;
		base.Start();
		IsSelected = true;
		finished = true;
	}
	
	// Update is called once per frame
	public override void Update() {
		if(finished){
			Comandos();
		}
	}
	
	public virtual void Comandos(){
		
	}
	
	public void MoveTo(Vector3 direction){
		Vector3Int pos;
		if (CubeHelper.CheckAvailablePosition(Position + direction,out pos,1)){
			cc.NotifyMoveTO(new Move(this,pos));
			finished =  false;
			/*Level.Singleton.RemoveEntity(Position);
			Debug.Log(pos.ToVector3);
			Level.Singleton.AddEntity(this,pos.ToVector3);
			transform.position = pos.ToVector3;*/
		}else{
			Debug.Log(direction);
		}
	}
	
	public bool PositionOutOfLimits(Vector3 direction){
		Vector3 position = direction+Position;
		if(position.x > 10 || position.x < 0 ||
			position.z > 10 || position.z < 0){
			return true;
		}
		return false;
	}
	
	public bool isAvailable(Vector3 direction){
		return CubeHelper.IsFree(new Vector3Int(Position+direction));
	}
	
	public bool isPressingButton(){
		return Level.Singleton.SensorSpaces.ContainsKey(new Vector3Int(Position));
	}
	
	public bool nextToCube(Vector3 direction){
		if(Level.Singleton.getEntity(Position+direction) is EducationalCube){
			return true;
		}
		return false;
	}
	
	public void setId(int id){
		if (id == 0){
			this.id = id;
		}
	}
	
	public override Command[] GetOptions ()
	{
		return new Command[]{};
	}
	
	public override void OnEndExecution ()
	{
		finished = true;
	}
	#region Get and Sets
	public Vector3 Position{
		get{
			return this.transform.position;
		}
	}
	
	public Vector3 Backward {
		get {
			return Vector3.back;
		}
	}

	public Vector3 Foward {
		get {
			return Vector3.forward;
		}
	}

	public Vector3 Left {
		get {
			return Vector3.left;
		}
	}

	public Vector3 Right {
		get {
			return Vector3.right;
		}
	}

	public Vector3 Up {
		get {
			return Vector3.up;
		}
	}
	
	
	public Vector3 Down {
		get {
			return Vector3.down;
		}
	}

	public int Id {
		get {
			return this.id;
		}
	}
	#endregion
}
