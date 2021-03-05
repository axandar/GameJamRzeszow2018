using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeleft : MonoBehaviour{
	private Text _t;
	private Slider _s;

	private void Awake(){
		_t = GetComponent<Text>();
		_s = GetComponentInChildren<Slider>();
	}

	private void Update(){
		var mins = (int) Mathf.Floor(GameManager.Instance.timer / 60f);
		var secs = (int) (GameManager.Instance.timer - mins * 60f);
		_t.text = mins.ToString("00") + ":" + secs.ToString("00");

		_s.value = (GameManager.Instance.timerStart - GameManager.Instance.timer) / 100f;
	}
}