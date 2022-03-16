using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private CardData data;
    private new RectTransform transform;
    [SerializeField] private GameObject statsContainerPrefab;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Transform statsHolder;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image portrait;

	public void Initialize(CardData data)
	{
        transform = GetComponent<RectTransform>();
        this.data = data;
        nameText.text = data.Name;

        AddStatContainer(data.Cost);
        
        if(data.Type == CardType.Structure)
		{
            AddStatContainer(((StructureData)data).Health);
            AddStatContainer(((StructureData)data).Damage);
        }

        portrait.sprite = data.Portrait;
        descriptionText.text = data.Description;
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardManager.instance.CardDragged();
    }

	public void OnPointerDown(PointerEventData eventData)
	{
        CardManager.instance.CardSelected(transform, data.Prefab);
    }

	public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.instance.CardDropped(data);
    }

    //Helper
    private void AddStatContainer(int amount)
	{
        Instantiate(statsContainerPrefab, statsHolder).GetComponent<CardStatsContainer>().SetAmount(amount);
    }
}
