using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	
	private UserSettings settings;
	private Player currentPlayer;
	
	void Awake ()
	{
		settings = UserSettings.Instance;
		if (settings.Players.Count == 0) {  
			//TODO Go to the name Registration page
		} else {
			currentPlayer = settings.CurrentPlayer;
		}
		
	}
	
	void OnGUI ()
	{
		GUI.Label (new Rect (10, 10, 100, 20), currentPlayer.Name);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
