using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;
    public UI_Controller UIController;

    public int ammoMagazine;
    public int magazineSize;
    public int damage;

    public float fireRateVar;
    public float reloadSpeed;
    public float range;

    public string weaponName;

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

        Debug.DrawRay(transform.position, transform.TransformDirection(0, 0, range), Color.red, range);

        if (Physics.Raycast(transform.position, transform.TransformDirection(0, 0, range), out rayHit, range))
        {

            print("Hit");

            if (rayHit.transform.tag == "Enemy")
            {
                print("Hit Enemy");
                rayHit.transform.gameObject.GetComponent<TestEnemy>().GetDamage(damage);

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
