using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeleft : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	private Text _t;
	private Slider _s;
	private float _timer;

	private void Start(){
		_timer = gameManager.Timer;
	}

	private void Awake(){
		_t = GetComponent<Text>();
		_s = GetComponentInChildren<Slider>();
	}

	private void Update(){
		_t.text = TextFromTimer(gameManager.Timer);

		_s.value = (_timer - gameManager.Timer) / 100f;
	}

	public static string TextFromTimer(float timer){
		var minutes = (int) Mathf.Floor(timer / 60f);
		var seconds = (int) (timer - minutes * 60f);
		return minutes.ToString("00") + ":" + seconds.ToString("00");
	}
}