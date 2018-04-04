using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	void Start () {
		
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
}
