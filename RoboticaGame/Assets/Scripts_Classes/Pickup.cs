using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class die ervoor zorgt dat de speler deze kan oppakken om zijn statistieken te verbeteren.
/// Hier word het type van de pickup aangegeven en de statistieken toegevoegd bij de speler.
/// </summary>
public class Pickup : MonoBehaviour {

    public int addedHealth;

    public float addedStamina;

    public Material[] materials;                                                //array voor de materials die de pickups hebben
    public MeshFilter[] meshes;                                                 //array voor de meshes die de pickups bevatten

    public Transform spawnedLoc;

    public enum PickupType                                                      //enum voor het type pickup
    {

        Health = 0,
        Stamina = 1

    }

    public PickupType pType;

    /// <summary>
    /// Checked op het moment dat de pickup gespawned is welk type het is.
    /// </summary>
	void Start ()
    {

        CheckType(pType);
		
	}

    /// <summary>
    /// OnTriggerEnter functie die een bepaalde statistiek toevoegd aan de speler zijn statistieken.
    /// Deze fucntie checked of de pickup collide met de speler.
    /// Deze functie bevat een switch statement. Deze statement checked welk type het is en voert dan uit wat dat type uit zou moeten voeren.
    /// </summary>
    void OnTriggerEnter (Collider trigger)
    {

        if(trigger.transform.tag == "Player")
        {

            switch (pType)
            {

                case Pickup.PickupType.Health:

                    if(MainCharacterController.ins.health != MainCharacterController.ins.maxHealth)                        //conditie die checked of de speler al niet het maximale aantal heeft
                    {
                           
                        int newHealth = MainCharacterController.ins.health + addedHealth;                                   //Variable voor het nieuwe aantal statistiek wat de speler krijgt
                        
                        if(newHealth >= MainCharacterController.ins.maxHealth)                                              //conditie die checked of het toegevoegde aantal groter is dan het maximale aantal
                        {

                            MainCharacterController.ins.health = MainCharacterController.ins.maxHealth;
                            UI_Controller.ins.UpdateHealthBar((float)MainCharacterController.ins.maxHealth);            

                        }
                        else
                        {

                            MainCharacterController.ins.CheckHealth(-addedHealth);

                        }  
                    }      
                    break;

                case Pickup.PickupType.Stamina:

                    if(MainCharacterController.ins.stamina != MainCharacterController.ins.maxStamina)
                    {

                        float newStamina = MainCharacterController.ins.stamina + addedStamina;

                        if(newStamina >= MainCharacterController.ins.maxStamina)
                        {

                            MainCharacterController.ins.stamina = MainCharacterController.ins.maxStamina;

                        }
                        else
                        {

                            MainCharacterController.ins.StaminaPickup(addedStamina);

                        }
                    }
                    break;

            }

            spawnedLoc.GetComponent<PickupChecker>().gotPickup = false;                                                         //geeft aan op de spawn locatie van de pickup dat deze al een pickup                                                                                                                                
                                                                                                                                //bevat zodat er geen pickups in elkaar kunnen spawnen
            Destroy(gameObject);                                                                                                

        }

    }

    /// <summary>
    /// Functie die checked welk type de pickup is
    /// Het visuele aspect van de pickup word aangepast gebaseerd op welk type de pickup is.
    /// </summary>
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
