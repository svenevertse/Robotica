using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunScript : MonoBehaviour {

    public delegate void FireGun(int ammoMagazine, float fireRate);
    public delegate void Reload(int newAmmo, float reloadSpeed);
    public FireGun fireGun;
    public Reload reload;

    public FullAutoGun fullAuto;
    public SemiAutoGun semiAuto;


	void Start () {
		
	}
	

	void Update () {
		
	}
}
