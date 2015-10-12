using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public float targetDistance;
    public float enemyLookDistance;
    public float enemyMovementSpeed;
    public float damping;
    public float attackDistance;
    public Transform target;
    Rigidbody rb;
    Renderer myRender;
    NavMeshAgent nav;

    // Use this for initialization
    void Start()
    {
        myRender = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance < enemyLookDistance)
        {
            myRender.material.color = Color.yellow;

            lookAtTarget();
        }


        if (targetDistance < attackDistance)
        {
            //move to player and attack
        }

        else
        {
            myRender.material.color = Color.blue;
        }*/
        lookAtTarget();
        moveToTarget();
        


    }


    // These functions could be useful for seeing and attacking the player
    void lookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        myRender.material.color = Color.blue;

    }

    void moveToTarget()
    {
        //nav.SetDestination(target.position);
        rb.AddForce(transform.forward * enemyMovementSpeed);
        myRender.material.color = Color.red;
    }  

}
