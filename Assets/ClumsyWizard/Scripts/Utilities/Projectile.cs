using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Projectile : MonoBehaviour
	{
		[SerializeField] protected float damageRadius;
		protected int damage;
		[SerializeField] protected LayerMask hitableLayer;
		[SerializeField] protected float lifetime;
		protected bool Initialized { get; private set; }

		public virtual void Initialize(int damage, Transform target, LayerMask hitableLayer)
		{
			this.damage = damage;
			this.hitableLayer |= hitableLayer;
			Destroy(gameObject, lifetime);
			Initialized = true;
		}

		protected void Damage()
		{
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRadius);

			if (hitColliders != null && hitColliders.Length != 0)
			{
				for (int i = 0; i < hitColliders.Length; i++)
				{
					if ((hitableLayer.value & (1 << hitColliders[i].gameObject.layer)) > 0)
					{
						IDamageable damageable = hitColliders[i].GetComponent<IDamageable>();

						if (damageable != null)
							damageable.Damage(damage);

						//TODO: Particle effects
						Destroy(gameObject);
					}
				}
			}
		}

		//Debug
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, damageRadius);
		}
	}

}