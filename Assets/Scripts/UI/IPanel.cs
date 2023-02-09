using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPanel
{
	public abstract void Open();
	public abstract void Close();
	public abstract void RefreshPanel();
}
