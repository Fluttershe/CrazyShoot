using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;

public abstract class UniversalGun : PlayerGun
{
	[SerializeField]
	float fireDispersion;

	[SerializeField]
	StateValue delayBeforeFire;

	[SerializeField]
	bool triggerHeld;
	
	protected override void Update()
	{
		triggerHeld = Input.GetMouseButton(0);

		if (triggerHeld)
		{
			// 射击
			Firing();
		}
		else
		{
			// 重设开火延迟
			delayBeforeFire.ResetCurrentValue();
		}
	}

	protected override void Firing()
	{
		// 检查弹药剩余，
		if (ammo.CurrentValue <= 0 && ammo.BaseValue > 0) return;

		// 等待开火前延迟
		if (delayBeforeFire.CurrentValue > 0)
		{
			delayBeforeFire.ModCurrent(-Time.deltaTime);
			return;
		}

		base.Firing();
	}

	protected override void InitializeObject(SpawnableObject obj)
	{
		base.InitializeObject(obj);
		obj.transform.Rotate(Vector3.forward, Random.Range(-fireDispersion, fireDispersion));
	}

	protected override void SpawnOver()
	{
		base.SpawnOver();
		ammo.ModCurrent(-1);
	}
}
