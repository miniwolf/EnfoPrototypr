using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Clickable : MonoBehaviour {

	protected HealthComponent health;
	protected ExperienceComponent experience;
	protected int maxMana;
	protected int currentMana;

	protected string characterName;
	protected string className;
	protected Sprite picture;
	protected GameObject selectedCircle;

	protected GameObject [,] buttons;



	public HealthComponent Health {
		get {
			return health;
		}
	}

	public ExperienceComponent Experience {
		get {
			return experience;
		}
	}

	public void activateSelectedCircle() {
		selectedCircle.SetActive(true);
	}

	public void deactivateSelectedCircle() {
		selectedCircle.SetActive(false);
	}

	public bool isSelected() {
		return selectedCircle.activeSelf;
	}

	public string getClassName() {
		return className;
	}

	public float getMaxHealth() {
		return health.MaxHealth;
	}

	public float getCurrentHealth() {
		return health.getCurrentHealth();
	}

	public int getMaxMana() {
		return maxMana;
	}

	public int getCurrentMana() {
		return currentMana;
	}

	public string getName() {
		return characterName;
	}

	public Sprite getPicure() {
		return picture;
	}

	public GameObject[,] getButtons() {
		return buttons;
	}

	public int getMaxExp() {
		return experience.getMaxExp();
	}

	public int getCurrentExp() {
		return experience.getCurrentExp();
	}

	public int getCurrentLevel() {
		return experience.getCurrentLevel();
	}

	public int getMaxLevel() {
		return experience.getMaxLevel();;
	}

	public GameObject instantiateButton(string resourcesPathToIcon, string description, bool isActionButton, Func<GameObject,bool> action, long price) {
		GameObject go =(GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		go.GetComponent<Image>().sprite = Resources.Load<Sprite>(resourcesPathToIcon);
		ActionButtonScript actionButtonScript = go.GetComponent<ActionButtonScript>();
		actionButtonScript.setDescription(description);
		actionButtonScript.setIsActionButton(isActionButton);
		if(!isActionButton) {
			actionButtonScript.setIsInShop(true);
		} else {
			actionButtonScript.setIsInShop(false);
		}
		actionButtonScript.setPrice(price);

		actionButtonScript.setAction(action);
		//actionButtonScript.setAction(totalBaseRepair);
		go.SetActive(false);
		return go;
	}

	/*Here there will be functions which the buttons can have*/
	public static bool totalBaseRepair(GameObject button) {
		UIManager.setBaseHealthText("Base: 30/30");
		UIManager.removeButtonFromInventory(button);
		return true;
	}
}
