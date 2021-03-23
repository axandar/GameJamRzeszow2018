﻿using System;
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
		var mins = (int) Mathf.Floor(gameManager.Timer / 60f);
		var secs = (int) (gameManager.Timer - mins * 60f);
		_t.text = mins.ToString("00") + ":" + secs.ToString("00");

		_s.value = (_timer - gameManager.Timer) / 100f;
	}
}