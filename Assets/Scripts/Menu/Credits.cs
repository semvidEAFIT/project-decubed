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
        // Set the path for the credits.txt file
        //path = "Assets/Scripts/Credits.txt";
 
        // Create reader & open file
        tr = new StreamReader(path);
 
        string temp;
        int count = 0;
        while((temp = tr.ReadLine()) != null)
        {
            // Read a line of text
            credits.Add(temp);
            positionRect.Add(new Rect(0f, (float)(Screen.height * 0.2 * count + Screen.height), (float)(Screen.width), (float)(Screen.height * 0.5)));
            Debug.Log(temp);
            count++;
        }
 
        // Close the stream
        tr.Close();
    }
	
    void OnGUI() 
    {
        GUI.skin = creditSkin;
        for (int i = 0; i < credits.Count; i++)
        {
            GUI.Label(positionRect[i], credits[i], "label");
            Rect tempRect = positionRect[i];
            tempRect.y = tempRect.y - creditSpeed;
            positionRect[i] = tempRect;
        }
    }
}