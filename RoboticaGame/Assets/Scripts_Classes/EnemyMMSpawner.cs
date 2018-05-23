using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMMSpawner : MonoBehaviour {

    public Transform[] spawnPos;

    public float spawnTime;

	void Start ()
    {

        StartCoroutine(Spawn(spawnTime));                   
		
	}

    void SpawnIn ()                                                 //functie die een enemy op een random positie spawned
    {
        int randomSpawn = Random.Range(0, spawnPos.Length);         //Int die word gerandomized en word gebruikt om een random Transform te pakken uit de spawnPos array

        GameObject spawned = Instantiate(Resources.Load(("Sci-Fi_Soldier_MainMenu"), typeof(GameObject)), spawnPos[randomSpawn].position, Quaternion.identity) as GameObject;   //spawned enemy op een van de posities van spawn pos
        spawned.GetComponent<EnemyMainMenu>().destinations = spawnPos;

        StartCoroutine(Spawn(spawnTime));                       //herstart de coroutine om om de zoveel tijd een enemy te spawnen

    }

    IEnumerator Spawn (float spawnTime)                         //coroutine die de loop start wat de enemies spawned 
    {

        yield return new WaitForSeconds(spawnTime);

        SpawnIn();                                              //voert de spawn functie uit

    }

}
