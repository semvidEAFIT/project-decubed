using UnityEngine;
using System.Collections.Generic;

public class CubeControllerInput : CubeController {
    private Dictionary<MoveOptionSelector, Command> commands;
    public Object selectorCube;
    private bool initedSelectors = false;

    protected override void Awake()
    {
        base.Awake();
        commands = new Dictionary<MoveOptionSelector, Command>();
    }

    public override void CommandFinished(Command command)
    {
        base.CommandFinished(command);
		
        if (Cube.IsSelected)
        {
            UpdateSelectors();
        }
    }

    private void UpdateSelectors()
    {
        foreach(MoveOptionSelector s in commands.Keys){
            Destroy(s.gameObject);
        }
        commands.Clear();
        foreach(Command c in Cube.Options){
            GameObject selectorGameObject = (GameObject)Instantiate(selectorCube);
			// Add the component 
            MoveOptionSelector selector = selectorGameObject.AddComponent<MoveOptionSelector>();
            selectorGameObject.name = "Selector" + gameObject.name;
            selector.Listener = this;
            selectorGameObject.transform.position = c.EndPosition.ToVector3;
            commands.Add(selector, c);
        }
    }

    void OnDestroy() { 
        foreach(MoveOptionSelector s in commands.Keys){
            if(s != null){
                Destroy(s.gameObject);
            }
        }
    }

    protected override void Update()
    {
        if (Cube.IsSelected)
        {
            if(!initedSelectors){
                UpdateSelectors();
				print ("ENTRO 1");
                initedSelectors = true;
            }
        }
        else 
        {
            if(commands.Count > 0){
                foreach (MoveOptionSelector s in commands.Keys)
                {
                    Destroy(s.gameObject);
                }
                commands.Clear();
                initedSelectors = false;
            }
        }
        base.Update();
    }

    public void NotifySelection(MoveOptionSelector selector)
    {
        AddCommand(commands[selector]);
    }
}
