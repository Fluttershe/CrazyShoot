using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour {

    public float gametime;//游戏时间（计时器）

	void Start () {
        gametime = 0;
	}
	
	void Update () {
        gametime = gametime + 1;
	}
}
