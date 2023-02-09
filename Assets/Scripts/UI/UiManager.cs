using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI����
/// </summary>
public class UiManager : MonoBehaviour
{
	public static UiManager Instance;
	/// <summary>
	/// ���ؽ���
	/// </summary>
	public FieldPanel fieldPanel;
	/// <summary>
	/// �ֿ����
	/// </summary>
	public BarnPanel barnPanel;
	/// <summary>
	/// ���н���
	/// </summary>
	public MarketPanel marketPanel;
	/// <summary>
	/// �����Ϣ
	/// </summary>
	public PlayerDataPanel playerDataPanel;
	//��ǰ����
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
	/// ��Ϸ��LOG����
	/// </summary>
	public Text gameText;
	List<string> gameTextStr = new List<string>();
	StringBuilder stringBuilder = new StringBuilder();
	public int GameTextLine = 9;
	/// <summary>
	/// �����Ϸ������
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
