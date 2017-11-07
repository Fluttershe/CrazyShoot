using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹的基类
/// </summary>
public abstract class Ammo : SpawnableObject, IAmmo {

	/// <summary>
	/// 子弹击中的效果
	/// </summary>
	[SerializeField]
	protected GameObject hittingEffect;

	/// <summary>
	/// 子弹飞行的速度
	/// </summary>
	[SerializeField]
	protected float speed = 10;

	/// <summary>
	/// 子弹存在的时长
	/// </summary>
	[SerializeField]
	protected StateValue existentTime;
	public StateValue ExistentTime
	{
		get { return ExistentTime; }
		set { ExistentTime = value; }
	}

	/// <summary>
	/// 子弹的伤害
	/// </summary>
	protected float damage;
	public float Damage
	{
		get { return damage; }
		set { damage = value; }
	}

	/// <summary>
	/// 子弹飞行的方向
	/// </summary>
	protected Vector2 direction = -Vector2.up;
	
	protected virtual void Update() {
		existentTime.ModCurrent(-Time.deltaTime);

		transform.Translate(direction * speed * Time.deltaTime);

		if (existentTime.CurrentValue <= 0) {
			ReleaseSelf();
		}
	}

	public override void Initialize()
	{
		base.Initialize();
		existentTime.ResetCurrentValue();
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider2D) {

		GameObject obj = ObjectPool.Acquire(hittingEffect);
		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;

        // 寻找击中对象上的Character
        Character character = collider2D.GetComponent<Character>();
		if (character != null) {
			// 对这个Character造成伤害
			character.TakeDamage(damage);
		}

        // 回收该子弹
        ReleaseSelf();
	}

	public void SetCollisionLayer(int layer)
	{
		gameObject.layer = layer;
	}
}
