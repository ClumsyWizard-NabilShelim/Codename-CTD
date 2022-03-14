using System.Collections;
using UnityEngine;
using ClumsyWizard.Utilities;
using TMPro;
using UnityEngine.UI;

public class EnemyStats : EntityStats
{ 
	protected override void RemoveEntity()
	{
		EnemyManager.instance.EnemyCount--;
		Destroy(gameObject);
	}
}