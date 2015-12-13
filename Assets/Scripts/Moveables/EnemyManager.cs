using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	private MonsterScript monster;
	private int spawnTime = 5;
	private float initialHealth = 10f;
	private float initialSpeed = 5f;
	private Transform[] spawnPoints = new Transform[2];

	public GameObject enemy;
	public GameObject healthBar;
	public Camera mainCamera;
	public EnumExtension.WarriorAnimation player;

	enum spawnPoint {
		LEFT = 0,
		RIGHT = 1
	}

	void Awake() {
		spawnPoints[(int)spawnPoint.LEFT] = GameObject.Find("EnemyLeftSpawnPoints").transform;
		spawnPoints[(int)spawnPoint.RIGHT] = GameObject.Find("EnemyRightSpawnPoints").transform;
	}

	void Start() {
		StartCoroutine(spawnUnit());
	}

	private IEnumerator spawnUnit() {
		while (true) {
			if ( WaveManager.IsSpawning ) {
				Spawn();
				yield return new WaitForSeconds(spawnTime);
			} else {
				yield return new WaitUntil(new System.Func<bool>(IsSpwaning));
			}
		}
	}

	private bool IsSpwaning() {
		return WaveManager.IsSpawning;
	}

	private void ScaleEnemies(MonsterScript monster, float healthScale, float speedScale) {
		healthScale = healthScale * WaveManager.WaveCount;
		monster.Health.UpdateHealth(healthScale, healthScale);
		monster.GetComponent<NavMeshAgent>().speed = speedScale;
		
		switch(WaveManager.WaveCount) {
		case 1:
			monster.GetComponent<Renderer>().material.color = Color.yellow;
			break;
		case 2:
			monster.GetComponent<Transform>().transform.localScale *= 1.2f;
			monster.GetComponent<Renderer>().material.color = Color.cyan;
			break;
		case 3:
			monster.GetComponent<Transform>().transform.localScale *= 1.4f;
			monster.GetComponent<Renderer>().material.color = Color.blue;
			break;
		case 4:
			// Final wave
			monster.GetComponent<Transform>().transform.localScale *= 1.9f;
			monster.GetComponent<Renderer>().material.color = Color.red;
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
		Image image = (Image) healthBarObject.transform.FindChild("Bar").GetComponent<Image>();
		monster.Health.HealthBar = new HealthBarScript(mainCamera, healthBarObject, image);

		monster.Target = GameObject.Find("Target");
		monster.Player = player;

		ScaleEnemies(monster, initialHealth, initialSpeed);
		return monster;
	}

	private void Spawn() {
		NavigationFactory.CreateLeftEnemy(Setup(spawnPoints[(int)spawnPoint.LEFT]), player);
		NavigationFactory.CreateRightEnemy(Setup(spawnPoints[(int)spawnPoint.RIGHT]), player);
	}
}
