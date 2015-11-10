using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {
	enum States {
		ATTACKING,
		IDLE,
		CHASING
	}

	public Transform player;
	public float range = 7;
	public float min_range = 1.5f;
	public Animator animator;

	private States state;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		States previousState = state;
		inRange();
		switchingState(previousState, state);
		switch (state) {
		case States.ATTACKING:
			attack();
			break;
		case States.CHASING:
			chase();
			break;
		}
	}

	private void chase() {
		agent.SetDestination(player.position);
	}

	private void attack() {
		animator.SetTrigger("Attack1Trigger");
	}

	private void switchingState(States from, States to) {
		if ( from != to ) return;

		switch(from) {
		case States.CHASING:
			animator.SetBool("Running", false);
			agent.SetDestination(transform.position);
			break;
		}

		switch(to) {
		case States.CHASING:
			animator.SetBool("Running", true);
			break;
		}
	}

	void inRange() {
		float distance = Vector3.Distance(transform.position, player.position);
		if ( distance <= range && distance > min_range ) {
			state = States.CHASING;
		} else if ( distance <= min_range ) {
			state = States.ATTACKING;
		} else {
			state = States.IDLE;
		}
	}
}
