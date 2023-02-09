using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ֲ��
/// </summary>
public class Plant : MonoBehaviour
{
	/// <summary>
	/// ֲ������
	/// </summary>
	public PlantType plantType;
	PlantState plantState;
	/// <summary>
	/// ����״̬
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
	/// ����
	/// </summary>
	public string plantName;
	/// <summary>
	/// ��ֲʱ��
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
	/// �ı�ͼƬ
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
/// ����״̬
/// </summary>
public enum PlantState
{
	/// <summary>
	/// ����
	/// </summary>
	Seed,
	/// <summary>
	/// ����
	/// </summary>
	Growing,
	/// <summary>
	/// ����
	/// </summary>
	ripe
}

/// <summary>
/// ֲ������
/// </summary>
public enum PlantType
{
	/// <summary>
	/// ����
	/// </summary>
	Tomato,
	/// <summary>
	/// ����
	/// </summary>
	Potato,
	/// <summary>
	/// �޻�
	/// </summary>
	Cotton
}
