using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	[RequireComponent(typeof(Rigidbody))]
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		[SerializeField] private float damageRadius;
		[SerializeField] private float lifetime;
		private int damage;
		[SerializeField] private LayerMask hitableLayer;

		public void Initialize(int damage, LayerMask damageableLayer)
		{
			this.damage = damage;
			hitableLayer |= damageableLayer;
			GetComponent<Rigidbody>().AddForce(moveSpeed * transform.forward, ForceMode.Impulse);
			Destroy(gameObject, lifetime);
		}

		private void Update()
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius, hitableLayer);

			if (hitColliders != null && hitColliders.Length != 0)
			{
				IDamageable damageable = hitColliders[0].GetComponent<IDamageable>();

				if (damageable != null)
					damageable.Damage(damage);

				//TODO: Particle effects
				Destroy(gameObject);
			}
		}

		//Debug
		private void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, damageRadius);
		}
	}

}