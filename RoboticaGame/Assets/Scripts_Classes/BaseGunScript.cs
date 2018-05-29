using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunScript : MonoBehaviour {

    public delegate void FireGun(int ammoMagazine, float fireRate); //Delegate functies voor het vuren en herladen van de wapens
    public delegate void Reload(int newAmmo, float reloadSpeed);
    public FireGun fireGun;
    public Reload reload;

    public FullAutoGun fullAuto; 
    public SemiAutoGun semiAuto;

    public GameObject fullAutoMesh;
    public GameObject semiAutoMesh;

	void Start () {

        SwitchWeapon(0);                                //activeert de functie die het wapen selecteerd
		
	}
	

	void Update () {

        CheckScrollInput();                             //activeert de functie het scrollwheel checked
		
	}

    void CheckScrollInput()                             //functie die checked of je het scrollwheel gebruikt
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0 && Time.timeScale == 1)   //conditie die checked of er naar boven gescrolled word
        {
            SwitchWeapon(0);                            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Time.timeScale == 1)  //conditie die checked of er naar beneden gescrolled word
        {
            SwitchWeapon(1);
        }

    }

    void SwitchWeapon (int currentWeapon)               //functie die de wapens switched en al de benodigde variablen set
    {

        switch (currentWeapon)
        {

            case 0:
                fullAuto.enabled = true;
                semiAuto.enabled = false;

                fullAuto.GetDelegate();

                fullAutoMesh.SetActive(true);
                semiAutoMesh.SetActive(false);

                UI_Controller.ins.UpdateWeaponText(0);
                UI_Controller.ins.UpdateAmmoCount(fullAuto.ammoMagazine);

                MainCharacterController.ins.MainCharAnim.SetInteger("CurWeapon", 0);
                break;

            case 1:
                semiAuto.enabled = true;
                fullAuto.enabled = false;

                fullAutoMesh.SetActive(false);
                semiAutoMesh.SetActive(true);

                semiAuto.GetDelegate();

                UI_Controller.ins.UpdateWeaponText(1);
                UI_Controller.ins.UpdateAmmoCount(semiAuto.ammoMagazine);

                MainCharacterController.ins.MainCharAnim.SetInteger("CurWeapon", 1);
                break;


        }
    }
}
