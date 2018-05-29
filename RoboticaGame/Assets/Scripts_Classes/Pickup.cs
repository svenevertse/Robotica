using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public int addedHealth;

    public float addedStamina;

    public Material[] materials;
    public MeshFilter[] meshes;

    public Transform spawnedLoc;

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

                    if(MainCharacterController.ins.health != 100)
                    {

                        int newHealth = MainCharacterController.ins.health + addedHealth;
                        
                        if(newHealth >= 100)
                        {

                            MainCharacterController.ins.health = 100;
                            UI_Controller.ins.UpdateHealthBar((float)100);

                        }
                        else
                        {

                            MainCharacterController.ins.CheckHealth(-addedHealth);

                        }  
                    }      
                    break;

                case Pickup.PickupType.Stamina:

                    if(MainCharacterController.ins.stamina != 100)
                    {

                        float newStamina = MainCharacterController.ins.stamina + addedStamina;

                        if(newStamina >= 100)
                        {

                            MainCharacterController.ins.stamina = 100;

                        }
                        else
                        {

                            MainCharacterController.ins.StaminaPickup(addedStamina);

                        }
                    }
                    break;

            }

            spawnedLoc.GetComponent<PickupChecker>().gotPickup = false;

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
