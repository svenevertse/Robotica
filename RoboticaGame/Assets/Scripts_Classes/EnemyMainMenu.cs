using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainMenu : MonoBehaviour {

    public NavMeshAgent agent;

    public Transform[] destinations;

    Vector3 distance;

    public float maxDistance;
    public Animator animator;

    int randomDes;
    float randomSpeed;

    void Start () {

        animator = GetComponent<Animator>();

        randomDes = Random.Range(0, destinations.Length);
        randomSpeed = Random.Range(0.6f, 0.7f);

        if(randomSpeed > 0.66f)
        {

            agent.speed = 7f;

        }
        else
        {

            agent.speed = 3f;

        }

        agent.SetDestination(destinations[randomDes].position);
        animator.SetFloat("Speed", randomSpeed);
		
	}
	
	void Update () {

        distance.z = Vector3.Distance(transform.position, destinations[randomDes].position);

        if(distance.z < maxDistance)
        {

            Destroy(gameObject);

        }
		
	}
}
