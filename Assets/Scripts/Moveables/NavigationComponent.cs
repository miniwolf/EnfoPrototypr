﻿using UnityEngine;
using System.Collections;

public class NavigationComponent {
	private NavMeshAgent agent;
	private Animator animator;
	private float seeRange = 7;

	public float SeeRange {
		get {
			return seeRange;
		}
		set {
			seeRange = value;
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
		if ( !agent.pathPending ) {
			return agent.pathStatus == NavMeshPathStatus.PathComplete && !agent.hasPath;
		}
		return false;
	}
}