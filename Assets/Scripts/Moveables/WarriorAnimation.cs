using UnityEngine;
using System.Collections;

public class WarriorAnimation : Clickable {
	public Animator animator;
	private NavMeshAgent agent;
	enum States {
		ATTACKING,
		IDLE,
		CHASING
	}
	private States state = States.IDLE;

	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress};

	public Warrior warrior;

	private Vector3 goalPosition;

	void Start() {
		agent = gameObject.GetComponent<NavMeshAgent>();
		goalPosition = gameObject.transform.position;
	}
	
	void Update() {
		// When the mouse is clicked...	
		if ( Input.GetMouseButtonDown(1) ) {
			// If the click was on an object then set the agent's
			// destination to the point where the click occurred.
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if ( Physics.Raycast(ray, out hit) ) {
				agent.SetDestination(hit.point);
				goalPosition = hit.point;
				animator.SetBool("Running", true);
			}
		}

		if ( V3Equal(gameObject.transform.position, goalPosition) ) {
			//character is not moving
			animator.SetBool("Running", false);
		}

		if ( Input.GetKeyUp("A") ) {
			state = States.ATTACKING;
		}
	}

	private bool V3Equal(Vector3 a, Vector3 b) {
		return Vector3.SqrMagnitude(a - b) < 0.00695;
	}
}