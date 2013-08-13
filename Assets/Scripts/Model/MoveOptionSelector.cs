using UnityEngine;
using System.Collections;

/// <summary>
/// Move option selector is an option component to
/// show an enabled next position in level to move.
/// </summary>
public class MoveOptionSelector : MonoBehaviour, IClickable {

    private CubeControllerInput listener;
	private Cube cube;
	public float minScale = 0.5f;
	private BoxCollider b;
	void Start(){
		transform.localScale = new Vector3(minScale,minScale,minScale);
		b = GetComponent<BoxCollider>();
		b.size = transform.localScale * 2;
	}
	
	void Update(){
		
	}
	
    public CubeControllerInput Listener
    {
        set { listener = value; }
    }
	
	#region ClickListener
    public void NotifyClick()
    {	
    	
        //listener.NotifyOptionSelected(this);
    }
    
    void OnMouseOver(){
    	transform.localScale = new Vector3(1,1,1);
        b.size = transform.localScale;
    }
    
    void OnMouseExit(){
		transform.localScale = new Vector3(minScale,minScale,minScale);
		b.size = transform.localScale * 2;
    }
    
    public void NotifyUnClick(){
		listener.NotifyOptionSelected(this);
    }
	
	public void NotifyChange(){
		
	}
	
	public Cube Cube {
		get {
			return this.cube;
		}
		set {
			cube = value;
		}
	}
	#endregion
}
