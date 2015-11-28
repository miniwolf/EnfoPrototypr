using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public static int spawnFlag = 0; // 0 represents left spawning enemies, 1 right sided enemies
	public GameObject enemy;
	public float spawnTime = 3f;
	public float waveTime = 90f;
	public Camera mainCamera;
	public Transform player;

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
		InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

	void Update() {
		switch(callStart) {
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
		NavigationFactory.CreateLeftEnemy(left.GetComponent<MonsterScript>(), player);

		GameObject right = (GameObject) Instantiate(enemy, spawnPoints[(int)spawnPoint.RIGHT].position, spawnPoints[(int)spawnPoint.RIGHT].rotation);
		right.GetComponent<HealthBarScript>().MainCamera = mainCamera;
		NavigationFactory.CreateRightEnemy(right.GetComponent<MonsterScript>(), player);
	}
}
