using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Structure Card", menuName = "Card/Structure")]
public class StructureData : CardData
{
	[SerializeField] private int health;
	[SerializeField] private int damage;

	public int Health
	{
		get
		{
			return health;
		}
	}
	public int Damage
	{
		get
		{
			return damage;
		}
	}
}
