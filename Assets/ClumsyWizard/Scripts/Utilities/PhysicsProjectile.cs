using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public class PhysicsProjectile : Projectile
	{
		[SerializeField] private float gravity;
		[SerializeField] private float apexHeight;
		Vector3 targetPos;

		public override void Initialize(int damage, Transform target, LayerMask hitableLayer)
		{
			base.Initialize(damage, target, hitableLayer);

			targetPos = target.position;

			float displacementY = targetPos.y - transform.position.y;
			Vector3 displacementXZ = new Vector3(targetPos.x - transform.position.x, 0.0f, targetPos.z - transform.position.z);
			float t = Mathf.Sqrt((-2 * apexHeight) / gravity) + Mathf.Sqrt((2 * (displacementY - apexHeight)) / gravity);

			Vector3 veclocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * apexHeight);
			Vector3 veclocityXZ = displacementXZ / t;

			Vector3 resultVeclocity = veclocityXZ + veclocityY;

			Physics.gravity = Vector3.up * gravity;

			GetComponent<Rigidbody>().velocity = resultVeclocity;
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((hitableLayer.value & (1 << other.gameObject.layer)) > 0)
			{
				Damage();
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(targetPos, 0.4f);
		}
	}
}