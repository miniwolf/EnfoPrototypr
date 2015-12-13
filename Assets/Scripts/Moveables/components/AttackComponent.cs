using UnityEngine;
using System.Collections;

public class AttackComponent {
	private bool attackRange = false;
	private float damage = 10;
	private float stunTime = 1.6f;
	private bool attacking = false;
	private Animator animator;

	public bool Attacking {
		get {
			return attacking;
		}
		set {
			attacking = value;
		}
	}

	public bool AttackRange {
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

	public AttackComponent(Animator animator) {
		this.animator = animator;
	}

	public void attack(HealthComponent healthComponent) {
		AnimateAttack();
		healthComponent.GetHit(damage);
		attacking = true;
		UIManager.setInfoChanged(true);
	}

	private void AnimateAttack() {
		if ( animator ) {
			animator.SetTrigger("Attack1Trigger");
		}
	}
}
