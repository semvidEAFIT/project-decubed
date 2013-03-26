using UnityEngine;
using System.Collections.Generic;

public class Island : MonoBehaviour
{
	public string levelName;
	public bool completed;
	public bool available;
	public GameObject[] requirements;
	public Vector3 initalPosition;
	public Vector3 finalPosition;
	
	// Use this for initialization
	void Start ()
	{
		available = false;
		initalPosition = transform.position;
		LevelSelector.Instance.AddIsland(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (available) {
			if (completed) {
				if (transform.position != finalPosition) {
					transform.position = Vector3.Lerp (transform.position, finalPosition, Time.deltaTime);
				}
			} else {
				if (transform.position != initalPosition) {
					transform.position = Vector3.Lerp (transform.position, initalPosition, Time.deltaTime);
				}
			}
		}
	}
	
	public void CheckRequirements ()
	{
		if (!completed) {  
			bool allCompleted = true;
			if (requirements != null) {
				foreach (GameObject gObj in requirements) {        
					Island island = (Island)gObj.GetComponent<Island> ();
					if (island != null) {    
						allCompleted = allCompleted && island.Completed;
					}
				}
			}
			if (allCompleted) {
				SetAvailable ();
			}
		}
	}
	
	public void SetAvailable ()
	{
		available = true;
		
	}
	
	#region Get and Sets
	public bool Completed {
		get {
			return this.completed;
		}
		set {
			completed = value;
		}
	}

	public Vector3 FinalPosition {
		get {
			return this.finalPosition;
		}
		set {
			finalPosition = value;
		}
	}

	public Vector3 InitalPosition {
		get {
			return this.initalPosition;
		}
		set {
			initalPosition = value;
		}
	}

	public string LevelName {
		get {
			return this.levelName;
		}
		set {
			levelName = value;
		}
	}
	#endregion
}
