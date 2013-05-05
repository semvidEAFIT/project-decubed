using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class Credits : MonoBehaviour 
{
    public GUISkin creditSkin;
    public float creditSpeed;
    private TextReader tr;
    public string path;
    private List<string> credits;
    private List<Rect> positionRect;
 
    // Use this for initialization
    void Start () 
    {
		credits = new List<string>();
		positionRect = new List<Rect>();
 
        // Create reader & open file
		try{
        	tr = new StreamReader(path);
			string temp;
        	int count = 0;
	        while((temp = tr.ReadLine()) != null)
	        {
	            credits.Add(temp);
	            positionRect.Add(new Rect(0f, (float)(Screen.height * 0.2 * count + Screen.height), (float)(Screen.width), (float)(Screen.height * 0.5)));
	            count++;
	        }
	 
	        // Close the stream
	        tr.Close();
		} 
		catch(FileLoadException e) {
			Debug.LogException(e);
			credits.Add("Error while loading credits file.");
		}        
    }
	
	void Update(){
		//NIGGAZ: Aquí hay que poner la escena que va después de que se muestren los créditos.
		if(positionRect[positionRect.Count - 1].y < -150f){
			Application.LoadLevel("MainMenu");
		}
		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel("MainMenu");
		}
	}
	
    void OnGUI() 
    {
        GUI.skin = creditSkin;
        for (int i = 0; i < credits.Count; i++)
        {
            GUI.Label(positionRect[i], credits[i], "label");
            Rect tempRect = positionRect[i];
            tempRect.y -= creditSpeed * Time.deltaTime;
            positionRect[i] = tempRect;
        }
    }
}