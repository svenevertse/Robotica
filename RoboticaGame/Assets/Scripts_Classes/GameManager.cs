using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentPoints;
    public int highScore;
    public int currentAmountEnemies;

    public UI_Controller UIController;
    public WaveBasedSystem waveSystem;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void GetPoints (int givenPoints)
    {

        currentPoints += givenPoints;
        UIController.UpdatePoints(currentPoints);

    }

    public void EraseEnemy ()
    {

        currentAmountEnemies--;
        waveSystem.curInLevel--;

        if(currentAmountEnemies < 1)
        {

            waveSystem.CalculateEnemyAmount(waveSystem.enemyAmount);

        }

    }

    public void UpdateEnemyAmount(int enemyAmount)
    {

        currentAmountEnemies = enemyAmount;

    }
}
