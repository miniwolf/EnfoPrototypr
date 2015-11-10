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
		className = "Shop building";
		currentLevel = 1;
		maxLevel = 1;
		currentExp = 0;
		maxExp = 100;

		picture = Resources.Load<Sprite>("Icons/dragon");
		buttons = new GameObject[4,3];
		buttons[0,0] = instantiateButton("Icons/knife", "A very strong kife, +15 attack");
		
		buttons[0,1] = instantiateButton("Icons/helmet", "A leather helmet which gives +10 damage protection");
		
		buttons[0,2] = instantiateButton("Icons/axe", "A really cool but useless axe, -1 attack");
		
		buttons[1,0] = instantiateButton("Icons/pants", "Some pants, to not go naked");
		
		buttons[1,1] = instantiateButton("Icons/shield", "A shield could come in handy, +10 defense");
		
		buttons[1,2] = instantiateButton("Icons/torso", "specially useful if you have a torso, +10 defense");
		
		buttons[2,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
		buttons[2,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
		buttons[2,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
		buttons[3,0] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
		buttons[3,1] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
		buttons[3,2] = instantiateButton("Icons/Weapons_Sword", "a normal sword +10 attack");
		
	}
}
