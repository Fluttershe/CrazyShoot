using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponMount : MonoBehaviour
{
    [SerializeField]
	private Slider heatBar;

	private IWeaponWithHeat[] weaponList;

	[SerializeField]
	private int currentWeapon;

	[SerializeField]
	float scollDelta;

	[SerializeField]
	FlashColor flash;

	private void Start()
	{
		weaponList = GetComponentsInChildren<IWeaponWithHeat>();
		// 如果没有装载武器，停止该MonoBehaviour
		if (weaponList == null || weaponList.Length == 0)
			enabled = false;

		// 挂载过热和散热事件
		weaponList[currentWeapon].OnOverheating += OnOverheating;
		weaponList[currentWeapon].OnOverheatEnded += OnOverheatEnded;
	}

	private void Update()
	{
        // 如果没有触摸，退出
        //if (Input.touchCount <= 0) return;

            // TODO: 引入StandardAsset的多平台输入
            //if (Input.touchCount == 1) //单点触碰移动摄像机
        if (Input.GetMouseButton(0))
		{
            //if (Input.touches[0].phase == TouchPhase.Began ||	 //手指在屏幕上开始
            //	Input.touches[0].phase == TouchPhase.Moved ||    //手指在屏幕上移动
            //	Input.touches[0].phase == TouchPhase.Stationary) //手指在屏幕上停止
            {
                // 瞄准
                Aiming();
				// 射击
				weaponList[currentWeapon].StartFire();
			}
        }
		else
		{
            weaponList[currentWeapon].StopFire();
		}

		scollDelta = Input.mouseScrollDelta.y;
		if (Input.mouseScrollDelta.y > 0.5f)
		{
			SwitchWeapon(currentWeapon + 1);
		}

		if (Input.mouseScrollDelta.y < -0.5f)
		{
			SwitchWeapon(currentWeapon - 1);
		}

		if (heatBar != null)
			heatBar.value = weaponList[currentWeapon].HeatPercent;
	}

	protected virtual void Aiming()
	{
		// 获取鼠标的坐标，鼠标是屏幕坐标，Z轴为0，这里不做转换  
		Vector3 mouse = Input.mousePosition;
		// 获取物体坐标，物体坐标是世界坐标，将其转换成屏幕坐标，和鼠标一直  
		Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
		// 屏幕坐标向量相减，得到指向鼠标点的目标向量，即黄色线段  
		Vector3 direction = mouse - obj;
		// 将Z轴置0,保持在2D平面内  
		direction.z = 0f;
		// 将目标向量长度变成1，即单位向量，这里的目的是只使用向量的方向，不需要长度，所以变成1  
		direction = direction.normalized;
		// 物体自身的Y轴和目标向量保持一直，这个过程XY轴都会变化数值  
		transform.up = -direction;
	}

	// 当武器过热时
	protected virtual void OnOverheating()
	{
		flash.StartFlash();
	}

	// 当武器过热完时
	protected virtual void OnOverheatEnded()
	{
		flash.StopFlash();
	}

	/// <summary>
	/// 切换武器
	/// </summary>
	/// <param name="index">需切换到的武器序号</param>
	protected virtual void SwitchWeapon(int index)
	{
		// 校准武器序号到合理范围
		if (index >= weaponList.Length) index = 0;
		if (index < 0) index = weaponList.Length - 1;

		// 如果武器序号和当前武器一样则不作任何处理并退出
		if (currentWeapon == index) return;

		// 移除上一个武器挂载的过热/散热事件
		weaponList[currentWeapon].OnOverheating -= OnOverheating;
		weaponList[currentWeapon].OnOverheatEnded -= OnOverheatEnded;

		// 挂载过热/散热事件到现在的武器上
		weaponList[index].OnOverheating += OnOverheating;
		weaponList[index].OnOverheatEnded += OnOverheatEnded;

		// 判断上一个武器的过热情况与切换到的武器的过热情况
		// 如果两武器状态不同，则根据切换到的武器状态开始/停止过热提示
		var overheatState = weaponList[currentWeapon].IsOverheated;
		if (overheatState != weaponList[index].IsOverheated)
		{
			if (weaponList[index].IsOverheated)
				flash.StartFlash();
			else
				flash.StopFlash();
		}

		currentWeapon = index;
	}
}
