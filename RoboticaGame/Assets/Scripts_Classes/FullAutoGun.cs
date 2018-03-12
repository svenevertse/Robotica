using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;

    public int ammoMagazine;
    public int magazineSize;

    public float fireRate;
    public float reloadSpeed;
    

	void Start () {

        ammoMagazine = magazineSize;

        baseGun.fireGun = FireGun;
        baseGun.reload = Reload;

    }
	
	void Update () {

        baseGun.fireGun(ammoMagazine, fireRate);
        baseGun.reload(magazineSize, reloadSpeed);

    }

    void FireGun (int ammo, float fireRate)
    {

        if(Input.GetButton ("Fire1") && ammoMagazine >= 1)
        {

            ammoMagazine --;
            print(ammo);
            print(fireRate);

        }
    }

    void Reload (int newAmmo, float reloadSpeed)
    {

        if(Input.GetButtonDown("Reload"))
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));

        }

    }

    IEnumerator ReloadTimer (float speed, int newAmmo)
    {

        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;

    }
}
