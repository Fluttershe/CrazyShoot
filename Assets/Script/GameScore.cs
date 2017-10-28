using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameScore : MonoBehaviour
{
	private static GameScore instance;
	private int score;

	public static void CreateInstance()
	{
		if (instance != null) return;

		instance = new GameObject("GameScore").AddComponent<GameScore>();
	}

	public static int Score
	{
		get { return instance.score; }
		set { instance.score = value; }
	}
}
