using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewDifficulty : MonoBehaviour {

    public int difficultyFloor;

    GameManager gameManager;

	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            gameManager.enemyDifficulty = (GameManager.Difficulty)difficultyFloor;

        }


    }
}
