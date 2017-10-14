using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通的子弹
/// </summary>
public class NormalAmmo : Ammo {

	/// <summary>
	/// 这个子弹是否已经造成过伤害了，用于防止一颗子弹同时伤害多个敌人的情况
	/// </summary>
	bool attacked;

	public override void Initialize()
	{
		base.Initialize();
		attacked = false;
	}

	protected override void OnTriggerEnter2D(Collider2D collider2D)
	{
		// 如果已经造成过伤害了，退出
		if (attacked) return;
		attacked = true;
		base.OnTriggerEnter2D(collider2D);
	}
}
