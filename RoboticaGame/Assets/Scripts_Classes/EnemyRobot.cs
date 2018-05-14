using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : EnemyBaseClass {

    public Coroutine attackCoroutine = null;

    public Animator animator;

    bool isDead;

	void Start ()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent.speed = speed;

        StartCoroutine(WaitForNewDestination(0));

    }
	

	void FixedUpdate ()
    {

        if(player != null && isDead == false)
        {

            transform.LookAt(player.transform);

        }

    }

    public override void Attack()
    {

        if(isDead == false)
        {

            player.GetComponent<MainCharacterController>().CheckHealth(attackDamage);
            animator.SetTrigger("Attack");


            if (mayAttack == true)
            {

                attackCoroutine = StartCoroutine(AttackRate());

            }

        }
   
    }

    public override void Movement()
    {

        if(agent.enabled == true && player != null)
        {

            agent.SetDestination(player.transform.position);

        }

        StartCoroutine(WaitForNewDestination(0.2f));

    }

    public override void GetDamage(int damage)
    {

        health -= damage;

        if (health < 1 && isDead == false)
        {

            gameManager.GetPoints(points);
            gameManager.EraseEnemy();
            agent.enabled = false;
            animator.SetTrigger("Death");
            isDead = true;
            Destroy(gameObject, 4f);

        }
        
    }

    public void CheckDifficulty (GameManager.Difficulty enemyDiff) {

        switch (enemyDiff)
        {

            case GameManager.Difficulty.Recruit :
                speed = diffStats.speed[0];
                attackDamage = diffStats.damage[0];
                points = diffStats.points[0];
                health = diffStats.health[0];
                break;

            case GameManager.Difficulty.Medium:
                speed = diffStats.speed[2];
                attackDamage = diffStats.damage[2];
                points = diffStats.points[2];
                health = diffStats.health[2];
                break;

            case GameManager.Difficulty.Veteran:
                speed = diffStats.speed[4];
                attackDamage = diffStats.damage[4];
                points = diffStats.points[4];
                health = diffStats.health[4];
                break;

        }

    }

    IEnumerator WaitForNewDestination (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        Movement ();

    }

    public IEnumerator AttackRate ()
    {

        yield return new WaitForSeconds(attackRate);

        Attack();
        
    }

}
