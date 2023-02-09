using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
	public List<Button> buttons;
	Button lastClickButton;
	int buttonIndex;
	public int initClickIndex = 0;

	private void Start()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			var button = buttons[i];
			button.onClick.AddListener(() => OnButtonClick(button));
		}
		if (initClickIndex >= 0)
		{
			OnButtonClick(buttons[initClickIndex]);
		}
	}

	void OnButtonClick(Button button)
	{
		buttonIndex = button.transform.GetSiblingIndex();
		button.gameObject.SetActive(false);
		if (lastClickButton != null)
		{
			lastClickButton.gameObject.SetActive(true);
			lastClickButton.transform.SetSiblingIndex(buttonIndex);
		}
		lastClickButton = button;
	}
}
