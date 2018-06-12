using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Parent class voor de wapens.
/// De wapens nemen alle functies over en overriden de FireGun fucntie.
/// </summary>
public class GunParentClass : MonoBehaviour {

    public string weaponName;

    public BaseGunScript baseGun;                                               //Variable voor de delegate class                                                                        
    public Transform muzzleFlashPos;                                            //positie waarvan de laser gevuurd moet worden                     

    public ParticleSystem bullet;

    public int ammoMagazine;                                                    //Het aantal kogels wat er op het moment in het magazijn zit                                
    public int magazineSize;                                                    //De capaciteit van het magazijn.                            
    public int damage;                                  

    public float fireRateVar;                                                  //De snelheid van vuren van het wapen
    public float reloadSpeed;
    public float range;

    public GameObject hitMarker;

    protected bool canFire;                                                     //bool die checked of het wapen kan vuren
    protected bool mayReload;                                                   //bool die checked of het wapen mag herladen
    protected RaycastHit rayHit;

    /// <summary>
    /// Start functie die aan het begin van het spel het magazijn vult en aangeeft dat het wapen kan vuren
    /// </summary>
    protected virtual void Start()
    {

        ammoMagazine = magazineSize;
        canFire = true;

        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, this);

    }

    /// <summary>
    /// Update functie die de vuur en herlaad functies uitvoert
    /// </summary>
    protected virtual void Update()
    {

        baseGun.fireGun(ammoMagazine, fireRateVar * Time.deltaTime);
        baseGun.reload(magazineSize, reloadSpeed);

    }

    /// <summary>
    /// Functie de delegate functies set voor het geselcteerde wapen
    /// </summary>
    public virtual void GetDelegate()
    {

        baseGun.fireGun = FireGun;                                                  
        baseGun.reload = Reload;                                                    


    }

    /// <summary>
    /// Functie die het schieten van het wapen uitvoerd.
    /// Deze functie checked ook of er gevuurd kan worden en checked de input.
    /// Deze functie word override in de child classes
    /// </summary>
    protected virtual void FireGun(int ammo, float fireRate)
    {

        if (Input.GetButton("Fire1") && ammoMagazine >= 1 && Time.timeScale == 1)       //conditie die checked of de linker muisknop ingedrukt is, het wapen geladen is en de game niet op pauze staat
        {

            if (canFire == true)                                                        
            {

                StartCoroutine(ShootGun(fireRate));                                     

            }
        }
        else
        {

            MainCharacterController.ins.MainCharAnim.SetBool("Shoot", false);

        }
    }

    /// <summary>
    /// Functie die het herladen van het wapen uitvoerd. 
    /// </summary>
    protected virtual void Reload(int newAmmo, float reloadSpeed)
    {

        if (ammoMagazine < magazineSize)                                                 //conditie die checked of het wapen niet volledig geladen is
        {

            mayReload = true;

        }

        if (Input.GetButtonDown("Reload") && mayReload == true)                          //conditie die cheked of de "R" knop ingedrukt word en het wapen mag herladen
        {

            StartCoroutine(ReloadTimer(reloadSpeed, newAmmo));                          //start de coroutine die het wapen laat herladen

        }

    }

    /// <summary>
    /// Coroutine die het wapen een kogel/projectile laat schieten.
    /// De volgende kogel word pas afgevuurd op het moment dat de WaitForSeconds voltooid is.
    /// </summary>
    protected virtual IEnumerator ShootGun(float fireRate)                                                                      
    {

        canFire = false;                                                                                                        //zet deze boolean uit zodat het wapen niet elk frame door kan blijven schieten

        ammoMagazine--;

        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, baseGun.currentSelectedGun);

        bullet.Play();                                                                                                          //activeert het particle systeem voor de laser
        SoundSystem.ins.PlayAudio(SoundSystem.SoundState.FireGun);

        MainCharacterController.ins.MainCharAnim.SetBool("Shoot", true);

        int reloadMessageTrigger = Mathf.RoundToInt((float)magazineSize / 100f * 30f);                                          //variable die aangeeft wanneer de speler minder dan 30% van zijn magazijn capaciteit beschikt

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

        if (ammoMagazine <= reloadMessageTrigger)                              //conditie die checked of er minder of evenveel ammo in het magazijn zit dan de variable die checked of er weinig kogels in het magazijn zit
        {

            UI_Controller.ins.ShowReloadText(true);                           //ativeert de reload text

        }

        yield return new WaitForSeconds(fireRate);

        canFire = true;                                                       //zet deze boolean weer aan om opnieuw een kogel/laser te kunnen schieten

    }

    /// <summary>
    /// coroutine die het wapen laat herladen
    /// </summary>
    protected virtual IEnumerator ReloadTimer(float speed, int newAmmo)                                                          
    {

        canFire = false;                                                                         //zet deze boolean uit zodat het wapen niet kan vuren als het wapen aan het herladen is
        mayReload = false;                                                                       //zet deze boolean uit zodat je het wapen niet kan herladen als het wapen al aan het herladen is
        SoundSystem.ins.PlayAudio(SoundSystem.SoundState.ReloadAR);
        UI_Controller.ins.reloadText.GetComponent<Text>().text = "Reloading!";
        UI_Controller.ins.ShowReloadText(true);                                                                                 //activeert de reload text

        yield return new WaitForSeconds(speed);

        ammoMagazine = newAmmo;                                                                                                  //vult het magazijn
        UI_Controller.ins.UpdateAmmoCount(ammoMagazine, baseGun.currentSelectedGun);                                             //update het UI element van de ammo
        UI_Controller.ins.reloadText.GetComponent<Text>().text = UI_Controller.ins.oldReloadText;
        UI_Controller.ins.ShowReloadText(false);
        canFire = true;                                                                                                         //laat het wapen weer vuren 

    }

    protected virtual IEnumerator Hitmarker(float disTime)                                                                     //coroutine die de hitmarker zien als de speler de enemy raakt
    {

        hitMarker.SetActive(true);

        yield return new WaitForSeconds(disTime);

        hitMarker.SetActive(false);

    }
}

