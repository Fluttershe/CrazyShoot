using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IWeaponWithHeat
{
	/// <summary>
	/// 是否过热
	/// </summary>
	bool IsOverheated { get; }

	/// <summary>
	/// 当前热量比例
	/// </summary>
	float HeatPercent { get; }

	/// <summary>
	/// 过热事件
	/// </summary>
	event Action OnOverheating;

	/// <summary>
	/// 过热完毕事件
	/// </summary>
	event Action OnOverheatEnded;

	/// <summary>
	/// 开火
	/// </summary>
	void StartFire();

	/// <summary>
	/// 停火
	/// </summary>
	void StopFire();
}
