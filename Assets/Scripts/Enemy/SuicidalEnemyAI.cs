using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;

public class SuicidalEnemyAI : EnemyAI
{
	private void Update()
	{
		Move();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("TownHall"))
		{
			IDamageable damageable = other.GetComponent<IDamageable>();

			if (damageable != null)
			{
				damageable.Damage(damage);
				stats.Damage(100);
			}
			else
			{
				Debug.LogWarning("TownHall has no IDamageable script");
			}
		}
	}
}
