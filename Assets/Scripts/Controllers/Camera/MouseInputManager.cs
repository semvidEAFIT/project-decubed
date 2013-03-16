using UnityEngine;
using System.Collections;

//TODO: Revisar si alguien usaba Clicker (le cambiamos el nombre a este script)
/// <summary>
/// Check clicked objects and notify them (must implement IClickeable interface)
/// </summary>
public class MouseInputManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		
	    if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray,out hitInfo, Mathf.Infinity);
			
            if(hitInfo.transform != null){
				
                MonoBehaviour[] scripts = hitInfo.transform.GetComponents<MonoBehaviour>();
                foreach(MonoBehaviour m in scripts){
                    if(m is IClickable){
                        ((IClickable)m).NotifyClick();
						break;
                    }
                }
            }
        }
	}
}
