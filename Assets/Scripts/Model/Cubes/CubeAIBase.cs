using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public abstract class CubeAIBase : Cube {
	
	private enum Conditionals {None, PositionAvailable, PressingSensor, NextToCube}
	private Conditionals conditional;
	private Vector3 variable1;
	private bool result;
	private CubeControllerInput cc;
	private int id;
	private Thread thread;
	private AutoResetEvent moveEvent;
	
	private Vector3 moveToVector;

	// Use this for initialization
	public override void Start ()
	{
		moveToVector = Vector3.zero;
		conditional = Conditionals.None;
		moveEvent = new AutoResetEvent (false);
		cc = this.gameObject.GetComponent<CubeControllerInput> ();
		id = 0;
		base.Start ();
		IsSelected = true;
		thread = new Thread (new ThreadStart (this.StartCube));
		thread.Start ();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
		if (thread.ThreadState == ThreadState.WaitSleepJoin) {
			if (moveToVector != Vector3.zero) {
				MoveTo (moveToVector);
				moveToVector = Vector3.zero;
			}
			if (conditional != Conditionals.None) {
				switch (conditional) {
				case Conditionals.NextToCube:
					result = isNextToCube (variable1);
					break;
				case Conditionals.PositionAvailable:
					result = isAvailable (variable1);
					break;
				case Conditionals.PressingSensor:
					result = isPressingSensor ();
					break;
				}
				conditional = Conditionals.None;
				moveEvent.Set ();
			}
		}
		
	}
	
	public void MoveTo (Vector3 direction)
	{
		Vector3Int pos;
		if (CubeHelper.CheckAvailablePosition (Position + direction, out pos, 1)) {
			cc.NotifyMoveTO (new Move (this, pos));
//			finished = false;
			/*Level.Singleton.RemoveEntity(Position);
			Debug.Log(pos.ToVector3);
			Level.Singleton.AddEntity(this,pos.ToVector3);
			transform.position = pos.ToVector3;*/
		} else {
			Debug.Log (direction);
			moveEvent.Set ();
		}
	}
	
	public abstract void StartCube ();
	
	public void Move (Vector3 direction)
	{
		moveToVector = direction;
		moveEvent.WaitOne ();
	}
		
	public bool PositionOutOfLimits (Vector3 direction)
	{
		Vector3 position = direction + Position;
		if (position.x > 10 || position.x < 0 ||
			position.z > 10 || position.z < 0) {
			return true;
		}
		return false;
	}
	
	public bool IsAvailable (Vector3 direction)
	{
		conditional = Conditionals.PositionAvailable;
		variable1 = direction;
		moveEvent.WaitOne ();
		return result;
	}
	
	public bool IsPressingSensor ()
	{
		conditional = Conditionals.PressingSensor;	
		moveEvent.WaitOne ();
		return result;
	}
	
	public bool isNextToCube (Vector3 direction)
	{
		conditional = Conditionals.NextToCube;
		variable1 = direction;
		moveEvent.WaitOne ();
		return result;
	}
	
	private bool isAvailable (Vector3 direction)
	{
		if (PositionOutOfLimits(direction)){
			return false;
		}
		return CubeHelper.IsFree (new Vector3Int (Position + direction));
	}
	
	private bool isPressingSensor(){
		return Level.Singleton.SensorSpaces.ContainsKey(new Vector3Int(Position));
	}
	
	private bool nextToCube(Vector3 direction){
		if(Level.Singleton.getEntity(Position+direction) is CubeAI){
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
		moveEvent.Set ();
	}
	
	
	#region Get and Sets
	public Vector3 Position{
		get{
			return transform.position;
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
