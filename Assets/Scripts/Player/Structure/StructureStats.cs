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
		Destroy(gameObject);
	}
}