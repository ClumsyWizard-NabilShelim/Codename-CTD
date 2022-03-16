using System.Collections;
using UnityEngine;

public enum CardType
{
	Structure,
	Magic
}

public class CardData : ScriptableObject
{
	[SerializeField] private new string name;

	[SerializeField] private int cost;

	[TextArea(4, 4)]
	[SerializeField] private string description;

	[SerializeField] private Sprite portrait;
	[SerializeField] private CardType type;
	[SerializeField] private GameObject prefab;

	public string Name
	{
		get
		{
			return name;
		}
	}

	public int Cost
	{
		get
		{
			return cost;
		}
	}
	public string Description
	{
		get
		{
			return description;
		}
	}
	public CardType Type
	{
		get
		{
			return type;
		}
	}
	public Sprite Portrait
	{
		get
		{
			return portrait;
		}
	}
	public GameObject Prefab
	{
		get
		{
			return prefab;
		}
	}
}
