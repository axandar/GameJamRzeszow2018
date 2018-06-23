using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("RoadGame");
    }

    public void Upgrades()
    {
        UpgradeManager.instance.LoadUpgrades();
    }

    public void Quit()
    {
        Application.Quit();
    }
}