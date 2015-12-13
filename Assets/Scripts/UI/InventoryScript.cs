using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {
	private GameObject[,] buttons;
	private const int xSlots = 2;
	private const int ySlots = 3;
	
	private RectTransform rt;
	private float iconHeight, iconWidth;
	private float xPos, yPos;
	private bool isFull = false;

	// Use this for initialization
	void Start () {
		buttons = new GameObject[xSlots,ySlots];
		rt = gameObject.GetComponent<RectTransform>();
		iconWidth = rt.rect.width/(xSlots+1);
		iconHeight = rt.rect.height/(ySlots+1);
		xPos = gameObject.transform.position.x;
		yPos = gameObject.transform.position.y;
	}

	private void AddButton(GameObject go, int x, int y){
		buttons[x,y] = go;
		RectTransform butRt = go.GetComponent<RectTransform>();
		go.transform.SetParent(gameObject.transform);
		go.transform.position = gameObject.transform.position;
		go.transform.localScale = new Vector3(1,1,1);
		go.transform.localPosition += new Vector3(-iconWidth * (0.5f - x), iconHeight * (1f - y), 0);
	}

	// Adds a button in the first open positioni of the inventory
	public bool AddButton(GameObject button){
		if ( isFull ) {
			return false;
		}
		for ( int i = 0; i < ySlots; i++ ) {
			for ( int j = 0; j < xSlots; j++ ) {
				if ( buttons[j,i] == null ) {
					AddButton(button, j, i);
					return true;
				}
			}
		}
		isFull = true;
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
					isFull = false;
					return;
				}
			}
		}
	}

}
