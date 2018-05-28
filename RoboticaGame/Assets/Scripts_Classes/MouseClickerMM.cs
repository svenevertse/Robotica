using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickerMM : MonoBehaviour {

    RaycastHit rayHit;

	void Start ()
    {
		
	}
	

	void Update ()
    {

        if(Input.GetButtonDown("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {

                if(rayHit.transform.tag == "Enemy")
                {

                    rayHit.transform.gameObject.GetComponent<EnemyMainMenu>().Death();

                }

            }

        }
		
	}


}
