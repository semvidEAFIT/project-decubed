using UnityEngine;
using System.Collections;

public class LevelData{
	
	private string id;
	private int stepCount;
	
	public LevelData (string id, int stepCount)
	{
		this.id = id;
		this.stepCount = stepCount;
	}

	#region Get and Sets

	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public int StepCount {
		get {
			return this.stepCount;
		}
		set {
			stepCount = value;
		}
	}
	#endregion
}
