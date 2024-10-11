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

    // Start is called before the first frame update
    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int numberOfEnemies = enemies.Length;

        timeUntilSpawn -= Time.deltaTime;
        if (numberOfEnemies < 30) {
            if(timeUntilSpawn <= 0) {
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
        }
        
    }

    private void SetTimeUntilSpawn() {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
