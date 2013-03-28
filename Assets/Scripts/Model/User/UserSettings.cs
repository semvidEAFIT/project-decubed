using UnityEngine;
using System.Collections.Generic;
using Boomlagoon.JSON;

public class UserSettings {

	#region Singleton
	private static UserSettings settings;
	
	public static UserSettings Instance {
		get {
			if (settings == null) {
				settings = new UserSettings ();
			}
			return settings;
		}
	}
	#endregion
	
	private List<Player> players;
	private Player currentPlayer;
	
	private UserSettings ()
	{
		LoadPlayerData ();
	}
	
	private void LoadPlayerData ()
	{
		if (PlayerPrefs.HasKey ("players")) {
			string jsonObject = PlayerPrefs.GetString ("players");
			int idPlayer = 0;
			if (PlayerPrefs.HasKey ("currentPlayer")) {
				idPlayer = PlayerPrefs.GetInt ("currentPlayer");
			}
			JSONArray array = JSONArray.Parse (jsonObject);
			for (int i = 0; i < array.Length; i++) {
				JSONObject obj = array [i].Obj;
				Player p = getPlayer (obj);
				players.Add (p);
				if (p.Id == idPlayer) {
					currentPlayer = p;
				}
			}
		} else {
			players = new List<Player> ();
		}
	}
	
	public void SavePlayerData ()
	{
		JSONArray array = new JSONArray ();
		foreach (Player p in players) {
			JSONObject player = new JSONObject ();
			player.Add ("id", p.Id);
			player.Add ("name", p.Name);
			JSONArray levels = new JSONArray ();
			foreach (LevelData l in p.Levels.Values) {
				JSONObject jLevel = new JSONObject ();
				jLevel.Add ("id", l.Id);
				jLevel.Add ("stepCount", l.StepCount);
				levels.Add (jLevel);
			}
			player.Add ("levels", levels);
			array.Add (player);
		}
		PlayerPrefs.SetString ("players", array.ToString ());
		PlayerPrefs.Save ();
	}
	
	#region Factory
	private static Player getPlayer (JSONObject obj)
	{
		JSONArray array = obj.GetArray ("levels");
		Dictionary<string,LevelData> levels = new Dictionary<string, LevelData> ();
		for (int i =0; i < array.Length; i++) {
			JSONObject lvl = array [i].Obj;
			LevelData lData = getLevelData (lvl);
			levels.Add (lData.Id, lData);
		}
		return new Player ((int)obj.GetNumber ("id"), obj.GetString ("name"), levels);
	}
	
	private static LevelData getLevelData (JSONObject level)
	{
		return new LevelData (
			level.GetString ("id"), 
			(int)level.GetNumber ("stepCount")
			);
	}
	#endregion
	
	#region Get and Sets
	
	public List<Player> Players {
		get {
			return this.players;
		}
	}

	public Player CurrentPlayer {
		get {
			return this.currentPlayer;
		}
		set {
			currentPlayer = value;
		}
	}
	#endregion
}
