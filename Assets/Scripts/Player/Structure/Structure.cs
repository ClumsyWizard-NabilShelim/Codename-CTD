using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;

public abstract class Structure : MonoBehaviour
{
    private StructureData data;
	private StructureStats stats;
	private bool initialized;

	[SerializeField] private float attackDelay;
	private float currentTime;
	[SerializeField] protected float attackRange;
	[SerializeField] private LayerMask enemyLayer;
	[SerializeField] private LayerMask obstacleLayer;
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform attackOrigin;
	protected Transform attackTarget;

	[Header("Stats")]
	private int damage;

	protected void Awake()
	{
		currentTime = attackDelay;
	}

    public virtual void Initialize(StructureData data)
	{
		this.data = data;
		stats = GetComponent<StructureStats>();
		stats.Initialize(data);
		damage = data.Damage;
		initialized = true;
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
				Vector3 direction = hitColliders[i].transform.position - attackOrigin.position;
				RaycastHit hit;
				Physics.Raycast(attackOrigin.position, direction, out hit, 1.0f, obstacleLayer);

				if (hit.collider == null)
				{
					float distance = Vector3.Distance(attackOrigin.position, hitColliders[i].transform.position);

					if (distance <= closestDistance)
					{
						closestDistance = distance;
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
			Projectile projectile = Instantiate(projectilePrefab, attackOrigin.position, attackOrigin.rotation).GetComponent<Projectile>();
			projectile.Initialize(damage, enemyLayer);
			currentTime = attackDelay;
		}
		else
		{
			currentTime -= Time.deltaTime;
		}
	}

	protected abstract void RotateTowardsTarget();

	//Debug
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
}
