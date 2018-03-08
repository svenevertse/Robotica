using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {


    public Slider staminaBar;
	
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void UpdateStaminaBar (float stamina)
    {

        staminaBar.value = stamina / 100;

    }
}
