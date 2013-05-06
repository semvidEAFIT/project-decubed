using UnityEngine;
using System.Collections;

//TODO: Revisar si alguien usaba Clicker (le cambiamos el nombre a este script)
/// <summary>
/// Check clicked objects and notify them (must implement IClickeable interface)
/// </summary>
public class MouseInputManager : MonoBehaviour {
	
	CameraDecubeLevel gameCamera;
	IClickable lastClicked;
	
	void Start ()
	{
		gameCamera = gameObject.GetComponent<CameraDecubeLevel> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool clickFound = false;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = this.camera.ScreenPointToRay (Input.mousePosition);
			//Physics.Raycast (ray, out hitInfo, Mathf.Infinity);
			RaycastHit[] hitsInfo = Physics.RaycastAll(ray,Mathf.Infinity, 1<<8);
			Transform iClickable = null;
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
		}
	}
	
	private void CallIClickable(Transform go){
		MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour m in scripts) {
			if (m is IClickable) {
			
				((IClickable)m).NotifyClick ();
                if (go.tag == "decubePrefab"){
                	lastClicked = (IClickable)m;
                }

                break;
			} 
		}
	}
}
