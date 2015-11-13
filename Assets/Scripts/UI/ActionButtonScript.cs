using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour {

	private bool isInShop;
	private int price = 0;
	private string description  = "test";
	
	public void setDescription(string desc){
		description = desc;
	}
	public void setPrice(int price){
		this.price = price;
	}

	public void setIsInShopButton(bool value){
		isInShop = value;
	}
	public void showDescription(){
		UIManager.showDescription(description);
	}

	public void stopShowDescription(){
		UIManager.stopShowDescription();
	}
	public void clickAction(){
		if(isInShop){
			UIManager.addButtonToInventory(Instantiate(this.gameObject),price);
		}else{
			//do action
		}
	}

}
