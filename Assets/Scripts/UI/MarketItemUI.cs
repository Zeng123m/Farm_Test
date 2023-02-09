using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketItemUI : MonoBehaviour
{
	public MarketItem marketItem;
	public GameObject root;
	public Image barnImage;
	public Text countText;
	public Text priceText;

	private void Awake()
	{
		SetItemUI(null, null);
	}

	public void SetItemUI(Sprite sprite, MarketItem marketItem)
	{
		this.marketItem = marketItem;
		if (sprite == null || marketItem == null)
		{
			root.SetActive(false);
		}
		else
		{
			barnImage.sprite = sprite;
			root.SetActive(true);
			countText.text = marketItem.count.ToString();
			priceText.text = marketItem.price.ToString();
		}
	}

}
