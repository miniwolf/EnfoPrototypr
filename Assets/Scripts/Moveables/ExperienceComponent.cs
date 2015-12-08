using UnityEngine;
using System.Collections;

public class ExperienceComponent {
	private int exp = 0;
	private int maxExp = 100;
	private int level = 1;
	private int maxLevel = 20;
	private float experienceGrowthRatio = 2f;

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

	public void addExp(int experience,  HealthComponent characterHealth, AttackComponent characterAttack) {
		int levelDifference = level;
		exp += experience;
		while(exp > maxExp && level < maxLevel){
			exp -= maxExp;
			maxExp = Mathf.FloorToInt(maxExp * experienceGrowthRatio);
			level++;
		}
		levelDifference = level - levelDifference;
		if(levelDifference > 0){
			LevelingManager.levelUp(levelDifference, characterHealth,characterAttack);
		}
		UIManager.setInfoChanged(true);
	}


}
