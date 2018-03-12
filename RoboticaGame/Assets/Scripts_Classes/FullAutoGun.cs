using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;
    public UI_Controller UIController;

    public int ammoMagazine;
    public int magazineSize;

    public float fireRateVar;
    public float reloadSpeed;
    public float range;

    bool canFire;
    RaycastHit rayHit;
    

	void Start () {

        ammoMagazine = magazineSize;
        canFire = true;
        UIController.UpdateAmmoCount(ammoMagazine);

        baseGun.fireGun = FireGun;
        baseGun.reload = Reload;

    }
	
	void Update () {

        baseGun.fireGun(ammoMagazine, fireRateVar * Time.deltaTime);
        baseGun.reload(magazineSize, reloadSpeed);

    }

    void FireGun (int ammo, float fireRate)
    {

        if(Input.GetButton ("Fire1") && ammoMagazine >= 1 && canFire == true)
        {

            StartCoroutine(ShootGun(fireRate));

        }
    }

    void Reload (int newAmmo, float reloadSpeed)
    {

        if(Input.GetButtonDown("Reload"))
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));

        }

    }

    IEnumerator ShootGun (float fireRate)
    {

        canFire = false;

        print("Fire Gun");
        ammoMagazine--;

        UIController.UpdateAmmoCount(ammoMagazine);

        Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.red, range);

        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit, range))
        {
            

            if(rayHit.transform.tag == "Enemy")
            {

                rayHit.transform.gameObject.GetComponent<TestEnemy>().GetDamage(10);

            }

        }

        yield return new WaitForSeconds(fireRate);

        canFire = true;

    }

    IEnumerator ReloadTimer (float speed, int newAmmo)
    {

        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;
        UIController.UpdateAmmoCount(ammoMagazine);

    }
}
