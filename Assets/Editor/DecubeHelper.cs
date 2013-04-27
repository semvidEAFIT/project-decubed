using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DecubeHelper : EditorWindow {
	
	Vector3 newPosition = Vector3.zero;
	bool groupEnabled;
	private GameObject prefab;
	bool clone = false;
	
	[MenuItem ("Window/Decube Window")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(DecubeHelper));
	}
	
	void AddGameObject(){
		if (prefab != null) {
			GameObject newObj = (GameObject)Instantiate (prefab);
			//newObj.name = prefab.name;
			newObj.transform.position = newPosition;
		}
	}
	
	void RotateAround(Vector3 direction){
		foreach (Transform t in Selection.transforms) {
				t.RotateAround (t.position, direction, 90);
		}
	}
	
	void MoveGameObject(Vector3 direction){
		if (clone){
			foreach (Transform t in Selection.transforms) {
				Instantiate(t.gameObject);
				t.position += direction;
			}
		} else{
			foreach (Transform t in Selection.transforms) {
				t.position += direction;
			}
		}
	}
	
	void OnGUI ()
	{
		int height = 5;
		GUILayout.BeginArea (new Rect (5, height, position.width - 10, 95));
			GUILayout.Label ("New Asset", EditorStyles.boldLabel);
			this.newPosition = EditorGUILayout.Vector3Field ("Position", newPosition);
			if (GUILayout.Button (new GUIContent ("Add GameObject"))) {
				AddGameObject();
			}
			prefab = (GameObject)EditorGUILayout.ObjectField (prefab, typeof(GameObject), true);
		GUILayout.EndArea();
		height += 95;
		
		GUILayout.BeginArea (new Rect (5, height, position.width - 10, 100));		
			GUILayout.Label ("Rotation", EditorStyles.boldLabel);
			
			GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("+x"))) {
					RotateAround(Vector3.right);
				}
				if (GUILayout.Button (new GUIContent ("+y"))) {
					RotateAround(Vector3.up);
				}
				if (GUILayout.Button (new GUIContent ("+z"))) {
					RotateAround(Vector3.forward);
				}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("-x"))) {
					RotateAround(Vector3.left);
				}
				if (GUILayout.Button (new GUIContent ("-y"))) {
					RotateAround(Vector3.down);
				}
				if (GUILayout.Button (new GUIContent ("-z"))) {
					RotateAround(Vector3.back);
				}
			GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		height += 70;
		
		GUILayout.BeginArea (new Rect (5, height, position.width - 10, 200));
			GUILayout.Label ("Moving Cubes", EditorStyles.boldLabel);
			GUILayout.BeginHorizontal ();
				if (GUILayout.Button (new GUIContent ("Up"))) {
					MoveGameObject(Vector3.up);
				}
				if (GUILayout.Button (new GUIContent ("Foward"))) {
					MoveGameObject(Vector3.forward);
				}
				if (GUILayout.Button (new GUIContent ("Left"))) {
					MoveGameObject(Vector3.left);
				}
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();		
				if (GUILayout.Button (new GUIContent ("Down"))) {
					MoveGameObject(Vector3.down);
				}
				if (GUILayout.Button (new GUIContent ("Back"))) {
					MoveGameObject(Vector3.back);
				}
				if (GUILayout.Button (new GUIContent ("Right"))) {
					MoveGameObject(Vector3.right);
				}
			GUILayout.EndHorizontal ();
			GUILayout.BeginVertical ();
				GUILayout.Label("Level Commands", EditorStyles.boldLabel);
				clone = GUILayout.Toggle(clone,"Clone Mode");
				if (GUILayout.Button (new GUIContent ("Correct Selection"))) {
					FixGameObjects();
				}
				if (GUILayout.Button (new GUIContent ("Correct All GameObjects"))) {
					FixAllGameObjects();
				}
				
				if (GUILayout.Button (new GUIContent ("Clone"))) {
					foreach (Transform t in Selection.transforms) {
						GameObject.Instantiate (t.gameObject);
					}
				}
			GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
	
	
	void FixAllGameObjects(){
		GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
		List<Vector3> positions = new List<Vector3>();
		foreach (GameObject g in gos) {
			if (g.tag == "decubePrefab"){
				Transform t = g.transform;
				if (positions.Contains(t.position)){
					DestroyImmediate(g);
				}else{
					positions.Add(t.position);
					t.position = new Vector3 (
						Mathf.Round (t.position.x), 
						Mathf.Round (t.position.y), 
						Mathf.Round (t.position.z)
					);
					t.rotation = Quaternion.Euler(
						FixRotation90(t.rotation.eulerAngles.x),
						FixRotation90(t.rotation.eulerAngles.y),
						FixRotation90(t.rotation.eulerAngles.z)
					);
				}
			}
		}
	}
	
	void FixGameObjects(){
		foreach (Transform t in Selection.transforms) {
			t.position = new Vector3 (
				Mathf.Round (t.position.x), 
				Mathf.Round (t.position.y), 
				Mathf.Round (t.position.z)
			);
			t.rotation = Quaternion.Euler(
				FixRotation90(t.rotation.eulerAngles.x),
				FixRotation90(t.rotation.eulerAngles.y),
				FixRotation90(t.rotation.eulerAngles.z)
			);
		}
	}
	
	float FixRotation90(float rotation){
		return Mathf.Round(rotation/90f) * 90f;
	}
	
}