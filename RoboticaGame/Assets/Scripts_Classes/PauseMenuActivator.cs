using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject HUD;

    public MainCharacterController mc;

    public bool activationToggle;

	void Start () {
		
	}
	
	void Update () {

        if (Input.GetButtonDown("Cancel"))
        {

            ToggleBool();

        }
	}

    public void ToggleBool ()
    {

        activationToggle =! activationToggle;
        ActivatePauseMenu(activationToggle);

    }

    void ActivatePauseMenu (bool activate)
    {

        if(activate == true)
        {

            Time.timeScale = 0;

        }
        else
        {

            Time.timeScale = 1;

        }

        mc.LockCursor(!activate);
        HUD.SetActive(!activate);
        pauseMenu.SetActive(activate);

    }
}
