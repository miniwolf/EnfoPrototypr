using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopClickable : Clickable {

	void Start () {
		maxMana = 0;
		maxHealth = 1000;
		currentMana = 0;
		currentHealth = 999;
		name = "Item Shop";
		picture = Resources.Load<Sprite>("Icons/dragon");
		buttons = new GameObject[4,3];
		buttons[0,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/knife");
		buttons[0,0].SetActive(false);
		
		buttons[0,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,1].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/helmet");
		buttons[0,1].SetActive(false);
		
		buttons[0,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,2].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/axe");
		buttons[0,2].SetActive(false);
		
		buttons[1,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[1,0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/pants");
		buttons[1,0].SetActive(false);
		
		buttons[1,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[1,1].SetActive(false);
		
		buttons[1,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[1,2].SetActive(false);
		
		buttons[2,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[2,0].SetActive(false);
		
		buttons[2,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[2,1].SetActive(false);
		
		buttons[2,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[2,2].SetActive(false);
		
		buttons[3,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[3,0].SetActive(false);
		
		buttons[3,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[3,1].SetActive(false);
		
		buttons[3,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[3,2].SetActive(false);
		
	}
}
