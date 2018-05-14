using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageGiver : MonoBehaviour {

    public EnemyRobot enemyController;


	void Start () {
		
	}

	
	void Update () {
		
	}

    void OnTriggerEnter(Collider trigger)
    {

        if (trigger.transform.tag == "Player")
        {

            enemyController.mayAttack = true;
            enemyController.Attack();
   

        }

    }

    void OnTriggerExit ()
    {

        enemyController.mayAttack = false;

        if(enemyController.isDead == false)
        {

            enemyController.StopCoroutine(enemyController.attackCoroutine);

        }

    }

}
