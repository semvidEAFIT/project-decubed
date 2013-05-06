using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Intro : MonoBehaviour {
	
	private Texture textBackground;
	private Component[] childrenTextureComps;
	private Component childrenText;
	private int index;
	private bool canLoad;
    private List<string> script;
    private TextReader tr;
	
	public List<Texture> story;
    public string path;
	public GUISkin creditSkin;
	
	// Use this for initialization
	void Start () {
		index = 0;
		canLoad = true;
		script = new List<string>();
		textBackground = Resources.Load("Art/Textures/GUI/hintBase_final") as Texture;
		childrenText = GetComponentInChildren<GUIText>();
		childrenTextureComps = GetComponentsInChildren<GUITexture>();
		childrenTextureComps[1].guiTexture.texture = story[index];
		try{
        	tr = new StreamReader(path);
			string temp;
	        while((temp = tr.ReadLine()) != null)
	        {
	            script.Add(temp);
	        }
	 
	        // Close the stream
	        tr.Close();
		} 
		catch(FileLoadException e) {
			Debug.LogException(e);
			script.Add("Error while loading story file.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel("MainMenu");
		}
		if(index < story.Count){
			if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && canLoad){
				index++;
				StartCoroutine("fade");
			}
		} 
		else {
			Application.LoadLevel("MainMenu");
		}
	}
	
	public IEnumerator fade(){
		canLoad = false;
		iTween.FadeTo(this.gameObject, 0, 1f);
		yield return new WaitForSeconds(1f);
		childrenText.guiText.text = script[index - 1];
		childrenTextureComps[0].guiTexture.texture = textBackground;
		childrenTextureComps[1].guiTexture.texture = story[index];
		iTween.FadeTo(this.gameObject, 1, 1f);
		yield return new WaitForSeconds(1f);
		canLoad = true;
	}
}