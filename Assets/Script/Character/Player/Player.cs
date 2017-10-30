using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : Character {

    GameObject ui;
    public string uiname;

    private void Start()
    {
        ui = GameObject.Find(uiname);
        ui.active = false;
    }

    public override void ReleaseSelf()
	{
		print("Gameover");
        Time.timeScale = 0;
        ui.active = true;
    }
}
