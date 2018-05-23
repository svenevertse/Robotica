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

    void SpawnIn ()
    {
        int randomSpawn = Random.Range(0, spawnPos.Length);

        GameObject spawned = Instantiate(Resources.Load(("Sci-Fi_Soldier_MainMenu"), typeof(GameObject)), spawnPos[randomSpawn].position, Quaternion.identity) as GameObject;
        spawned.GetComponent<EnemyMainMenu>().destinations = spawnPos;

        StartCoroutine(Spawn(spawnTime));

    }

    IEnumerator Spawn (float spawnTime)
    {

        yield return new WaitForSeconds(spawnTime);

        SpawnIn();

    }

}
