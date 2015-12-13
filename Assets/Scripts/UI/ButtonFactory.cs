using UnityEngine;
using System;
using System.Collections;

public class ButtonFactory {
	public static GameObject CreateButton(GameObject gameObject, string description, long price, bool isInShop, bool isActionButton, Func<GameObject,bool> action) {
		ActionButtonScript actionButtonScript = gameObject.GetComponent<ActionButtonScript>();
		actionButtonScript.Description = description;
		actionButtonScript.Price = price;
		actionButtonScript.IsInShop = isInShop;
		actionButtonScript.IsActionButton = isActionButton;
		actionButtonScript.setAction(action);
		return gameObject;
	}
}
