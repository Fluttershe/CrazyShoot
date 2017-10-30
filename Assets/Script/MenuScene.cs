using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public void load()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
