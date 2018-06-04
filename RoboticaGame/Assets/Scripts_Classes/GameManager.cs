using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int currentPoints;
    public int currentAmountEnemies;

    public Image img;
    public Text text;

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

    public static GameManager ins;

    void Awake ()
    {

        ins = this;

    }

    void Start()
    {

        difficultyStats = GetComponent <DifficultyStats>();

        StartCoroutine(FadeImage(true));
        StartCoroutine(FadeText(10f));

        MainCharacterController.ins.LockCursor(true);
        MainCharacterController.ins.enabled = false;
        StartCoroutine(StartPlayerMovement(1.5f));

        Time.timeScale = 1;

        enemyDifficulty = Difficulty.Recruit;

        XmlManager.ins.Load();
        UI_Controller.ins.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

    }

    public void GetPoints(int givenPoints)
    {

        currentPoints += givenPoints;
        UI_Controller.ins.UpdatePoints(currentPoints);

        if (currentPoints > XmlManager.ins.newHighscore.highscore)
        {

            UpdateHighscore();

        }

    }

    public void UpdateHighscore()
    {

        XmlManager.ins.newHighscore.highscore = currentPoints;
        XmlManager.ins.Save();
        UI_Controller.ins.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

    }

    public void EraseEnemy()
    {

        currentAmountEnemies--;
        WaveBasedSystem.ins.curInLevel--;

        if (currentAmountEnemies < 1)
        {

            SoundSystem.ins.PlayAudio(SoundSystem.SoundState.WaveEnding);
            difficultyStats.CalulateDifficulty();
            WaveBasedSystem.ins.CalculateEnemyAmount(WaveBasedSystem.ins.enemyAmount);
            

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

        yield return new WaitForSeconds(2f);
        img.color = new Color(0, 0, 0, 0);

    }

    IEnumerator FadeText(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

            text.color = new Color(1, 0, 0, 0);

    }


    IEnumerator StartPlayerMovement (float time)
    {

        yield return new WaitForSeconds(time);

        MainCharacterController.ins.enabled = true;

    }
}
