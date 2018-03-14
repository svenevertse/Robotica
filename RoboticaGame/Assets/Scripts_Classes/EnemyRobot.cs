using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : EnemyBaseClass {


	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
		
	}
	

	void Update () {

        Movement();


    }

    public override void Attack()
    {


        
    }

    public override void Movement()
    {

        agent.SetDestination(player.transform.position);
        
    }

    public override void GetDamage(int damage)
    {

        health -= damage;

        if(health < 1)
        {

            Destroy(gameObject);

        }
        
    }
}
