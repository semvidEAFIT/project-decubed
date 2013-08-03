using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;

/// <summary>
/// Level. Singleton Class
/// </summary>
public class Level : MonoBehaviour
{
	
	#region Variables
	
	/// <summary>
	/// The actual selected cube.
	/// </summary>
	private Cube selectedCube;
	private ArrayList sensors;
    private Dictionary<Vector3Int, GameEntity> entities;
	private Dictionary<Vector3Int, List<BasicSensor>> sensorSpaces; // Dictionary that determines which spaces activate the sensors
	public int sensorsLeft = 0;
	public int stepCount = 0;
    private static Level singleton;
	private Texture2D[] images = new Texture2D[4];
    private GUISkin skin;
    private bool showHint = false;
    private bool isMenu = false;
    private bool isExiting = false;
    private CameraDecubeLevel levelCamera;
	/// <summary>
	/// x -> inf limit in y and x.
	/// y -> sup limit in y and x
	/// </summary>
	private static Vector2 dimension = new Vector2(0, 10);
	
    private TextReader tr;
	private List<string> hints;
	private string path;
    private bool askedForHint = false;
	#endregion

    private void restartLevel()
    {
        if(!isMenu){
            Lumos.Event("Restarted Level" + Application.loadedLevelName + ", Remaining sensors", sensorsLeft);
            Lumos.Event("Restarted Level" + Application.loadedLevelName + ", Time", Time.timeSinceLevelLoad);
            Lumos.Event("Restarted Level" + Application.loadedLevelName + ", StepCount", stepCount);
        }
        Application.LoadLevel(Application.loadedLevel);
    }

    private void ExitLevel()
    {
        if (!isMenu)
        {
            if (sensorsLeft != 0)
            {
                Lumos.Event("Quit Level " + Application.loadedLevelName + ", Remaining sensors", sensorsLeft);
                Lumos.Event("Quit Level " + Application.loadedLevelName + ", Stepcount", stepCount);
                Lumos.Event("Quit Level " + Application.loadedLevelName + ", Time", Time.timeSinceLevelLoad);
            }
            else
            {
                Lumos.Event("Solved Level " + Application.loadedLevelName + ", Stepcount", stepCount);
                Lumos.Event("Solved Level " + Application.loadedLevelName + ", Time", Time.timeSinceLevelLoad);
            }
        }
        Application.LoadLevel("WorldSelector");
    }

	#region Entities Dictionary Management
	
	public void AddEntity (GameEntity entity, Vector3 position)
	{
		AddEntity (entity, new Vector3Int (position));
	}
	
	public void AddEntity (GameEntity entity, Vector3Int position)
	{
	try{
		entities.Add (position, entity);
		}catch(Exception e){
			Debug.Log(position);
		}
		if (entity is BasicSensor) {
			AddSensor ((BasicSensor)entity);
			sensorsLeft += 1;
		} else {
			if (sensorSpaces.ContainsKey (position)) {
				Cube c = (Cube)entity;
				c.SetMood(Cube.Mood.Proud);
				foreach (BasicSensor s in sensorSpaces[position]) {
					s.NotifyPressed (position);
				}
			}
			if(position.y > 1 && ContainsElement(position.ToVector3 + Vector3.down) && Level.Singleton.getEntity(position.ToVector3 + Vector3.down) is Cube){
				Cube c = (Cube)Level.Singleton.getEntity(position.ToVector3 + Vector3.down);
				c.SetMood(Cube.Mood.Angry);	
			}
		}
	}
	
	public void RemoveEntity (Vector3 position)
	{
		RemoveEntity (new Vector3Int (position));
			
	}
	
	public void RemoveEntity (Vector3Int position)
	{
		GameEntity temp = entities [position];
		entities.Remove (position);
	
		if (temp is BasicSensor) {
			RemoveSensor ((BasicSensor)temp);
		} else {
			if (sensorSpaces.ContainsKey (position)) {
				
				foreach (BasicSensor s in sensorSpaces[position]) {
					s.NotifyUnpressed (position);
				}
			}
			if(entities.ContainsKey(new Vector3Int( position.ToVector3+Vector3.down))){
			try{
				if(getEntity(position.ToVector3+Vector3.down)is Cube){
					Cube c = (Cube)getEntity(position.ToVector3+Vector3.down);
					c.SetMood(Cube.Mood.Normal);
				}
			}catch(Exception e){}
			}
		}	
	}
	
	public GameEntity getEntity (Vector3 position)
	{
		return entities [new Vector3Int (position)];
	}
	
	///TODO: REvisar si borrar o no
	public bool ContainsElement (Vector3 position)
	{
		return entities.ContainsKey (new Vector3Int (position));
	}
	
	public bool ContainsElement (Vector3Int position)
	{
		return entities.ContainsKey (position);
	}
	
	#endregion
	
	#region Sensor Spaces Management
	
	public void AddSensor (BasicSensor sensor)
	{
		foreach (Vector3 direction in sensor.Directions) {
			Vector3Int pos = new Vector3Int (sensor.transform.position + direction);
			if (!sensorSpaces.ContainsKey (pos)) {
				sensorSpaces [pos] = new List<BasicSensor> ();
			}
			sensorSpaces [pos].Add (sensor);
		}
	}
	
	public void RemoveSensor (BasicSensor sensor)
	{
		foreach (Vector3 direction in sensor.Directions) {
			Vector3Int pos = new Vector3Int (sensor.transform.position + direction);
			sensorSpaces [pos].Remove (sensor);
		}
	}
	
	public bool ContainsSensor(Vector3 position){
		return SensorSpaces.ContainsKey (new Vector3Int (position));
	}
	
