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
    private Vector2 moveTo;
    private bool move;
    [SerializeField] private float moveSpeed = 20.0f;

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
        CardManager.instance.CardSelected(this, data.Prefab);
    }

	public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.instance.CardDropped(data);
    }

    public void MoveTo(Vector2 moveTo)
	{
        this.moveTo = moveTo;
        move = true;
	}

	private void Update()
	{
		if(move)
		{
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, moveTo, moveSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.localPosition, moveTo) <= 0.25f)
			{
                move = false;
			}
		}
	}

	//Helper
	private void AddStatContainer(int amount)
	{
        Instantiate(statsContainerPrefab, statsHolder).GetComponent<CardStatsContainer>().SetAmount(amount);
    }
}
