using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    public Text ammoText;
    public Text currentPointText;
    public Text waveText;
    public Text highscoreText;
    public Text difficultyText;

    public Image hgSelected;
    public Image scarSelected;
    public Image reloadImg;
    public Image healthFill;
    public Image staminaFill;

    public string oldReloadText;

    public GameObject reloadText;

    public static UI_Controller ins;

    void Awake ()
    {

        ins = this;

    }
	
	void Start ()
    {

        UpdatePoints(0);

        oldReloadText = reloadText.GetComponent<Text>().text;

        ShowReloadText(false);
		
	}

    public void UpdateStaminaBar (float stamina)
    {

        staminaFill.fillAmount = stamina / 100;

    }

    public void UpdateAmmoCount (int ammoAmount)
    {

        ammoText.text = "" + ammoAmount;

    }

    public void UpdateWeaponText (int wType)
    {

        switch (wType)
        {

            case 0 :

                scarSelected.enabled = true;
                hgSelected.enabled = false;
                break;

            case 1:

                hgSelected.enabled = true;
                scarSelected.enabled = false;
                break;


        }

    }

    public void UpdateHealthBar(float health)
    {

        healthFill.fillAmount = health / 100;

    }

    public void UpdatePoints (int points)
    {

        currentPointText.text = "Points : " + points;

    }

    public void UpdateWaveText (int currentWave)
    {

        waveText.text = "Wave : " + currentWave;

    }

    public void UpdateHighscoreText (int highscore)
    {

        highscoreText.text = "Highscore : " + highscore;

    }

    public void UpdateDifficultyText (GameManager.Difficulty difficulty)
    {

        difficultyText.text = "Diffuclty : " + difficulty;

    }

    public void ShowReloadText (bool toggle)
    {

        reloadText.SetActive(toggle);

    }
}
