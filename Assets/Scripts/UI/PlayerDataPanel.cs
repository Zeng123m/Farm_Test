using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Íæ¼ÒÐÅÏ¢
/// </summary>
public class PlayerDataPanel : MonoBehaviour, IPanel
{
	PlayerData PlayerData { get { return GameController.Instance.playerData; } }
	public Text coinText;
	public Button addCoinButton;

	private void Start()
	{
		addCoinButton.onClick.AddListener(() =>
		{
			GameController.Instance.AddCoin(100);
		});
	}

	public void Open()
	{
		gameObject.SetActive(true);
		RefreshPanel();
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void RefreshPanel()
	{
		coinText.text = PlayerData.coin.ToString();
	}
}
