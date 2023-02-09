using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理
/// </summary>
public class UiManager : MonoBehaviour
{
	public static UiManager Instance;
	/// <summary>
	/// 土地界面
	/// </summary>
	public FieldPanel fieldPanel;
	/// <summary>
	/// 仓库界面
	/// </summary>
	public BarnPanel barnPanel;
	/// <summary>
	/// 超市界面
	/// </summary>
	public MarketPanel marketPanel;
	/// <summary>
	/// 玩家信息
	/// </summary>
	public PlayerDataPanel playerDataPanel;
	//当前界面
	public IPanel curPanel;
	public Button fieldButton;
	public Button barnButton;
	public Button marketButton;
	public int lastButtonIndex;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		curPanel = fieldPanel;
		fieldPanel.Open();
		fieldButton.onClick.AddListener(fieldPanel.Open);
		barnButton.onClick.AddListener(barnPanel.Open);
		marketButton.onClick.AddListener(marketPanel.Open);
		playerDataPanel.Open();
	}

	#region GameText
	/// <summary>
	/// 游戏内LOG文字
	/// </summary>
	public Text gameText;
	List<string> gameTextStr = new List<string>();
	StringBuilder stringBuilder = new StringBuilder();
	public int GameTextLine = 9;
	/// <summary>
	/// 添加游戏内文字
	/// </summary>
	/// <param name="text"></param>
	public void AddGameText(string text)
	{
		Debug.Log(text);
		if (gameTextStr.Count >= GameTextLine)
		{
			gameTextStr.RemoveAt(0);
		}
		gameTextStr.Add(text);
		stringBuilder.Clear();
		for (int i = 0; i < gameTextStr.Count; i++)
		{
			if (i == gameTextStr.Count - 1)
			{
				stringBuilder.Append(gameTextStr[i]);
			}
			else
			{
				stringBuilder.AppendLine(gameTextStr[i]);
			}
		}
		gameText.text = stringBuilder.ToString();
	}

	public void ClearGameText()
	{
		gameTextStr.Clear();
		gameText.text = "";
	}
	#endregion

}
