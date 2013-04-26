using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpriteSheet : MonoBehaviour {
	
	public int materialIndex;
	public List<int> sequenceFrameCount = new List<int>();
	
	public bool loop;
	public bool reverse;
	public bool running = true;
	public bool smoothTransition; // This is when the sequence changes based on the get and sets
	private bool smoothActive = false;
	public int frameWidth;
	public int frameHeight;
	public int currentSequence;
	public int currentFrame;
	
	public float fps;
	
	private SpriteSequence[] sequences;
	private int colCount;
	private int rowCount;
	public int currentRow;
	public int currentCol;
	
	private List<ISpriteSheet> listeners;
	
	// Use this for initialization
	void Start () {
		if (frameWidth != 0){
			colCount = renderer.materials[materialIndex].mainTexture.width / frameWidth;
		}
		if (frameHeight != 0){
			rowCount = renderer.materials[materialIndex].mainTexture.height / frameHeight;
		}
		sequences = new SpriteSequence[sequenceFrameCount.Count];
		int counter = 0;
		for (int i =0 ; i < sequences.Length ; i++){
			sequences[i] = new SpriteSequence(counter, counter + sequenceFrameCount[i] - 1);
			counter += sequenceFrameCount[i];
		}
		//renderer.materials[materialIndex].SetTextureScale("_MainTex", new Vector2(1f/colCount,1f/rowCount));
		StartCoroutine(UpdateSprite());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	private IEnumerator UpdateSprite()
    {
    	while (true){
	    	if (running){
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

	public int CurrentSequence {
		get {
			return this.currentSequence;
		}
		set {
			currentSequence = value;
		}
	}
}
