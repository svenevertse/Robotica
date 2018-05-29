using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewDifficulty : MonoBehaviour {

    public int difficultyFloor;

	void Start () {

    }
	
	void Update () {
		
	}

    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            GameManager.ins.enemyDifficulty = (GameManager.Difficulty)difficultyFloor;
            UI_Controller.ins.UpdateDifficultyText((GameManager.Difficulty)difficultyFloor);

        }
    }
}
