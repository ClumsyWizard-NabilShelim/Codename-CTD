using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyStats))]
public abstract class EnemyAI : MonoBehaviour
{
	private Rigidbody rb;
	protected EnemyStats stats;

	[Header("Moving")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float stoppingThreshold;
	[SerializeField] public Transform[] Waypoints { private get; set; }
	private int currentWaypoint;

	[Header("Attacking")]
	[SerializeField] protected int damage;

	protected void Start()
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

		rb.velocity = direction.normalized * moveSpeed;

		if(direction.magnitude <= stoppingThreshold)
		{
			currentWaypoint++;
		}
	}
}