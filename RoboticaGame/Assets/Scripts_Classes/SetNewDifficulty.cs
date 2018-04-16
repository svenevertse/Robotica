using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewDifficulty : MonoBehaviour {

    public int difficultyFloor;

    GameManager gameManager;
    UI_Controller uiController;

	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiController = GameObject.Find("UI_Controller").GetComponent<UI_Controller>();

    }
	
	void Update () {
		
	}

    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            gameManager.enemyDifficulty = (GameManager.Difficulty)difficultyFloor;
            uiController.UpdateDifficultyText((GameManager.Difficulty)difficultyFloor);

        }
    }
}
