using UnityEngine;
using System.Collections.Generic;

public class CubeControllerInput : CubeController {
	
	#region Attributes	
    private Dictionary<MoveOptionSelector, Command> moveOptions;
    private bool initedSelectors = false;
	public Object moveOptionSelector; //TODO: volverlo privado
	#endregion
	
	#region CubeController
	
    protected override void Awake()
    {
        base.Awake();
        moveOptions = new Dictionary<MoveOptionSelector, Command>();
    }

    protected override void Update()
    {
        base.Update();
    }
	
    void OnDestroy() { 
        foreach(MoveOptionSelector s in moveOptions.Keys){
            if(s != null){
                Destroy(s.gameObject);
            }
        }
    }
  
	public override void CommandFinished(Command command)
    {		
//        if (Cube.IsSelected)
//        {
//            UpdateMoveOptionsSelectors();
//        }
    }
	
	#endregion
	
	#region Move Option Selectors
	
    public void UpdateMoveOptionsSelectors()
    {
       	RemoveCommandOptions();
        foreach(Command c in Cube.GetOptions()){
            GameObject selectorGameObject = (GameObject)Instantiate(moveOptionSelector);
			
			// Add the component 
            MoveOptionSelector selector = selectorGameObject.AddComponent<MoveOptionSelector>();
			selector.Cube = Cube;
            selectorGameObject.name = "Selector" + gameObject.name;
            selector.Listener = this;
            selectorGameObject.transform.position = c.EndPosition.ToVector3;
            moveOptions.Add(selector, c);
        }
    }
	
	public void clearMoveOptions() {
		 foreach(MoveOptionSelector s in moveOptions.Keys){
            Destroy(s.gameObject);
        }
	}
	
	#endregion
	
	public void NotifyOptionSelected(MoveOptionSelector selector)
    {
        AddCommand(moveOptions[selector]);
    }
    
    public void RemoveCommandOptions(){
    	moveOptions.Clear();
    	clearMoveOptions();
    }
	
	public void NotifyMoveTO(Command c){
		AddCommand(c);
	}

}
