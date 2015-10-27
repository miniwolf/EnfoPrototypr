using UnityEngine;
using System.Collections;

public class EnemyClickable : Clickable {
	
	void Start () {
		maxMana = 100;
		maxHealth = 100;
		currentMana = 66;
		currentHealth = 97;
		name = "Weak Goblin";
		picture = Resources.Load<Sprite>("Icons/arthas");
		buttons = new GameObject[4,3];
		buttons[0,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		//buttons[0,1].SetActive(false);
		buttons[3,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		//buttons[3,2].SetActive(false);
	}

}
