using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 植物
/// </summary>
public class Plant : MonoBehaviour
{
	/// <summary>
	/// 植物类型
	/// </summary>
	public PlantType plantType;
	PlantState plantState;
	/// <summary>
	/// 生长状态
	/// </summary>
	public PlantState State
	{
		get { return plantState; }
		set
		{
			if (value != plantState)
			{
				plantState = value;
				ChangePlantSprite();
			}
		}
	}
	/// <summary>
	/// 名字
	/// </summary>
	public string plantName;
	/// <summary>
	/// 种植时间
	/// </summary>
	public float startTime;
	SpriteRenderer spriteRenderer;
	public Sprite seedSprite;
	public Sprite growSprite;
	public Sprite ripeSptire;
	public float growTime;
	public float ripeTime;

	private void Start()
	{
		ChangePlantSprite();
	}

	/// <summary>
	/// 改变图片
	/// </summary>
	void ChangePlantSprite()
	{
		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
		switch (State)
		{
			case PlantState.Seed:
				spriteRenderer.sprite = seedSprite;
				break;
			case PlantState.Growing:
				spriteRenderer.sprite = growSprite;
				break;
			case PlantState.ripe:
				spriteRenderer.sprite = ripeSptire;
				break;
			default:
				break;
		}
	}
}

/// <summary>
/// 生长状态
/// </summary>
public enum PlantState
{
	/// <summary>
	/// 种子
	/// </summary>
	Seed,
	/// <summary>
	/// 生长
	/// </summary>
	Growing,
	/// <summary>
	/// 成熟
	/// </summary>
	ripe
}

/// <summary>
/// 植物类型
/// </summary>
public enum PlantType
{
	/// <summary>
	/// 番茄
	/// </summary>
	Tomato,
	/// <summary>
	/// 土豆
	/// </summary>
	Potato,
	/// <summary>
	/// 棉花
	/// </summary>
	Cotton
}
