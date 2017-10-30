using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text score;

    private void Start()
    {
        score = this.GetComponent<Text>();
    }

    private void Update ()
    {
        score.text = "Your score:"+(int)GameTime.PassedTime;
	}
}
