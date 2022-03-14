using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;

[RequireComponent(typeof(EnemySpawnManager))]
public class EnemyManager : StaticInstance<EnemyManager>
{
	private EnemySpawnManager enemySpawnManager;
	[SerializeField] private int enemyCount;

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
				GameManager.instance.WavesCleared++;
				enemySpawnManager.SpawnEnemies();
			}
		}
	}

	private void Start()
	{
		enemySpawnManager = GetComponent<EnemySpawnManager>();
		enemySpawnManager.SpawnEnemies();
	}
}