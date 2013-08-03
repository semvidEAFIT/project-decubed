using UnityEngine;
using System.Collections;

public class CubeMachine : MonoBehaviour {
	
	public GameObject cube;
	public int cubeCount;
	// Use this for initialization
	void Start () {
		cube.transform.position = this.transform.position;
		StartCoroutine("CubeSpawner");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public IEnumerator CubeSpawner(){
		while(cubeCount>0){
			yield return new WaitForSeconds(3);
			GameObject.Instantiate(cube);
			cubeCount --;
		}
	}
}
