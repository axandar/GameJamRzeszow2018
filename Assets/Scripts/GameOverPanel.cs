using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	public void Restart(){
		gameManager.InGame = false;
		SceneManager.LoadSceneAsync("RoadGame");
	}

	public void ToMenu(){
		SceneManager.LoadSceneAsync("MainMenu");
	}
}