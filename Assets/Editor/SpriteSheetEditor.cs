using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(SpriteSheet))]
public class SpriteSheetEditor : Editor {
	[SerializeField]
	private int[] sequence;
	private int sequenceCount;
	private int materialIndex;
	
	public override void OnInspectorGUI(){
		SpriteSheet spriteSheet = (SpriteSheet)target;
		
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField("Animation Parameters", EditorStyles.boldLabel);
		spriteSheet.Active = EditorGUILayout.Toggle("Active",spriteSheet.Active);
		spriteSheet.Loop = EditorGUILayout.Toggle("Loop",spriteSheet.Loop);
		spriteSheet.Reverse = EditorGUILayout.Toggle("Reverse",spriteSheet.Reverse);
		spriteSheet.SmoothTransition = EditorGUILayout.Toggle("Smooth Transition",spriteSheet.SmoothTransition);
		spriteSheet.Fps = EditorGUILayout.FloatField("FPS",spriteSheet.Fps);
		
		EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);
		spriteSheet.materialIndex = EditorGUILayout.IntSlider("Material",spriteSheet.materialIndex,0,spriteSheet.gameObject.renderer.sharedMaterials.Length-1);
		spriteSheet.FrameWidth = EditorGUILayout.IntField("Sprite Width",spriteSheet.FrameWidth);
		spriteSheet.FrameHeight = EditorGUILayout.IntField("Sprite Height",spriteSheet.FrameHeight);
		
		EditorGUILayout.LabelField("Sequence List", EditorStyles.boldLabel);
		
		EditorGUILayout.EndVertical();
		//sequenceCount = EditorGUILayout.IntField("Number of Sequences",sequenceCount);
		//sequenceCount = Mathf.Abs(sequenceCount);
//		if (sequenceCount != sequence.Length){
//			sequence = new int[sequenceCount];
//		}
//		for (int i = 0 ; i < sequenceCount ; i++){
//			sequence[i] = EditorGUILayout.IntField("Sq" +  i + ". Frame Count",sequence[i]);
//		}
//		spriteSheet.SequenceFrameCount = sequence;
		
		if (spriteSheet.FrameWidth != 0 && spriteSheet.FrameHeight != 0){
			int colCount = spriteSheet.renderer.sharedMaterials[materialIndex].mainTexture.width / spriteSheet.FrameWidth;
			int rowCount = spriteSheet.renderer.sharedMaterials[materialIndex].mainTexture.height / spriteSheet.FrameHeight;
			spriteSheet.renderer.sharedMaterials[materialIndex].SetTextureScale("_MainTex", new Vector2(1f/colCount,1f/rowCount));
		}
	}

}
