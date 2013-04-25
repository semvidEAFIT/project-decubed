using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpriteSheet : MonoBehaviour {
	
	public int materialIndex;
	public int[] sequenceFrameCount;
	
	private bool loop;
	private bool reverse;
	private bool active = true;
	private bool smoothTransition; // This is when the sequence changes based on the get and sets
	
	public int frameWidth;
	public int frameHeight;
	public int currentSequence = 0;
	public int currentFrame;
	
	public float fps = 15f;
	
	private SpriteSequence[] sequences;
	private int colCount;
	private int rowCount;
	private int currentRow;
	private int currentCol;
	
	private List<ISpriteSheet> listeners;
	
	// Use this for initialization
	void Start () {
		colCount = renderer.materials[materialIndex].mainTexture.width / frameWidth;
		rowCount = renderer.materials[materialIndex].mainTexture.height / frameHeight;
		sequences = new SpriteSequence[sequenceFrameCount.Length];
		int counter = 0;
		for (int i =0 ; i < sequences.Length ; i++){
			sequences[i] = new SpriteSequence(counter, counter + sequenceFrameCount[i] - 1);
			counter += sequenceFrameCount[i];
		}
		renderer.materials[materialIndex].SetTextureScale("_MainTex", new Vector2(1f/colCount,1f/rowCount));
		StartCoroutine(UpdateSprite());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	private IEnumerator UpdateSprite()
    {
    	while (true){
	    	if (active){
	    		SpriteSequence sequence = sequences[currentSequence];
	    		if (currentFrame < sequence.InitFrame || currentFrame > sequence.EndFrame){
	    			currentFrame = sequence.InitFrame;
	    		} 
	    		if (loop){
	    			if (reverse){
	    				if (currentFrame <= sequence.InitFrame){
	    					currentFrame = sequence.EndFrame;
	    				}else{
	    					currentFrame--;
	    				}
	    			}else{
	    				if (currentFrame >= sequence.EndFrame){
	    					currentFrame = sequence.InitFrame;
	    				}else{
	    					currentFrame++;
	    				}
	    			}
	    		}else{
	    			if (reverse){
	    				if (sequence.InitFrame < currentFrame){
		    				currentFrame--;
		    			}
	    			}else{
		    			if (sequence.EndFrame > currentFrame){
		    				currentFrame++;
		    			}
	    			}
	    		}
		    	UpdateFramePosition();
		    	
		    	Vector2 offset = new Vector2( (currentCol)/((float)colCount),(rowCount - 1 - currentRow)/((float)rowCount));
	            renderer.materials[materialIndex].SetTextureOffset("_MainTex", offset);
		    	
	    	}
	    	yield return new WaitForSeconds(1f / fps);
    	}
    }
    
    private void UpdateFramePosition(){
    	currentCol = currentFrame % colCount;
    	currentRow = currentFrame / colCount;
    }
    
    #region Get and Sets
    
   	
   	public int MaterialIndex {
		get {
			return this.materialIndex;
		}
		set {
			materialIndex = value;
		}
	}

	public int[] SequenceFrameCount {
		get {
			return this.sequenceFrameCount;
		}
		set {
			sequenceFrameCount = value;
		}
	}

	public bool Loop {
		get {
			return this.loop;
		}
		set {
			loop = value;
		}
	}

	public bool Reverse {
		get {
			return this.reverse;
		}
		set {
			reverse = value;
		}
	}

	public bool Active {
		get {
			return this.active;
		}
		set {
			active = value;
		}
	}

	public bool SmoothTransition {
		get {
			return this.smoothTransition;
		}
		set {
			smoothTransition = value;
		}
	}

	public int FrameWidth {
		get {
			return this.frameWidth;
		}
		set {
			frameWidth = value;
		}
	}

	public int FrameHeight {
		get {
			return this.frameHeight;
		}
		set {
			frameHeight = value;
		}
	}

	public int CurrentSequence {
		get {
			return this.currentSequence;
		}
		set {
			currentSequence = value;
		}
	}

	public int CurrentFrame {
		get {
			return this.currentFrame;
		}
		set {
			currentFrame = value;
		}
	}

	public float Fps {
		get {
			return this.fps;
		}
		set {
			fps = value;
		}
	}
	
	public SpriteSequence[] Sequences {
		get {
			return this.sequences;
		}
	}

	public int ColCount {
		get {
			return this.colCount;
		}
	}

	public int RowCount {
		get {
			return this.rowCount;
		}
	}

	public int CurrentRow {
		get {
			return this.currentRow;
		}
	}

	public int CurrentCol {
		get {
			return this.currentCol;
		}
	}
    #endregion
}
