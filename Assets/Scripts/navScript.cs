using UnityEngine;
using System.Collections;

public class navScript : MonoBehaviour {

    public GameObject target;
    float seconds = 0.75f;

	// Use this for initialization
	void Start () {
        //InvokeRepeating("moveOSC", 0, seconds);
    }

    // Update is called once per frame
    void Update () {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    void moveOSC()
    {
        float move = 1.0f;
        OSCHandler.Instance.SendMessageToClient("SuperCollider", "/move", move);
    }
}
