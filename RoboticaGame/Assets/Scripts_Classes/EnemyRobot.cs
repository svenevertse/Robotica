using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : EnemyBaseClass {

	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent.speed = speed;
		
	}
	

	void Update () {

        Movement();


    }

    public override void Attack()
    {

        player.GetComponent<MainCharacterController>().CheckHealth(attackDamage);

     
        if(mayAttack == true)
        {

            StartCoroutine(AttackRate());

        }
  
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

            gameManager.GetPoints(points);
            gameManager.EraseEnemy();
            Destroy(gameObject);

        }
        
    }

    public IEnumerator AttackRate ()
    {

        yield return new WaitForSeconds(attackRate);

        Attack();
        
    }

}
