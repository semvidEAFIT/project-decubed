using UnityEngine;
using System.Collections.Generic;

public class Player{
	
	private int id;
	private string name;
	private Dictionary<string,LevelData> levels;
	
	public Player (int id, string name, Dictionary<string,LevelData> levels)
	{
		this.id = id;
		this.name = name;
		this.levels = levels;
	}
	
	#region Get and Sets
	public int Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public Dictionary<string, LevelData> Levels {
		get {
			return this.levels;
		}
		set {
			levels = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}
	#endregion
}
