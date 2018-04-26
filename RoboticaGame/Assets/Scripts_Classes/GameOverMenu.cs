using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public Text highscoreText;
    public Text currentScoreText;

    void Start()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    public void RestartGame()
    {

        SceneManager.LoadScene("SvenTest", LoadSceneMode.Single);

    }

    public void BackToMainMenu()
    {

        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);

    }

    public void LoadHighscore()
    {

        XmlManager.ins.Load();

        highscoreText.text = "Highscore : " + XmlManager.ins.newHighscore.highscore.ToString();

    }

   public  void GetCurrentScore()
    {

        currentScoreText.text = "Current Score : " + GameObject.Find("GameManager").GetComponent<GameManager>().currentPoints;

    }
}
