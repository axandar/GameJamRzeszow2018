using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour{

	[SerializeField] private UpgradeManager upgradeManager;
	
	public GameObject menu;
	public GameObject upgrades;

	public void Awake(){
		Menu();
	}

	public void Menu(){
		menu.SetActive(true);
		upgrades.SetActive(false);
	}

	public void Play(){
		SceneManager.LoadSceneAsync("RoadGame");
	}

	public void Upgrades(){
		menu.SetActive(false);
		upgrades.SetActive(true);
	}

	public void Quit(){
		Application.Quit();
	}
}