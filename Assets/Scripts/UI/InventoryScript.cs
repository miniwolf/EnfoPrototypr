using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {
	private GameObject [,] buttons;
	//public GUISkin skin;
	private const int xSlots = 2;
	private const int ySlots = 3;
	
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
		Debug.Log("iconwidth = " + iconWidth + " iconheight = " + iconHeight);
		for(int i = 0; i < xSlots; i++){
			for(int j = 0; j < ySlots; j++){
				buttons[i,j] = null;
			}
		}
		
		//test

		GameObject go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,0,0);
		/*
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,0,1);
		go = (GameObject) Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		AddButton (go,1,2);*/
	}
	
	public void AddButton(GameObject go, int x, int y){
		if(x < xSlots && y <ySlots && x >=0 && y>=0){
			buttons[x,y] = go;
		}
		RectTransform butRt = go.GetComponent<RectTransform>();
		go.transform.SetParent(gameObject.transform);
		go.transform.position = gameObject.transform.position;
		/*go.transform.position += new Vector3( rt.rect.width/2, -rt.rect.height/2,0f);*/
		//go.transform.localPosition = gameObject.transform.localPosition;

		//go.transform.localPosition = new Vector3(0f,0f,0f);
		go.transform.localScale = new Vector3(1,1,1);
		//go.transform.position += new Vector3 (-iconWidth*(2-x), iconHeight*(3-y),0f);
		go.transform.localPosition += new Vector3 (-iconWidth * (0.5f - x),iconHeight * (1f - y) ,0);
	}

	/*Adds a button in the first open positioni of the inventory*/
	public bool AddButton(GameObject button){
		for(int i = 0; i < ySlots; i++){
			for(int j = 0; j < xSlots; j++){
				if(buttons[j,i] == null){
					AddButton(button,j,i);
					return true;
				}
			}
		}
		return false;
	}

	public void removeButton(int x, int y){
		Destroy(buttons[x,y]);
		buttons[x,y] = null;
	}

	public void removeButton(GameObject button){
		for(int i = 0; i < xSlots; i++){
			for(int j = 0; j < ySlots; j++){
				if(buttons[i,j] == button){
					removeButton(i,j);
					return;
				}
			}
		}
	}

}
