using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// ���ع���
/// </summary>
public class FieldManager : MonoBehaviour
{
	public List<Field> fields;
	public GameObject fieldOutline;
	public Field selectedField;
	float[] growValue;

	private void Awake()
	{
		fieldOutline.gameObject.SetActive(false);
		growValue = new float[fields.Count];
	}

	private void OnEnable()
	{
		StartCoroutine(nameof(AtuoCheckFieldPlant));
	}

	private void Start()
	{
		foreach (var field in fields)
		{
			field.onRipe = OnPlantRipe;
		}
	}


	/// <summary>
	/// ���ֲ������
	/// </summary>
	/// <returns></returns>
	IEnumerator AtuoCheckFieldPlant()
	{
		var wait = new WaitForSeconds(1f);
		
		while (true)
		{
			CheckFieldPlantGrow();
			yield return wait;
		}
	}

	/// <summary>
	/// ���ֲ������
	/// </summary>
	public void CheckFieldPlantGrow()
	{
		for (int i = 0; i < fields.Count; i++)
		{
			growValue[i] = fields[i].GetPlantGrowValue();
		}
		UiManager.Instance.fieldPanel.SetGrowSlider(growValue);
	}

	/// <summary>
	/// ѡ��ǰ����
	/// </summary>
	/// <param name="field"></param>
	public void SelectField(Field field)
	{
		selectedField = field;
		if (selectedField == null)
		{
			fieldOutline.gameObject.SetActive(false);
			return;
		}
		fieldOutline.transform.SetParent(selectedField.transform);
		fieldOutline.transform.localPosition = Vector3.zero;
		UiManager.Instance.fieldPanel.RefreshPanel();
		fieldOutline.gameObject.SetActive(true);
	}

	/// <summary>
	/// ��������ֲ��
	/// </summary>
	/// <param name="plantType"></param>
	public void SetPlant(PlantType plantType)
	{
		selectedField.SetPlant(plantType);
	}


	void OnPlantRipe(Field field)
	{
		if (selectedField == field)
		{
			UiManager.Instance.fieldPanel.RefreshPanel();
		}
	}
}
