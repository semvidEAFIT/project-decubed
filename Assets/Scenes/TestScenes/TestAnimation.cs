using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour, ISpriteSheet {
	
	SpriteSheet sprite;
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteSheet>();
		sprite.AddSpriteSheetListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.L)){
			sprite.loop = !sprite.loop;
		}
		
		if (Input.GetKeyDown(KeyCode.A)){
			sprite.running = !sprite.running;
		}
		
		if (Input.GetKeyDown(KeyCode.R)){
			sprite.reverse = !sprite.reverse;
		}
		
		if (Input.GetKeyDown(KeyCode.S)){
			sprite.smoothTransition = !sprite.smoothTransition;
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			sprite.CurrentSequence = (sprite.CurrentSequence + 1) % sprite.sequenceFrameCount.Count;
		}
	}
	
	public void SequenceEnded (SpriteSheet spriteSheet)
	{
		Debug.Log("Termine:" + spriteSheet.currentFrame + ", " + spriteSheet.lastFrame);
	}
}


















