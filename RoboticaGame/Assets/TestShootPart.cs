using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShootPart : MonoBehaviour {

    public float speed;

    public Transform direction;

	void Start () {

        Destroy(gameObject, 5f);
		
	}
	
	void FixedUpdate () {

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed * Time.deltaTime);

    }

}
