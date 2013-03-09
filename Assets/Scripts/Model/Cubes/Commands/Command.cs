
/// <summary>
/// Command.
/// TODO: Tener en cuenta propuesta de no mostrar movimiento final 
/// , por ejemplo en el hielo, solo la siguiente, y que igual se mueva hasta al final (para aumetnar dificultad 
/// y darle emocion al juego)
/// </summary>
public abstract class Command
{
	#region variables
	
	private Cube cube;
	private Vector3Int endPosition;
	private CubeController listener;
	
	#endregion
	
	public Command (Cube cube, Vector3Int endPosition)
	{
		this.cube = cube;
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
		listener.CommandFinished (this);
	}
	
	public abstract void Execute ();
	
	#endregion
	
	#region Gets and Sets
	
	public CubeController Listener {
		set { listener = value; }
	}
	
	public Vector3Int EndPosition {
		get { return endPosition; }
	}

	protected Cube Cube {
		get { return cube;}
	}
	
	#endregion
}
