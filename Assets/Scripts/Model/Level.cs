using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

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
	
	/// <summary>
	/// x -> inf limit in y and x.
	/// y -> sup limit in y and x
	/// </summary>
	private static Vector2 dimension = new Vector2(0, 10);
	
	#endregion
	
	void OnGUI ()
	{
        float d = Screen.height / 6;
        if(GUI.Button(new Rect(5, 5, d, d), "Exit\nLevel")){
            exitLevel();
        }

        if(GUI.Button(new Rect(d + 10, 5, d, d), "Restart\nLevel")){
            restartLevel();
        }

        Rect stepCountRect = new Rect(Screen.width - d - 5, 5, d, d);
        GUI.Box(stepCountRect, "");
        GUI.TextField(stepCountRect, stepCount.ToString());

		if (sensorsLeft == 0){      
			GUI.Label( new Rect(  0, 20, Screen.width - 20,40), "Level Completed");
		}
	}

    private void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void exitLevel()
    {
        Application.LoadLevel("MainMenu");
    }

	#region Entities Dictionary Management
	
	public void AddEntity (GameEntity entity, Vector3 position)
	{
		AddEntity (entity, new Vector3Int (position));
	}
	
	public void AddEntity (GameEntity entity, Vector3Int position)
	{
		entities.Add (position, entity);
		if (entity is BasicSensor) {
			AddSensor ((BasicSensor)entity);
			sensorsLeft += 1;
		} else {
			if (sensorSpaces.ContainsKey (position)) {		
				Cube c = (Cube)entity;
				c.setMood(Cube.Mood.Proud);
				foreach (BasicSensor s in sensorSpaces[position]) {
					s.NotifyPressed (position);
				}
			}
			//TODO: temporal mientras se arregla el cubo hielo y se le ponen las caras al resto
			try{
			Cube cu = (Cube)entity;
			if(position.y > 1 && Level.Singleton.getEntity(position.ToVector3 + Vector3.down) is Cube){
				
				Cube c = (Cube)Level.Singleton.getEntity(position.ToVector3 + Vector3.down);
					c.setMood(Cube.Mood.Angry);
				
			}}catch (Exception e){
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
					c.setMood(Cube.Mood.Normal);
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
	
	public void SensorActivated ()
	{
		
		//TODO revisar si termino nivel
		sensorsLeft --;
		
		if (sensorsLeft == 0){  
			
		}
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
	
	#region Monobehavious Methods
	
	void Awake ()
	{
		entities = new Dictionary<Vector3Int, GameEntity> (new Vector3EqualityComparer ());
		sensorSpaces = new Dictionary<Vector3Int, List<BasicSensor>> (new Vector3EqualityComparer ());
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