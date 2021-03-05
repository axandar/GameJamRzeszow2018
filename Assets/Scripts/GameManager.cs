using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>{
	public static bool DEBUG => true;

	public long points;
	public bool inGame;
	public float timer = 120f;
	public float timerStart = 120f;

	protected override void Awake(){
		base.Awake();
		timerStart = timer;
	}

	private void Update(){
		if(!inGame){
			return;
		}

		if(timer > 0){
			timer -= Time.deltaTime;
		} else{
			timer = 0f;
			StartCoroutine(FindObjectOfType<PlayerCar>().PlayerSlideOut());
		}
	}
}