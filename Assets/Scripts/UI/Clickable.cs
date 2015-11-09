using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

	protected int maxHealth;
	protected int currentHealth;
	protected int maxMana;
	protected int currentMana;
	protected int maxExp;
	protected int currentExp;
	protected int maxLevel;
	protected int currentLevel;

	protected string name;
	protected string className;
	protected Sprite picture;
	protected GameObject [,] buttons;


	public string getClassName(){
		return className;
	}
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
	public int getMaxExp(){
		return maxExp;
	}
	public int getCurrentExp(){
		return currentExp;
	}
	public int getCurrentLevel(){
		return currentLevel;
	}
	public int getMaxLevel(){
		return maxLevel;
	}

}
