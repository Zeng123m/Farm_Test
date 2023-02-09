using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仓库
/// </summary>
[System.Serializable]
public class Barn
{
	public List<BarnItem> items;
	public BarnData data;

	/// <summary>
	/// 获取最大容积
	/// </summary>
	/// <returns></returns>
	public int GetMaxCount()
	{
		return Mathf.FloorToInt(data.level * 10 * (1 + data.level / 20) * (1 + 0.05f * (data.windmill + data.dryer)));
	}

	/// <summary>
	/// 添加物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="addCount"></param>
	public bool TryAddItem(PlantType plantType, int addCount)
	{
		if (addCount == 0)
		{
			return true;
		}
		if (addCount < 0)
		{
			return TryRemoveItem(plantType, -addCount);
		}
		int maxCount = GetMaxCount();
		if (addCount > maxCount)
		{
			return false;
		}
		int itemCount = items.Count;
		if (itemCount == 0)
		{
			items.Add(new BarnItem(plantType, addCount));
		}
		else
		{
			bool hasItem = false;
			for (int i = 0; i < itemCount; i++)
			{
				if (items[i].plantType == plantType)
				{
					if(items[i].count+ addCount > maxCount)
					{
						return false;
					}
					items[i].count += addCount;
					hasItem = true;
					break;
				}
			}
			if (!hasItem)
			{
				items.Add(new BarnItem(plantType, addCount));
			}
		}
		return true;
	}

	/// <summary>
	/// 移除物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="removeCount"></param>
	/// <param name="isNotEnoughSuccess"></param>
	/// <returns></returns>
	bool TryRemoveItem(PlantType plantType, int removeCount, bool isNotEnoughSuccess = false)
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
