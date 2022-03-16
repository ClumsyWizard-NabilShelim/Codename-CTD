using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
	[Header("Card")]
	[SerializeField] private RectTransform cardHolder;
	[SerializeField] private GameObject cardPrefab;
	[SerializeField] private List<CardData> cardDatas;
	private List<RectTransform> cards = new List<RectTransform>();

	[Header("Hand")]
	[SerializeField] private int cardSpacing;
	[SerializeField] private int maxHandSize;
	private int currentHandSize;
	[SerializeField] private int startingHandSize;
	private Vector2 cardSize;

	private void Start()
	{
		for (int i = 0; i < startingHandSize; i++)
		{
			AddCard(cardDatas[Random.Range(0, cardDatas.Count)]);
		}

		currentHandSize = 0;

		cardSize = cards[0].sizeDelta;
		FitCards();
	}

	private void AddCard(CardData data)
	{
		if (currentHandSize >= maxHandSize)
			return;

		Card card = Instantiate(cardPrefab, cardHolder).GetComponent<Card>();
		cards.Add(card.GetComponent<RectTransform>());
		card.Initialize(data);
		currentHandSize++;
	}

	public void RemoveCard(RectTransform transform)
	{
		if (cards.Count == 0)
			return;

		cards.Remove(transform);
		Destroy(transform.gameObject);
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
			cards[i].localPosition = moveTo;
		}
	}
}
