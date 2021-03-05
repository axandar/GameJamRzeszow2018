using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour{
	private Text _coinText;

	public void Start(){
		_coinText = GetComponent<Text>();
	}
	
	public void Update(){
		_coinText.text = UpgradeManager.Instance.coins.ToString();
	}
}