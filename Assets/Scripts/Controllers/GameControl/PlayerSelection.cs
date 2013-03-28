using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour {
	
	private UserSettings settings;
	private string playerName;
	void Awake ()
	{
		settings = UserSettings.Instance;
		this.playerName = "";
	}
	
	// Use this for initialization
	void Start ()
	{
	
	}
	void OnGUI ()
	{
		GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 50, 20), "Name:");
		playerName = GUI.TextField (new Rect (Screen.width / 2f + 50, Screen.height / 2f,100,20),playerName,20);
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
