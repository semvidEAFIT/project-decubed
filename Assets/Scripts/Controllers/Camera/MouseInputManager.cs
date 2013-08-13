using UnityEngine;
using System.Collections;
/// <summary>
/// Check clicked objects and notify them (must implement IClickeable interface)
/// </summary>
public class MouseInputManager : MonoBehaviour {
	
	CameraDecubeLevel gameCamera;
	IClickable lastClicked;
	private bool pressed = false;
	
	void Start ()
	{
		gameCamera = gameObject.GetComponent<CameraDecubeLevel> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool clickFound = false;
		Ray ray = this.camera.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hitsInfo = Physics.RaycastAll(ray,Mathf.Infinity, 1<<8);
		Transform iClickable = null;
		
		if (Input.GetMouseButtonDown (0)) {
			pressed = true;
			foreach (RaycastHit hitInfo in hitsInfo){
				if (hitInfo.transform != null) {
					if (hitInfo.transform.tag == "selector"){
                        if (!clickFound)
                        {
                            iClickable = hitInfo.transform;
                            clickFound = true;
                        }
                        else 
                        {
                            if ((hitInfo.transform.position - ray.origin).sqrMagnitude < (iClickable.position - ray.origin).sqrMagnitude)
                            {
                                iClickable = hitInfo.transform;
                            }
                        }
					}
				}
			}

            if(!clickFound)
            {
                foreach (RaycastHit hitInfo in hitsInfo)
                {
                    if (hitInfo.transform.tag == "decubePrefab")
                    {
                        if (iClickable == null)
                        {
                            iClickable = hitInfo.transform;
                            clickFound = true;
                        }
                        else 
                        {
                            if ((hitInfo.transform.position - ray.origin).sqrMagnitude < (iClickable.position - ray.origin).sqrMagnitude)
                            {
                                iClickable = hitInfo.transform;       
                            }
                        }
                    }
                }
            }

            if (iClickable != null)
            {
                CallIClickable(iClickable);
                if (iClickable.tag == "decubePrefab"){
                	gameCamera.LookingObject = iClickable.gameObject;
                }
            }
            
			if (!clickFound) {
				if (lastClicked != null){
					lastClicked.NotifyUnClick();
				}
				gameCamera.LookingObject = null;
			}
		} else if (Input.GetMouseButtonUp (0)){
			if (pressed){
				foreach (RaycastHit hitInfo in hitsInfo){
					if (hitInfo.transform != null) {
						if (hitInfo.transform.tag == "selector"){
	                        CallIUnClickable(hitInfo.transform);
	                    }
	                }
		        }
		        pressed = false;
	        }
		}		
	}
	
	private void CallIClickable(Transform go){
		MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour m in scripts) {
			if (m is IClickable) {
				((IClickable)m).NotifyClick ();
                if (go.tag == "decubePrefab"){
					if (lastClicked != null){
						((IClickable)lastClicked).NotifyChange();
					}
                	lastClicked = (IClickable)m;
                }
                break;
			} 
		}
	}
	
	private void CallIUnClickable(Transform go){
		MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour m in scripts) {
			if (m is IClickable) {
				((IClickable)m).NotifyUnClick();
                break;
			} 
		}
	}
	
}
