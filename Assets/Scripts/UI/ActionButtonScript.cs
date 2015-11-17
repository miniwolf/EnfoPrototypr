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
	public string getDescription(){
		return description;
	}
	public void setPrice(int price){
		this.price = price;
	}
	public int getPrice(){
		return price;
	}
	public void setIsInShop(bool value){
		isInShop = value;
	}
	public bool getIsInShop(){
		return isInShop;
	}
	public void showDescription(){
		UIManager.showDescription(description);
	}
	public void stopShowDescription(){
		UIManager.stopShowDescription();
	}
	public void clickAction(){
		if(isInShop){
			GameObject newButton = Instantiate(this.gameObject);
			ActionButtonScript actionButtonScript = newButton.GetComponent<ActionButtonScript>();
			actionButtonScript.setDescription(description);
			actionButtonScript.setPrice(price);
			actionButtonScript.setIsInShop(isInShop);

			UIManager.addButtonToInventory(newButton,price);
		}else{
			//do action
		}
	}

}
