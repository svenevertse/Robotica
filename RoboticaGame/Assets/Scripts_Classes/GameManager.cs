using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int currentPoints;
    public int currentAmountEnemies;

    public UI_Controller UIController;
    public WaveBasedSystem waveSystem;

    public Image img;

    public MainCharacterController cController;

    DifficultyStats difficultyStats;

    public enum Difficulty
    {

        Recruit = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3,
        Veteran = 4

    }

    public Difficulty enemyDifficulty;

    void Start()
    {

        difficultyStats = GetComponent <DifficultyStats>();

        StartCoroutine(FadeImage(true));

        cController.LockCursor(true);
        cController.enabled = false;
        StartCoroutine(StartPlayerMovement(1.5f));

        Time.timeScale = 1;

        XmlManager.ins.Load();
        UIController.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

        enemyDifficulty = Difficulty.Veteran;

    }

    public void GetPoints(int givenPoints)
    {

        currentPoints += givenPoints;
        UIController.UpdatePoints(currentPoints);

        if (currentPoints > XmlManager.ins.newHighscore.highscore)
        {

            UpdateHighscore();

        }

    }

    public void UpdateHighscore()
    {

        XmlManager.ins.newHighscore.highscore = currentPoints;
        XmlManager.ins.Save();
        UIController.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

    }

    public void EraseEnemy()
    {

        currentAmountEnemies--;
        waveSystem.curInLevel--;

        if (currentAmountEnemies < 1)
        {

            difficultyStats.CalulateDifficulty();
            waveSystem.CalculateEnemyAmount(waveSystem.enemyAmount);
            

        }

    }

    public void UpdateEnemyAmount(int enemyAmount)
    {

        currentAmountEnemies = enemyAmount;

    }

    IEnumerator FadeImage(bool fadeAway)
    {

        if (fadeAway == true)
        {

            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {

                img.color = new Color(0, 0, 0, i);
                yield return null;

            }
        }
    }

    IEnumerator StartPlayerMovement (float time)
    {

        yield return new WaitForSeconds(time);

        cController.enabled = true;

    }
}
