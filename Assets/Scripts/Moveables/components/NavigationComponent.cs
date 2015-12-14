using UnityEngine;
using System.Collections;

public class NavigationComponent {
	private NavMeshAgent agent;
	private Animator animator;
	private float seeRange = 7;
	private bool DoubleCheck = false;

	public float SeeRange {
		get {
			return seeRange;
		}
	}

	public NavigationComponent(NavMeshAgent agent, Animator animator) {
		this.agent = agent;
		this.animator = animator;
	}
	
	public void MoveTo(Vector3 position) {
		SetRunning(true);
		agent.SetDestination(position);
	}

	public void Stop() {
		SetRunning(false);
	}

	public void SetRunning(bool running) {
		if ( animator ) {
			animator.SetBool("Running", running);
		}
	}

	public bool ReachedDestination() {
		if ( !agent.pathPending && !agent.hasPath && agent.pathStatus == NavMeshPathStatus.PathComplete ) {
			if ( agent.remainingDistance <= agent.stoppingDistance ) {
				if ( !DoubleCheck ) {
					DoubleCheck = true;
				} else {
					DoubleCheck = false;
					return true;
				}
			}
		}

		return false;
	}

	public void Disable() {
		agent.enabled = false;
	}
}