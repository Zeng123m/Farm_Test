using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Sirenix.OdinInspector;

/// <summary>
/// 仓库界面
/// </summary>
public class BarnPanel : MonoBehaviour, IPanel
{
	public int UiItemCount = 4;
	public GameObject barnItemUiPrefab;
	public Transform barnItemUiRoot;
	public GameObject itemDataRoot;
	List<BarnItemUI> barnItemUis = new List<BarnItemUI>();
	BarnItemUI selectedBarnItemUI;
	/// <summary>
	/// 数量滑块
	/// </summary>
	public Slider sellCountSlider;
	public Text sellCountText;
	public Text maxCountText;
	public Button sellButton;
	int sellCount;
	int levelUpNeedCoin = 100;
	PlantSO PlantSO { get { return GameController.Instance.plantSO; } }

	[Title("升级")]
	public Text levelText;
	/// <summary>
	/// 风车
	/// </summary>
	public Text windmillCountText;
	/// <summary>
	/// 烘干机
	/// </summary>
	public Text dryerCountText;
	public Button levelUpButton;
	public Button windmillLevelUpButton;
	public Button dryerLevelUpButton;

	private void Awake()
	{
		for (int i = 0; i < UiItemCount; i++)
		{
			var itemUiobj = Instantiate(barnItemUiPrefab, barnItemUiRoot);
			barnItemUis.Add(itemUiobj.GetComponent<BarnItemUI>());
		}
	}

	private void Start()
	{
		sellCountSlider.onValueChanged.AddListener((value) =>
		{
			sellCount = (int)value;
			sellCountText.text = sellCount.ToString();
		});
		sellButton.onClick.AddListener(OnSellButtonClick);
		levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
		windmillLevelUpButton.onClick.AddListener(OnWindmillLevelUpButtonClick);
		dryerLevelUpButton.onClick.AddListener(OnDryerLevelUpButtonClick);
	}

	/// <summary>
	/// 刷新界面
	/// </summary>
	public void RefreshPanel()
	{
		var items = GameController.Instance.barnManager.barn.items;
		var plantData = PlantSO.data;
		for (int i = 0; i < barnItemUis.Count; i++)
		{
			if (i < items.Count)
			{
				var item = items[i];
				barnItemUis[i].SetItemUI(plantData[item.plantType].plantSprite, item);
			}
			else
			{
				barnItemUis[i].SetItemUI(null, null);
			}
		}
		var data = GameController.Instance.barnManager.barn.data;
		levelText.text = data.level.ToString();
		windmillCountText.text = data.windmill.ToString();
		dryerCountText.text = data.dryer.ToString();
		maxCountText.text = GameController.Instance.barnManager.barn.GetMaxCount().ToString();
		levelUpButton.gameObject.SetActive(GameController.Instance.barnManager.CanLevelUp());
		windmillLevelUpButton.gameObject.SetActive(GameController.Instance.barnManager.CanWindmillLevelUp());
		dryerLevelUpButton.gameObject.SetActive(GameController.Instance.barnManager.CanDryerLevelUp());
		if (selectedBarnItemUI != null)
		{
			var barnItem = selectedBarnItemUI.barnItem;
			if (barnItem != null && barnItem.count > 0)
			{
				itemDataRoot.SetActive(true);
				sellCountSlider.minValue = 0;
				sellCountSlider.maxValue = barnItem.count;
				sellCount = (int)sellCountSlider.value;
				sellCountText.text = sellCount.ToString();
			}
			else
			{
				itemDataRoot.SetActive(false);
			}
		}
	}

	public void Open()
	{
		UiManager.Instance.curPanel.Close();
		UiManager.Instance.curPanel = this;
		gameObject.SetActive(true);
		SelectBarnItemUI(barnItemUis[0]);
		RefreshPanel();
	}

	public void Close()
	{
		UiManager.Instance.curPanel = null;
		gameObject.SetActive(false);
	}

	public void SelectBarnItemUI(BarnItemUI barnItemUI)
	{
		if (selectedBarnItemUI == barnItemUI)
		{
			return;
		}
		if (selectedBarnItemUI != null)
		{
			selectedBarnItemUI.OnSelected(false);
		}
		selectedBarnItemUI = barnItemUI;
		selectedBarnItemUI.OnSelected(true);
		RefreshPanel();
	}

	#region 按键
	/// <summary>
	/// 出售
	/// </summary>
	void OnSellButtonClick()
	{
		var plantType = selectedBarnItemUI.barnItem.plantType;
		GameController.Instance.barnManager.TryAddItem(plantType, -sellCount);
		GameController.Instance.marketManager.AddItem(plantType, sellCount);
		RefreshPanel();
		var data = GameController.Instance.plantSO;
		var plantName = data.data[plantType].plantName;
		UiManager.Instance.AddGameText(string.Format("{0}个{1}转移到超市", sellCount, plantName));
	}

	/// <summary>
	/// 升级
	/// </summary>
	void OnLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.barnManager.barn.data.level + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.barnManager.LevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	/// <summary>
	/// 风车升级
	/// </summary>
	void OnWindmillLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.barnManager.barn.data.windmill + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.barnManager.WindmillLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	/// <summary>
	/// 烘干机升级
	/// </summary>
	void OnDryerLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (GameController.Instance.barnManager.barn.data.dryer + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			GameController.Instance.barnManager.DryerLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}
	#endregion
}
