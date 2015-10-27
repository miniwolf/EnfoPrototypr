using UnityEngine;
using System.Collections;

public class WarriorAnimation : MonoBehaviour 
{
	public Animator animator;
	private NavMeshAgent agent;

	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress};

	public Warrior warrior;

	private Vector3 goalPosition;

	void Start() {
		agent = gameObject.GetComponent<NavMeshAgent>();
		goalPosition = gameObject.transform.position;
	}

	public bool V3Equal(Vector3 a, Vector3 b) {
		return Vector3.SqrMagnitude(a - b) < 0.00695;
	}
	
	void Update() {
		RaycastHit hit;
		
		// When the mouse is clicked...	
		if ( Input.GetMouseButtonDown(1) ) {
			// If the click was on an object then set the agent's
			// destination to the point where the click occurred.
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit)) {
				agent.SetDestination(hit.point);
				goalPosition = hit.point;
				animator.SetBool("Moving", true);
				animator.SetBool("Running", true);

				//Apply inputs to animator
				animator.SetFloat("Input X", hit.point.z);
				animator.SetFloat("Input Z", -(hit.point.x));
			}
		}

		if ( V3Equal(gameObject.transform.position, goalPosition) ) {
			//character is not moving
			animator.SetBool("Moving", false);
			animator.SetBool("Running", false);
		}

		if (Input.GetButtonDown("Fire1")) {
			animator.SetTrigger("Attack1Trigger");
			if (warrior == Warrior.Brute)
				StartCoroutine (COStunPause(1.2f));
			else if (warrior == Warrior.Sorceress)
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}

	}

	public IEnumerator COStunPause(float pauseTime) {
		yield return new WaitForSeconds(pauseTime);
	}

	void OnGUI() {
		if ( GUI.Button(new Rect (25, 85, 100, 30), "Attack1") ) {
			animator.SetTrigger("Attack1Trigger");
			if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)  //if character is Brute or Sorceress
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}
	}
}