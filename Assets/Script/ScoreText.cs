using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    [SerializeField]
    private Text text;

    PlayerStatistics state;

    private void Start()
    {
        state = PlayerStatistics.GetStat();
    }

    private void Update()
    {
        text.text =
       "你摧毁了 " + state.LastBasicEnemyKilled + " 辆敌车\n";
    }
}
