using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class die de delegate functies van de wapens controleert 
/// Ook controleerd deze functie het wapen switchen
/// </summary>
public class BaseGunScript : MonoBehaviour {

    public delegate void FireGun(int ammoMagazine, float fireRate); //Delegate functies voor het vuren en herladen van de wapens
    public delegate void Reload(int newAmmo, float reloadSpeed);
    public FireGun fireGun;
    public Reload reload;

    public AssaultRifle fullAuto;                       
    public HandGun semiAuto;

    public GunParentClass currentSelectedGun;                       //variable die bijhoud welk wapen op het moment geselecteerd is.

    public GameObject fullAutoMesh;
    public GameObject semiAutoMesh;

    /// <summary>
    /// Start functie die aangeeft welk wapen er aan het begin geselecteerd moet zijn
    /// Ook update deze functie de HUD elementen voor het aantal ammo wat deze wapens op het moment in hun magazijn hebben zitten
    /// </summary>
	void Start () {

        SwitchWeapon(0);                                //activeert de functie die het wapen selecteerd

        UI_Controller.ins.UpdateAmmoCount(semiAuto.ammoMagazine, fullAuto);
        UI_Controller.ins.UpdateAmmoCount(fullAuto.ammoMagazine, semiAuto);
        

    }
	

	void Update () {

        CheckScrollInput();                             //activeert de functie het scrollwheel checked
		
	}

    /// <summary>
    /// Functie die checked of je het scrollwheel gebruikt en gebaseerd op welke kant je dat beweegt word de functie aangeroepen die het wapen wisselt
    /// </summary>
    void CheckScrollInput()                             
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0 && Time.timeScale == 1)   
        {
            SwitchWeapon(0);                            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Time.timeScale == 1)  
        {
            SwitchWeapon(1);
        }

    }

    /// <summary>
    /// Functie die de wapens switched, De meshes aan en uit zet, De classes van de wapens aan en uit zet en de Animatie aanpast.
    /// </summary>
    void SwitchWeapon (int currentWeapon)              
    {

        switch (currentWeapon)
        {

            case 0:
                fullAuto.enabled = true;
                semiAuto.enabled = false;

                fullAutoMesh.SetActive(true);
                semiAutoMesh.SetActive(false);

                currentSelectedGun = fullAuto;

                UI_Controller.ins.UpdateWeaponText(0);                                              //Update het wapen icoontje in de HUD

                MainCharacterController.ins.MainCharAnim.SetInteger("CurWeapon", 0);
                break;

            case 1:
                semiAuto.enabled = true;
                fullAuto.enabled = false;

                fullAutoMesh.SetActive(false);
                semiAutoMesh.SetActive(true);

                currentSelectedGun = semiAuto;

                UI_Controller.ins.UpdateWeaponText(1);

                MainCharacterController.ins.MainCharAnim.SetInteger("CurWeapon", 1);
                break;

        }

        currentSelectedGun.GetDelegate();                                                       //Voert de delegate functie voor het geselecteerde uit zodat de functies alleen beschikbaar zijn voor                                                                  
                                                                                                //het geselecteerde wapen
    }
}
