using UnityEngine;
using System.Collections;

public class ActionInspectorScript : MonoBehaviour {

	public GameObject [,] buttons;
	//public GUISkin skin;
	public const int xSlots = 4;
	public const int ySlots = 3;
	
	private RectTransform rt;
	private float iconHeight;
	private float iconWidth;
	private float xPos;
	private float yPos;
	// Use this for initialization
	void Start () {
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
	}

	public void AddButton(GameObject go, int x, int y){
		if(!go){
			return;
		}
		if(x < xSlots && y <ySlots && x >=0 && y>=0){
			buttons[x,y] = go;
		}

		RectTransform butRt = go.GetComponent<RectTransform>();
		go.SetActive(true);
		go.transform.SetParent(gameObject.transform);
		go.transform.localPosition = new Vector3(0f,0f,0f);
		go.transform.localScale = new Vector3(1,1,1);
		//go.transform.localPosition += new Vector3(iconWidth/1.2f + iconWidth*(x)*1.1f,-iconHeight/1.2f -iconHeight*(y)*1.1f,0f);
		go.transform.localPosition = new Vector3 (-iconWidth*(4-x), iconHeight*(3-y),0f);
	}	

	public void AddAllButtons(GameObject[,] array){
		if(array == null){
			/*it happens with enemies*/
			return;
		}
		for(int i = 0; i < xSlots; i++){
			for(int j = 0; j < ySlots; j++){
				AddButton(array[i,j],i,j);
			}
		}
	}

	public void ResetButtons(){
		for(int i = 0; i < xSlots; i++){
			for(int j = 0; j < ySlots; j++){
				if(buttons[i,j] != null){
					buttons[i,j].SetActive(false);
					buttons[i,j] = null;
				}
			}
		}
	}
}
