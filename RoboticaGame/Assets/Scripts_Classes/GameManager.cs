using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int currentPoints;
    public int currentAmountEnemies;

    public UI_Controller UIController;
    public WaveBasedSystem waveSystem;

    public enum Difficulty
    {

        Recruit = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3,
        Veteran = 4

    }

    public Difficulty enemyDifficulty;

	void Start () {

        XmlManager.ins.Load();
        UIController.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

        enemyDifficulty = Difficulty.Veteran;
		
	}
	
	void Update () {
		
	}

    public void GetPoints (int givenPoints)
    {

        currentPoints += givenPoints;
        UIController.UpdatePoints(currentPoints);

        if(currentPoints > XmlManager.ins.newHighscore.highscore)
        {

            UpdateHighscore ();

        } 

    }

    public void UpdateHighscore ()
    {

        XmlManager.ins.newHighscore.highscore = currentPoints;
        XmlManager.ins.Save();
        UIController.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

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
