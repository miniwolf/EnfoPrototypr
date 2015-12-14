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
		buttons[0,0] = instantiateButton("Icons/potion_health", "+50 health", false, applyHealthPotion, 50L);
	}
}
