using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBasedSystem : MonoBehaviour {

    public int enemyAmount;
    public int enemyInLevelCap;
    public int curInLevel;
    public int spacing;
    public int newEnemyAmount;

    int currentWave;

    public float increasePercentage;
    public float timeTillWave;
    public float timeTillNewEnemySpawn;

    public string pathEnemyPrefab;

    public GameObject[] spawnPositions;
    public GameManager gameManager;
    public UI_Controller UIController;

	void Start () {

        StartCoroutine(StartWave(timeTillWave));

        StartCoroutine(SpawnNewInWave(timeTillNewEnemySpawn));
		
	}
	

	void Update () {

        CalculateNewEnemies();
		
	}

    public void CalculateEnemyAmount (float enemyAmountFloat)
    {

        enemyAmount += Mathf.RoundToInt(enemyAmountFloat / 100 * increasePercentage);

        StartCoroutine(StartWave(timeTillWave));


    }

    public void StartWaveSpawning ()
    {

        int spawnAmount;

        spacing = enemyAmount;
        print(spacing);

        if(enemyAmount > enemyInLevelCap)
        {

            spawnAmount = enemyInLevelCap;

        }
        else
        {

            spawnAmount = enemyAmount;

        }

        for (int i = 0; i < spawnAmount; i++)
        {

            int r = Random.Range(0, spawnPositions.Length);
            GameObject instancedEnemy = Instantiate(Resources.Load(pathEnemyPrefab, typeof(GameObject)), spawnPositions[r].transform.position, Quaternion.identity) as GameObject;
            instancedEnemy.name = "Enemy" + (i + 1);
            instancedEnemy.GetComponent<EnemyRobot>().CheckDifficulty(gameManager.enemyDifficulty);
            spacing --;
            curInLevel = i + 1;

        }

        currentWave++;
        UIController.UpdateWaveText(currentWave);

        gameManager.UpdateEnemyAmount(enemyAmount);

        


    }

    public void CalculateNewEnemies ()
    {

        if (enemyAmount >= enemyInLevelCap)
        {

            newEnemyAmount = enemyInLevelCap - curInLevel;

        }
        else
        {

            newEnemyAmount = spacing;

        }

    }

    public void SpawnNewEnemiesInWave ()
    {

        if (spacing >= 1 && curInLevel < enemyInLevelCap)
        {

            for (int i = 0; i < newEnemyAmount; i++)
            {

                int r = Random.Range(0, spawnPositions.Length);
                GameObject instancedEnemy = Instantiate(Resources.Load(pathEnemyPrefab, typeof(GameObject)), spawnPositions[r].transform.position, Quaternion.identity) as GameObject;
                instancedEnemy.GetComponent<EnemyRobot>().CheckDifficulty(gameManager.enemyDifficulty);
                spacing--;
                curInLevel++;
                instancedEnemy.name = "Enemy" + curInLevel;

            }

        }

        StartCoroutine(SpawnNewInWave(timeTillNewEnemySpawn));

    }


    public IEnumerator StartWave (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        StartWaveSpawning();


    }

    public IEnumerator SpawnNewInWave (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        SpawnNewEnemiesInWave();


    }
}
