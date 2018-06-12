using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Child class van de GunParentClass.
/// Neemt alle variblen over van zijn parent class.
/// Neemt alle functies over van zijn parent class en override de FireGun functie
/// </summary>
public class HandGun : GunParentClass {

    /// <summary>
    /// Deze Functie override zijn parent class zijn functie.
    /// Dat hebben we zo gedaan om de verschillende type inputs die de wapens nodig hebben van elkaar te scheiden.
    /// </summary>
    protected override void FireGun(int ammo, float fireRate)
    {

        if (Input.GetButtonDown("Fire1") && ammoMagazine >= 1 && Time.timeScale == 1)
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

}
