using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public PauseMenuActivator activator;

    public GameObject [] buttons;

    public Text headTextMenu;

    void Start ()
    {

        gameObject.SetActive(false);

    }

	public void BackToGame ()
    {

        activator.ToggleBool();

    }

    public void RestartGame ()
    {

        activator.ToggleBool();

        SceneManager.LoadScene("SvenTest", LoadSceneMode.Single);

    }

    public void DissableButtons(int buttonIndex)
    {

        foreach (GameObject gb in buttons)
        {

            gb.SetActive(false);

        }

        buttons[buttonIndex].SetActive(true);

    }



    public void ChangeHeadName(string newHeadText)
    {

        headTextMenu.text = newHeadText;

    }

    public void GetToMainMenu ()
    {

        activator.ToggleBool();

        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);

    }

}
