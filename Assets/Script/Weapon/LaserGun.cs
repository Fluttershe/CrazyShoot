using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour, IWeaponWithHeat
{
    [SerializeField]
	LayerMask mask;

	[SerializeField]
	RaycastHit2D[] hits = new RaycastHit2D[10];

	[SerializeField]
	LineRenderer laserLine;

	[SerializeField]
	Transform firingPoint;

	[SerializeField]
	float damagerPerSec;

	[SerializeField]
	float heatPerSec;

	[SerializeField]
	float heatDispSpeed;

	[SerializeField]
	StateValue heat;

	[SerializeField]
	StateValue delayBeforeFire;

	[SerializeField]
	StateValue delayBeforeHeatDisp;

	[SerializeField]
	bool triggerHeld;

	[SerializeField]
	bool overheated;

	bool firing;

	public event Action OnOverheating;
	public event Action OnOverheatEnded;

	public float HeatPercent
	{
		get
		{
			return heat.CurrentValue / heat.BaseValue;
		}
	}

	public bool IsOverheated
	{ get { return overheated; } }

	public void StartFire()
	{
		firing = true;
	}

	public void StopFire()
	{
		firing = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (firing && !overheated)
		{
			// 重设散热延迟
			delayBeforeHeatDisp.ResetCurrentValue();
			// 射击
			Firing();
		}
		else
		{
			// 重设开火延迟
			delayBeforeFire.ResetCurrentValue();

			// 如果散热延迟未过
			if (delayBeforeHeatDisp.CurrentValue > 0)
			{
				delayBeforeHeatDisp.ModCurrent(-Time.deltaTime);
			}
			else
			{
				if (heat.CurrentValue > 0)
					heat.ModCurrent(-heatDispSpeed * Time.deltaTime);
				else if (overheated)
				{
					overheated = false;
					OnOverheatEnded.Invoke();
				}
			}

			laserLine.enabled = false;
		}
	}

	protected void Firing()
	{
		// 等待开火前延迟
		if (delayBeforeFire.CurrentValue > 0)
		{
            delayBeforeFire.ModCurrent(-Time.deltaTime);
			return;
		}
		
		ContactFilter2D filter = new ContactFilter2D();
		filter.useTriggers = true;
		filter.useLayerMask = true;
		filter.SetLayerMask(mask);

		int results = Physics2D.Raycast(firingPoint.position, -firingPoint.up, filter, hits);

		if (results == 0)
			laserLine.SetPosition(1, firingPoint.position - firingPoint.up * 50);
		else
		{
			laserLine.SetPosition(1, new Vector3(hits[0].point.x, hits[0].point.y, -5));
			if (hits[0].collider.GetComponent<Character>() != null)
			{
				hits[0].collider.GetComponent<Character>().TakeDamage(damagerPerSec * Time.deltaTime);
			}
		}

		laserLine.SetPosition(0, firingPoint.position);
		laserLine.enabled = true;

		// 增加热量
		heat.ModCurrent(heatPerSec * Time.deltaTime);

		if (heat.CurrentValue >= heat.BaseValue)
		{
			overheated = true;
			OnOverheating.Invoke();
		}
	}
}