	public void SensorActivated ()
	{
		sensorsLeft --;
		if (sensorsLeft == 0 && !isExiting){  
			StartCoroutine(GoBack());
			levelCamera.PlayWin();
			
			isExiting = true;
			if (UserSettings.Instance.CurrentPlayer.Levels.ContainsKey(Application.loadedLevelName)){
				UserSettings.Instance.CurrentPlayer.Levels.Remove(Application.loadedLevelName);
			}
			UserSettings.Instance.CurrentPlayer.Levels.Add(Application.loadedLevelName,new LevelData(Application.loadedLevelName,stepCount));
			UserSettings.Instance.SavePlayerData();
		}
	}
	
	IEnumerator GoBack(){
		yield return new WaitForSeconds(3f);
		ExitLevel();
	}
	
	public void SensorDeactivated(){
		sensorsLeft++;
	}
	
	public void notifySwitches(Vector3Int position){
		if (sensorSpaces.ContainsKey (position)) {
			foreach (BasicSensor s in sensorSpaces[position]) {
				if(s is SensorSwitch){
					s.NotifyPressed (position);
				}
			}
		}
	}
	#endregion
	
	#region Monobehaviours Methods
	
	void Awake ()
	{
        if (Application.loadedLevelName == "MainMenu" || Application.loadedLevelName == "Options" || Application.loadedLevelName == "PlanetSelector" || Application.loadedLevelName == "ProfileSelector")
        {
            isMenu = true;
        }
        if (!isMenu)
        {
            Lumos.Event("Started Level " + Application.loadedLevelName);
        }
		entities = new Dictionary<Vector3Int, GameEntity> (new Vector3EqualityComparer ());
		sensorSpaces = new Dictionary<Vector3Int, List<BasicSensor>> (new Vector3EqualityComparer ());
        images[0] = Resources.Load("Art/Textures/GUI/button_exit") as Texture2D;
        images[1] = Resources.Load("Art/Textures/GUI/button_hint") as Texture2D;
        images[2] = Resources.Load("Art/Textures/GUI/button_restart") as Texture2D;
        skin = Resources.Load("Art/Textures/GUI/ingame_skin") as GUISkin;
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject))){
        	if (go.name == "Camera"){
        		CameraDecubeLevel c = go.GetComponent<CameraDecubeLevel>();
        		levelCamera = c;
        		break;
        	}
        }
		hints = new List<string>();
		path = "Assets/Resources/Txt/hints.txt";
		try{
        	tr = new StreamReader(path);
			string temp;
	        while((temp = tr.ReadLine()) != null)
	        {
				if(hints.Count == 12){
	            	hints.Add(temp);
				}
	            hints.Add(temp);
	        }
	 
	        // Close the stream
	        tr.Close();
		} 
		catch(FileLoadException e) {
			Debug.LogException(e);
		}
	}

    void OnGUI()
    {
        if (isMenu) return;

        if(skin != null){
            GUI.skin = skin;
        }

        float d = images[0].width * 0.75f;
        if (GUI.Button(new Rect(5, 5, d, d), images[0]))
        {
            ExitLevel();
        }

        if (GUI.Button(new Rect(d + 10, 5, d, d), images[2]))
        {
            restartLevel();
        }

        if (GUI.Button(new Rect(Screen.width - d -5, Screen.height - d - 5, d, d), images[1])) 
        {
            showHint = !showHint;
        }
        
        float hintWidth = 512.0f * 0.75f , hintHeight = 128.0f * 0.75f;
        if (showHint)
        {
            if(!askedForHint){
                Lumos.Event("Asked for hint on level " + Application.loadedLevelName, Time.timeSinceLevelLoad);
                askedForHint = true;
            }
            //Quemo el tama�o de la textura de fondo
            GUI.TextArea(new Rect(Screen.width / 2 - hintWidth / 2 - 5, Screen.height - hintHeight - 5, hintWidth, hintHeight), hints[Int16.Parse(Application.loadedLevelName) - 1]);
        }

        Rect stepCountRect = new Rect(Screen.width - d - 5, 5, d, d);
        GUI.Box(stepCountRect, "");
        GUI.Label(stepCountRect, stepCount.ToString());

        if (sensorsLeft == 0)
        {
            GUI.Label(new Rect(0, 20, Screen.width, 80), "Ganaste!!");
			GUI.Label(new Rect(0, 70, Screen.width, 80), "Numero de pasos: " + stepCount);
        }
    }

	#endregion
	
	#region Steps Management
	
	public void addStep(){
		stepCount++;
	}
	
	#endregion
	
	#region Gets and Sets
	
    public static Vector2 Dimension
    {
        get { return Level.dimension; }
    }
	
	public static Level Singleton
    {
        get
        {
            if (singleton == null)
            {
                singleton = new GameObject("Level").AddComponent<Level>();
            }
            return singleton;
        }
    }
	
	/// <summary>
	/// The cube that is selected in the whole world.
	/// </summary>
	public Cube SelectedCube
    {
        set
        {
            if (selectedCube != value)
       	 	{
            	if(selectedCube != null){
            	    selectedCube.IsSelected = false;
            	}
            	if (value != null){
            		value.IsSelected = true;
            	}
            	selectedCube = value;
			}
        }	
		
		get { return selectedCube; }
    }
	
	
	

	public Dictionary<Vector3Int, List<BasicSensor>> SensorSpaces {
		get {
			return this.sensorSpaces;
		}
	}
	#endregion

	public bool IsInDimension (Vector3 position)
	{
		return position.x >= dimension.x && position.x <= dimension.y && // Check x
			position.z >= dimension.x && position.z <= dimension.y; // Check y
	}
}