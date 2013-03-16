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
    public Rect restartButton = new Rect(0,0, Screen.width*0.1f, Screen.height *0.1f);
	
    private static Level singleton;
	
	/// <summary>
	/// x -> inf limit in y and x.
	/// y -> sup limit in y and x
	/// </summary>
	private static Vector2 dimension = new Vector2(0, 10);
	
	#endregion
//			
	#region Entities Dictionary Management
	
	public void AddEntity(GameEntity entity, Vector3 position){
//		if (e is Sensor)
//        {
//            sensors.Add(e);
//        }
		entities.Add(new Vector3Int(position), entity);
	}
		
	public void RemoveEntity(Vector3 position){
		entities.Remove(new Vector3Int(position));
	}
	
	public GameEntity getEntity(Vector3 position){
		return entities[new Vector3Int(position)];
	}
	
	///TODO: REvisar si borrar o no
	public bool ContainsElement(Vector3 position){
		return entities.ContainsKey(new Vector3Int(position));
	}
	
	public bool ContainsElement(Vector3Int position){
		return entities.ContainsKey(position);
	}
	
	#endregion
	
	#region Monobehavious Methods
	
	void Awake()
    {
        entities = new Dictionary<Vector3Int, GameEntity>(new Vector3EqualityComparer());
    }

	#endregion
	
	//	
//	#region Monobehavious Methods
//    void Awake()
//    {
//        entities = new Dictionary<Vector3Int, Entity>(new Vector3EqualityComparer());
//        sensors = new ArrayList();
//    }
//	
//	void Start(){
//	}
//
//    void OnDestroy()
//    {
//        singleton = null;
//    }
//
//    void OnGUI() { 
//        if(GUI.Button(restartButton, "Restart")){
//            Application.LoadLevel(Application.loadedLevelName);    
//        }
//    }
//	#endregion
//	
////	public void NotifyChangePressed(Sensor s)
////    {
////        if (s.IsPressed)
////        {
////            sensors.Remove(s);
////            if(sensors.Count == 0){
////                //GameController.Singleton.NotifyEndLevel(Application.loadedLevelName);
////				//Application.LoadLevel(UnityEngine.Random.Range(1,Application.levelCount-1));
////				Debug.Log("Termino Juego");
////				//TODO Terminar Juego
////            }
////        } else if(!sensors.Contains(s)){
////                sensors.Add(s);
////        }
////    }
//	
	
	#region Gets and Sets
	
    public static Vector2 Dimension
    {
        get { return Level.dimension; }
    }

    public Dictionary<Vector3Int, GameEntity> Entities
    {
        get { return entities; }
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
            	value.IsSelected = true;
            	selectedCube = value;
			}
        }	
		
		get { return selectedCube; }
    }
	
	
	
	#endregion

	public bool IsInDimension (Vector3 position)
	{
		return position.x >= dimension.x && position.x <= dimension.y && // Check x
			position.z >= dimension.x && position.z <= dimension.y; // Check y
	}
}
