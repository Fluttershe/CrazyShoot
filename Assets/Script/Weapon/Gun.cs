using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : SpawnPoint {

	/// <summary>
	/// 开火的效果
	/// </summary>
	[SerializeField]
	protected GameObject firingEffect;

	[SerializeField]
	protected AudioSource fireSound;

	/// <summary>
	/// 子弹射出的位置
	/// </summary>
	[SerializeField]
	protected Transform firingPoint;

	/// <summary>
	/// 子弹所属的阵营
	/// </summary>
	[SerializeField]
	protected string ammoFaction;
	int ammoLayer;

	/// <summary>
	/// 该武器的伤害
	/// </summary>
	[SerializeField]
	protected float damage;

	/// <summary>
	/// 武器开火
	/// </summary>
	private bool firing;

	protected override void Start()
	{
		base.Start();
		if (firingPoint == null) firingPoint = transform;
		ammoLayer = LayerMask.NameToLayer(ammoFaction);
	}

	protected override void SpawnOver()
	{
		base.SpawnOver();

		GameObject obj = ObjectPool.Acquire(firingEffect);
		obj.transform.position = firingPoint.position;
		obj.transform.rotation = firingPoint.rotation;

		if (fireSound != null)
			fireSound.Play(0);
	}

	protected override void InitializeObject(SpawnableObject obj)
	{
        base.InitializeObject(obj);
		obj.transform.position = firingPoint.position;

        Ammo ammo = (Ammo)obj;
		if (ammo == null) return;

        // 设置子弹到对应的伤害
        ammo.Damage = damage;

		// 设置子弹的阵营
		ammo.gameObject.layer = ammoLayer;
    }

	protected override void Update()
	{
		Firing();
	}

	protected virtual void Firing()
	{
		// 如果冷却未完, 退出
		if (!CountingCooldown()) return;

		// 如果开火，生成子弹
		if (firing)
			SpawnObject();
	}

	public void StartFire()
	{
		firing = true;
	}

	public void StopFire()
	{
		firing = false;
	}
}
