using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SloikThrow : MonoBehaviour{
	public GameObject sloikPrefab;
	public float cooldown = 1f;
	public int currSloik = 6;
	public GameObject objectUI;

	private float _cooldown;
	private Vector2 _mousePos;


	private void Update(){
		if(Input.GetKeyDown(KeyCode.Q)){
			currSloik--;
		}

		if(Input.GetKeyDown(KeyCode.E)){
			currSloik++;
		}

		if(currSloik < 0){
			currSloik = 7;
		}

		if(currSloik > 7){
			currSloik = 0;
		}

		objectUI.GetComponent<UIScript>().ChangeToIndex(currSloik);

		if(_cooldown > 0f){
			_cooldown -= Time.deltaTime;
		}

		if(!Input.GetMouseButtonDown(0) || !GameManager.Instance.inGame || !(_cooldown <= 0f)){
			return;
		}
		_cooldown = cooldown;

		_mousePos = Input.mousePosition;
		var sloik = Instantiate(sloikPrefab, transform.position, Quaternion.identity);
		var sloikComponent = sloik.GetComponent<Sloik>();
		sloikComponent.SetTarget(Camera.main.ScreenToWorldPoint(_mousePos));
		sloikComponent.SetType(currSloik);
	}
}