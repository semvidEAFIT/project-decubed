using UnityEngine;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour {
	
	private UserSettings settings;
	private Player selectedPlayer;
	public GUISkin skin;
	public Texture2D logo;
	
	void Awake ()
	{
		settings = UserSettings.Instance;
	}
	
	// Use this for initialization
	void Start ()
	{
	}
	
	void OnGUI ()
	{
		GUI.skin = skin;
		
		var centeredStyle = GUI.skin.GetStyle ("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;
		
		int logoWidth = logo.width / 2;
		int logoHeight = logo.height / 2;
		GUI.Label (new Rect (Screen.width / 2 - logoWidth / 2, 20, logoWidth, logoHeight), logo);
		
		int boxWidth = 380;
		int boxHeight = 170;
		GUI.BeginGroup (new Rect (Screen.width / 2 - boxWidth / 2, 2 * (Screen.height / 3) - boxHeight / 2, boxWidth, boxHeight));
		
		GUI.Box (new Rect (0, 0, boxWidth, boxHeight), "Select a game");
		int labelHeight = 140;
		int buttonHeight = 35;
		if (GUI.Button (new Rect (20, buttonHeight, 100, 100), "Game 1")) {
			settings.CurrentPlayer = settings.Players [0];
			settings.SavePlayerData ();
			Application.LoadLevel ("MainMenu");
		}
		if (settings.Players [0].Levels.Count > 0) {
			GUI.Label (new Rect (20, labelHeight, 100, 20), "Levels " + settings.Players [0].Levels.Count);
		} else {
			GUI.Label (new Rect (20, labelHeight, 100, 20), "New Game!");
		}
		if (GUI.Button (new Rect (140, buttonHeight, 100, 100), "Game 2")) {
			settings.CurrentPlayer = settings.Players [1];
			settings.SavePlayerData ();
			Application.LoadLevel ("MainMenu");
		}
		if (settings.Players [1].Levels.Count > 0) {
			GUI.Label (new Rect (140, labelHeight, 100, 20), "Levels " + settings.Players [1].Levels.Count);
		} else {
			GUI.Label (new Rect (140, labelHeight, 100, 20), "New Game!");
		}
		if (GUI.Button (new Rect (260, buttonHeight, 100, 100), "Game 3")) {
			settings.CurrentPlayer = settings.Players [2];
			settings.SavePlayerData ();
			Application.LoadLevel ("MainMenu");
		}
		if (settings.Players [2].Levels.Count > 0) {
			GUI.Label (new Rect (260, labelHeight, 100, 20), "Levels " + settings.Players [2].Levels.Count);
		} else {
			GUI.Label (new Rect (260, labelHeight, 100, 20), "New Game!");
		}
//		height += 20;
//		GUI.BeginScrollView (new Rect (10, height, 280, 240), Vector2.zero, new Rect (0, 0, 250, settings.Players.Count * 20), false, true);
//		height += 250;
//		for (int i = 0; i < settings.Players.Count; i++) {
//			Player player = settings.Players [i];
//			
//			if (GUI.Button (new Rect (0, 20 * i, 260, 20), player.Name)) {
//				selectedPlayer = player;
//			}
//		}
//		
//		GUI.EndScrollView ();
//		
//		GUI.Label (new Rect (10, height, 70, 20), "Add Player:");
//		tPlayerName = GUI.TextField (new Rect (80, height, 180, 20), tPlayerName, 20);
//		if (GUI.Button (new Rect (270, height, 20, 20), "+")) {
//			settings.Players.Add (new Player (settings.getNextId (), tPlayerName, new Dictionary<string,LevelData> ()));
//			tPlayerName = "";
//		}
//		height += 20;
		GUI.EndGroup ();
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
