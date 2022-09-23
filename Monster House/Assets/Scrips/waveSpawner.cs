using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timebtwWaves = 5f;

    public float countdown = 2f;

    private int waveNum = 0;

    void Update()
    {
       
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timebtwWaves;
        }

        countdown = countdown-Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveNum++;

        for (int i = 0; i < waveNum; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
