using UnityEngine;
using System.Collections.Generic;

public class GameEntity : MonoBehaviour {
	
	// Use this for initialization
	public virtual void Start () {
        Level.Singleton.AddEntity(this, this.transform.position);
	}
	
	public virtual void Update(){
		
	}
	
}
