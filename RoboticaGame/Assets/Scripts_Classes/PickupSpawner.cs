using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    public float spawnTime;

    public Transform[] spawnLocations;

	void Start () {

        StartCoroutine(SpawnTimer(spawnTime));

    }

    void SpawnPickup ()
    {

        int randomIntPickup = Random.Range(0, 2);
        int randomIntSpawn = Random.Range(0, spawnLocations.Length);

        if (spawnLocations[randomIntSpawn].GetComponent<PickupChecker>().gotPickup == false)
        {

            GameObject instancedPickup = Instantiate(Resources.Load(("Pickup"), typeof(GameObject)), spawnLocations[randomIntSpawn].position, Quaternion.identity) as GameObject;
            spawnLocations[randomIntSpawn].GetComponent<PickupChecker>().gotPickup = true;
            instancedPickup.GetComponent<Pickup>().pType = (Pickup.PickupType)randomIntPickup;
            instancedPickup.GetComponent<Pickup>().spawnedLoc = spawnLocations[randomIntSpawn];

        }

        StartCoroutine(SpawnTimer(spawnTime));

    }

    IEnumerator SpawnTimer (float time)
    {

        yield return new WaitForSeconds(time);

        SpawnPickup();
    
    }
}
