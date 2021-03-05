using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour{
	private Text _pointText;

	public void Start(){
		_pointText = GetComponent<Text>();
	}

	public void Update(){
		_pointText.text = GameManager.Instance.points.ToString();
	}
}