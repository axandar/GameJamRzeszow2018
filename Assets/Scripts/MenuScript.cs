using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour{
	public GameObject menu;
	public GameObject upgrades;

	public void Awake(){
		UpgradeManager.Instance.LoadUpgrades();
		Menu();
	}

	public void Menu(){
		menu.SetActive(true);
		upgrades.SetActive(false);
		UpgradeManager.Instance.SaveUpgrades();
	}

	public void Play(){
		SceneManager.LoadSceneAsync("RoadGame");
	}

	public void Upgrades(){
		UpgradeManager.Instance.LoadUpgrades();
		menu.SetActive(false);
		upgrades.SetActive(true);
	}

	public void Quit(){
		Application.Quit();
	}
}