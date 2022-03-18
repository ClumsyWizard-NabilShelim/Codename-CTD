using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;

public class Structure : MonoBehaviour
{
	private StructureStats stats;

	private bool initialized;

	[SerializeField] private float attackDelay;
	private float currentTime;
	[SerializeField] protected float attackRange;
	[SerializeField] private LayerMask enemyLayer;
	[SerializeField] private LayerMask obstacleLayer;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform projectileSpawnPoint;
	protected Transform attackTarget;

	[Header("Visuals")]
	[SerializeField] private Transform rotateableMesh;
	[SerializeField] private float lookSpeed = 10.0f;

	[Header("Stats")]
	private int damage;

	protected void Awake()
	{
		currentTime = attackDelay;
	}

    public virtual void Initialize(StructureData data)
	{
		stats = GetComponent<StructureStats>();
		stats.Initialize(data);
		damage = data.Damage;
		initialized = true;
		GetComponent<Collider>().enabled = true;
	}

	private void Update()
	{
		if (!initialized)
			return;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

		if (hitColliders != null && hitColliders.Length != 0)
		{
			float closestDistance = float.MaxValue;

			for (int i = 0; i < hitColliders.Length; i++)
			{
				Vector3 direction = hitColliders[i].transform.position - projectileSpawnPoint.position;
				RaycastHit hit;
				Physics.Raycast(transform.position, direction, out hit, 1.0f, obstacleLayer);

				if (hit.collider == null)
				{
					if (direction.magnitude <= closestDistance)
					{
						closestDistance = direction.magnitude;
						attackTarget = hitColliders[i].transform;
					}
				}
				else
				{
					attackTarget = null;
				}
			}
		}
		else
		{
			attackTarget = null;
		}

		if (attackTarget == null)
			return;

		RotateTowardsTarget();

		if (currentTime <= 0)
		{
			Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Projectile>();
			projectile.Initialize(damage, attackTarget, enemyLayer);
			currentTime = attackDelay;
		}
		else
		{
			currentTime -= Time.deltaTime;
		}
	}

	private void RotateTowardsTarget()
	{
		Vector3 directionToTarget = attackTarget.position - rotateableMesh.position;
		Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
		Quaternion targetRotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
		rotateableMesh.rotation = Quaternion.Slerp(rotateableMesh.rotation, targetRotation, lookSpeed * Time.deltaTime);
	}

	//Debug
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
