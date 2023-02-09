using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// ֲ����������
/// </summary>
[CreateAssetMenu(fileName = "PlantSO", menuName = "SO/PlantSO")]
public class PlantSO : SerializedScriptableObject
{
	public Dictionary<PlantType, PlantResData> data = new Dictionary<PlantType, PlantResData>();
}

/// <summary>
/// ֲ����Դ����
/// </summary>
[System.Serializable]
public class PlantResData
{
	/// <summary>
	/// ����
	/// </summary>
	public string plantName;
	/// <summary>
	/// ��������ʱ��
	/// </summary>
	public float growTime;
	/// <summary>
	/// ��������ʱ��
	/// </summary>
	public float ripeTime;
	/// <summary>
	/// ���ۼ۸�
	/// </summary>
	public int price;
	/// <summary>
	/// ����ͼ
	/// </summary>
	public Sprite seedSprite;
	/// <summary>
	/// �ɳ�ͼ
	/// </summary>
	public Sprite growSprite;
	/// <summary>
	/// ����ͼ
	/// </summary>
	public Sprite ripeSprite;
	/// <summary>
	/// ��ʵͼ
	/// </summary>
	public Sprite plantSprite;
}
