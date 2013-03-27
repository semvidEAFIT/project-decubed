using UnityEngine;
using System.Collections;

public class NaturalPlanet :MonoBehaviour, IClickable {
	
	public void NotifyClick ()
	{
		Application.LoadLevel("WorldSelector");
	}
}
