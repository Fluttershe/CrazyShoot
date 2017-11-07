using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class OrbitalLaserGun : PlayerGun
{
	public override PlayerWeaponType WeaponType { get { return PlayerWeaponType.OrbitalLaser; } }

	protected override void InitializeObject(SpawnableObject obj)
	{
		base.InitializeObject(obj);
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = -8;
		obj.transform.position = pos;
		obj.transform.rotation = Quaternion.identity;
	}
}
