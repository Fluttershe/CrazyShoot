using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IAmmo
{
	float Damage { get; set; }

	void SetCollisionLayer(int layer);
}
