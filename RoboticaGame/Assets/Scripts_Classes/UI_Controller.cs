using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    public Slider staminaBar;
    public Slider healthBar;

    public Text ammoText;
    public Text weaponText;
    public Text currentPointText;
    public Text waveText;
    public Text highscoreText;
	
	void Start () {

        UpdatePoints(0);
		
	}
	
	
	void Update () {
		
	}

    public void UpdateStaminaBar (float stamina)
    {

        staminaBar.value = stamina / 100;

    }

    public void UpdateAmmoCount (int ammoAmount)
    {

        ammoText.text = "Ammo : " + ammoAmount;

    }

    public void UpdateWeaponText (string weaponName)
    {

        weaponText.text = "Weapon : " + weaponName;

    }

    public void UpdateHealthBar(float health)
    {

        healthBar.value = health / 100;

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
}
