using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShootPart : MonoBehaviour {

    public float speed;
    public float destroyTime;

    public Transform direction;

	void Start () {

        Destroy(gameObject, destroyTime);
		
	}
	
	void FixedUpdate () {

        GetComponent<Rigidbody>().velocity = direction.forward * speed * Time.deltaTime;

    }

    void OnCollisionEnter (Collision col) {

        Destroy(gameObject);

    }

}
