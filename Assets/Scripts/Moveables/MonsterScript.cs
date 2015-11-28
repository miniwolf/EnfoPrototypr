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
		currentLevel = 2;
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