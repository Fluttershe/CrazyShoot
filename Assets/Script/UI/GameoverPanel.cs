using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameoverPanel : MonoBehaviour
{
	[SerializeField]
	private Text text;

	private void OnDisable()
	{
		this.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		this.gameObject.SetActive(true);
	}

	public void ShowState()
	{
		PlayerStatistics state = PlayerStatistics.GetStat();
		text.text =
			"你摧毁了 " + state.LastBasicEnemyKilled + " 辆敌车\n";
	}
}
