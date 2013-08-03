using UnityEngine;
using System.Collections;

public class RandomAnimation : MonoBehaviour {
	
	public float minWait = 5f;
	public float maxWait = 10f;
	private SpriteSheet ss;
	
	// Use this for initialization
	void Start () {
		ss = GetComponent<SpriteSheet>();
		StartCoroutine(RandomSecuence());
	}
	
	IEnumerator RandomSecuence(){
		while(true){
			
			ss.CurrentSequence = Random.Range(0,ss.SecuenceCount-1);
			
			yield return new WaitForSeconds(Random.Range(minWait,maxWait));
			
		}
	}
}
