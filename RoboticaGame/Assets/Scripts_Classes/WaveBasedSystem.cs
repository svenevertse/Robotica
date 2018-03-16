using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBasedSystem : MonoBehaviour {

    public int enemyAmount;
    public int enemyInLevelCap;
    public int curInLevel;

    int currentWave;

    public float increasePercentage;
    public float timeTillWave;
    public float timeTillNewEnemySpawn;

    public GameObject[] spawnPositions;
    public GameManager gameManager;
    public UI_Controller UIController;

	void Start () {

        StartCoroutine(StartWave(timeTillWave));

		
	}
	

	void Update () {
		
	}

    public void CalculateEnemyAmount (float enemyAmountFloat)
    {

        enemyAmount += Mathf.RoundToInt(enemyAmountFloat / 100 * increasePercentage);

        StartCoroutine(StartWave(timeTillWave));


    }

    public void SpawnEnemies ()
    {

        currentWave++;
        UIController.UpdateWaveText(currentWave);

        gameManager.UpdateEnemyAmount(enemyAmount);

        StartCoroutine(SpawnNewInWave(0));

    }

    public void SpawnNewEnemiesInWave ()
    {
        int spacing;

        spacing = enemyInLevelCap - curInLevel;

        if(spacing > enemyAmount)
        {

            spacing = enemyAmount;

        }

        if(spacing > gameManager.currentAmountEnemies)
        {

            spacing = gameManager.currentAmountEnemies;

        }

        //curInLevel spacing enemyCap
        for (int i = 0; i < spacing; i++)
        {

            int r = Random.Range(0, spawnPositions.Length);
            GameObject instancedEnemy = Instantiate(Resources.Load("TestEnemy", typeof(GameObject)), spawnPositions[r].transform.position, Quaternion.identity) as GameObject;
            curInLevel = i + 1;

        }


    }


    public IEnumerator StartWave (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        SpawnEnemies();


    }

    public IEnumerator SpawnNewInWave (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        SpawnNewEnemiesInWave();


    }
}
