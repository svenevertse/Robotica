using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {

    private GameObject _arms;
    private GameObject _body;

    public Animator animArms;
    public Animator animLegs;

    public List<GameObject> RagBones;

	// Use this for initialization
	void Start ()
    {
        _arms = GameObject.FindGameObjectWithTag("MC_Arms");
        _body = GameObject.FindGameObjectWithTag("MC_Body");

        animArms = _arms.GetComponent<Animator>();
        animLegs = _body.GetComponent<Animator>();

        RagBones = new List<GameObject>();

        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.tag == "MC_Bones")
            {
                RagBones.Add(child.gameObject);

                Rigidbody rb = child.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.detectCollisions = false;
            }
        }

        animArms.enabled = true;
        animLegs.enabled = true;      
	}

    void PlayaDeath()
    {
        animArms.enabled = false;
        animLegs.enabled = false;

        foreach (GameObject bn in RagBones)
        {
            Rigidbody rb = bn.GetComponent<Rigidbody>();
            rb.detectCollisions = true;
            rb.isKinematic = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("q"))
        {
            PlayaDeath();
        }
		
	}
}
