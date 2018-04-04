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

    public bool mayAttack;

    public NavMeshAgent agent;

    public GameObject player;

    public GameManager gameManager;

    public abstract void Movement();

    public abstract void Attack();

    public abstract void GetDamage(int damage);
}
