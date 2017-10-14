using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : SpawnableObject {

	private static Character _player;

	[SerializeField]
    private GameObject BaoZha;

	[SerializeField]
	private Slider healthBar;

    public static bool ZhenDong;

    /// <summary>
    /// 玩家对象
    /// </summary>
    public static Character Player {
		get {
			// 如果没有缓存有玩家对象
			if (_player == null) {
				// 找出所有Character对象
				Character[] chars = FindObjectsOfType<Character>();
				
				// 在这些Character对象中寻找Player
				foreach (Character c in chars) {
					if (c.tag == "Player") {
						_player = c;
						break;
					}
				}

				// 如果找不到，报告错误
				if (_player == null)
					Debug.LogError("无法找到玩家对象");
			}

			return _player;
		}
	}

	/// <summary>
	/// 该角色的生命值
	/// </summary>
	[SerializeField]
	protected StateValue health;

	/// <summary>
	/// 承受伤害
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(float damage) {

		health.CurrentValue -= damage;

		if (healthBar != null)
			healthBar.value = health.CurrentValue / health.BaseValue;

		if (health.CurrentValue <= 0) {
			ObjectPool.Acquire(BaoZha).transform.position = transform.position;
            ZhenDong = true;
            ReleaseSelf();
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		health.ResetCurrentValue();
		healthBar.value = health.CurrentValue / health.BaseValue;
	}
}
