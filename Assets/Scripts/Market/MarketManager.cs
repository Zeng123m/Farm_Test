using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ���й���
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
	/// ��ʱ�۳�
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
	/// ����۳�
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
		UiManager.Instance.AddGameText(string.Format("�Զ��۳�{0}{1}��������{2}", itemName, sellCount.ToString(), addCoin.ToString()));
		UiManager.Instance.marketPanel.RefreshPanel();
	}

	/// <summary>
	/// ������Ʒ
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

	#region ����
	int maxLevel = 200;
	int maxPlants = 2;
	int maxShelve = 4;
	/// <summary>
	/// ����
	/// </summary>
	public void LevelUp()
	{
		if (CanLevelUp())
		{
			market.data.level++;
		}
	}

	/// <summary>
	/// ��ֲ����
	/// </summary>
	public void PlantsLevelUp()
	{
		if (CanPlantsLevelUp())
		{
			market.data.plants++;
		}
	}

	/// <summary>
	/// ��ɻ�����
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
