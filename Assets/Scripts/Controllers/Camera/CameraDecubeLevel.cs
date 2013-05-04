using UnityEngine;
using System.Collections;

public class CameraDecubeLevel : MonoBehaviour {

	#region Variables
	public GameObject target;
	public float distance = 10f;
	public float zoomMaxLimit = 15f;
	public float zoomMinLimit = 5f;
	public float zoomSpeed = 20f;
	public float xRotSpeed = 150f;
	public float yRotSpeed = 100f;
	public float yRotMinLimit = -10f;
	public float yRotMaxLimit = 90f;
	public float ySpeed = 0.25f;
	public Vector3 center = new Vector3(5,0,5);
	
	private float yMin = 0f;
	private float yMax = 0f;
	private float xRot = 0f;
	private float yRot = 0f;
	private float currentZoomSpeed = 0f;
	private float currentYSpeed = 0f;
	private Vector3 lastMousePosition;
	private GameObject lookingObject;
	#endregion

	void Start ()
	{
		target = new GameObject("Camera Center");
		target.transform.position = new Vector3(5,0,5);
		Vector3 angles = transform.eulerAngles;
		xRot = angles.x;
		yRot = angles.y;
		if (rigidbody != null) {
			rigidbody.freezeRotation = true;
		}
		SetUpYLimits();
		lastMousePosition = Input.mousePosition;
	}
	
	void SetUpYLimits(){
		GameObject[] objects = (GameObject[])FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in objects){
			yMin = Mathf.Min(yMin,go.transform.position.y);
			yMax = Mathf.Max(yMax,go.transform.position.y);
		}
		yMin += -1f;
		yMax += 5f;
		Debug.Log("Min" + yMin);
		Debug.Log("Max" + yMax);
	}
	
	void Update(){
		// Zoom Logic
		float scrollVelocity = -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		if ((distance < zoomMaxLimit && scrollVelocity > 0f) || (distance > zoomMinLimit && scrollVelocity < 0f)){
			currentZoomSpeed += scrollVelocity * Time.deltaTime;
		}
		currentZoomSpeed = Mathf.Lerp(currentZoomSpeed,0f,Time.deltaTime);
		distance = Mathf.Clamp(distance + currentZoomSpeed,zoomMinLimit,zoomMaxLimit);
		
		float deltaY = (Input.mousePosition.y - lastMousePosition.y) * ySpeed;
		lastMousePosition = Input.mousePosition;
		if (lookingObject == null){
			if (Input.GetMouseButton(1)){
				if ( (target.transform.position.y > yMin && deltaY < 0f ) || (target.transform.position.y < yMax && deltaY > 0f)){
					currentYSpeed += deltaY * Time.deltaTime;
				}
			}
			currentYSpeed = Mathf.Lerp(currentYSpeed,0f,Time.deltaTime);
			float newY =  Mathf.Clamp(target.transform.position.y + currentYSpeed, yMin,yMax);
			float newX = Mathf.Lerp(target.transform.position.x,center.x,Time.deltaTime);
			float newZ = Mathf.Lerp(target.transform.position.z,center.z,Time.deltaTime);
			target.transform.position = new Vector3(newX,newY,newZ);
		}else{
			// Lerp for the specific cube
			target.transform.position = Vector3.Lerp(target.transform.position,lookingObject.transform.position,Time.deltaTime);
		}
	}
	
	void LateUpdate ()
	{
		if (target != null) {
			xRot -= Input.GetAxis("Horizontal") * xRotSpeed * 0.02f;
			yRot += Input.GetAxis("Vertical") * yRotSpeed * 0.02f;
			yRot = ClampAngle( yRot,yRotMinLimit,yRotMaxLimit);
			Quaternion rotation = Quaternion.Euler( yRot,xRot ,0);
			Vector3 position = rotation * new Vector3(0f,0f,-distance) + target.transform.position;
			transform.rotation = rotation;
			transform.position = position;
			
			
		}
	}
	
	#region Get and Sets
	public GameObject LookingObject
    {
        get
        {
            return this.lookingObject;
        }
        set
        {
        	this.lookingObject = value;
        }
    }
	#endregion
	
	#region Static Methods
	static float ClampAngle (float angle, float min, float max)
	{
		return Mathf.Clamp(angle,min,max);
	}
	#endregion
	
}
