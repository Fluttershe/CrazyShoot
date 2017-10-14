using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Character {

	/// <summary>
	/// 该敌人的移动速度
	/// </summary>
	[SerializeField]
	protected float speed = 3;

	/// <summary>
	/// 冷却槽
	/// </summary>
	[SerializeField]
	protected Slider cooldownBar;

	/// <summary>
	/// 是否从正面出现
	/// </summary>
	public bool FromFront { get; set; }

	/// <summary>
	/// 移动到的地方
	/// </summary>
	public Vector3 TargetPosition { get; set; }

	/// <summary>
	/// 攻击玩家
	/// </summary>
	protected virtual void AttackPlayer() { }

	protected virtual void Update() {
		AttackPlayer();
	}
}
