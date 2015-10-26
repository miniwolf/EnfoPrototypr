using UnityEngine;
using System.Collections;

public class ActionInspectorScript : MonoBehaviour {

	public GameObject [,] buttons;
	//public GUISkin skin;
	public int xSlots = 4;
	public int ySlots = 3;
	
	private RectTransform rt;
	private float iconHeight;
	private float iconWidth;
	private float xPos;
	private float yPos;
	// Use this for initialization
	void Awake () {
		buttons = new GameObject[xSlots,ySlots];
		rt = gameObject.GetComponent<RectTransform>();
		iconWidth = rt.rect.width/(xSlots+1);
		iconHeight = rt.rect.height/(ySlots+1);
		xPos = gameObject.transform.position.x;
		yPos = gameObject.transform.position.y;

		for(int i = 0; i < xSlots; i++){
			for(int j = 0; j < ySlots; j++){
				buttons[i,j] = null;
			}
		}

		//test
		GameObject go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,0,0);
		/*go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,0,1);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,0,2);*/

		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,1,0);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,1,1);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,1,2);

		/*go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,2,0);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,2,1);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,2,2);

		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,3,0);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,3,1);*/
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,3,2);
	}

	public void AddButton(GameObject go, int x, int y){
		if(x < xSlots && y <ySlots && x >=0 && y>=0){
			buttons[x,y] = go;
		}
		RectTransform butRt = go.GetComponent<RectTransform>();
		go.transform.SetParent(gameObject.transform);
		go.transform.localScale += new Vector3(1,1,1);
		go.transform.localPosition += new Vector3(iconWidth/1.2f + iconWidth*(x)*1.1f,-iconHeight/1.2f -iconHeight*(y)*1.1f,0f);





	}
	
}
