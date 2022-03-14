using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : EntityStats
{
	protected override void RemoveEntity()
	{
		GameManager.instance.GameOver();
		Destroy(gameObject);
	}
}
