using UnityEngine;
using System.Collections;
using EnumExtension;

public class MonsterScript : Clickable {
	private NavigationComponent nav;


	public NavigationComponent Nav {
		set {
			this.nav = value;
		}
	}


	/*test*/
	private WarriorAnimation character;
	/*endoftest*/

	// Use this for initialization
	void Start() {
		/*test*/
		character = GameObject.Find("NinjaContainer").GetComponent<WarriorAnimation>();
		/*endOfTest*/


		health = new HealthComponent();
		health.HealthBar = GetComponent<HealthBarScript>();
		experience = new ExperienceComponent();
		maxMana = 0;
		health.MaxHealth = 20;
		currentMana = 0;
		health.CurrentHealth = 20;
		characterName = "Arthas";
		className = "Dark Knight";
		experience.CurrentLevel = 1;
		experience.MaxLevel = 1;
		experience.CurrentExp = 0;
		experience.MaxExp = 100;
		selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		picture = Resources.Load<Sprite>("Icons/arthas");
	}
	
	void Update() {
		if ( health.Health <= 0 ) {
			character.Experience.addExp(101,character.Health, character.Attack);
			Destroy(gameObject);
			return;
		} 
		nav.Update();
	}

	void OnTriggerEnter(Collider trigger) {
		nav.OnTriggerEnter(trigger);
	}
}