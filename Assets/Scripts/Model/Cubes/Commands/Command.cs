public abstract class Command
{
	#region variables
	private Cube receiver;
	private Vector3Int endPosition;
	//private CubeController listener;
	#endregion
	
	public Command (Cube receiver, Vector3Int endPosition)
	{
		this.receiver = receiver;
		this.endPosition = endPosition;
	}
	
	#region Command Methods
	public virtual void EndExecution ()
	{
		if (endPosition.x < 0 
			|| endPosition.x >= Level.Dimension 
			|| endPosition.z < 0 
			|| endPosition.z >= Level.Dimension) {
			Cube.FallOutOfBounds (endPosition.ToVector3);
		}
		//listener.CommandFinished (this);
	}
	
	public abstract void Execute ();
	#endregion
	
	public override string ToString ()
	{
		return string.Format ("[Command: EndPosition={0}]", EndPosition);
	}
	
	#region Gets and Sets
//	public CubeController Listener {
//		set { listener = value; }
//	}
	
	public Vector3Int EndPosition {
		get { return endPosition; }
	}

	protected Cube Cube {
		get { return receiver;}
	}
	#endregion
}
