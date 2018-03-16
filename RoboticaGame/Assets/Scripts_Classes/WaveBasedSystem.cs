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

        for(int i = 0; i < enemyAmount; i++)
        {

            int r = Random.Range(0, spawnPositions.Length);
            GameObject instancedEnemy = Instantiate(Resources.Load("TestEnemy", typeof(GameObject)), spawnPositions[r].transform.position, Quaternion.identity) as GameObject;

        }

    }


    public IEnumerator StartWave (float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        SpawnEnemies();


    }
}
