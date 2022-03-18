using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private EnemySpawner[] enemySpawners;
    public bool SpawnEnemies { get; set; }
    [SerializeField] private float timeBetweenWaves;
    private float currentTime;

    private void Start()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();
        currentTime = timeBetweenWaves;
    }

    private void Update()
	{
        if (!SpawnEnemies)
            return;

        if (currentTime <= 0)
        {
            int numberOfRoads = Random.Range(1, enemySpawners.Length);

            for (int i = 0; i < numberOfRoads; i++)
            {
                enemySpawners[i].Spawn();
            }
            SpawnEnemies = false;
            currentTime = timeBetweenWaves;
        }
        else
		{
            currentTime -= Time.deltaTime;
		}
    }
}
