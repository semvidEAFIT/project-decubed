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
        if (Cube.IsSelected)
        {
            if(!initedSelectors){
                UpdateMoveOptionsSelectors();
                initedSelectors = true;
            }
        }
        else 
        {
            if(moveOptions.Count > 0){
                foreach (MoveOptionSelector s in moveOptions.Keys)
                {
                    Destroy(s.gameObject);
                }
                moveOptions.Clear();
                initedSelectors = false;
            }
        }
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
        if (Cube.IsSelected)
        {
            UpdateMoveOptionsSelectors();
        }
    }
	
	#endregion
	
	#region Move Option Selectors
	
    private void UpdateMoveOptionsSelectors()
    {
       	clearMoveOptions();
		
        foreach(Command c in Cube.Options){
            GameObject selectorGameObject = (GameObject)Instantiate(moveOptionSelector);
			
			// Add the component 
            MoveOptionSelector selector = selectorGameObject.AddComponent<MoveOptionSelector>();
            selectorGameObject.name = "Selector" + gameObject.name;
            selector.Listener = this;
            selectorGameObject.transform.position = c.EndPosition.ToVector3;
            moveOptions.Add(selector, c);
        }
    }
	
	private void clearMoveOptions() {
		 foreach(MoveOptionSelector s in moveOptions.Keys){
            Destroy(s.gameObject);
        }
		
        moveOptions.Clear();
	}
	
	#endregion
	
	public void NotifyOptionSelected(MoveOptionSelector selector)
    {
        AddCommand(moveOptions[selector]);
		clearMoveOptions();
    }

}
