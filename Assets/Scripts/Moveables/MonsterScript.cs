using UnityEngine;
using System.Collections;

public class MonsterScript : Clickable {
	private NavigationComponent nav;
	private HealthComponent health;

	public NavigationComponent Nav {
		set {
			this.nav = value;
		}
	}

	public HealthComponent Health {
		get {
			return health;
		}
	}

	// Use this for initialization
	void Start() {
		health = new HealthComponent();
		health.HealthBar = GetComponent<HealthBarScript>();
		maxMana = 666;
		maxHealth = 750;
		currentMana = 666;
		currentHealth = 400;
		characterName = "Arthas";
		className = "Dark Knight";
		currentLevel = 2;
		maxLevel = 10;
		currentExp = 78;
		maxExp = 100;
		selectedCircle = this.gameObject.transform.FindChild("SelectedCircle").gameObject;
		picture = Resources.Load<Sprite>("Icons/arthas");
	}
	
	void Update() {
		if ( health.Health <= 0 ) {
			Destroy(gameObject);
			return;
		} 
		nav.Update();
	}

	void OnTriggerEnter(Collider trigger) {
		nav.OnTriggerEnter(trigger);
	}
}