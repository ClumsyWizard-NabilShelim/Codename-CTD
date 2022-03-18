using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;

[RequireComponent(typeof(EnemySpawnManager))]
public class EnemyManager : StaticInstance<EnemyManager>
{
	private EnemySpawnManager enemySpawnManager;
	public int enemyCount;

	public int EnemyCount
	{
		get
		{
			return enemyCount;
		}

		set
		{
			enemyCount = value;

			if(enemyCount <= 0)
			{
				GameManager.instance.WaveCleared();
				enemySpawnManager.SpawnEnemies = true;
			}
		}
	}

	private void Start()
	{
		enemySpawnManager = GetComponent<EnemySpawnManager>();
		enemySpawnManager.SpawnEnemies = true;
	}
}