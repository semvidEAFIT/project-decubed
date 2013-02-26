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
    private Dictionary<Vector3Int, Entity> entities;
    private static int dimension = 10;
    public Rect restartButton = new Rect(0,0, Screen.width*0.1f, Screen.height *0.1f);
	private ArrayList sensors;
    private static Level singleton;
	#endregion
//			
	#region Entities Dictionary Management
	public void AddEntity(Entity entity, Vector3 position){
//		if (e is Sensor)
//        {
//            sensors.Add(e);
//        }
		entities.Add(new Vector3Int(position),entity);
	}
		
	public void RemoveEntity(Vector3 position){
		entities.Remove(new Vector3Int(position));
	}
	
	public Entity getEntity(Vector3 position){
		return entities[new Vector3Int(position)];
	}
	
	public bool ContainsElement(Vector3 position){
		return entities.ContainsKey(new Vector3Int(position));
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
	
    public static int Dimension
    {
        get { return Level.dimension; }
    }

    public Dictionary<Vector3Int, Entity> Entities
    {
        get {return entities; }
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
	#endregion
}
