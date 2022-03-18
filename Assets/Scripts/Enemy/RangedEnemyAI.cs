using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;

public class RangedEnemyAI : EnemyAI
{
	[SerializeField] private LayerMask enemyLayer;
	[SerializeField] private float attackRanged;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform projectileSpawnPoint;
	[SerializeField] private float attackDelay;
	private float currentTime;

	private Collider targetCol;

	private void Start()
	{
		currentTime = attackDelay;
	}

	private void Update()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, attackRanged, enemyLayer);

		if(cols != null && cols.Length != 0)
		{
			Vector3 direction = cols[0].transform.position - transform.position;

			if (!Physics.Raycast(transform.position, direction, 1.0f, enemyLayer))
			{
				targetCol = cols[0];
			}
		}

		if (targetCol == null)
			Move();
		else
			Attack();
	}

	private void Attack()
	{
		rb.velocity = Vector3.zero;
		transform.LookAt(targetCol.transform);

		if (currentTime <= 0)
		{
			Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Projectile>();
			projectile.Initialize(damage, targetCol.transform, enemyLayer);
			currentTime = attackDelay;
		}
		else
		{
			currentTime -= Time.deltaTime;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRanged);
	}
}
