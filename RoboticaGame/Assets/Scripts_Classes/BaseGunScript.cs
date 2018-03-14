﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunScript : MonoBehaviour {

    public delegate void FireGun(int ammoMagazine, float fireRate);
    public delegate void Reload(int newAmmo, float reloadSpeed);
    public FireGun fireGun;
    public Reload reload;

    public FullAutoGun fullAuto;
    public SemiAutoGun semiAuto;

    public UI_Controller UIController;


	void Start () {

        SwitchWeapon(0);
		
	}
	

	void Update () {

        CheckScrollInput();
		
	}

    void CheckScrollInput()
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SwitchWeapon(0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SwitchWeapon(1);
        }

    }

    void SwitchWeapon (int currentWeapon)
    {

        switch (currentWeapon)
        {

            case 0:
                fullAuto.enabled = true;
                semiAuto.enabled = false;

                fullAuto.GetDelegate();

                UIController.UpdateWeaponText(fullAuto.weaponName);
                UIController.UpdateAmmoCount(fullAuto.ammoMagazine);
                break;

            case 1:
                semiAuto.enabled = true;
                fullAuto.enabled = false;

                semiAuto.GetDelegate();

                UIController.UpdateWeaponText(semiAuto.weaponName);
                UIController.UpdateAmmoCount(semiAuto.ammoMagazine);
                break;


        }
    }
}
