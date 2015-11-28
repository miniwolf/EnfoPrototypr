using UnityEngine;
using System.Collections;

public class NavigationComponent {
	enum States {
		ATTACKING,
		IDLE,
		CHASING
	}

	private float range = 7; // TODO: public is only for debug
	private float min_range = 1.5f; // TODO: public is only for debug
	
	private const int maxWaypoints = 6;
	private int currentWayPoint = 0;

	private Transform player;
	private GameObject target;
	private Transform[] wayPoints;
	private NavMeshAgent agent;
	private States state = States.IDLE;
	private Animator animator;

	private MonsterScript monster;

	public NavigationComponent(NavMeshAgent agent, MonsterScript monster, Transform[] wayPoints, GameObject target, Transform player) {
		this.agent = agent;
		this.monster = monster;
		this.wayPoints = wayPoints;
		this.target = target;
		this.player = player;
	}

	public void OnTriggerEnter(Collider trigger) {
		if ( trigger.gameObject.tag == "WayPoint" ) {
			currentWayPoint++;
		}
	}

    // Update is called once per frame
    public void Update() {
		if (animator && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {
			return;
		}
		switchingState(state, inRange()); // Updating state
		switch (state) {
		case States.ATTACKING:
			attack();
			break;
		case States.CHASING:
			chase();
			break;
		case States.IDLE:
			idle();
			break;
		default:
			return;
		}
    }

	private void idle() {
		if ( currentWayPoint >= maxWaypoints ) {
			agent.SetDestination(target.transform.position);
		} else {
			agent.SetDestination(wayPoints[currentWayPoint].transform.position);
		}
	}

	private void chase() {
		agent.SetDestination(player.position);
	}
	
	private void attack() {
		if ( animator ) {
			animator.SetTrigger("Attack1Trigger");
		}
	}

	private void switchingState(States from, States to) {
		if ( from == to ) {
			return;
		}
		state = to;
		
		switch(from) {
		case States.CHASING:
			if ( animator ) {
				animator.SetBool("Running", false);
			}
			agent.SetDestination(monster.transform.position);
			break;
		}
		
		switch(to) {
		case States.CHASING:
			if ( animator ) {
				animator.SetBool("Running", true);
			}
			break;
		}
	}
	
	private States inRange() {
		float distance = Vector3.Distance(monster.transform.position, player.position);
		return distance <= range && distance > min_range ?
			States.CHASING : distance <= min_range ?
			States.ATTACKING : States.IDLE
		;
	}

/*
    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2f);
    }
*/
}