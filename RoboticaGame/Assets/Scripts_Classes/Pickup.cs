using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public int addedHealth;

    public float addedStamina;

    public Material[] materials;
    public MeshFilter[] meshes;

    public enum PickupType
    {

        Health = 0,
        Stamina = 1

    }

    public PickupType pType;

	void Start () {

        CheckType(pType);
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            switch (pType)
            {

                case Pickup.PickupType.Health:

                    if(trigger.GetComponent<MainCharacterController>().health != 100)
                    {

                        trigger.GetComponent<MainCharacterController>().CheckHealth(-addedHealth);

                    }      
                    break;

                case Pickup.PickupType.Stamina:

                    if(trigger.GetComponent<MainCharacterController>().stamina != 100)
                    {

                        trigger.GetComponent<MainCharacterController>().StaminaPickup(addedStamina);

                    }
                    break;

            }

            Destroy(gameObject);

        }

    }

    void CheckType (PickupType pType)
    {

        switch (pType)
        {

            case Pickup.PickupType.Health:

                GetComponent<MeshFilter>().sharedMesh = meshes[0].sharedMesh;
                GetComponent<Renderer>().material = materials[0];
                break;

            case Pickup.PickupType.Stamina:

                GetComponent<MeshFilter>().sharedMesh = meshes[1].sharedMesh;
                GetComponent<Renderer>().material = materials[1];
                break;


        }
    }
}
