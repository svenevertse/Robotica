using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

    public int health;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void GetDamage (int damage)
    {

        health -= damage;

        if(health < 1)
        {

            Destroy(gameObject);

        }
    }

}
