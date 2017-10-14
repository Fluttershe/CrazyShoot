using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
	protected override void Start()
	{
		base.Start();
		cooldown = cooldownLimit.RandomFloat();
	}

	public float CooldownPercentage
	{
		get { return cooldown / cooldownLimit.x; }
	}

	protected override void InitializeObject(SpawnableObject obj)
	{
		base.InitializeObject(obj);
	}

	public void ResetCooldown()
	{
		cooldown = cooldownLimit.x;
	}
}
