using UnityEngine;
using UnityEditor;

public class DecubeHelper : EditorWindow {
	
	Vector3 newPosition = Vector3.zero;
	bool groupEnabled;
	private GameObject prefab;
	
	[MenuItem ("Window/Decube Window")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(DecubeHelper));
	}
	
	void OnGUI ()
	{
		int cubeCount = 1;
		int buttonCount = 1;
		
		GUILayout.BeginArea (new Rect (0, 5, 270, 400));
		GUILayout.Label ("Adding Cubes", EditorStyles.boldLabel);
		this.newPosition = EditorGUILayout.Vector3Field ("New Position", newPosition);
		if (GUILayout.Button (new GUIContent ("Add Cube"))) {
			if (prefab != null) {
				GameObject newObj = (GameObject)Instantiate (prefab);
				newObj.name = "Cube";
				newObj.transform.position = newPosition;
				cubeCount++;
			}
		}
		prefab = (GameObject)EditorGUILayout.ObjectField (prefab, typeof(GameObject), true);
		GUILayout.Label ("Cube Count: " + cubeCount, EditorStyles.label);
		GUILayout.EndArea ();
		
		
		GUILayout.BeginArea (new Rect (0, 120, 280, 300));
		GUILayout.Label ("Adding Terrains", EditorStyles.boldLabel);
		this.newPosition = EditorGUILayout.Vector3Field ("New Position", newPosition);
		if (GUILayout.Button (new GUIContent ("Terrain"))) {
			if (prefab != null) {
				GameObject newObj = (GameObject)Instantiate (prefab);
				newObj.name = "Cube";
				newObj.transform.position = newPosition;
				buttonCount++;
			}
		}
		prefab = (GameObject)EditorGUILayout.ObjectField (prefab, typeof(GameObject), true);
		GUILayout.Label ("Terrain Count: " + buttonCount, EditorStyles.label);
		GUILayout.EndArea ();
		
		
		GUILayout.BeginArea (new Rect (0, 240, 280, 300));
		
		GUILayout.Label ("Rotation", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (new GUIContent ("  +x  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.right, 90);
			}
		}
		if (GUILayout.Button (new GUIContent ("  +y  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.up, 90);
				;
			}
		}
		if (GUILayout.Button (new GUIContent ("  +z  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.forward, 90);
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (new GUIContent ("  -x  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.left, 90);
			}
		}
		if (GUILayout.Button (new GUIContent ("  -y  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.down, 90);
			}
		}
		if (GUILayout.Button (new GUIContent ("  -z  "))) {
			foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, Vector3.back, 90);
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
		GUILayout.BeginArea (new Rect (0, 310, 280, 300));
		GUILayout.Label ("Moving Cubes", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button (new GUIContent ("   Up   "))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.up;
			}
		}
		if (GUILayout.Button (new GUIContent ("Foward"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.forward;
			}
		}
		if (GUILayout.Button (new GUIContent (" Left "))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.left;
			}
		}
		GUILayout.EndHorizontal ();
			

		GUILayout.BeginHorizontal ();		
		if (GUILayout.Button (new GUIContent ("Down"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.down;
			}
		}
		if (GUILayout.Button (new GUIContent (" Back "))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.back;
			}
		}
		if (GUILayout.Button (new GUIContent (" Right"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.right;
			}
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginVertical ();
		if (GUILayout.Button (new GUIContent ("Clone"))) {
			foreach (Transform t in Selection.transforms) {
				GameObject.Instantiate (t.gameObject);
			}
		}
		if (GUILayout.Button (new GUIContent ("Correct Position"))) {
			foreach (Transform t in Selection.transforms) {
				t.position = new Vector3 (Mathf.Round (t.position.x), Mathf.Round (t.position.y), Mathf.Round (t.position.z));
			}
		}

		
		if (GUILayout.Button (new GUIContent ("Clone"))) {
			foreach (Transform t in Selection.transforms) {
				GameObject.Instantiate (t.gameObject);
				cubeCount++;
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
	
}