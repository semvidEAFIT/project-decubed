using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public abstract class CubeController : MonoBehaviour
{

    #region Atributtes
    private Cube cube;
    private Queue<Command> commandQueue;

    public Cube Cube
    {
        get { return cube; }
    }

    #endregion
	
	#region MonoBehaviour
    
	protected virtual void Awake() {
        commandQueue = new Queue<Command>();
        cube = gameObject.GetComponent<Cube>();
    }

    protected virtual void Update()
    {
        if (commandQueue.Count > 0 && cube.IsSelected)
        {
			Command c = commandQueue.Dequeue();
			cube.Command = c;
            ExecuteCommand(c);
        }
    }
	
	#endregion
	
	#region Command Management
	
    protected void AddCommand(Command c) {
        commandQueue.Enqueue(c);
    }

    private void ExecuteCommand(Command command)
    {
        command.Listener = this;
        command.Execute();
    }
    
    public abstract void CommandFinished(Command command);

	#endregion
}
