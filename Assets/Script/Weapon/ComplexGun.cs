using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;

public class ComplexGun : Gun, IWeaponWithHeat
{
    public AudioSource SanRe;

	[SerializeField]
	float fireDispersion;

	[SerializeField]
	float heatPerShoot;

	[SerializeField]
	float heatDispSpeed;

	[SerializeField]
	StateValue heat;

	[SerializeField]
	StateValue reloadTime;

	[SerializeField]
	StateValue magazine;

	[SerializeField]
	StateValue delayBeforeFire;
	
	[SerializeField]
	StateValue delayBeforeHeatDisp;

	[SerializeField]
	bool triggerHeld;
	[SerializeField]
	bool overheated;

	public bool IsOverheated
	{ get { return overheated; } }

	private Action onOverheating;
	public event Action OnOverheating
	{
		add { onOverheating += value; }
		remove { onOverheating -= value; }
	}

	private Action onHeatDiedDown;
	public event Action OnOverheatEnded
	{
		add { onHeatDiedDown += value; }
		remove { onHeatDiedDown -= value; }
	}

	public float HeatPercent
	{
		get { return heat.CurrentValue / heat.BaseValue; }
	}

	protected override void Update()
	{
		triggerHeld = Input.GetMouseButton(0);

		if (triggerHeld && !overheated)
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
                SanRe.Play();
                delayBeforeHeatDisp.ModCurrent(-Time.deltaTime);
			}
			else
			{
				if (heat.CurrentValue > 0)
					heat.ModCurrent(-heatDispSpeed * Time.deltaTime);
				else if (overheated)
				{
					overheated = false;
					if (onHeatDiedDown != null)
						onHeatDiedDown.Invoke();
				}
			}
		}
	}

	protected override void Firing()
	{
		// 检查弹夹容量
		if (magazine.CurrentValue <= 0 && magazine.BaseValue > 0)
		{
			// 如果重装时间未过完
			if (reloadTime.CurrentValue > 0)
			{
				// 计时并退出
				reloadTime.ModCurrent(-Time.deltaTime);
				return;
			}
			else // 如果重装完成
			{
				// 重设重装时间和弹夹
				reloadTime.ResetCurrentValue();
				magazine.ResetCurrentValue();
				cooldown = 0;
			}
		}

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
		magazine.ModCurrent(-1);
		
		// 增加热量
		heat.ModCurrent(heatPerShoot);

		// 如果过热
		if (heat.CurrentValue > heat.BaseValue)
		{
			overheated = true;
			if (onOverheating != null)
				onOverheating.Invoke();
		}
	}
}
