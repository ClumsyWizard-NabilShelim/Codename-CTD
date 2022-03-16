using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ClumsyWizard.Utilities;
using TMPro;

public class StructureStats : EntityStats
{
	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private Image healthBar;

	public void Initialize(StructureData data)
	{
		health = data.Health;
		currentHealth = health;
		UpdateUI();
	}

	public override void Damage(int amount)
	{
		base.Damage(amount);
		UpdateUI();
	}

	private void UpdateUI()
	{
		healthBar.fillAmount = currentHealth / (float)health;
		healthText.text = currentHealth.ToString() + " / " + health.ToString();
	}

	protected override void RemoveEntity()
	{
		Destroy(gameObject);
	}
}