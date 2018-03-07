using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {

    public float speed = 5f;
    public float rayDis = 1f;


	void Start () {
		
	}
	
	void Update () {

        Movement();
		
	}

    void Movement () {

        if (Input.GetAxis("Vertical") > 0)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime);
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (Physics.Raycast(transform.position, Vector3.back, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.back * speed * -Input.GetAxis("Vertical") * Time.deltaTime);
            }
        }

    }
}
