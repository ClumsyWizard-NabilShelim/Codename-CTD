using System.Collections;
using UnityEngine;
using TMPro;

public class CardStatsContainer : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI amountText;

	public void SetAmount(int amount)
	{
		amountText.text = amount.ToString();
	}
 }