using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public PauseMenuActivator activator;

    void Start ()
    {

        gameObject.SetActive(false);

    }

	public void BackToGame ()
    {

        activator.ToggleBool();

    }

}
