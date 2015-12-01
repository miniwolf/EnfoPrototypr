using UnityEngine;
using System.Collections;

public class EnemyManager : LevelManager {
	public GameObject enemy;
	private MonsterScript monster;
	public float spawnTime = 5f;
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
				if(countDownTimer.isSpawning)
				{
					callStart = false;
					Start();
				}
				break;
			case false:
				if(!countDownTimer.isSpawning)
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

		ScaleEnemies();
	}

	void ScaleEnemies()
	{
		float healthScale = waveCount * 10f;
		float speedScale = waveCount * 1.25f;

		leftMonster.Health.UpdateHealth(healthScale, healthScale);
		rightMonster.Health.UpdateHealth(healthScale, healthScale);
		/*
		leftMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		leftMonster.GetComponent<NavMeshAgent>().acceleration = speedScale;

		rightMonster.GetComponent<NavMeshAgent>().speed = speedScale;
		rightMonster.GetComponent<NavMeshAgent>().acceleration = speedScale;
		*/
		leftMonster.GetComponent<Transform>().transform.localScale = 0.5f * waveCount * leftMonster.GetComponent<Transform>().transform.localScale;
		rightMonster.GetComponent<Transform>().transform.localScale = 0.5f * waveCount * rightMonster.GetComponent<Transform>().transform.localScale;

		//leftMonster.GetComponent<CapsuleCollider>().radius = waveCount / 2;
		//rightMonster.GetComponent<CapsuleCollider>().radius = waveCount / 2;

	}
}
