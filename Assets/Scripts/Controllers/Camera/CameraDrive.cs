using UnityEngine;
using System.Collections;

/// <summary>
/// Camera drive.
/// 
/// TODO: Corregir bug de movimiento
/// </summary>
public class CameraDrive : MonoBehaviour {
	
	#region Attributes 
	public float error = 10.0f;
	public float speedRot = 90.0f;
	public float speedMov = 30.0f;
	
	public GameObject lookingObject;
	protected Vector3 lookingObjectPosition;
	
	private const int positionOffset = 5;
	
	#endregion
	
	#region MonoBehaviour
	
	protected virtual void Start ()
	{
		lookingObjectPosition = lookingObject.transform.position;
	}
	
	
	protected virtual void Update ()
	{
		camera.transform.LookAt (lookingObject.transform);
		
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			camera.transform.RotateAround (lookingObject.transform.position, new Vector3 (0, -1, 0), speedRot * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			camera.transform.RotateAround (lookingObject.transform.position, new Vector3 (0, 1, 0), speedRot * Time.deltaTime);
		}
		
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			
			lookingObjectPosition.Set (lookingObjectPosition.x, 
				lookingObjectPosition.y + (speedMov) * Time.deltaTime, 
				lookingObjectPosition.z);
			
			camera.transform.Translate (0, speedMov * Time.deltaTime, 0, lookingObject.transform);
			
		} else if (camera.transform.position.y >= lookingObject.transform.position.y + positionOffset && 
			(Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow))) {
			
			//if (lookingObjectPosition.y + error >= lookingObject.transform.position.y) {
				
				lookingObjectPosition.Set (lookingObjectPosition.x, 
					lookingObjectPosition.y + (-speedMov) * Time.deltaTime, 
					lookingObjectPosition.z);
				
				camera.transform.Translate (0, -speedMov * Time.deltaTime, 0, lookingObject.transform);
				
			//}
		}
		
		if (camera.transform.position.y - 2 >= lookingObjectPosition.y && Input.GetAxis ("Mouse ScrollWheel") > 0) {
			Vector3 mov = (lookingObjectPosition - camera.transform.position) * Time.deltaTime;
			mov = mov.normalized;
			camera.transform.position += mov;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			Vector3 mov = (lookingObject.transform.position - camera.transform.position) * -Time.deltaTime;
			mov = mov.normalized;
			camera.transform.position += mov;
		} 
			
	
	}
	
	#endregion
	
}
