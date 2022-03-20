using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ClumsyWizard.Utilities;
using TMPro;

public class StructureStats : MonoBehaviour, IDamageable
{
	private int health;
	private int currentHealth;

	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private Image healthBar;

	[SerializeField] private GameObject deathEffect;

	public void Initialize(StructureData data)
	{
		health = data.Health;
		currentHealth = health;
		UpdateUI();
	}

	public void Damage(int amount)
	{
		currentHealth -= amount;
		UpdateUI();
		AudioManager.PlayAudio("StructureHit");
		if (currentHealth <= 0)
			RemoveEntity();
	}

	private void UpdateUI()
	{
		healthBar.fillAmount = currentHealth / (float)health;
		healthText.text = currentHealth.ToString() + " / " + health.ToString();
	}

	private void RemoveEntity()
	{
		GridManager.instance.GetNodeFrom(transform.position).Empty = true;
		CameraShake.instance.ShakeObject(ShakeDuration.Small, ShakeMagnitude.Large);
		AudioManager.PlayAudio("Explosion");
		GameObject effect = Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
		Destroy(effect, 1.0f);
		Destroy(gameObject);
	}
}