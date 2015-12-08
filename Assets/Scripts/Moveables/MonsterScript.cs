using UnityEngine;
using System.Collections;
using EnumExtension;

public class MonsterScript : Clickable {
	private NavigationComponent nav;
	private GameObject attackCircle;
	private bool isRightClicked = false;

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


		attackCircle = this.gameObject.transform.FindChild("AttackCircle").gameObject;
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

	void fadeCircle() {
		MeshRenderer mesh = attackCircle.GetComponent<MeshRenderer>();
		Color c = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, mesh.material.color.a - Time.deltaTime);
		mesh.material.color = c;
		if(mesh.material.color.a <= 0) {
			Color c2 = new Color(mesh.material.color.r, mesh.material.color.g, mesh.material.color.b, 1);
			mesh.material.color = c2;
			attackCircle.SetActive(false);
			isRightClicked = false;
		}
		Debug.Log("alfa = " + mesh.material.color.a);

	}

	void Update() {
		if ( health.Health <= 0 ) {
			character.Experience.addExp(101,character.Health, character.Attack);
			Destroy(gameObject);
			return;
		} 
		if(isRightClicked){
			fadeCircle();
		}
		nav.Update();
	}

	public void rightClicked() {
		if(isRightClicked == true){
			return;
		}
		attackCircle.SetActive(true);
		isRightClicked = true;
	}

	void OnTriggerEnter(Collider trigger) {
		nav.OnTriggerEnter(trigger);
	}
}