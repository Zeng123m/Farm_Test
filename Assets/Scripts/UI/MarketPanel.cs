using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���н���
/// </summary>
public class MarketPanel : MonoBehaviour, IPanel
{
	public int UiItemCount = 4;
	public GameObject barnItemUiPrefab;
	public Transform barnItemUiRoot;
	List<MarketItemUI> marketItemUis = new List<MarketItemUI>();
	PlantSO PlantSO { get { return GameController.Instance.plantSO; } }
	/// <summary>
	/// ������
	/// </summary>
	public Text sellCountText;
	int levelUpNeedCoin = 100;
	[Title("����")]
	public Text levelText;
	/// <summary>
	/// ��ֲ����
	/// </summary>
	public Text plantsCountText;
	/// <summary>
	/// ��������
	/// </summary>
	public Text shelveCountText;
	public Button levelUpButton;
	public Button plantsLevelUpButton;
	public Button shelveLevelUpButton;

	private void Awake()
	{
		for (int i = 0; i < UiItemCount; i++)
		{
			var itemUiobj = Instantiate(barnItemUiPrefab, barnItemUiRoot);
			marketItemUis.Add(itemUiobj.GetComponent<MarketItemUI>());
		}
	}

	private void Start()
	{
		levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
		plantsLevelUpButton.onClick.AddListener(OnPlantsLevelUpButtonClick);
		shelveLevelUpButton.onClick.AddListener(OnShelveLevelUpButtonClick);
	}

	public void Open()
	{
		UiManager.Instance.curPanel.Close();
		UiManager.Instance.curPanel = this;
		gameObject.SetActive(true);
		RefreshPanel();
		GameController.Instance.marketManager.Show();
	}

	public void Close()
	{
		UiManager.Instance.curPanel = null;
		gameObject.SetActive(false);
		GameController.Instance.marketManager.Hide();
	}

	/// <summary>
	/// ˢ�½���
	/// </summary>
	public void RefreshPanel()
	{
		var items = GameController.Instance.marketManager.market.items;
		var plantData = PlantSO.data;
		for (int i = 0; i < marketItemUis.Count; i++)
		{
			if (i < items.Count)
			{
				var item = items[i];
				marketItemUis[i].SetItemUI(plantData[item.plantType].plantSprite, item);
			}
			else
			{
				marketItemUis[i].SetItemUI(null, null);
			}
		}
		var data = GameController.Instance.marketManager.market.data;
		levelText.text = data.level.ToString();
		plantsCountText.text = data.plants.ToString();
		shelveCountText.text = data.shelve.ToString();
		sellCountText.text = GameController.Instance.marketManager.market.GetSellCount().ToString();
		levelUpButton.gameObject.SetActive(GameController.Instance.marketManager.CanLevelUp());
		plantsLevelUpButton.gameObject.SetActive(GameController.Instance.marketManager.CanPlantsLevelUp());
		shelveLevelUpButton.gameObject.SetActive(GameController.Instance.marketManager.CanShelveLevelUp());
	}

	#region ����
	/// <summary>
	/// ����
	/// </summary>
	void OnLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.marketManager.market.data.level + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.marketManager.LevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("��Ҳ���({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	/// <summary>
	/// ��ֲ����
	/// </summary>
	void OnPlantsLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.marketManager.market.data.plants + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.marketManager.PlantsLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("��Ҳ���({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	/// <summary>
	/// ��������
	/// </summary>
	void OnShelveLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.marketManager.market.data.shelve + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.marketManager.ShelveLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("��Ҳ���({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}
	#endregion
}
