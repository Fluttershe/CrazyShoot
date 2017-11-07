
using System;
using UnityEngine;

public abstract class PlayerGun : Gun, IPlayerWeapon
{
	[SerializeField]
	protected StateValue ammo;

	public abstract PlayerWeaponType WeaponType { get; }

	public int AmmoCount { get { return (int)ammo.CurrentValue; } }

	public void AddAmmo(int amount)
	{
		ammo.ModCurrent(amount);
	}
}
