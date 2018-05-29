using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : EnemyBaseClass {

    public Coroutine attackCoroutine = null;

    public Animator animator;

    public bool isDead;

	void Start ()
    {
    
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;                                                            //set de snelheids waarde van de navagent

        StartCoroutine(WaitForNewDestination(2f));                                       //Start de coroutine die om de paar miliseconden checked waar de speler is

        GameObject particle = Instantiate(Resources.Load("SmokeEffect 1", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        particle.transform.Rotate(-90f, 0, 0);
        particle.GetComponent<ParticleSystem>().Play();

        Destroy(particle, 3f);

    }
	
	void Update ()         
    {

        if(player != null && isDead == false)                                           //conditie die checked of de speler in de scene is en of de enemy niet dood is
        {

            transform.LookAt(player.transform);                                         //LookAt functie om de enemy naar de speler te laten kijken

        }

    }

    public override void Attack()                                                       //functie die overgenomen is van de EnemyBaseClass class
    {

        if(isDead == false)                                                             //conditie die checked of de enemy niet dood is om er voor te zorgen dat hij deze functie niet uitvoert als hij dood is
        {

            MainCharacterController.ins.CheckHealth(attackDamage);                     //geeft damage aan de speler en set de aanval animatie trigger
            animator.SetTrigger("Attack");
            
            agent.isStopped = true;                                                     //laat de navagent stoppen met bewegen om er voor de zorgen dat de enemy niet loopt en slaat tegelijk

            if (mayAttack == true)                                                      //checked of de speler nog steeds in de hitbox van de enemy zit om dan nog een aanval uit te voeren
            {

                attackCoroutine = StartCoroutine(AttackRate());                         //cached de coroutine om hem later ook uit te kunnen zetten

            }

        }
   
    }

    public override void Movement()                                                     //functie die overgenomen is van de EnemyBaseClass class        
    {

        if(agent.enabled == true && player != null)                                     //conditie checked of de navagent aanstaat en de speler niet dood is om ervoor te zorgen dat de enemy alleen mag bewegen als dat moet
        {

            agent.isStopped = false;                                                     //staat toe dat de navagent kan bewegen
            agent.SetDestination(player.transform.position);                            //laat de enemy bewegen naar de speler

        }

        StartCoroutine(WaitForNewDestination(0.2f));                                    //start de coroutine om naar de nieuwe locatie van de speler te zoeken

    }

    public override void GetDamage(int damage)                                          //functie die schade ontvangt die door de speler gemaakt is
    {

        health -= damage;                                                               

        if (health < 1 && isDead == false)                                              //conditie die checked of de enemy geen health meer heeft en of de enemy niet al dood is
        {

            GameManager.ins.GetPoints(points);                                              //voert alle functies uit die uitgevoert moeten worden en set alle variablen op het moment als de enemy dood is
            GameManager.ins.EraseEnemy();
            agent.enabled = false;
            animator.SetTrigger("Death");
            isDead = true;
            Destroy(gameObject, 4f);

        }
        
    }

    public void CheckDifficulty (GameManager.Difficulty enemyDiff) {                    //functie alle statistieken van de enemy set gebaseerd op welke moeilijkheidsgraad de speler op dat moment is

        switch (enemyDiff)
        {

            case GameManager.Difficulty.Recruit :                                       //set alle waardes voor de statistieken van de enemy
                speed = diffStats.speed[0];
                attackDamage = diffStats.damage[0];
                points = diffStats.points[0];
                health = diffStats.health[0];
                animator.SetFloat("Speed", 0.4f);
                break;

            case GameManager.Difficulty.Medium:
                speed = diffStats.speed[2];
                attackDamage = diffStats.damage[2];
                points = diffStats.points[2];
                health = diffStats.health[2];
                animator.SetFloat("Speed", 0.4f);
                break;

            case GameManager.Difficulty.Veteran:
                speed = diffStats.speed[4];
                attackDamage = diffStats.damage[4];
                points = diffStats.points[4];
                health = diffStats.health[4];
                animator.SetFloat("Speed", 0.7f);
                break;

        }

    }

    IEnumerator WaitForNewDestination (float waitTime)                              //coroutine die na enkele milisconden de movement laat uitvoeren
    {

        yield return new WaitForSeconds(waitTime);

        Movement ();

    }

    public IEnumerator AttackRate ()                                               //coroutine die na enkele seconden  de enemy opnieuw laat aanvallen
    {  

        yield return new WaitForSeconds(attackRate);

        Attack();
        
    }

}
