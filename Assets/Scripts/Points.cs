using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	private Text _pointText;

	public void Start(){
		_pointText = GetComponent<Text>();
	}

	public void Update(){
		_pointText.text = gameManager.Points.ToString();
	}
}