using UnityEngine;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour {
	
	private static LevelSelector singleton;
	private List<Island> islands;
	
	void Awake ()
	{
		singleton = this;
		islands = new List<Island> ();
	}
	
	public void AddIsland(Island island){
		islands.Add(island);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	#region Singleton
	public static LevelSelector Instance {
		get {
			return singleton;
		}
		set {
			singleton = value;
		}
	}
	#endregion
}
