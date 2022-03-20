using ClumsyWizard.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HandManager))]
public class CardManager : StaticInstance<CardManager>
{
	private HandManager handManager;
	private Card selectedCard;
	private Transform cardPrefabPreview;
	private Node nodeUnderMouse;
	private RaycastHit hit;
	private Ray ray;

	private void Start()
	{
		handManager = GetComponent<HandManager>();
	}

	private void Update()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = Camera.main.transform.position - mousePos;
		Debug.DrawLine(Camera.main.transform.position, direction.normalized * Mathf.Infinity, Color.red, 0.1f);
	}

	public void CardSelected(Card card, GameObject previewPrefab)
	{
		selectedCard = card;
		cardPrefabPreview = Instantiate(previewPrefab).transform;

		SetNodeUnderMouse();

		if (nodeUnderMouse != null)
			cardPrefabPreview.position = new Vector3(nodeUnderMouse.WorldPosition.x, 0.5f, nodeUnderMouse.WorldPosition.z);
	}

	public void CardDragged()
	{
		if (cardPrefabPreview != null)
		{
			SetNodeUnderMouse();

			if (nodeUnderMouse != null && nodeUnderMouse.Empty)
				cardPrefabPreview.position = new Vector3(nodeUnderMouse.WorldPosition.x, 0.5f, nodeUnderMouse.WorldPosition.z);
		}
	}

	private void SetNodeUnderMouse()
	{
		ray = Utility.ShootRayFromMouse();

		if(Physics.Raycast(ray, out hit))
		{
			nodeUnderMouse = GridManager.instance.GetNodeFrom(hit.point);
		}
		else
		{
			nodeUnderMouse = null;
		}
	}

	public void CardDropped(CardData data)
	{
		if (cardPrefabPreview != null && nodeUnderMouse != null && nodeUnderMouse.Empty)
		{
			if (data.Type == CardType.Structure)
			{
				cardPrefabPreview.GetComponent<Structure>().Initialize((StructureData)data);
				nodeUnderMouse.Empty = false;
			}

			handManager.RemoveCard(selectedCard);
		}
		else
		{
			Destroy(cardPrefabPreview.gameObject);
		}
	}
}
