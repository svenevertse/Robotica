using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject[] buttons;

    public Text headTextMenu;
    public Text highScoreText;

	void Start () {

        DissableButtons(0);

        Cursor.lockState = CursorLockMode.None;
		
	}
	
	void Update () {
		
	}

    public void StartGame ()
    {

        SceneManager.LoadScene("SvenTest", LoadSceneMode.Single);

    }

    public void QuitGame ()
    {

        Application.Quit();

    }

    public void DissableButtons (int buttonIndex)
    {

        foreach (GameObject gb in buttons)
        {

            gb.SetActive(false);

        }

        buttons[buttonIndex].SetActive(true);

    }

    public void ResetHighscore ()
    {

        XmlManager.ins.newHighscore.highscore = 0;
        XmlManager.ins.Save();

        GetHighScore();

    }



    public void ChangeHeadName (string newHeadText)
    {

        headTextMenu.text = newHeadText;

    }

    public void GetHighScore ()
    {

        XmlManager.ins.Load();

        highScoreText.text = "Highscore : " + XmlManager.ins.newHighscore.highscore.ToString();

    }
}
