using UnityEngine;
using System.Collections;

/// <summary>
/// Camera drive.
/// 
/// TODO: Corregir bug de movimiento
/// </summary>
public class LevelSelectorCam : MonoBehaviour {

	#region Variables
	public GameObject target;
	public float distance = 20;
	public float xSpeed = 150f;
	public float ySpeed = 100f;
	public float yMinLimit = -360f;
	public float yMaxLimit = 360f;
	float x = 0f;
	float y = 0f;
	#endregion

	void Start ()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
		
		if (rigidbody != null) {
			rigidbody.freezeRotation = true;
		}
	}
	
	void LateUpdate ()
	{
		if (target != null) {
			x -= Input.GetAxis("Horizontal") * xSpeed * 0.02f;
			y += Input.GetAxis("Vertical") * ySpeed * 0.02f;
			y = ClampAngle( y,yMinLimit,yMaxLimit);
			Quaternion rotation = Quaternion.Euler( y,x ,0);
			Vector3 position = rotation * new Vector3(0f,0f,-distance) + target.transform.position;
			transform.rotation = rotation;
			transform.position = position;
		}
	}
	
	static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360) 
			angle += 360;
		if (angle > 360) 
			angle -= 360;
		return Mathf.Clamp(angle,min,max);
	}	
}
