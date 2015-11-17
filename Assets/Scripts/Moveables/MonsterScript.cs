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

	private States state = States.IDLE;
	private NavMeshAgent agent;
	private int health;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	void Update () {
		if ( !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") ) {
			switchingState(state, inRange()); // Updating state
			switch (state) {
			case States.ATTACKING:
				attack();
				break;
			case States.CHASING:
				chase();
				break;
			default:
				return;
			}
		}
	}

	public void GetHit(int Damage) {

	}

	private void chase() {
		agent.SetDestination(player.position);
	}

	private void attack() {
		animator.SetTrigger("Attack1Trigger");
	}

	private void switchingState(States from, States to) {
		if ( from == to ) {
			return;
		}
		state = to;

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

	private States inRange() {
		float distance = Vector3.Distance(transform.position, player.position);
		return distance <= range && distance > min_range ?
			States.CHASING : distance <= min_range ? 
			States.ATTACKING : States.IDLE
		;
	}
}