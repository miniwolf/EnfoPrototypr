using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

	protected int maxHealth;
	protected int currentHealth;
	protected int maxMana;
	protected int currentMana;

	protected string name;
	protected Sprite picture;
	protected GameObject [,] buttons;


	public int getMaxHealth(){
		return maxHealth;
	}
	public int getCurrentHealth(){
		return currentHealth;
	}
	public int getMaxMana(){
		return maxMana;
	}
	public int getCurrentMana(){
		return currentMana;
	}
	public string getName(){
		return name;
	}
	public Sprite getPicure(){
		return picture;
	}
	public GameObject[,] getButtons(){
		return buttons;
	}

}
