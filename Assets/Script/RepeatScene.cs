using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepeatScene : MonoBehaviour {
    public void load()
    {
        SceneManager.LoadScene(Application.loadedLevelName);
        Time.timeScale = 1;
    }
}
