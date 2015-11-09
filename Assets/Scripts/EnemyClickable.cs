using UnityEngine;
using System.Collections;

public class EnemyClickable : Clickable {
	
	void Start () {
		maxMana = 100;
		maxHealth = 100;
		currentMana = 66;
		currentHealth = 97;
		name = "Arthas";
		className = "Fighter";
		currentLevel = 2;
		maxLevel = 10;
		currentExp = 58;
		maxExp = 100;

		picture = Resources.Load<Sprite>("Icons/arthas");
		buttons = new GameObject[4,3];
		buttons[0,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,0].SetActive(false);

		buttons[0,1] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,1].SetActive(false);

		buttons[0,2] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		buttons[0,2].SetActive(false);

		buttons[1,0] = (GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
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
