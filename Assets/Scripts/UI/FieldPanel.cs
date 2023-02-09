using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Sirenix.OdinInspector;

/// <summary>
/// 土地界面
/// </summary>
public class FieldPanel : MonoBehaviour, IPanel
{
	FieldManager FieldManager { get { return GameController.Instance.fieldManager; } }
	PlantSO PlantSO { get { return GameController.Instance.plantSO; } }
	public GameObject plantButtonPrefab;
	public Transform plantButtonRoot;
	public Transform panelRoot;
	public Button removeButton;
	public Button closeButton;
	public Button reapButton;
	public List<Slider> growSliders;
	int levelUpNeedCoin = 100;
	public Text indexText;
	[Title("升级")]
	public Text levelText;
	public Text scarecrowCountText;
	public Text sprinklerCountText;
	public Button levelUpButton;
	public Button scarecrowLevelUpButton;
	public Button sprinklerLevelUpButton;

	private void Start()
	{
		var data = PlantSO.data;
		foreach (var item in data)
		{
			var button = Instantiate(plantButtonPrefab, plantButtonRoot).GetComponent<Button>();
			button.transform.GetChild(0).GetComponent<Image>().sprite = item.Value.plantSprite;
			button.onClick.AddListener(() =>
			{
				FieldManager.SetPlant(item.Key);
				RefreshPanel();
			});
		}
		removeButton.onClick.AddListener(OnRemoveButtonClick);
		closeButton.onClick.AddListener(OnCloseButtonClick);
		reapButton.onClick.AddListener(OnReapButtonClick);
		levelUpButton.onClick.AddListener(OnLevelUpButtonClick);
		scarecrowLevelUpButton.onClick.AddListener(OnScarecrowLevelUpButtonClick);
		sprinklerLevelUpButton.onClick.AddListener(OnSprinklerLevelUpButtonClick);
	}

	public void Open()
	{
		UiManager.Instance.curPanel.Close();
		UiManager.Instance.curPanel = this;
		gameObject.SetActive(true);
		FieldManager.gameObject.SetActive(true);
		RefreshPanel();
	}

	public void Close()
	{
		UiManager.Instance.curPanel = null;
		gameObject.SetActive(false);
		FieldManager.gameObject.SetActive(false);
	}

	/// <summary>
	/// 刷新界面信息
	/// </summary>
	public void RefreshPanel()
	{
		var field = FieldManager.selectedField;
		if (field != null)
		{
			panelRoot.gameObject.SetActive(true);
			int index = field.index;
			var data = field.fieldData;
			indexText.text = index.ToString();
			levelText.text = data.level.ToString();
			scarecrowCountText.text = data.scarecrow.ToString();
			sprinklerCountText.text = data.sprinkler.ToString();
			levelUpButton.gameObject.SetActive(field.CanLevelUp());
			scarecrowLevelUpButton.gameObject.SetActive(field.CanScarecrowLevelUp());
			sprinklerLevelUpButton.gameObject.SetActive(field.CanSprinklerLevelUp());
			reapButton.gameObject.SetActive(field.plant != null && field.plant.State == PlantState.ripe);
			removeButton.gameObject.SetActive(field.plant != null && field.plant.State != PlantState.ripe);
			closeButton.gameObject.SetActive(true);
			if (field.plant == null)
			{
				plantButtonRoot.gameObject.SetActive(true);
			}
			else
			{
				plantButtonRoot.gameObject.SetActive(false);
			}
		}
		else
		{
			panelRoot.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 设置植物生长进度条
	/// </summary>
	/// <param name="values"></param>
	public void SetGrowSlider(float[] values)
	{
		for (int i = 0; i < growSliders.Count; i++)
		{
			var slider = growSliders[i];
			var value = values[i];
			slider.gameObject.SetActive(value >= 0 && value != 1);
			if (value < 0)
			{
				value = 0;
			}
			slider.value = value;
		}
	}

	#region 按键

	void OnCloseButtonClick()
	{
		FieldManager.SelectField(null);
		panelRoot.gameObject.SetActive(false);
	}

	void OnRemoveButtonClick()
	{
		FieldManager.selectedField.ClearPlant();
		FieldManager.CheckFieldPlantGrow();
		FieldManager.SelectField(null);
		RefreshPanel();
	}

	void OnReapButtonClick()
	{
		FieldManager.selectedField.Reap();
		RefreshPanel();
	}

	void OnLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (FieldManager.selectedField.fieldData.level + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			FieldManager.selectedField.LevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	void OnScarecrowLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (FieldManager.selectedField.fieldData.scarecrow + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			FieldManager.selectedField.ScarecrowLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	void OnSprinklerLevelUpButtonClick()
	{
		int needCoin = levelUpNeedCoin * (FieldManager.selectedField.fieldData.sprinkler + 1);
		if (GameController.Instance.playerData.coin >= needCoin)
		{
			GameController.Instance.AddCoin(-needCoin);
			FieldManager.selectedField.SprinklerLevelUp();
			RefreshPanel();
		}
		else
		{
			UiManager.Instance.AddGameText(string.Format("金币不足({0}/{1})", GameController.Instance.playerData.coin.ToString(), needCoin.ToString()));
		}
	}

	#endregion

}
