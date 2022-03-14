using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] enemies;
	private Transform[] waypoints;
	[SerializeField] private float timeBetweenSpawn;
	[SerializeField] private int maxEnemies;
	[SerializeField] private int minEnemies;

	private void Awake()
	{
		waypoints = new Transform[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			waypoints[i] = transform.GetChild(i);
		}
	}

	public void Spawn()
	{
		int numberOfEnemies = Random.Range(minEnemies, maxEnemies + 1);
		StartCoroutine(SpawnEnemy(numberOfEnemies));
	}

	private IEnumerator SpawnEnemy(int count)
	{
		for (int i = 0; i < count; i++)
		{
			EnemyAI enemyAI = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity, transform).GetComponent<EnemyAI>();
			enemyAI.Waypoints = waypoints;
			EnemyManager.instance.EnemyCount++;
			yield return new WaitForSeconds(timeBetweenSpawn);
		}
	}
}
