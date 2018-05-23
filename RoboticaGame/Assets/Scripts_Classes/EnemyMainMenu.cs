using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainMenu : MonoBehaviour {

    public NavMeshAgent agent;

    public Transform[] destinations;

    Vector3 distance;                                           //vector3 variable die bijhoud wat de afstand is tussen de enemy en zijn destination

    public float maxDistance;
    public Animator animator;

    int randomDes;                                              //Int die word gerandomized en word gebruikt om een random Transform te pakken uit de destinations array
    float randomSpeed;                                          //float word gerandomized en word gebruikt om random navagent speed + een random animatie te geven

    void Start () {

        animator = GetComponent<Animator>();

        randomDes = Random.Range(0, destinations.Length);       
        randomSpeed = Random.Range(0.6f, 0.7f);

        if(randomSpeed > 0.66f)                                 //conditie die checked of de randomSpeed grotere is dan 0.66 om de navagent speed te veranderen op de snelheid van de animatie
        {

            agent.speed = 7f;

        }
        else
        {

            agent.speed = 3f;

        }

        agent.SetDestination(destinations[randomDes].position);         //zet de positie waar de enmie heen gaat bewegen
        animator.SetFloat("Speed", randomSpeed);                        //zet de speed value van de animator van de enemy
		
	}
	
	void Update () {

        distance.z = Vector3.Distance(transform.position, destinations[randomDes].position);        //de z waarde van de distance variable word elk frame geset om te kijken of de enemy uit beeld is

        if(distance.z < maxDistance)                                                                 //conditie die checked of de afstand klijner is dan maximaal mag
        {

            Destroy(gameObject);

        }
		
	}
}
