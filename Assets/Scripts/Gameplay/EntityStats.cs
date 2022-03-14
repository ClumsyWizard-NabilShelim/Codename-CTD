using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;

public abstract class EntityStats : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	private int currentHealth;

	private void Awake()
	{
		currentHealth = health;
	}

	public void Damage(int amount)
	{
		currentHealth -= amount;

		if(currentHealth <= 0)
			RemoveEntity();
	}

	protected abstract void RemoveEntity();
}