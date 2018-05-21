using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageGiver : MonoBehaviour
{

    public EnemyRobot enemyController;

    void OnTriggerEnter(Collider trigger)               //OnTriggerEnter functie om te checken  of de hitbox de speler zijn collider raakt
    {

        if (trigger.transform.tag == "Player")
        {

            enemyController.mayAttack = true;           //set de boolen om het sein te geven dat de enemy damage mag geven aan de speler
            enemyController.Attack();                   //voert de aanval functie uit van de enemy        


        }

    }

    void OnTriggerExit()                               //OnTriggerEnter functie om te checken of de speler uit de hitbox van de enemy is
    {

        enemyController.mayAttack = false;              //geeft aan dat de enemy de speler niet meer kan aanvallen

        if (enemyController.isDead == false)             //conditie checked of de enemy niet dood is om er voor te zorgen dat hij deze functie niet uitvoert als hij dood is
        {
            {

                enemyController.StopCoroutine(enemyController.attackCoroutine);     //stopt de aanvals loop van de enemy op moment dat hij dood is

            }

        }

    }
}
