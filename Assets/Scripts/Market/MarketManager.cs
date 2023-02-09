using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 超市管理
/// </summary>
public class MarketManager : MonoBehaviour
{
	public Market market;
	public Transform customRoot;
	public GameObject customerPrefab;
	GameObject customer;

	private void OnEnable()
	{
		StartCoroutine(nameof(AutoPurchase));
	}

	/// <summary>
	/// 定时售出
	/// </summary>
	/// <returns></returns>
	IEnumerator AutoPurchase()
	{
		if (customer == null)
		{
			customer = Instantiate(customerPrefab, Vector3.right * 20, Quaternion.identity, customRoot);
		}
		var wait = new WaitForSeconds(8f);
		var waitBuy = new WaitForSeconds(1f);
		while (true)
		{
			yield return wait;
			customer.transform.position = Vector3.right * 20;
			customer.transform.DOMoveX(0, 1f);
			yield return waitBuy;
			RandomPurchase();
			yield return waitBuy;
			customer.transform.DOMoveX(-20, 1f);
		}
	}

	/// <summary>
	/// 随机售出
	/// </summary>
	void RandomPurchase()
	{
		if (market.items.Count == 0)
		{
			return;
		}
		var item = market.items[Random.Range(0, market.items.Count)];
		int sellCount = Mathf.Min(item.count, market.GetSellCount());
		int addCoin = sellCount * item.price;
		GameController.Instance.AddCoin(addCoin);
		AddItem(item.plantType, -sellCount);
		var plantOS = GameController.Instance.plantSO;
		string itemName = plantOS.data[item.plantType].plantName;
		UiManager.Instance.AddGameText(string.Format("自动售出{0}{1}个，收入{2}", itemName, sellCount.ToString(), addCoin.ToString()));
		UiManager.Instance.marketPanel.RefreshPanel();
	}

	/// <summary>
	/// 增加物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="addCount"></param>
	public void AddItem(PlantType plantType, int addCount)
	{
		int price = GameController.Instance.plantSO.data[plantType].price;
		market.AddItem(plantType, addCount, price);
	}

	public void Show()
	{
		customRoot.gameObject.SetActive(true);
	}

	public void Hide()
	{
		customRoot.gameObject.SetActive(false);
	}

	#region 升级
	int maxLevel = 200;
	int maxPlants = 2;
	int maxShelve = 4;
	/// <summary>
	/// 升级
	/// </summary>
	public void LevelUp()
	{
		if (CanLevelUp())
		{
			market.data.level++;
		}
	}

	/// <summary>
	/// 绿植升级
	/// </summary>
	public void PlantsLevelUp()
	{
		if (CanPlantsLevelUp())
		{
			market.data.plants++;
		}
	}

	/// <summary>
	/// 烘干机升级
	/// </summary>
	public void ShelveLevelUp()
	{
		if (CanShelveLevelUp())
		{
			market.data.shelve++;
		}
	}

	public bool CanLevelUp()
	{
		return market.data.level < maxLevel;
	}

	public bool CanPlantsLevelUp()
	{
		return market.data.plants < maxPlants;
	}

	public bool CanShelveLevelUp()
	{
		return market.data.plants == maxPlants && market.data.shelve < maxShelve;
	}
	#endregion
}
