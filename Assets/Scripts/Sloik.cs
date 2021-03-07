using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloik : MonoBehaviour{

	[SerializeField] private UpgradeManager upgradeManager;
	
	public float rotateSpeed = 350f;
	public float flySpeed = 25f;
	public float nearScale = 1f;
	public float rearScale = 1.5f;

	public int type;
	public Vector2 target;
	public Vector2 initialPos;

	private float _initialDistance;

	public Sprite sloikGolabki; //0 type
	public Sprite sloikGrochowka; //1
	public Sprite sloikBigos; //2
	public Sprite sloikSchabowy; //3
	public Sprite sloikMix; //4
	public Sprite sloikLazanki; //5
	public Sprite sloikParowki; //6
	public Sprite sloikMeksyk; //7

	private SpriteRenderer _sr;
	public GameObject sloikEffectPrefab;

	private void Awake(){
		_sr = GetComponent<SpriteRenderer>();
		_sr.sprite = sloikParowki;

		initialPos = transform.position;
	}

	private void Update(){
		var transform1 = transform;
		
		transform1.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));

		var pos = transform1.position;
		pos = Vector2.MoveTowards(pos, target, Time.deltaTime * flySpeed);

		transform1.position = pos;

		if((Vector2) transform1.position == target){
			EagleHasLanded();
		}

		var localScale = transform1.localScale;
		var dist = Vector2.Distance(transform1.position, target);
		if(dist >= _initialDistance / 2f){
			localScale = new Vector3(Mathf.Lerp(localScale.x, rearScale, Time.deltaTime), 
				Mathf.Lerp(localScale.y, rearScale, Time.deltaTime), 
				Mathf.Lerp(localScale.z, rearScale, Time.deltaTime));
			transform.localScale = localScale;
		} else{
			localScale = new Vector3(Mathf.Lerp(localScale.x, nearScale, Time.deltaTime), 
				Mathf.Lerp(localScale.y, nearScale, Time.deltaTime), 
				Mathf.Lerp(localScale.z, nearScale, Time.deltaTime));
			transform.localScale = localScale;
		}
	}

	private void EagleHasLanded(){
		var actualPosition = gameObject.transform.position;

		if(type == SloikEffectController.EFFECT_BIRD){
			var birdsNumber = 0;
			switch(upgradeManager.upgradeLevelGolabki){
				case 0:
					birdsNumber = 2;
					break;
				case 1:
					birdsNumber = 3;
					break;
				case 2:
					birdsNumber = 4;
					break;
			}

			Debug.Log(birdsNumber);
			for(int i = 0; i < birdsNumber; i++){
				var sloikEffectObject = Instantiate(sloikEffectPrefab, actualPosition, Quaternion.identity);
				var effectController = sloikEffectObject.GetComponent<SloikEffectController>();
				effectController.SetSloikEffect(type);
			}
		} else{
			var sloikEffectObject = Instantiate(sloikEffectPrefab, actualPosition, Quaternion.identity);
			var effectController = sloikEffectObject.GetComponent<SloikEffectController>();
			effectController.SetSloikEffect(type);
		}

		Destroy(gameObject);
	}

	public void SetTarget(Vector2 target){
		this.target = target;
		_initialDistance = Vector2.Distance(target, initialPos);
		Debug.DrawLine(transform.position, target, Color.red, 15f);
	}

	public void SetType(int type){
		this.type = type;
		_sr.sprite = GetSloikFromType(type);
	}

	private Sprite GetSloikFromType(int type){
		switch(type){
			case 0:
				return sloikGolabki;

			case 1:
				return sloikGrochowka;

			case 2:
				return sloikBigos;

			case 3:
				return sloikSchabowy;

			case 4:
				return sloikMix;

			case 5:
				return sloikLazanki;

			case 6:
				return sloikParowki;

			case 7:
				return sloikMeksyk;

			default:
				return null;
		}
	}
}