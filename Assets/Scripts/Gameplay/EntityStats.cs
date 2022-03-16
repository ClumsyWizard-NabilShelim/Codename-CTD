using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;

public abstract class EntityStats : MonoBehaviour, IDamageable
{
	[SerializeField] protected int health;
	protected int currentHealth;

	private void Awake()
	{
		currentHealth = health;
	}

	public virtual void Damage(int amount)
	{
		currentHealth -= amount;

		if(currentHealth <= 0)
			RemoveEntity();
	}

	protected abstract void RemoveEntity();
}