using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;
    public UI_Controller UIController;
    public Transform muzzleFlashPos;
    public MainCharacterController player;

    public ParticleSystem bullet;

    public int ammoMagazine;
    public int magazineSize;
    public int damage;

    public float fireRateVar;
    public float reloadSpeed;
    public float range;

    public string weaponName;

    public GameObject hitMarker;

    bool canFire;
    bool mayReload;
    RaycastHit rayHit;
    

	void Start ()
    {

        ammoMagazine = magazineSize;
        canFire = true;
        UIController.UpdateAmmoCount(ammoMagazine);

        GetDelegate();

       
    }
	
	void Update ()
    {

        baseGun.fireGun(ammoMagazine, fireRateVar * Time.deltaTime);
        baseGun.reload(magazineSize, reloadSpeed);

    }

    public void GetDelegate ()
    {

        baseGun.fireGun = FireGun;
        baseGun.reload = Reload;


    }

    void FireGun (int ammo, float fireRate)
    {

        if (Input.GetButton("Fire1") && ammoMagazine >= 1 && Time.timeScale == 1)
        {

            if (canFire == true)
            {

                StartCoroutine(ShootGun(fireRate));

            }
        }
        else
        {

            player.mainCharAnimArms.SetBool("Rapid", false);

        }
    }

    void Reload (int newAmmo, float reloadSpeed)
    {

        if(ammoMagazine < magazineSize)
        {
            mayReload = true;
        }

        if(Input.GetButtonDown("Reload") && mayReload == true)
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));

        }

    }

    IEnumerator ShootGun (float fireRate)
    {

        canFire = false;

        ammoMagazine--;

        UIController.UpdateAmmoCount(ammoMagazine);

        bullet.Play();

        player.mainCharAnimArms.SetBool("Rapid", true);

        if (Physics.Raycast(transform.position, transform.TransformDirection(0, 0, range), out rayHit, range))
        {

            if (rayHit.transform.tag == "Enemy")
            {

                StartCoroutine(Hitmarker(0.5f));
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().GetDamage(damage);
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().animator.SetInteger("DamageID", Random.Range(0, 2));
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().animator.SetTrigger("Damage");

            }
        }

        if (ammoMagazine <= 5)
        {

            UIController.ShowReloadText(true);

        }

        yield return new WaitForSeconds(fireRate);

        canFire = true;

    }

    IEnumerator ReloadTimer (float speed, int newAmmo)
    {

        canFire = false;
        mayReload = false;
        UIController.reloadText.GetComponent<Text>().text = "Reloading!";
        UIController.ShowReloadText(true);

        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;
        UIController.UpdateAmmoCount(ammoMagazine);
        UIController.reloadText.GetComponent<Text>().text = UIController.oldReloadText;
        UIController.ShowReloadText(false);
        canFire = true;

    }

    IEnumerator Hitmarker (float disTime)
    {

        hitMarker.SetActive(true);

        yield return new WaitForSeconds(disTime);

        hitMarker.SetActive(false);

    }
}
