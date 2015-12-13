using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Clickable : MonoBehaviour {
	protected HealthComponent healthComponent = new HealthComponent();
	protected ExperienceComponent experienceComponent;
	protected int maxMana;
	protected int currentMana;

	protected string characterName;
	protected string className;
	protected Sprite picture;
	protected GameObject selectedCircle;

	protected GameObject[,] buttons;

	public HealthComponent Health {
		get {
			return healthComponent;
		}
	}

	public void SetSelectedCircle(bool selected) {
		selectedCircle.SetActive(selected);
	}

	public bool isSelected() {
		return selectedCircle.activeSelf;
	}

	public string getClassName() {
		return className;
	}

	public float getMaxHealth() {
		return healthComponent.MaxHealth;
	}

	public float getCurrentHealth() {
		return healthComponent.Health;
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
		if ( experienceComponent != null ) {
			return experienceComponent.getMaxExp();
		} else {
			return 0;
		}
	}

	public int getCurrentExp() {
		if ( experienceComponent != null ) {
			return experienceComponent.getCurrentExp();
		} else {
			return 0;
		}
	}

	public int getCurrentLevel() {
		if ( experienceComponent != null ) {
			return experienceComponent.getCurrentLevel();
		} else {
			return 0;
		}
	}

	public int getMaxLevel() {
		if ( experienceComponent != null ) {
			return experienceComponent.getMaxLevel();
		} else {
			return 0;
		}
	}

	public GameObject instantiateButton(string resourcesPathToIcon, string description, bool isActionButton, Func<GameObject,bool> action, long price) {
		GameObject go =(GameObject) GameObject.Instantiate(Resources.Load ("Prefabs/UI/ActionButton"),Vector3.zero,Quaternion.identity);
		go.GetComponent<Image>().sprite = Resources.Load<Sprite>(resourcesPathToIcon);
		ActionButtonScript actionButtonScript = go.GetComponent<ActionButtonScript>();
		actionButtonScript.Description = description;
		actionButtonScript.IsActionButton = isActionButton;
		actionButtonScript.IsInShop = !isActionButton;
		actionButtonScript.Price = price;
		actionButtonScript.setAction(action);
		go.SetActive(false);
		return go;
	}

	/*Here there will be functions which the buttons can have*/
	public static bool totalBaseRepair(GameObject button) {
		UIManager.setBaseHealthText("Base: 30/30");
		UIManager.removeButtonFromInventory(button);
		return true;
	}

	public static bool applyHealthPotion(GameObject button) {
		HealthComponent playerHealth = GameObject.Find("NinjaContainer").GetComponent<EnumExtension.WarriorAnimation>().Health;
		if ( playerHealth.CurrentHealth == playerHealth.MaxHealth ) {
			return false;
		}
		playerHealth.CurrentHealth += 50;
		if ( playerHealth.MaxHealth < playerHealth.CurrentHealth ) {
			playerHealth.CurrentHealth = playerHealth.MaxHealth;
		}
		UIManager.removeButtonFromInventory(button);
		return true;
	}
}
