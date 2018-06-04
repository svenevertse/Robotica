using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SemiAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;
    public Transform muzzleFlashPos;

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


    void Start()
    {

        ammoMagazine = magazineSize;
        canFire = true;
        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 1);

        GetDelegate();

    }

    void Update()
    {

        baseGun.fireGun(ammoMagazine, fireRateVar * Time.deltaTime);
        baseGun.reload(magazineSize, reloadSpeed);

    }

    public void GetDelegate()
    {

        baseGun.fireGun = FireGun;
        baseGun.reload = Reload;


    }

    void FireGun(int ammo, float fireRate)
    {

        if (Input.GetButtonDown("Fire1") && ammoMagazine >= 1 && canFire == true && Time.timeScale == 1)
        {

            if(canFire == true)
            {

                StartCoroutine(ShootGun(fireRate));

            }
        }
        else
        {

            MainCharacterController.ins.MainCharAnim.SetBool("Shoot", false);

        }
    }

    void Reload(int newAmmo, float reloadSpeed)
    {

        if(ammoMagazine < magazineSize)
        {

            mayReload = true;

        }

        if (Input.GetButtonDown("Reload") && mayReload == true)
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));

        }

    }

    IEnumerator ShootGun(float fireRate)
    {

        canFire = false;

        ammoMagazine--;

        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 1);

        bullet.Play();
        SoundSystem.ins.PlayAudio(SoundSystem.SoundState.FireGun);

        MainCharacterController.ins.MainCharAnim.SetBool("Shoot", true);

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

        if (ammoMagazine <= 2)
        {

            UI_Controller.ins.ShowReloadText(true);

        }

        yield return new WaitForSeconds(fireRate);

        canFire = true;

    }

    IEnumerator ReloadTimer (float speed, int newAmmo)
    {

        canFire = false;
        mayReload = false;
        SoundSystem.ins.PlayAudio(SoundSystem.SoundState.ReloadHG);
        UI_Controller.ins.reloadText.GetComponent<Text>().text = "Reloading!";
        UI_Controller.ins.ShowReloadText(true);

        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;
        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 1);
        UI_Controller.ins.reloadText.GetComponent<Text>().text = UI_Controller.ins.oldReloadText;
        UI_Controller.ins.ShowReloadText(false);
        canFire = true;

    }

    IEnumerator Hitmarker (float disTime)
    {

        hitMarker.SetActive(true);

        yield return new WaitForSeconds(disTime);

        hitMarker.SetActive(false);

    }
}
