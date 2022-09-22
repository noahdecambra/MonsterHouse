using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{

    public GameObject[] baseEnemyPrefabs;
    private GameObject _bossPrefab;
    public GameObject[] bossEnemyPrefabs;
    private GameObject _enemyPrefab;

    public Transform spawnPoint;

    public float timebtwWaves = 5f;

    public float countdown = 2f;

    private int waveNum = 0;
    private int _wavesUntilBoss=5;
    private bool _isBossWave;

    void Update()
    {
       
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timebtwWaves;
        }

        countdown -=Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveNum++;
        _wavesUntilBoss--;
        if (_wavesUntilBoss<=0)
        {
            _bossPrefab = bossEnemyPrefabs[Random.Range(0, bossEnemyPrefabs.Length)];
            _isBossWave = true;
            SpawnEnemy(_isBossWave);
            _isBossWave = false;
        }
        for (int i = 0; i < waveNum; i++)
        {
            SpawnEnemy(_isBossWave);
            yield return new WaitForSeconds(.3f);
        }

    }

    void SpawnEnemy(bool bosswave)
    {
        _enemyPrefab = baseEnemyPrefabs[Random.Range(0, baseEnemyPrefabs.Length)];
        if (bosswave)
        {
            Instantiate(_bossPrefab, spawnPoint.position, spawnPoint.rotation);
            _wavesUntilBoss = 5;
        }
        Instantiate (_enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
