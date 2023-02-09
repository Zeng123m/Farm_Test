using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// ��Ϸ�ܿ���
/// </summary>
public class GameController : MonoBehaviour
{
	public static GameController Instance;
	/// <summary>
	/// ���ع���
	/// </summary>
	public FieldManager fieldManager;
	/// <summary>
	/// �ֿ����
	/// </summary>
	public BarnManager barnManager;
	/// <summary>
	/// ���й���
	/// </summary>
	public MarketManager marketManager;
	/// <summary>
	/// �����Ϣ
	/// </summary>
	public PlayerData playerData;
	/// <summary>
	/// ֲ����������
	/// </summary>
	public PlantSO plantSO;

	private void Awake()
	{
		Instance = this;
		plantSO = GetPlantSO();
	}

	/// <summary>
	/// ��ȡֲ����������
	/// </summary>
	/// <returns></returns>
	PlantSO GetPlantSO()
	{
		var handle = Addressables.LoadAssetAsync<PlantSO>("PlantSO");
		handle.WaitForCompletion();
		return handle.Result;
	}

	/// <summary>
	/// �ӽ��
	/// </summary>
	/// <param name="addCount"></param>
	public void AddCoin(int addCount)
	{
		playerData.coin += addCount;
		if(playerData.coin < 0)
		{
			playerData.coin = 0;
		}
		UiManager.Instance.playerDataPanel.RefreshPanel();
	}
}
