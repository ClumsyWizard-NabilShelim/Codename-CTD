using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClumsyWizard.Utilities
{
	public class NonPhysicsProjectile : Projectile
	{
		[SerializeField] private float moveSpeed;

		public override void Initialize(int damage, Transform target, LayerMask damageableLayer)
		{
			base.Initialize(damage, target, damageableLayer);

			GetComponent<Rigidbody>().AddForce(moveSpeed * transform.forward, ForceMode.Impulse);
		}

		private void Update()
		{
			Damage();
		}
	}
}