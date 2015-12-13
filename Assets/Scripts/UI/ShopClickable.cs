using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopClickable : Clickable {
	void Start () {
		healthComponent = new HealthComponent();
		maxMana = 0;
		healthComponent.MaxHealth = 1000;
		currentMana = 0;
		healthComponent.CurrentHealth = 999;
		characterName = "Item Shop";
		className = "Shop building";

		picture = Resources.Load<Sprite>("Icons/shop");
		selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		buttons = new GameObject[4,3];
		buttons[0,0] = instantiateButton("Icons/knife", "A very strong kife, +15 attack", false,totalBaseRepair, 100L);
		buttons[0,1] = instantiateButton("Icons/helmet", "A leather helmet which gives +10 damage protection", false,totalBaseRepair, 50L);
		buttons[0,2] = instantiateButton("Icons/axe", "A really cool but useless axe, -1 attack", false,totalBaseRepair, 10L);
		buttons[1,0] = instantiateButton("Icons/pants", "Some pants, to not go naked", false,totalBaseRepair,500L);
		buttons[1,1] = instantiateButton("Icons/shield", "A shield could come in handy, +10 defense", false,totalBaseRepair, 333L);
		buttons[1,2] = instantiateButton("Icons/torso", "specially useful if you have a torso, +10 defense", false,totalBaseRepair, 240L);
		buttons[2,0] = instantiateButton("Icons/potion_health", "+50 health", false, applyHealthPotion, 50L);
	}
}
