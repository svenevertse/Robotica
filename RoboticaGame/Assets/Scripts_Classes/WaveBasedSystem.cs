using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBasedSystem : MonoBehaviour {

    public int enemyAmount;                                                                     //Het aantal dat er over de gehele wave beschikbaar is
    public int enemyInLevelCap;                                                                 //Het maximale aantal dat op dat moment in het level mag zijn
    public int curInLevel;                                                                      //Het aantal dat op dat moment in het level zit
    public int spacing;                                                                         //Het aantal enemies dat nog niet in het level zit omdat het maximale aantal al in het level zit
    public int newEnemyAmount;                                                                  

    int currentWave;

    public float increasePercentage;
    public float timeTillWave;
    public float timeTillNewEnemySpawn;

    public string pathEnemyPrefab;

    public GameObject[] spawnPositions;
    public GameObject[] spawnPosRecruit;
    public GameObject[] spawnPosMedium;
    public GameObject[] spawnPosVeteran;

    public static WaveBasedSystem ins;

    void Awake ()
    {

        ins = this;

    }

	void Start ()
    {

        StartCoroutine(StartWave(5));
		
	}
	

	void Update ()
    {

        CalculateNewEnemies();
		
	}

    public void CalculateEnemyAmount (float enemyAmountFloat)
    {

        enemyAmount += Mathf.RoundToInt(enemyAmountFloat / 100 * increasePercentage);

        StartCoroutine(StartWave(timeTillWave));


    }

    public void StartWaveSpawning ()
    {

        spacing = enemyAmount;

        SoundSystem.ins.PlayAudio(SoundSystem.SoundState.WaveStart);

        StartCoroutine(SpawnNewInWave(timeTillNewEnemySpawn));

        currentWave++;
        UI_Controller.ins.UpdateWaveText(currentWave);

        GameManager.ins.UpdateEnemyAmount(enemyAmount);

    }

    public void CalculateNewEnemies ()
    {

        if (enemyAmount > enemyInLevelCap)
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

            CheckDifficulty(GameManager.ins.enemyDifficulty);
            int r = Random.Range(0, spawnPositions.Length);
            GameObject instancedEnemy = Instantiate(Resources.Load(pathEnemyPrefab, typeof(GameObject)), spawnPositions[r].transform.position, Quaternion.identity) as GameObject;
            curInLevel++;
            spacing--;
            instancedEnemy.name = "Enemy" + curInLevel;
            instancedEnemy.GetComponent<EnemyRobot>().diffStats = GameObject.Find("GameManager").GetComponent<DifficultyStats>();
            instancedEnemy.GetComponent<EnemyRobot>().CheckDifficulty(GameManager.ins.enemyDifficulty);

            StartCoroutine(SpawnNewInWave(timeTillNewEnemySpawn));

        }

        StartCoroutine(SpawnNewInWave(timeTillNewEnemySpawn));

    }

    public void CheckDifficulty (GameManager.Difficulty enemyDiff)
    {

        switch (enemyDiff)
        {

            case GameManager.Difficulty.Recruit:

                spawnPositions = spawnPosRecruit;
                break;

            case GameManager.Difficulty.Medium:

                spawnPositions = spawnPosMedium;
                break;

            case GameManager.Difficulty.Veteran:

                spawnPositions = spawnPosVeteran;
                break;

        }

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
