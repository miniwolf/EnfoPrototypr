using UnityEngine;
using System.Collections;

public class navScript : MonoBehaviour {

    private GameObject target;
    private Transform[] wayPoint = new Transform[6];
    private int currentWayPoint = 0;
    private int maxWaypoints = 6;


    // When enemy prefab is spawned, this function tags which side the enemy belongs to
    void Awake()
    {
        if (EnemyManager.spawnFlag == 0) gameObject.tag = "EnemyLeft";
        if (EnemyManager.spawnFlag == 1) gameObject.tag = "EnemyRight";
    }

    // Use this for initialization
    void Start ()
    {
        if(gameObject.tag == "EnemyLeft")
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
        if(currentWayPoint >= maxWaypoints)
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
            Debug.Log(currentWayPoint);
        }
    }

}