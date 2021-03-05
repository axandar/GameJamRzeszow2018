using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour{
	//todo dodawac wartosc predkosc z EnemyManager za kazdym refresh

	public float speed;
	public int life;
	public bool isDead;
	public GameObject roadLine;
	public int coinValue = 1;

	public Sprite[] sprites;

	private int _screenWidth;
	private EnemyManager _enemyManager;
	private PlayerCar _playerCar;
	private Rigidbody2D _rigidBody;
	private SpriteRenderer _spriteRenderer;

	//Effects
	private float _effectWearOff;
	private bool _isImmortal;
	private int _slowDownValue;

	private void Start(){
		_enemyManager = FindObjectOfType<EnemyManager>();
		_playerCar = FindObjectOfType<PlayerCar>();
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		var random = UnityEngine.Random.Range(0, sprites.Length - 1);
		_spriteRenderer.sprite = sprites[random];
	}

	private void Awake(){
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Update(){
		DriveForward();
		EffectWearOff();

		if(!(transform.position.x > 14f)){
			return;
		}

		_playerCar.TakeHeart(1);
		_enemyManager.RemoveEnemyFromList(this);
		Destroy(gameObject);
	}

	private void DriveForward(){
		var actualEnemySpeed = speed + _enemyManager.speedUpValue - _slowDownValue;
		var position = transform.position;
		_rigidBody.MovePosition(new Vector3(position.x + FindObjectOfType<MovingRoad>().currSpeed * Time.deltaTime * actualEnemySpeed, position.y, 0f));
	}

	public void OnTriggerEnter2D(Collider2D col){
		var layer = col.gameObject.layer;
		if(layer == LayerMask.NameToLayer("Player")){
			col.GetComponent<PlayerCar>().TakeHeart(1);
			_enemyManager.spawnedEnemies.Remove(this);
			Destroy(gameObject);
		} else if(layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("Obstacle")){
			_enemyManager.spawnedEnemies.Remove(this);
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

	public void Kill(){
		if(_isImmortal){
			return;
		}

		Debug.Log("Kill");
		isDead = true;
		UpgradeManager.Instance.AddCoins(1);
		Destroy(gameObject);
	}

	private void ClearEffects(){
		_isImmortal = false;
		_slowDownValue = 0;
	}

	public void Slow(){
		ClearEffects();
		Debug.Log("Slow");
		var effectLevel = UpgradeManager.Instance.upgradeLevelGrochowka;
		switch(effectLevel){
			case 0:
				_slowDownValue = 1;
				break;
			case 1:
				_slowDownValue = 2;
				break;
			case 2:
				_slowDownValue = 3;
				break;
		}

		_effectWearOff = 3;
	}

	public void Immortal(){
		ClearEffects();

		_isImmortal = true;

		var effectLevel = UpgradeManager.Instance.upgradeLevelSchabowy;
		switch(effectLevel){
			case 0:
				_effectWearOff = 1;
				break;

			case 1:
				_effectWearOff = 2;
				break;

			case 2:
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