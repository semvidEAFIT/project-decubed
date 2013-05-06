using UnityEngine;
using System.Collections.Generic;

public class Island : MonoBehaviour
{
	public string levelName;
	public bool completed;
	public bool available;
	private bool islandOut;
	public GameObject[] requirements;
	public Vector3 initialPosition;
	public Vector3 upDirection;
	private Vector3 finalPosition;
	
	// Use this for initialization
	void Start ()
	{
		islandOut = false;
		available = false;
		finalPosition = transform.position;
		initialPosition = finalPosition + upDirection;
		LevelSelector.Instance.AddIsland (this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( (available && !completed) || (islandOut && completed)) {
			if (transform.position != initialPosition) {
				transform.position = Vector3.Lerp (transform.position, initialPosition, Time.deltaTime);
			}
		} else {
			if (transform.position != finalPosition) {
				transform.position = Vector3.Lerp (transform.position, finalPosition, Time.deltaTime);
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
			SetAvailable (allCompleted);
		} else {
			SetAvailable (completed);
		}
	}
	
	public void SetAvailable (bool available)
	{
		this.available = available;
	}
	
	void OnMouseDown ()
	{
		if (available) {
			LevelSelector.Instance.GoToLevel (this);
		}
	}
	
	void OnMouseEnter ()
	{
		this.islandOut = true;
	}
	
	void OnMouseExit ()
	{
		this.islandOut = false;
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

	public Vector3 InitialPosition {
		get {
			return this.initialPosition;
		}
		set {
			initialPosition = value;
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
