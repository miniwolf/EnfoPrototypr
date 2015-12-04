using UnityEngine;
using System.Collections;

public class AttackComponent {

	private float seeRange = 7;
	private float attackRange = 3f;
	private float damage = 10;
	private float stunTime = 1.6f;

	public float SeeRange {
		get {
			return seeRange;
		}
		set {
			seeRange = value;
		}
	}
	public float AttackRange {
		get {
			return attackRange;
		}
		set {
			attackRange = value;
		}
	}
	public float Damage {
		get {
			return damage;
		}
		set {
			damage = value;
		}
	}
	public float StunTime {
		get {
			return stunTime;
		}
		set {
			stunTime = value;
		}
	}

	public void attack(HealthComponent enemyHealth) {
		enemyHealth.GetHit(damage);
		UIManager.setInfoChanged(true);
	}


}
