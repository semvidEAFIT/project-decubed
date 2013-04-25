using UnityEngine;
using System.Collections;
 
class Sprite : MonoBehaviour
{

	public bool animationActive = true;
	public bool reverse = false;
	public bool loop = false;
	public int currentRow = 0;
	public int index = 0;

    public int[] rowColumns;
    public float fps = 20f;
	public Vector2 spriteSize; // example 16 columns by 2 rows

    void Start()
    {
        renderer.materials[0].SetTextureScale("_MainTex", new Vector2(1f/spriteSize.x,1f/spriteSize.y));
        StartCoroutine(updateTiling());
    }

	void Update(){
	}

	private void rotateAnimation(Vector3 direction, bool rotation)
	{

	}
 
    private IEnumerator updateTiling()
    {
        while (animationActive)
        {
			if (!reverse){
				if (loop){
					index++;
					index = index % rowColumns[currentRow];
				}else if (index < rowColumns[currentRow]){
					index++;
				}

			}else{
				if (loop){
					index--;
					if (index < 0){
						index = rowColumns[currentRow] - 1;
					}
				}else if (index > 0){
					index--;
				}
			}
            //split into x and y indexes
            Vector2 offset = new Vector2( ((float)index)/spriteSize.x,((float)currentRow)/spriteSize.y);
            renderer.materials[0].SetTextureOffset("_MainTex", offset);
            yield return new WaitForSeconds(1f / fps);
        }
    }
}
