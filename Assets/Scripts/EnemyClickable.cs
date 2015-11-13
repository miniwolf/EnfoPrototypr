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
		buttons = null; //enemies do not have buttons
		/*buttons[0,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack",false);

		buttons[0,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[0,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[1,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[1,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[1,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[2,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[2,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[2,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[3,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[3,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");

		buttons[3,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");*/


	}

}
