using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 超市物品
/// </summary>
[System.Serializable]
public class MarketItem
{
	public PlantType plantType;
	public int count;
	public int price;

	public MarketItem(PlantType plantType, int count,int price)
	{
		this.plantType = plantType;
		this.count = count;
		this.price = price;
	}
}
