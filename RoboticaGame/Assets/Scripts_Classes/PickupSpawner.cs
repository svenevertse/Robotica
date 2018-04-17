using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public GameManager gameManager;

    public float spawnTime;

    public Transform[] spawnLocations;

	void Start () {

        gameManager = GetComponent<GameManager>();

        StartCoroutine(SpawnTimer(spawnTime));

    }

    void SpawnPickup ()
    {

        int randomIntPickup = Random.Range(0, 2);
        int randomIntSpawn = Random.Range(0, spawnLocations.Length);

        GameObject instancedPickup = Instantiate(Resources.Load(("Pickup"), typeof(GameObject)),  spawnLocations[randomIntSpawn].position, Quaternion.identity) as GameObject;
        instancedPickup.GetComponent<Pickup>().pType = (Pickup.PickupType)randomIntPickup;

        StartCoroutine(SpawnTimer(spawnTime));

    }

    IEnumerator SpawnTimer (float time)
    {

        yield return new WaitForSeconds(time);

        SpawnPickup();
    
    }
}
