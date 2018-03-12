using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {


    public Slider staminaBar;
    public Text ammoText;
	
	void Start () {
		
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
}
