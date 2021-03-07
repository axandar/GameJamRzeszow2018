using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	public void Restart(){
		gameManager.inGame = false;
		gameManager.timer = 120f;
		SceneManager.LoadSceneAsync("RoadGame");
	}

	public void ToMenu(){
		SceneManager.LoadSceneAsync("MainMenu");
	}
}