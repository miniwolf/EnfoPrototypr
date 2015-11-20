using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour {

	private bool isActionButton;
	private bool isInShop;
	private long price = 0L;
	private string description  = "test";
	
	public void setDescription(string desc){
		description = desc;
	}
	public string getDescription(){
		return description;
	}
	public void setPrice(long price){
		this.price = price;
	}
	public long getPrice(){
		return price;
	}
	public void setIsInShop(bool value){
		isInShop = value;
	}
	public bool getIsInShop(){
		return isInShop;
	}
	public void setIsActionButton(bool value){
		isActionButton = value;
	}
	public bool getIsActionButton(){
		return isActionButton;
	}

	public void showDescription(){
		UIManager.showDescription(description,isActionButton, price);
	}
	public void stopShowDescription(){
		UIManager.stopShowDescription();
	}
	public void sellBack(){
		Debug.Log("isActionButon = " + isActionButton + ". isInShop = " + isInShop);
		//if((isActionButton == false) & (isInShop == false)){
		if(!isActionButton && !isInShop){	
			Debug.Log("Im here");
			UIManager.sellForPrice(this.price);
			UIManager.removeButtonFromInventory(this.gameObject);
		}
	}

	public void clickAction(){
		if(Input.GetMouseButtonUp(0)){//left click
			if(isInShop){
				if(UIManager.getGold() >= price){
					GameObject newButton = Instantiate(this.gameObject);
					ActionButtonScript actionButtonScript = newButton.GetComponent<ActionButtonScript>();
					actionButtonScript.setDescription(description);
					actionButtonScript.setPrice(price/2);
					actionButtonScript.setIsInShop(false);
					actionButtonScript.setIsActionButton(false);
					UIManager.addButtonToInventory(newButton,price);
				}

				}else{
					//do action
				}
		}else if(Input.GetMouseButtonUp(1)){//right click
			sellBack();
		}
	}

}
