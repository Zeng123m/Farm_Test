using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 超市
/// </summary>
[System.Serializable]
public class Market
{
	public List<MarketItem> items;
	public MarketData data;

	/// <summary>
	/// 获取售出数量
	/// </summary>
	/// <returns></returns>
	public int GetSellCount()
	{
		return Mathf.FloorToInt(data.level * 2 * (1 + data.level / 20) * (1 + 0.05f * (data.plants + data.shelve)));
	}

	/// <summary>
	/// 添加物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="addCount"></param>
	public void AddItem(PlantType plantType, int addCount, int price)
	{
		if (addCount == 0)
		{
			return;
		}
		if (addCount < 0)
		{
			RemoveItem(plantType, -addCount);
			return;
		}
		int itemCount = items.Count;
		if (itemCount == 0)
		{
			items.Add(new MarketItem(plantType, addCount, price));
		}
		else
		{
			bool hasItem = false;
			for (int i = 0; i < itemCount; i++)
			{
				if (items[i].plantType == plantType)
				{
					items[i].count += addCount;
					hasItem = true;
					break;
				}
			}
			if (!hasItem)
			{
				items.Add(new MarketItem(plantType, addCount, price));
			}
		}
	}

	/// <summary>
	/// 移除物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="removeCount"></param>
	/// <param name="isNotEnoughSuccess"></param>
	/// <returns></returns>
	public bool RemoveItem(PlantType plantType, int removeCount, bool isNotEnoughSuccess = false)
	{
		int itemCount = items.Count;
		if (itemCount == 0)
		{
			return false;
		}
		else
		{
			for (int i = 0; i < itemCount; i++)
			{
				if (items[i].plantType == plantType)
				{
					int hasCount = items[i].count;
					if (hasCount >= removeCount)
					{
						hasCount -= removeCount;
						if (hasCount != 0)
						{
							items[i].count = hasCount;
						}
						else
						{
							items.RemoveAt(i);
						}
						return true;
					}
					else
					{
						if (isNotEnoughSuccess)
						{
							items.RemoveAt(i);
							return true;
						}
						return false;
					}
				}
			}
			return false;
		}
	}
}
