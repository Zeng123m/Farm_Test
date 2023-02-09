using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仓库物品
/// </summary>
[System.Serializable]
public class BarnItem
{
    public PlantType plantType;
    public int count;

    public BarnItem(PlantType plantType, int count)
	{
		this.plantType = plantType;
		this.count = count;
	}
}
