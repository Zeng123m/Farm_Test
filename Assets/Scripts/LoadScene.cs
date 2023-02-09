using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// º”‘ÿ≥°æ∞
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
