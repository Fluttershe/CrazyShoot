using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayerWeaponType
{
	MachineGun,
	MissileLauncher,
	OrbitalLaser,
}

public interface IPlayerWeapon
{
	/// <summary>
	/// 该武器的类型
	/// </summary>
	PlayerWeaponType WeaponType { get; }

	/// <summary>
	/// 该武器的弹药数
	/// </summary>
	int AmmoCount { get; }

	/// <summary>
	/// 增加武器弹药
	/// </summary>
	/// <param name="amount"></param>
	void AddAmmo(int amount);

	/// <summary>
	/// 武器开火
	/// </summary>
	void StartFire();

	/// <summary>
	/// 武器停火
	/// </summary>
	void StopFire();
}
