using ClumsyWizard.Utilities;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyStats))]
public abstract class EnemyAI : MonoBehaviour
{
	protected Rigidbody rb;
	protected EnemyStats stats;

	[Header("Moving")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float stoppingThreshold;
	[SerializeField] public Transform[] Waypoints { private get; set; }
	private int currentWaypoint;

	[Header("Attacking")]
	[SerializeField] protected int damage;

	protected void Awake()
	{
		currentWaypoint = 0;

		rb = GetComponent<Rigidbody>();
		stats = GetComponent<EnemyStats>();
	}

	protected void Move()
	{
		if (Waypoints == null || currentWaypoint >= Waypoints.Length || GameManager.instance.IsGameOver)
		{
			rb.velocity = Vector3.zero;
			return;
		}

		Vector3 direction = Waypoints[currentWaypoint].position - transform.position;
		direction.y = 0.0f;
		transform.LookAt(direction);

		rb.velocity = direction.normalized * moveSpeed;

		if(direction.magnitude <= stoppingThreshold)
		{
			currentWaypoint++;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("TownHall"))
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