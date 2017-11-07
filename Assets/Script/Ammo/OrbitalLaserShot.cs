using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class OrbitalLaserShot : SpawnableObject, IAmmo
{
	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("发射后多久出现激光")]
	float showUpDelay;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("激光从开始出现到完全出现所需时间")]
	float spreadTime;

	[SerializeField]
	[Range(0f, 5f)]
	[Tooltip("激光完全出现持续时间")]
	float fullShotTime;

	[SerializeField]
	[Range(1f, 6f)]
	[Tooltip("激光完全出现时的宽度")]
	float fullWidth;

	new LineRenderer renderer;
	new BoxCollider2D collider;

	Vector2 size;
	const int kVerticalPosition = -4;
	
	/// <summary>
	/// 激光的每秒伤害
	/// </summary>
	public float Damage { get; set; }

	private void Awake()
	{
		renderer = GetComponent<LineRenderer>();
		collider = GetComponent<BoxCollider2D>();
		size = collider.size;
	}

	public override void Initialize()
	{
		base.Initialize();

		// 停用触发器和绘线器
		collider.enabled = false;
		renderer.enabled = false;

		// 把触发器和绘线器的宽度都设为0
		size.x = 0;
		collider.size = size;
		renderer.startWidth = 0;
		renderer.endWidth = 0;

		// 将y轴位置设为指定值
		var pos = transform.position;
		pos.y = kVerticalPosition;
		transform.position = pos;

		// 开始布置激光
		StartCoroutine(DeployLaser());
	}

	public override void ReleaseSelf()
	{
		base.ReleaseSelf();
		collider.enabled = false;
		renderer.enabled = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// 尝试获取Character
		var c = collision.GetComponent<Character>();

		// 没有Character就退出
		if (c == null) return;

		// 有则施加伤害
		c.TakeDamage(Damage * Time.deltaTime);
	}

	/// <summary>
	/// 布置激光流程
	/// </summary>
	/// <returns></returns>
	IEnumerator DeployLaser()
	{
		// 等待激光发射延迟
		yield return new WaitForSeconds(showUpDelay);

		// 启用触发器和绘线器
		collider.enabled = true;
		renderer.enabled = true;

		// 绘制激光出现过程
		var time = spreadTime;
		while (time > 0)
		{
			time -= Time.deltaTime;
			var delta = Time.deltaTime / spreadTime;
			size.x += delta * fullWidth;
			collider.size = size;
			renderer.startWidth += delta * fullWidth;
			renderer.endWidth += delta * fullWidth;
			yield return null;
		}

		// 激光出现完成
		size.x = fullWidth;
		collider.size = size;
		renderer.startWidth = fullWidth;
		renderer.endWidth = fullWidth;

		// 让激光发射一会
		yield return new WaitForSeconds(fullShotTime);

		// 绘制激光消失过程
		time = spreadTime;
		while (time > 0)
		{
			time -= Time.deltaTime;
			var delta = Time.deltaTime / spreadTime;
			size.x -= delta * fullWidth;
			collider.size = size;
			renderer.startWidth -= delta * fullWidth;
			renderer.endWidth -= delta * fullWidth;
			yield return null;
		}

		// 激光消失
		size.x = 0;
		collider.size = size;
		renderer.startWidth = 0;
		renderer.endWidth = 0;

		ReleaseSelf();
	}

	public void SetCollisionLayer(int layer)
	{
		gameObject.layer = layer;
	}
}
