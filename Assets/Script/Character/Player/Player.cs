using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player : Character {
	public override void ReleaseSelf()
	{
		print("Gameover");
		health.ResetCurrentValue();
	}
}
