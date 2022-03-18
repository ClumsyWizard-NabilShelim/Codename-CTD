using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour, IDamageable
{
	[SerializeField] private int health;
	private int currentHealth;

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
		GameManager.instance.GameOver();
		Destroy(gameObject);
	}
}
