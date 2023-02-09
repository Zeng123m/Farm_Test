using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// ����
/// </summary>
public class Field : MonoBehaviour
{
	public FieldData fieldData;
	public Plant plant;
	public int index;
	public UnityAction<Field> onRipe;

	/// <summary>
	/// ��ȡֲ��������ֵ
	/// </summary>
	/// <returns></returns>
	public float GetPlantGrowValue()
	{
		if (plant == null)
		{
			return -1f;
		}
		if (plant.State == PlantState.ripe)
		{
			return 1f;
		}
		if (Time.time <= plant.startTime + plant.growTime)
		{
			plant.State = PlantState.Seed;
			return 1f - (plant.startTime + plant.growTime - Time.time) / plant.growTime;
		}
		else if (Time.time <= plant.startTime + plant.growTime + plant.ripeTime)
		{
			plant.State = PlantState.Growing;
			return 1f - (plant.startTime + plant.growTime + plant.ripeTime - Time.time) / plant.ripeTime;
		}
		else
		{
			plant.State = PlantState.ripe;
			onRipe?.Invoke(this);
			return 1f;
		}
	}

	/// <summary>
	/// ����ֲ��
	/// </summary>
	/// <param name="plantType"></param>
	public void SetPlant(PlantType plantType)
	{
		var handle = Addressables.InstantiateAsync("PlantPrefab", transform.position, transform.rotation, transform);
		handle.WaitForCompletion();
		var plantObj = handle.Result;
		plant = plantObj.AddComponent<Plant>();
		plant.startTime = Time.time;
		var data = GameController.Instance.plantSO.data[plantType];
		plant.seedSprite = data.seedSprite;
		plant.growSprite = data.growSprite;
		plant.ripeSptire = data.ripeSprite;
		plant.growTime = data.growTime;
		plant.ripeTime = data.ripeTime;
		plant.plantName = data.plantName;
		plant.plantType = plantType;
		plant.State = PlantState.Seed;
	}

	/// <summary>
	/// ���ֲ��
	/// </summary>
	public void ClearPlant()
	{
		if (plant == null)
		{
			return;
		}
		Addressables.ReleaseInstance(plant.gameObject);
		plant = null;
	}

	/// <summary>
	/// �����ع���
	/// </summary>
	private void OnMouseUpAsButton()
	{
		//if (plant != null && plant.State == PlantState.ripe)
		//{
		//	Reap();
		//}
		GameController.Instance.fieldManager.SelectField(this);
	}

	/// <summary>
	/// �ո�
	/// </summary>
	public void Reap()
	{
		if (plant == null)
		{
			return;
		}
		int resCount = GetResCount();
		bool isSuccess = GameController.Instance.barnManager.TryAddItem(plant.plantType, resCount);
		if (isSuccess)
		{
			UiManager.Instance.AddGameText(string.Format("�ջ�{0}: {1}", plant.plantName, resCount.ToString()));
			ClearPlant();
		}
		else
		{
			UiManager.Instance.AddGameText("�ֿ�Ų�����");
		}
	}

	/// <summary>
	/// ��ȡ�ո���Դ��
	/// </summary>
	/// <returns></returns>
	int GetResCount()
	{
		return Mathf.FloorToInt(fieldData.level * 5 * (1 + fieldData.level / 20) * (1 + 0.05f * (fieldData.scarecrow + fieldData.sprinkler)));
	}

	int maxLevel = 200;
	int maxScarecrow = 2;
	int maxSprinkler = 4;
	/// <summary>
	/// ����
	/// </summary>
	public void LevelUp()
	{
		if (CanLevelUp())
		{
			fieldData.level++;
		}
	}

	/// <summary>
	/// ����������
	/// </summary>
	public void ScarecrowLevelUp()
	{
		if (CanScarecrowLevelUp())
		{
			fieldData.scarecrow++;
		}
	}

	/// <summary>
	/// ��ˮ������
	/// </summary>
	public void SprinklerLevelUp()
	{
		if (CanSprinklerLevelUp())
		{
			fieldData.sprinkler++;
		}
	}

	public bool CanLevelUp()
	{
		return fieldData.level < maxLevel;
	}

	public bool CanScarecrowLevelUp()
	{
		return fieldData.scarecrow < maxScarecrow;
	}

	public bool CanSprinklerLevelUp()
	{
		return fieldData.scarecrow == maxScarecrow && fieldData.sprinkler < maxSprinkler;
	}
}
