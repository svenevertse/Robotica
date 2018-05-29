using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class EnemyBaseClass : MonoBehaviour {

    public float speed;
    public float attackRate;

    public int health;
    public int attackDamage;
    public int points;

    public bool mayAttack;                              //boolean die aan kan geven of de enemy kan aanvallen

    public NavMeshAgent agent;

    public GameObject player;

    public DifficultyStats diffStats;

    public abstract void Movement();                    //abstracte functie voor de movement van de enemy

    public abstract void Attack();                      //abstracte functie voor de aanval van de enmemy

    public abstract void GetDamage(int damage);         //abstracte functie voor het krijgen van damage van de enemy
}
