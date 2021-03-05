using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karetka : MonoBehaviour{
	//todo dodawac wartosc predkosc z EnemyManager za kazdym refresh

	public float speed;
	public int life;
	public bool isDead;
	public GameObject roadLine;
	public int coinValue = 1;

	private int _screenWidth;
	private EnemyManager _enemyManager;
	private PlayerCar _playerCar;
	private Rigidbody2D _rigidBody;

	//Effects
	private float _effectWearOff;
	private bool _isImmortal;
	private int _slowDownValue;

	private void Start(){
		_playerCar = FindObjectOfType<PlayerCar>();
		_enemyManager = FindObjectOfType<EnemyManager>();
	}

	private void Awake(){
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Update(){
		DriveForward();
		EffectWearOff();

		if(transform.position.x > 25f){
			Destroy(gameObject);
		}
	}

	private void DriveForward(){
		var actualEnemySpeed = speed + _enemyManager.speedUpValue;
		var position = transform.position;
		_rigidBody.MovePosition(new Vector3(
			position.x + FindObjectOfType<MovingRoad>().currSpeed * Time.deltaTime * actualEnemySpeed,
			position.y, 0f));
	}

	public void OnTriggerEnter2D(Collider2D col){
		if(col.name == "Ałto"){
			col.GetComponent<PlayerCar>().TakeHeart(1);
			Destroy(gameObject);
		}

		if(col.name != "Ałto" && col.name != "Słoik"){
			Destroy(gameObject);
		}
	}

	private void EffectWearOff(){
		if(_effectWearOff < 0){
			_effectWearOff = 0;
			ClearEffects();
		} else if(_effectWearOff > 0){
			_effectWearOff -= Time.deltaTime;
		}
	}

	private void Kill(){
		if(!_isImmortal){
			isDead = true;
			//todo death animation
		}
	}

	private void ClearEffects(){
		_isImmortal = false;
		_slowDownValue = 0;
	}

	public void Slow(){
		ClearEffects();

		var effectLevel = UpgradeManager.Instance.upgradeLevelGrochowka;
		switch(effectLevel){
			case 1:
				_slowDownValue = 1;
				break;

			case 2:
				_slowDownValue = 2;
				break;

			case 3:
				_slowDownValue = 3;
				break;
		}

		_slowDownValue *= -1; //grochowa powinna przyspieszac przeciwnikow

		_effectWearOff = 3;
	}

	public void Immortal(){
		ClearEffects();

		_isImmortal = true;

		var effectLevel = UpgradeManager.Instance.upgradeLevelSchabowy;
		switch(effectLevel){
			case 1:
				_effectWearOff = 1;
				break;

			case 2:
				_effectWearOff = 2;
				break;

			case 3:
				_effectWearOff = 3;
				break;
		}
	}

	public void Explosion(){
		ClearEffects();
		Kill();
	}

	public void Bird(){
		ClearEffects();
		Kill();
	}

	public void LifeUp(){
		return; // no effect
	}

	public void InstaKill(){
		ClearEffects();
		Kill();
	}
}