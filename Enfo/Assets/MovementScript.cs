﻿using UnityEngine;

public class MovementScript : MonoBehaviour {
    NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if ( Input.GetMouseButtonDown(0) ) {
            RaycastHit hit;

            if ( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) ) {
                agent.destination = hit.point;
            }
        }
    }
}