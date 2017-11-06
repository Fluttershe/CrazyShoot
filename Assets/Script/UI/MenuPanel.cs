using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {

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
            "到目前为止:\n" +
            "你总共赚了 " + state.Cash + " 元\n" +
            "你摧毁了" + state.BasicEnemyKilled + " 名敌人\n" ;
	}
}
