using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private EnemySpawner[] enemySpawners;
    private int maxRoads;

    private void Start()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();

        maxRoads = 1;
    }

    public void SpawnEnemies()
	{
        Random.InitState(Random.Range(0, int.MaxValue));
        int numberOfRoads = Random.Range(1, maxRoads + 1);

		for (int i = 0; i < numberOfRoads; i++)
		{
            enemySpawners[i].Spawn();
		}

        if (maxRoads < enemySpawners.Length - 1)
            maxRoads++;
    }
}
