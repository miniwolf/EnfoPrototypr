using UnityEngine;
using System.Collections;

public class HealthComponent {
	private float maxHealth = 1000;
	private float health = 1000;
	private HealthBarScript healthBar;
	
	public float Health {
		get {
			return health;
		}
	}
	
	public HealthBarScript HealthBar {
		set {
			healthBar = value;
		}
	}

	public void GetHit(float Damage) {
		health -= Damage;
		healthBar.SetHealth(health, maxHealth);
	}
}
