using UnityEngine;
using System.Collections;

public class EnemyManager : LevelManager {
	public GameObject enemy;
	private MonsterScript monster;
	private float spawnTime = 4f;
	private float initialHealth = 10f;
	private float initialSpeed = 7.5f;

	public Camera mainCamera;
	public Transform player;
	MonsterScript leftMonster;
	MonsterScript rightMonster;

	enum spawnPoint {
		LEFT = 0,
		RIGHT = 1
	}

	private Transform[] spawnPoints = new Transform[2];
	private bool callStart = false;

	void Awake() {
		spawnPoints[(int)spawnPoint.LEFT] = GameObject.Find("EnemyLeftSpawnPoints").transform;
		spawnPoints[(int)spawnPoint.RIGHT] = GameObject.Find("EnemyRightSpawnPoints").transform;
	}

	void Start() {
		InvokeRepeating("Spawn", 0, spawnTime);
	}

	void Update() {

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

	void Spawn() {
		GameObject left = (GameObject) Instantiate(enemy, spawnPoints[(int)spawnPoint.LEFT].position, spawnPoints[(int)spawnPoint.LEFT].rotation);
		left.GetComponent<HealthBarScript>().MainCamera = mainCamera;
		leftMonster = left.GetComponent<MonsterScript>();
		NavigationFactory.CreateLeftEnemy(leftMonster, player);

		GameObject right = (GameObject) Instantiate(enemy, spawnPoints[(int)spawnPoint.RIGHT].position, spawnPoints[(int)spawnPoint.RIGHT].rotation);
		right.GetComponent<HealthBarScript>().MainCamera = mainCamera;
		rightMonster = right.GetComponent<MonsterScript>();
		NavigationFactory.CreateRightEnemy(rightMonster, player);

		ScaleEnemies(initialHealth, initialSpeed);
	}

	void ScaleEnemies(float healthScale, float speedScale)
	{
		healthScale = healthScale * waveCount;
		speedScale = speedScale + waveCount;
		leftMonster.Health.UpdateHealth(healthScale, healthScale);
		rightMonster.Health.UpdateHealth(healthScale, healthScale);
		
		leftMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		leftMonster.GetComponent<NavMeshAgent>().acceleration = speedScale;

		rightMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		rightMonster.GetComponent<NavMeshAgent>().acceleration = speedScale;
		
		switch(waveCount)
		{
			case 1:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.0f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.0f * rightMonster.GetComponent<Transform>().transform.localScale;
				break;
			case 2:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.2f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.2f * rightMonster.GetComponent<Transform>().transform.localScale;
				break;
			case 3:
				leftMonster.GetComponent<Transform>().transform.localScale = 1.4f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.4f * rightMonster.GetComponent<Transform>().transform.localScale;
				break;
			case 4:
				// Final wave
				leftMonster.GetComponent<Transform>().transform.localScale = 1.9f * leftMonster.GetComponent<Transform>().transform.localScale;
				rightMonster.GetComponent<Transform>().transform.localScale = 1.9f * rightMonster.GetComponent<Transform>().transform.localScale;

				CancelInvoke();
				break;
		}
	}
}
