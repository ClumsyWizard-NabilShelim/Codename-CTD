using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;
using TMPro;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageable
{
	private bool dead;
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
		if (dead)
			return;

		dead = true;

		AudioManager.PlayAudio("Pop");

		GameObject effect = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
		Destroy(effect, 1.0f);

		EnemyManager.instance.EnemyCount--;
		Destroy(gameObject);
	}
}