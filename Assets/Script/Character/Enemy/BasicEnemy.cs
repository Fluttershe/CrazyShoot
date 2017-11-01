using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicEnemy : Enemy {

	[SerializeField]
	protected Vector3 frontRotation;
	
	[SerializeField]
	protected Vector3 backRotation;

	[SerializeField]
	protected EnemyGun gun;

	[SerializeField]
	protected SortingGroup sGroup;

	private bool stop;
	private Vector3 MovingDirection;

	protected override void Update()
	{
		if (!stop)
		{
			gun.StopFire();
			float distance = speed * Time.deltaTime;

			// 如果距离目标点足够近
			if ((transform.position - TargetPosition).sqrMagnitude < (distance * distance))
			{
				stop = true;
				transform.position = TargetPosition;
				gun.StartFire();
			}
			else
			{
				// 向玩家移动
				transform.Translate(MovingDirection * distance, Space.World);
			}
		}

		if (shootingWarning != null && gun.CooldownPercentage < 0.4f)
			shootingWarning.StartFlash();
		else
			shootingWarning.StopFlash();
	}

	public override void Initialize()
	{
		base.Initialize();
		sGroup.sortingOrder = (int)(TargetPosition.y * -100);
		stop = false;

		if (FromFront)
			gun.transform.rotation = Quaternion.Euler(frontRotation);
		else
			gun.transform.rotation = Quaternion.Euler(backRotation);

		gun.ResetCooldown();
		MovingDirection = (TargetPosition - transform.position).normalized;
	}

	public override void ReleaseSelf()
	{
		PlayerStatistics.GetStat().LastBasicEnemyKilled ++;
		PlayerStatistics.GetStat().LastCash +=5;
		base.ReleaseSelf();
		gun.StopFire();
	}
}
