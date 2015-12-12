using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : LevelManager {
	public GameObject enemy;
	private MonsterScript monster;
	private float spawnTime = 5f;
	private float initialHealth = 10f;
	private float initialSpeed = 5f;

	public Camera mainCamera;
	public EnumExtension.WarriorAnimation player;
	public GameObject healthBar;

	enum spawnPoint {
		LEFT = 0,
		RIGHT = 1
	}

	private Transform[] spawnPoints = new Transform[2];
	private bool callStart = false;

	public void Awake() {
		spawnPoints[(int)spawnPoint.LEFT] = GameObject.Find("EnemyLeftSpawnPoints").transform;
		spawnPoints[(int)spawnPoint.RIGHT] = GameObject.Find("EnemyRightSpawnPoints").transform;
	}

	public void Start() {
		InvokeRepeating("Spawn", 0, spawnTime);
	}

	public void Update() {
		switch (callStart) {
			case true:
				if(WaveManager.isSpawning)
				{
					callStart = false;
					Start();
				}
				break;
			case false:
				if(!WaveManager.isSpawning)
				{
					callStart = true;
					CancelInvoke();
				}
				break;
		}
	}

	private void ScaleEnemies(MonsterScript monster, float healthScale, float speedScale) {
		healthScale = healthScale * waveCount;
		//speedScale = speedScale + waveCount;
		leftMonster.Health.UpdateHealth(healthScale, healthScale);
		rightMonster.Health.UpdateHealth(healthScale, healthScale);
		
		leftMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		rightMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		
		switch(waveCount) {
			case 1:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.0f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.0f * rightMonster.GetComponent<Transform>().transform.localScale;
				leftMonster.GetComponent<Renderer>().material.color = Color.yellow;
				rightMonster.GetComponent<Renderer>().material.color = Color.yellow;
				break;
			case 2:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.2f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.2f * rightMonster.GetComponent<Transform>().transform.localScale;
				leftMonster.GetComponent<Renderer>().material.color = Color.cyan;
				rightMonster.GetComponent<Renderer>().material.color = Color.cyan;
				break;
			case 3:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.4f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.4f * rightMonster.GetComponent<Transform>().transform.localScale;
				leftMonster.GetComponent<Renderer>().material.color = Color.blue;
				rightMonster.GetComponent<Renderer>().material.color = Color.blue;
				break;
			case 4:
				// Final wave
				leftMonster.GetComponent<Transform>().transform.localScale = 1.9f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.9f * rightMonster.GetComponent<Transform>().transform.localScale;
				leftMonster.GetComponent<Renderer>().material.color = Color.red;
				rightMonster.GetComponent<Renderer>().material.color = Color.red;
				CancelInvoke();
				break;
		}
	}

	private MonsterScript Setup(Transform point) {
		GameObject left = (GameObject) Instantiate(enemy, point.position, point.rotation);
		MonsterScript monster = left.GetComponent<MonsterScript>();
		GameObject healthBarObject = (GameObject) Instantiate(healthBar, new Vector3(0,0,0), new Quaternion(0,0,0,0));
		healthBarObject.transform.SetParent(left.transform);
		healthBarObject.transform.localPosition = new Vector3(0,3,0);
		Image image = healthBarObject.GetComponentInChildren<Image>();
		monster.Health.HealthBar = new HealthBarScript(mainCamera, healthBarObject, image);
		ScaleEnemies(monster, initialHealth, initialSpeed);
		return monster;
	}

	private void Spawn() {
		NavigationFactory.CreateLeftEnemy(Setup(spawnPoints[(int)spawnPoint.LEFT]), player);
		NavigationFactory.CreateRightEnemy(Setup(spawnPoints[(int)spawnPoint.RIGHT]), player);
	}
}
