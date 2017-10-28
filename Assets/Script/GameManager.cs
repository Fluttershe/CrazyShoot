using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static void CreateInstance()
	{
		if (instance != null) return;

		instance = new GameObject("GameManager").AddComponent<GameManager>();
	}
}
