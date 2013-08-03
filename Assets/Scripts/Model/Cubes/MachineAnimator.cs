using UnityEngine;
using System.Collections;

public class MachineAnimator : MonoBehaviour {
	private bool duplication;
	// Use this for initialization
	void Start () {
		duplication = true;
		CubeAnimations.AnimateDuplication(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EndExecution(){
		if(duplication){
			CubeAnimations.AnimateMove(this.gameObject, Vector3.down, FindEndPosition());
			duplication = false;
		}else{
			CubeAIBase c = GetComponent<CubeAIBase>();
			Level.Singleton.AddEntity( c ,new Vector3Int(FindEndPosition()));
		}
	}
	
	public Vector3 FindEndPosition(){
		Vector3 endPos = transform.position;
		while(endPos.y>1){
			if(CubeHelper.IsFree(new Vector3Int(endPos+Vector3.down))){
				endPos.y--;
			}else {
				break;
			}
		}
		return endPos;
	}
}
