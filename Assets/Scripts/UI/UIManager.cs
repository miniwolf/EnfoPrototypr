﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using EnumExtension;

public class UIManager : MonoBehaviour {
	private RaycastHit hit;
	private Ray ray;
	private Camera mCamera;
	private Clickable click;

	public Text healthText;
	public Text manaText;
	public Text name;
	public Text levelAndClassText;
	public static Text gold;
	public static Text baseHealth;
	public Image picture;

	public Slider experienceBar;

	private static GameObject itemDescriptionPanel;
	private static Text itemDescriptionText;

	public GameObject mainCamera;
	public ActionInspectorScript actionInspector;
	public static InventoryScript inventory;

	private static bool infoChanged = false;

	// Use this for initialization
	void Start () {
		click = null;
		mCamera = mainCamera.GetComponent<Camera>();
		itemDescriptionPanel = GameObject.Find("ButtonDescriptionPanel");
		itemDescriptionText = GameObject.Find("ButtonDescriptionText").GetComponent<Text>();
		inventory = GameObject.Find("InventoryPanel").GetComponent<InventoryScript>();
		gold = GameObject.Find("GoldText").GetComponent<Text>();
		baseHealth = GameObject.Find("BaseHealthText").GetComponent<Text>();
		DeactivateInfo();

		click = GameObject.Find("NinjaContainer").GetComponent<WarriorAnimation>();
		click.activateSelectedCircle();
		healthText.text = click.getCurrentHealth() + "/" + click.getMaxHealth();
		manaText.text = click.getCurrentMana() + "/" + click.getMaxMana();
		name.text = click.getName();
		picture.sprite = click.getPicure();
		actionInspector.AddAllButtons(click.getButtons());
		
		experienceBar.maxValue = click.getMaxExp();
		experienceBar.value = click.getCurrentExp();
		levelAndClassText.text = "Level " +click.getCurrentLevel() + " " + click.getClassName();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButton(0) ) {
			this.ray = mCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Clickable"))){
				/*if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
					return;
				}*/
				DeactivateInfo();
				actionInspector.ResetButtons();
				click = hit.transform.gameObject.GetComponent<Clickable>();
				click.SetSelectedCircle(true);
				healthText.text = click.getCurrentHealth() + "/" + click.getMaxHealth();
				manaText.text = click.getCurrentMana() + "/" + click.getMaxMana();
				name.text = click.getName();
				picture.sprite = click.getPicure();
				actionInspector.AddAllButtons(click.getButtons());

				experienceBar.maxValue = click.getMaxExp();
				experienceBar.value = click.getCurrentExp();
				levelAndClassText.text = "Level " + click.getCurrentLevel() + " " + click.getClassName();

			}
		}
		if ( Input.GetMouseButton(1) ) {
			if ( click.isSelected() && click.tag == "Player" ) {
				this.ray = mCamera.ScreenPointToRay(Input.mousePosition);
				if ( Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Clickable")) &&
					 hit.transform.gameObject.tag == "Enemy") {
					hit.transform.gameObject.GetComponent<MonsterScript>().rightClicked();
				}
			}

		}
		if ( infoChanged ) {
			updateCharacterInfo();
		}
	}

	public static void setInfoChanged(bool value) {
		infoChanged = value;
	}

	public void updateCharacterInfo() {
		healthText.text = click.getCurrentHealth() + "/" + click.getMaxHealth();
		manaText.text = click.getCurrentMana() + "/" + click.getMaxMana();
		experienceBar.maxValue = click.getMaxExp();
		experienceBar.value = click.getCurrentExp();
		levelAndClassText.text = "Level " +click.getCurrentLevel() + " " + click.getClassName();
	}

	public static void showDescription(string desc, bool isActionButton, long price){
		itemDescriptionPanel.SetActive(true);
		itemDescriptionText.text = (isActionButton ? "" : "Price: " + price + ". ") + desc;
	}

	public static long getGold(){
		return long.Parse(gold.text);
	}

	public static void sellForPrice(long price){
		long result = long.Parse(gold.text)+price;
		gold.text = result.ToString();
	}

	public static void stopShowDescription(){
		itemDescriptionPanel.SetActive(false);
		itemDescriptionText.text = "";
	}

	public static void addButtonToInventory(GameObject button, long price){
		int actualGold = int.Parse(gold.text);
		if ( inventory.AddButton(button) ) {
			gold.text = (actualGold - price).ToString();
		} else {
			Destroy(button);
		}
	}
	public static void removeButtonFromInventory(GameObject button){
		inventory.removeButton(button);
	}

	public void DeactivateInfo() {
		if ( click != null ) {
			click.SetSelectedCircle(false);
		}
		picture.sprite = Resources.Load<Sprite>("Icons/slot");
		healthText.text = "0/0";
		manaText.text = "0/0";
		name.text = "";
		levelAndClassText.text = "";
		experienceBar.value = 0;
		itemDescriptionPanel.SetActive(false);
		itemDescriptionText.text = "";
	}
	
	public static void setBaseHealthText(string healthText) {
		baseHealth.text = healthText;
	}
}
