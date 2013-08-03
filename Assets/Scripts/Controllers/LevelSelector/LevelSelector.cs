using UnityEngine;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class LevelSelector : MonoBehaviour {
	
	private static LevelSelector singleton;
	private List<Island> islands;
	private Player player;
	
	void Awake ()
	{
		singleton = this;
		player = UserSettings.Instance.CurrentPlayer;
	}
	
	public void AddIsland (Island island)
	{
		if (islands == null) {
			islands = new List<Island> ();
		}
		islands.Add (island);
	}
	
	// Use this for initialization
	void Start ()
	{
		GameObject[] objs = (GameObject[])FindObjectsOfType (typeof(GameObject));
		foreach (GameObject gObj in objs) {
			Island island = gObj.GetComponent<Island> ();
			if (island != null) {
				if (player.Levels.ContainsKey (island.LevelName)) {
					island.Completed = true;
				}
			}
		}
		foreach (Island island in islands) {   
			island.CheckRequirements ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	
	void SaveWorld ()
	{
		UserSettings.Instance.SavePlayerData ();
	}
	
	public void GoToLevel (Island island)
	{
		
//		Debug.Log(island.LevelName);
//		string levelName = island.LevelName;
		//Todo goto level with this name
		Application.LoadLevel(island.LevelName);
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
