using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 仓库管理
/// </summary>
public class BarnManager : MonoBehaviour
{
	public Barn barn;

	/// <summary>
	/// 添加物品
	/// </summary>
	/// <param name="plantType"></param>
	/// <param name="addCount"></param>
	/// <returns></returns>
	public bool TryAddItem(PlantType plantType, int addCount)
	{
		return barn.TryAddItem(plantType, addCount);
	}

	#region 升级
	int maxLevel = 200;
	int maxWindmill = 2;
	int maxDryer = 4;
	/// <summary>
	/// 升级
	/// </summary>
	public void LevelUp()
	{
		if (CanLevelUp())
		{
			barn.data.level++;
		}
	}

	/// <summary>
	/// 风车升级
	/// </summary>
	public void WindmillLevelUp()
	{
		if (CanWindmillLevelUp())
		{
			barn.data.windmill++;
		}
	}

	/// <summary>
	/// 烘干机升级
	/// </summary>
	public void DryerLevelUp()
	{
		if (CanDryerLevelUp())
		{
			barn.data.dryer++;
		}
	}

	public bool CanLevelUp()
	{
		return barn.data.level < maxLevel;
	}

	public bool CanWindmillLevelUp()
	{
		return barn.data.windmill < maxWindmill;
	}

	public bool CanDryerLevelUp()
	{
		return barn.data.windmill == maxWindmill && barn.data.dryer < maxDryer;
	}
	#endregion
}
