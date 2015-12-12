using UnityEngine;
using System.Collections;

public class HealthComponent {
	private float maxHealth = 10;
	private float health = 10;
	private HealthBarScript healthBar;

	public float Health {
		get {
			return health;
		}

		set {
			health = value;
		}
	}

	public float MaxHealth {
		get {
			return maxHealth;
		}
		set {
			maxHealth = value;
		}
	}

	public float CurrentHealth {
		get {
			return health;
		}
		set {
			health = value;
		}
	}
	
	public HealthBarScript HealthBar {
		set {
			healthBar = value;
		}
	}

	public void UpdateHealth(float newMaxHealth, float newHealth)
	{
		maxHealth = newMaxHealth;
		health = newHealth;
		healthBar.SetHealth(health, maxHealth);
	}

	public void GetHit(float Damage) {
		health -= Damage;
		healthBar.SetHealth(health, maxHealth);
		UIManager.setInfoChanged(true);
	}

	public void Update() {
		healthBar.Update();
	}
}
	
