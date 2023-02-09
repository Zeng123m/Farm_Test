using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 加载场景
/// </summary>
public class LoadScene : MonoBehaviour
{
    public string sceneName;
	public bool isAuto;

	private void Start()
	{
		if (isAuto)
		{
			Load();
		}
	}

	public void Load()
	{
		Addressables.LoadSceneAsync(sceneName);
	}
}
