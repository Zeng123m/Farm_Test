using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 植物配置数据
/// </summary>
[CreateAssetMenu(fileName = "PlantSO", menuName = "SO/PlantSO")]
public class PlantSO : SerializedScriptableObject
{
	public Dictionary<PlantType, PlantResData> data = new Dictionary<PlantType, PlantResData>();
}

/// <summary>
/// 植物资源数据
/// </summary>
[System.Serializable]
public class PlantResData
{
	/// <summary>
	/// 名字
	/// </summary>
	public string plantName;
	/// <summary>
	/// 生长所需时间
	/// </summary>
	public float growTime;
	/// <summary>
	/// 成熟所需时间
	/// </summary>
	public float ripeTime;
	/// <summary>
	/// 出售价格
	/// </summary>
	public int price;
	/// <summary>
	/// 种子图
	/// </summary>
	public Sprite seedSprite;
	/// <summary>
	/// 成长图
	/// </summary>
	public Sprite growSprite;
	/// <summary>
	/// 成熟图
	/// </summary>
	public Sprite ripeSprite;
	/// <summary>
	/// 果实图
	/// </summary>
	public Sprite plantSprite;
}
