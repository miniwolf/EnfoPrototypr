using UnityEngine;
using System.Collections;

public class LevelingManager : MonoBehaviour {
	private const float healthMultiplier = 1.1f;
	private const float damageMultiplier = 1.05f;
	private const float stunTimeMultiplier = 1.05f;
	private const float seeRangeMultiplier = 1f;
	private const float attackRangeMultiplier = 1f;

	public static void levelUp(int levels, HealthComponent health, AttackComponent attack) {
		health.MaxHealth *= healthMultiplier;
		health.CurrentHealth *= healthMultiplier;
		attack.Damage *= damageMultiplier;
		attack.StunTime *= stunTimeMultiplier;
	}
}
