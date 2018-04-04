﻿using System.Collections;
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

    public void CheckDifficulty (GameManager.Difficulty enemyDiff) {

        switch (enemyDiff)
        {

            case GameManager.Difficulty.Recruit :
                speed = 7;
                attackDamage = 10;
                points = 25;
                health = 100;
                break;

            case GameManager.Difficulty.Medium:
                speed = 7;
                attackDamage = 15;
                points = 50;
                health = 150;
                break;

            case GameManager.Difficulty.Veteran:
                speed = 7;
                attackDamage = 20;
                points = 100;
                health = 250;
                break;

        }

    }

    public IEnumerator AttackRate ()
    {

        yield return new WaitForSeconds(attackRate);

        Attack();
        
    }

}
