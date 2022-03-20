using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	private int currentHealth;

	[SerializeField] private GameObject deathEffect;

	private void Start()
	{
		currentHealth = health;
	}

	public void Damage(int amount)
	{
		currentHealth -= amount;

		if (currentHealth <= 0)
			RemoveEntity();
	}

	private void RemoveEntity()
	{
		GameObject effect = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
		Destroy(effect, 1.0f);

		GameManager.instance.GameOver();
		Destroy(gameObject);
	}
}
