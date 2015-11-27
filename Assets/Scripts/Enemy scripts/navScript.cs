using UnityEngine;
using System.Collections;

public class navScript : MonoBehaviour {

	GameObject target;
	Transform[] wayPoint = new Transform[6];
	int currentWayPoint = 0;
	int maxWaypoints = 6;
	public static int agentSpeed = 5;
	public static int agentAccel = 5;
	private NavMeshAgent agent;
	private Rigidbody rb;
	private CapsuleCollider capsuleCol;
	private float sinkSpeed = 2.5f;
	private bool isSinking;

	// When enemy prefab is spawned, this function tags which side the enemy belongs to
	void Awake()
	{
		if (EnemyManager.spawnFlag == 0) gameObject.tag = "EnemyLeft";
		if (EnemyManager.spawnFlag == 1) gameObject.tag = "EnemyRight";
	}

	// Use this for initialization
	void Start ()
	{
		isSinking = false;
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
		capsuleCol = GetComponent<CapsuleCollider>();

		ScaleEnemy();

		if (gameObject.tag == "EnemyLeft")
		{
			wayPoint[0] = GameObject.Find("WayPoint1L").transform;
			wayPoint[1] = GameObject.Find("WayPoint2L").transform;
			wayPoint[2] = GameObject.Find("WayPoint3L").transform;
			wayPoint[3] = GameObject.Find("WayPoint4L").transform;
			wayPoint[4] = GameObject.Find("WayPoint5L").transform;
			wayPoint[5] = GameObject.Find("WayPoint6L").transform;
			target = GameObject.Find("Target");
		}

		if (gameObject.tag == "EnemyRight")
		{
			wayPoint[0] = GameObject.Find("WayPoint1R").transform;
			wayPoint[1] = GameObject.Find("WayPoint2R").transform;
			wayPoint[2] = GameObject.Find("WayPoint3R").transform;
			wayPoint[3] = GameObject.Find("WayPoint4R").transform;
			wayPoint[4] = GameObject.Find("WayPoint5R").transform;
			wayPoint[5] = GameObject.Find("WayPoint6R").transform;
			target = GameObject.Find("Target");
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(isSinking)
		{
			StartSinking();
		}
		else if (currentWayPoint >= maxWaypoints)
		{
			gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
		}
		else
		{
			gameObject.GetComponent<NavMeshAgent>().SetDestination(wayPoint[currentWayPoint].transform.position);
		}
	}

	void OnTriggerEnter(Collider trigger)
	{
		if (trigger.gameObject.tag == "WayPoint")
		{
			currentWayPoint += 1;
		}
		else if(trigger.gameObject.tag == "Target")
		{
			isSinking = true;
		}
	}

	void StartSinking()
	{
		agent.enabled = false;
		rb.isKinematic = true;
		capsuleCol.enabled = false;
		gameObject.transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		Destroy(gameObject, 2f);
	}

	void ScaleEnemy()
	{
		// Enemy parameters are scaled up on creation when wave number increases
		agent.speed = agentSpeed * WaveManager.waveCount;
		agent.acceleration = agentAccel * WaveManager.waveCount;
		gameObject.transform.localScale = WaveManager.waveCount * gameObject.transform.localScale;
	}
}