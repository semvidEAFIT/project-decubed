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
			RaycastHit hitInfo;
			Physics.Raycast (ray, out hitInfo, Mathf.Infinity);
			
			if (hitInfo.transform != null) {
				
				MonoBehaviour[] scripts = hitInfo.transform.GetComponents<MonoBehaviour> ();
				foreach (MonoBehaviour m in scripts) {

					if (m is IClickable) {
						MoveOptionSelector selector = m.gameObject.GetComponent<MoveOptionSelector> ();
						if (selector != null) {
							gameCamera.LookingObject = selector.Cube.gameObject;
							clickFound = true;
						}
						((IClickable)m).NotifyClick ();
						break;
					} 
				}
				if (!clickFound) {
					gameCamera.LookingObject = null;
				}
			}
		}
	}
}
