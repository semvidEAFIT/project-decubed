using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	// Use this for initialization
	public float orbit = 1f;
	public float diameter = 8f;
	private float speed =1f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y  == diameter*orbit/2 && transform.position.x < diameter*orbit/2){
			transform.Translate(Vector3.right.x*speed*Time.deltaTime,Vector3.right.y*speed*Time.deltaTime, Vector3.right.z*speed*Time.deltaTime);
		}
		if(transform.position.y  > -diameter*orbit/2 && transform.position.x == diameter*orbit/2){
			transform.Translate(Vector3.down.x*speed*Time.deltaTime,Vector3.down.y*speed*Time.deltaTime, Vector3.down.z*speed*Time.deltaTime);
		}
		if(transform.position.y == -diameter*orbit/2 && transform.position.x > -diameter*orbit/2){
			transform.Translate(Vector3.left.x*speed*Time.deltaTime,Vector3.left.y*speed*Time.deltaTime, Vector3.left.z*speed*Time.deltaTime);
		}
		if(transform.position.y < diameter*orbit/2 && transform.position.x == -diameter*orbit/2){
			transform.Translate(Vector3.up	.x*speed*Time.deltaTime,Vector3.up.y*speed*Time.deltaTime, Vector3.up.z*speed*Time.deltaTime);	
		}
		if(transform.position.y > diameter*orbit/2){
			transform.position = new Vector3(-diameter*orbit/2,diameter*orbit/2,0);
		}
		if(transform.position.x > diameter*orbit/2){
			
			transform.position =  new Vector3(diameter*orbit/2,diameter*orbit/2,0);
		}
		if(transform.position.y < -diameter*orbit/2){
			transform.position = new Vector3( diameter*orbit/2,-diameter*orbit/2,0);
		}
		if(transform.position.x < -diameter*orbit/2){
			transform.position = new Vector3( -diameter*orbit/2,-diameter*orbit/2,0);
		}
	}
}
