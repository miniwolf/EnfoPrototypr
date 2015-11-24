using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public static int spawnFlag = 0; // 0 represents left spawning enemies, 1 right sided enemies
	public GameObject enemy;
	public float spawnTime = 3f;
<<<<<<< 96c92ac2d5f285773d77f801e5a9891d522f37b8:Assets/Scripts/Moveables/EnemyManager.cs
	public Transform[] spawnPoints;
	public float waveTime = 90f;
=======

	private Transform[] spawnPoints = new Transform[2];
>>>>>>> Waypoint system added:Assets/Scripts/Enemy scripts/EnemyManager.cs
	private bool callStart = false;

	void Awake()
	{
		spawnPoints[0] = GameObject.Find("EnemyLeftSpawnPoints").transform;
		spawnPoints[1] = GameObject.Find("EnemyRightSpawnPoints").transform;
	}

	void Start()
	{
		InvokeRepeating("Spawn", spawnTime, spawnTime);
	}

	void Update()
	{
		switch(callStart)
		{
			case true:
				if(!countDownTimer.isSpawning)
				{
					callStart = false;
					Start();
				}
				break;
			case false:
				if(countDownTimer.isSpawning)
				{
					callStart = true;
					CancelInvoke();
				}
				break;
		}
	}

	void Spawn()
	{
		int spawnPointIndexLeft = 0;
		int spawnPointIndexRight = 1;

		spawnFlag = 0;
		Instantiate(enemy, spawnPoints[spawnPointIndexLeft].position, spawnPoints[spawnPointIndexLeft].rotation);

		spawnFlag = 1;
		Instantiate(enemy, spawnPoints[spawnPointIndexRight].position, spawnPoints[spawnPointIndexRight].rotation);
	}
}
