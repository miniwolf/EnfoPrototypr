using UnityEngine;
using System.Collections;

public class ExperienceComponent {
	private int exp = 0;
	private int maxExp = 100;
	private int level = 1;
	private int maxLevel = 20;
	private float experienceGrowthRatio = 2f;
	private HealthComponent healthComponent;
	private AttackComponent attackComponent;

	public ExperienceComponent(HealthComponent healthComponent, AttackComponent attackComponent) {
		this.healthComponent = healthComponent;
		this.attackComponent = attackComponent;
	}

	public int CurrentExp {
		get {
			return exp;
		}
		set {
			exp = value;
		}
	}
	public int MaxExp {
		get {
			return  maxExp;
		}
		set {
			maxExp = value;
		}
	}
	public int CurrentLevel {
		get {
			return level;
		}
		set {
			level = value;
		}
	}
	public int MaxLevel {
		get {
			return maxLevel;
		}
		set {
			maxLevel = value;
		}
	}

	public int getCurrentExp() {
		return exp;
	}
	public int getMaxExp() {
		return maxExp;
	}
	public int getCurrentLevel() {
		return level;
	}
	public int getMaxLevel() {
		return maxLevel;
	}

	private void levelUp() {
		while ( exp > maxExp && level < maxLevel ) {
			exp -= maxExp;
			maxExp = Mathf.FloorToInt(maxExp * experienceGrowthRatio);
			level++;
		}
	}

	public void addExp(int experience) {
		int levelDifference = level;
		exp += experience;
		levelUp();
		if ( level - levelDifference > 0 ) {
			LevelingManager.levelUp(levelDifference, healthComponent, attackComponent);
		}
		UIManager.setInfoChanged(true);
	}
}
