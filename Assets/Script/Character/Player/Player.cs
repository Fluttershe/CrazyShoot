using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : Character
{
    public override void ReleaseSelf()
	{
		print("Gameover");
		health.CurrentValue = health.BaseValue * 10;
		GameManager.Gameover();
    }
}
