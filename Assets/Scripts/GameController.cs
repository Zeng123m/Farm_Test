using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 游戏总控制
/// </summary>
public class GameController : MonoBehaviour
{
	public static GameController Instance;
	/// <summary>
	/// 土地管理
	/// </summary>
	public FieldManager fieldManager;
	/// <summary>
	/// 仓库管理
	/// </summary>
	public BarnManager barnManager;
	/// <summary>
	/// 超市管理
	/// </summary>
	public MarketManager marketManager;
	/// <summary>
	/// 玩家信息
	/// </summary>
	public PlayerData playerData;
	/// <summary>
	/// 植物配置数据
	/// </summary>
	public PlantSO plantSO;

	private void Awake()
	{
		Instance = this;
		plantSO = GetPlantSO();
	}

	/// <summary>
	/// 获取植物配置数据
	/// </summary>
	/// <returns></returns>
	PlantSO GetPlantSO()
	{
		var handle = Addressables.LoadAssetAsync<PlantSO>("PlantSO");
		handle.WaitForCompletion();
		return handle.Result;
	}

	/// <summary>
	/// 加金币
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
