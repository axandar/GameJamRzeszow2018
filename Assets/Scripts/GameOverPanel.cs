using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public void Restart()
    {
        GameManager.instance.inGame = false;
        GameManager.instance.timer = 120f;
        SceneManager.LoadSceneAsync("RoadGame");
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}