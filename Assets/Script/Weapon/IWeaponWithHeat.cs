using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IWeaponWithHeat
{
	float HeatPercent { get; }
	void StartFire();
	void StopFire();
}
