using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;

    private float timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;

        timeUntilSpawn -= Time.deltaTime;
        if (numberOfEnemies < 150)
        {
            if (timeUntilSpawn <= 0)
            {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }

    }

    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
