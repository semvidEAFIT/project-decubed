using UnityEngine;
using System.Collections;

//TODO: Revisar si alguien usaba Clicker (le cambiamos el nombre a este script)
/// <summary>
/// Check clicked objects and notify them (must implement IClickeable interface)
/// </summary>
public class MouseInputManager : MonoBehaviour {
	
	CameraDrive gameCamera;
	
	void Start ()
	{
		gameCamera = gameObject.GetComponent<CameraDrive> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool clickFound = false;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			//Physics.Raycast (ray, out hitInfo, Mathf.Infinity);
			RaycastHit[] hitsInfo = Physics.RaycastAll(ray,Mathf.Infinity, 1<<8);
			Transform decubePrefab = null;
			foreach (RaycastHit hitInfo in hitsInfo){
				if (hitInfo.transform != null) {
					if (hitInfo.transform.tag == "selector"){
						decubePrefab = null;
						CallIClickable(hitInfo.transform);
						clickFound = true;
						break;
					}
				}
			}

            if(!clickFound){
                foreach (RaycastHit hitInfo in hitsInfo)
                {
                    if (hitInfo.transform.tag == "decubePrefab")
                    {
                        if (decubePrefab == null)
                        {
                            decubePrefab = hitInfo.transform;
                            clickFound = true;
                        }
                        else 
                        {
                            if ((hitInfo.transform.position - ray.origin).sqrMagnitude < (decubePrefab.position - ray.origin).sqrMagnitude)
                            {
                                decubePrefab = hitInfo.transform;       
                            }
                        }
                    }
                }
            }
            if (decubePrefab != null)
            {
                CallIClickable(decubePrefab);
            }
			if (!clickFound) {
				gameCamera.LookingObject = null;
			}
		}
	}
	
	private void CallIClickable(Transform go){
		MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour m in scripts) {
			if (m is IClickable) {
				((IClickable)m).NotifyClick ();
                MoveOptionSelector selector = m.gameObject.GetComponent<MoveOptionSelector> ();
				if (selector != null) {
					gameCamera.LookingObject = selector.Cube.gameObject;
				}
                break;
			} 
		}
	}
}
