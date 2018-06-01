using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullAutoGun : MonoBehaviour {

    public BaseGunScript baseGun;                               //de delegate class voor de wapens
    public Transform muzzleFlashPos;                            //de positie waaruit de bullet particle schiet

    public ParticleSystem bullet;

    public int ammoMagazine;                                    //variable die bijhoud hoeveel ammo er in het magezijn zit
    public int magazineSize;                                    //de hoeveelheid ammo dat in een magazijn kan zitten
    public int damage;

    public float fireRateVar;
    public float reloadSpeed;
    public float range;

    public GameObject hitMarker;

    bool canFire;
    bool mayReload;
    RaycastHit rayHit;
    

	void Start ()
    {

        ammoMagazine = magazineSize;                                                //vult het magazijn aan het begin van het spel
        canFire = true;                                                             //laat toe dat het wapen kan schieten zodra het wapen geactiveerd is
        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 0);                                 

        GetDelegate();                                                              

    }
	
	void Update ()
    {

        baseGun.fireGun(ammoMagazine, fireRateVar * Time.deltaTime);                //voert de FireGun functie uit
        baseGun.reload(magazineSize, reloadSpeed);                                  //voert de reload functie uit

        ReloadImg();

    }

    public void GetDelegate ()
    {

        baseGun.fireGun = FireGun;                                                  //set de firgun delegate functie voor de assault rifle
        baseGun.reload = Reload;                                                    //set de reload delegate functie voor de assault rifle


    }

    void FireGun (int ammo, float fireRate)                                             
    {

        if (Input.GetButton("Fire1") && ammoMagazine >= 1 && Time.timeScale == 1)       //conditie die checked of de linker muisknop ingedrukt is, het wapen geladen is en de game niet op pauze staat
        {

            if (canFire == true)                                                        //conditie die checked of het wapen kan vuren
            {

                StartCoroutine(ShootGun(fireRate));                                     //start de coroutine die het wapen laat vuren 

            }
        }
        else
        {

            MainCharacterController.ins.MainCharAnim.SetBool("Shoot", false);                                

        }
    }

    void Reload (int newAmmo, float reloadSpeed)
    {

        if(ammoMagazine < magazineSize)                                                 //conditie die checked of het wapen niet volledig geladen is
        {

            mayReload = true;  
            
        }

        if(Input.GetButtonDown("Reload") && mayReload == true)                          //conditie die cheked of de "R" knop ingedrukt word en het wapen mag herladen
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));                          //start de coroutine die het wapen laat herladen

        }

    }

    IEnumerator ShootGun (float fireRate)                           //coroutine snelheid van het schieten controleert
    {

        canFire = false;                                            //zet deze boolean uit zodat het wapen niet elk frame door kan blijven schieten

        ammoMagazine--;

        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 0);

        bullet.Play();                                              //activeert het particle systeem voor de laser

        MainCharacterController.ins.MainCharAnim.SetBool("Shoot", true);

        if (Physics.Raycast(transform.position, transform.TransformDirection(0, 0, range), out rayHit, range))
        {

            if (rayHit.transform.tag == "Enemy")                                                                                //conditie die checked of de raycast de enemy detecteert
            {

                StartCoroutine(Hitmarker(0.5f));                                                                                //start de coroutine voor de hitmarker
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().GetDamage(damage);                                       //geeft schade aan de enemy
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().animator.SetInteger("DamageID", Random.Range(0, 2));     //set de int die random animaties aan de enemy geeft voor de damage
                rayHit.transform.gameObject.GetComponent<EnemyRobot>().animator.SetTrigger("Damage");                           //activeert de damage animatie

            }
        }

        if (ammoMagazine <= 5)                              //conditie die checked of er minder of evenveel ammo in het magazijn zit
        {

            UI_Controller.ins.ShowReloadText(true);              //ativeert de reload text

        }

        yield return new WaitForSeconds(fireRate);

        canFire = true;                                             //zet deze boolean weer aan om opnieuw een kogel/laser te kunnen schieten

    }

    void ReloadImg ()
    {

        if (UI_Controller.ins.reloadImg.enabled == true)                                                     
        {

            UI_Controller.ins.reloadImg.fillAmount += Mathf.Lerp(0f, 1f, reloadSpeed / 100 * 0.48f);

        }

    }

    IEnumerator ReloadTimer (float speed, int newAmmo)                                          //coroutine die het wapen laat herladen 
    {

        canFire = false;                                                                         //zet deze boolean uit zodat het wapen niet kan vuren als het wapen aan het herladen is
        mayReload = false;                                                                       //zet deze boolean uit zodat je het wapen niet kan herladen als het wapen al aan het herladen is
        UI_Controller.ins.reloadText.GetComponent<Text>().text = "Reloading!";
        UI_Controller.ins.ShowReloadText(true);                                                       //activeert de reload text
        UI_Controller.ins.reloadImg.enabled = true;                                                  
        
        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;                                                                  //vult het magazijn
        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, 0);                                             //update het UI element van de ammo
        UI_Controller.ins.reloadText.GetComponent<Text>().text = UI_Controller.ins.oldReloadText;
        UI_Controller.ins.ShowReloadText(false);
        UI_Controller.ins.reloadImg.enabled = false;
        UI_Controller.ins.reloadImg.fillAmount = 0f;
        canFire = true;                                                                         //laat het wapen weer vuren 

    }

    IEnumerator Hitmarker (float disTime)                                                       //coroutine die de hitmarker zien als de speler de enemy raakt
    {

        hitMarker.SetActive(true);                                                              

        yield return new WaitForSeconds(disTime);

        hitMarker.SetActive(false);

    }
}
