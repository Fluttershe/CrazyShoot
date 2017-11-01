using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 抽象单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingleBehaviour<T> : MonoBehaviour where T : SingleBehaviour<T>
{
	private static T instance;

	/// <summary>
	/// 实例property，会自动创建实例
	/// </summary>
	protected static T Instance
	{
		get
		{
			if (instance == null)
				CreateInstance();
			return instance;
		}

		set
		{
			if (instance == null)
				CreateInstance();
			instance = value;
		}
	}

	/// <summary>
	/// 创建实例（如果还未创建的话）
	/// </summary>
	protected static void CreateInstance()
	{
		if (instance != null) return;

		instance = new GameObject("SingleBehaviours").AddComponent<T>();
	}

	protected virtual void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this);
			return;
		}

		instance = (T)this;
	}
}
