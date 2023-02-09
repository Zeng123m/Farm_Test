using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarnItemUI : MonoBehaviour
{
	public BarnItem barnItem;
	public Image barnImage;
	public Text countText;
	public GameObject select;

	private void Awake()
	{
		SetItemUI(null, null);
		GetComponent<Button>().onClick.AddListener(() => UiManager.Instance.barnPanel.SelectBarnItemUI(this));
	}

	public void SetItemUI(Sprite sprite, BarnItem barnItem)
	{
		this.barnItem = barnItem;
		if (sprite == null || barnItem == null)
		{
			barnImage.enabled = false;
			countText.enabled = false;
		}
		else
		{
			barnImage.enabled = true;
			countText.enabled = true;
			barnImage.sprite = sprite;
			countText.text = barnItem.count.ToString();
		}
	}

	public void OnSelected(bool isSelected)
	{
		select.SetActive(isSelected);
	}

}
