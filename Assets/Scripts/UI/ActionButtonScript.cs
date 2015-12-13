using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ActionButtonScript : MonoBehaviour {
	private bool isActionButton;
	private bool isInShop;
	private long price = 0L;
	private string description = "test";
	private Func<GameObject,bool> action;

	public void setAction(Func<GameObject,bool> action){
		this.action = action;
	}

	public string Description {
		get {
			return description;
		}
		set {
			description = value;
		}
	}

	public long Price {
		set {
			price = value;
		}
	}

	public bool IsInShop {
		set {
			isInShop = value;
		}
	}

	public bool IsActionButton {
		set {
			isActionButton = value;
		}
	}

	public void showDescription(){
		UIManager.showDescription(description,isActionButton, price);
	}

	public void stopShowDescription(){
		UIManager.stopShowDescription();
	}

	public void sellBack(){
		if ( !isActionButton && !isInShop ) {
			UIManager.sellForPrice(this.price);
			UIManager.removeButtonFromInventory(this.gameObject);
		}
	}

	public void clickAction(){
		if ( Input.GetMouseButtonUp(0) ) { //left click
			if ( isInShop ) {
				if ( UIManager.getGold() >= price ) {
					GameObject newButton = ButtonFactory.CreateButton((GameObject) Instantiate(gameObject), description, price / 2, false, false, action);
					UIManager.addButtonToInventory(newButton, price);
				}
			} else {
				action(gameObject);
			}
		} else if ( Input.GetMouseButtonUp(1) ) { //right click
			sellBack();
		}
	}

}
