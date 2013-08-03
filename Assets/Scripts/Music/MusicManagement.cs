using UnityEngine;
using System.Collections;

public class MusicManagement : MonoBehaviour {
	
	private int i;
	public AudioClip[] sounds;
	
	// Use this for initialization
	void Start () {
		i = 0;
		audio.clip = sounds[0];
		audio.Play();
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(!audio.isPlaying){
			if (i == sounds.Length-1){
				i=0;
			}else{
				i++;
			}
			audio.clip = sounds[i];
			audio.Play();
		}
	}
}
