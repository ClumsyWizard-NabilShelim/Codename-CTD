using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public class FollowProjectile : Projectile
	{
		[SerializeField] private float moveSpeed;
		private Transform target;
		private Rigidbody rb;

		public override void Initialize(int damage, Transform target, LayerMask damageableLayer)
		{
			this.target = target;
			rb = GetComponent<Rigidbody>();
			base.Initialize(damage, target, damageableLayer);
		}

		private void Update()
		{
			if (target == null|| !Initialized)
				return;

			rb.velocity = transform.forward * moveSpeed;
			transform.LookAt(target);
		}

		private void OnTriggerEnter(Collider other)
		{
			if ((hitableLayer.value & (1 << other.gameObject.layer)) > 0)
			{
				Damage();
			}
		}
	}
}