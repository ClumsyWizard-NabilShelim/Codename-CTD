using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClumsyWizard.Utilities;

public class HandManager : StaticInstance<HandManager>
{
	[Header("Card")]
	[SerializeField] private RectTransform cardHolder;
	[SerializeField] private RectTransform cardSpawnPoint;
	[SerializeField] private GameObject cardPrefab;
	[SerializeField] private List<CardData> cardDatas;
	private List<Card> cards = new List<Card>();

	[Header("Hand")]
	[SerializeField] private int cardSpacing;
	[SerializeField] private int maxHandSize;
	private int currentHandSize;
	private Vector2 cardSize;

	private void Start()
	{
		DrawCards();
	}

	private void AddCard(CardData data)
	{
		if (currentHandSize >= maxHandSize)
			return;

		Card card = Instantiate(cardPrefab, cardSpawnPoint.position, cardPrefab.transform.rotation, cardHolder).GetComponent<Card>();
		cards.Add(card);
		card.Initialize(data);
		currentHandSize++;
	}

	public void RemoveCard(Card card)
	{
		if (cards.Count == 0)
			return;

		cards.Remove(card);
		Destroy(card.gameObject);
		currentHandSize--;
		FitCards();
	}

	private void FitCards()
	{
		float numberOfCards = cards.Count;
		float widthOfCard = cardSize.x;

		float startingX = 0 - (widthOfCard * ((numberOfCards / 2.0f) - 1)) - (widthOfCard / 2.0f);
		startingX -= cardSpacing;
		for (int i = 0; i < cards.Count; i++)
		{
			Vector2 moveTo = new Vector2((startingX + (i * cardSize.x)) + (i * cardSpacing), cardSize.y / 2.0f);
			cards[i].MoveTo(moveTo);
		}
	}

	public void DrawCards()
	{
		for (int i = 0; i < maxHandSize; i++)
		{
			AddCard(cardDatas[Random.Range(0, cardDatas.Count)]);
		}

		if(cardSize == Vector2.zero)
			cardSize = cards[0].GetComponent<RectTransform>().sizeDelta;

		FitCards();
	}
}
