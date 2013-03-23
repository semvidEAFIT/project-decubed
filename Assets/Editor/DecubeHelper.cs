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
		
		GUILayout.Label ("Adding Cubes", EditorStyles.boldLabel);

		this.newPosition = EditorGUILayout.Vector3Field ("New Position", newPosition);
		if (GUILayout.Button (new GUIContent ("Add Cube"))) {
			if (prefab != null) {
				GameObject newObj = (GameObject)Instantiate (prefab);
				newObj.name = "Cube";
				newObj.transform.position = newPosition;
			}
		}
		prefab = (GameObject)EditorGUILayout.ObjectField (prefab, typeof(GameObject), true);
		
		
		GUILayout.Label ("Moving Cubes", EditorStyles.boldLabel);
//		GUILayout.Label ("Move cube with Arrows and w and s keys of selected cubes", EditorStyles.label);
//		
		if (GUILayout.Button (new GUIContent ("Back"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.back;
			}
		}
		if (GUILayout.Button (new GUIContent ("Foward"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.forward;
			}
		}
		if (GUILayout.Button (new GUIContent ("Left"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.left;
			}
		}
		if (GUILayout.Button (new GUIContent ("Right"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.right;
			}
		}
		if (GUILayout.Button (new GUIContent ("Up"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.up;
			}
		}
		if (GUILayout.Button (new GUIContent ("Down"))) {
			foreach (Transform t in Selection.transforms) {
				t.position += Vector3.down;
			}
		}
//		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
//
//		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
//
//		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
//
//		EditorGUILayout.EndToggleGroup ();

	}
	
}
