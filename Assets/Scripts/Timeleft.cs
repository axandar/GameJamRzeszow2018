using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeleft : MonoBehaviour
{
    private Text _t;
    private Slider _s;

    private void Awake()
    {
        _t = _t ?? GetComponent<Text>();
        _s = _s ?? GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        int mins = (int)Mathf.Floor(GameManager.instance.timer / 60f);
        int secs = (int)(GameManager.instance.timer - mins * 60f);
        _t.text = mins.ToString("00") + ":" + secs.ToString("00");

        _s.value = (GameManager.instance.timerStart - GameManager.instance.timer) / 100f;
    }
}