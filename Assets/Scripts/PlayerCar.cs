using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour{
	private bool Alive => hitsLeft > 0;

	public int hitsLeft = 3;
	public KeyCode lineUp = KeyCode.W;
	public KeyCode lineDown = KeyCode.S;
	public Image[] hearts;
	public Sprite lostHeart;
	public Sprite heart;

	public Animator gameOverScreen;

	public float[] lanes ={5.25f, 3.1f, 1f, -1.1f, -3.25f};

	public int currLane = 2;

	private float mid = 0.25f;
	private float startPos = 25f;
	private bool _crashed;

	//Effects
	private float _duration;

	private EnemyManager _enemyManager;
	public bool isImmortal;

	private void Start(){
		GameManager.Instance.points = 0;
		_enemyManager = FindObjectOfType<EnemyManager>();
		UpgradeManager.Instance.LoadUpgrades();

		hitsLeft = UpgradeManager.Instance.playerLives + UpgradeManager.Instance.upgradeLivesLevel;

		for(var i = 0; i < 5; i++){
			if(hitsLeft <= i){
				hearts[i].enabled = false;
			}
		}

		var transform1 = transform;
		var position = transform1.position;
		position = new Vector3(-startPos, position.y, position.z);
		transform1.position = position;
		StartCoroutine(PlayerSlideIn());
	}

	private void Update(){
		if(Alive){
			PlayerAliveLogic();
			if(GameManager.Instance.inGame){
				GameManager.Instance.points++;
			}
		} else{
			if(!_crashed){
				_crashed = true;
				Crashed();
			}
		}

		CheckHearts();
		WearOff();
	}

	public IEnumerator PlayerSlideOut(){
		GameManager.Instance.inGame = false;

		var pos = transform.position;
		pos.x = Mathf.MoveTowards(pos.x, startPos, Time.deltaTime * 6f);
		transform.position = pos;

		yield return null;
		if(transform.position.x < startPos){
			StartCoroutine(PlayerSlideOut());
		} else{
			OnPlayerSlidedOut();
		}
	}

	private IEnumerator PlayerSlideIn(){
		var pos = transform.position;

		pos.x = Mathf.MoveTowards(pos.x, mid, Time.deltaTime * 3f);
		transform.position = pos;

		yield return null;

		if(transform.position.x < mid){
			StartCoroutine(PlayerSlideIn());
		} else{
			OnPlayerSlidedIn();
		}
	}

	//called when slide in finishes
	private void OnPlayerSlidedIn(){
		GameManager.Instance.inGame = true;
	}

	//called when slide out finishes
	private void OnPlayerSlidedOut(){
		gameOverScreen.Play("GameOverPanel");
		UpgradeManager.Instance.SaveUpgrades(); //remove all earned coins
		FindObjectOfType<MovingRoad>().slowdown = true;
	}

	private void Crashed(){
		hitsLeft = -1;
		FindObjectOfType<MovingRoad>().slowdown = true;
		GameManager.Instance.inGame = false;

		gameOverScreen.Play("GameOverPanel");
		UpgradeManager.Instance.LoadUpgrades(); //remove all earned coins

		StartCoroutine(CrashRoutine());
	}

	private IEnumerator CrashRoutine(){
		var pos = transform.position;

		pos.x = Mathf.MoveTowards(pos.x, -startPos, Time.deltaTime * 5f * FindObjectOfType<MovingRoad>().currSpeed);
		transform.position = pos;

		yield return null;

		if(transform.position.x > -11f){
			StartCoroutine(CrashRoutine());
		} else{
			OnCrashed();
		}
	}

	//called when crash animation finishes
	private void OnCrashed(){
		Debug.Log("OnCrashed()");
	}

	private void PlayerAliveLogic(){
		if(Input.GetKeyDown(lineUp)){
			currLane--;
		}

		if(Input.GetKeyDown(lineDown)){
			currLane++;
		}

		CheckLineInBounds();

		var transform1 = transform;
		if(GameManager.DEBUG){
			if(Input.GetKeyDown(KeyCode.P)){
				var pos = transform1.position;
				pos.x = mid;
				transform1.position = pos;
				StartCoroutine(PlayerSlideOut());
			}

			if(Input.GetKeyDown(KeyCode.O)){
				var pos = transform.position;
				pos.x = -startPos;
				transform1.position = pos;
				StartCoroutine(PlayerSlideIn());
			}

			if(Input.GetKeyDown(KeyCode.I)){
				Crashed();
			}

			if(Input.GetKeyDown(KeyCode.KeypadPlus)){
				hitsLeft++;
			}

			if(Input.GetKeyDown(KeyCode.KeypadMinus)){
				hitsLeft--;
			}
		}

		var playerpos = transform.position;
		playerpos.y = Mathf.MoveTowards(transform1.position.y, lanes[currLane], Time.deltaTime * 9f);
		if(GameManager.Instance.points % 50 == 0){ //random car bump
			playerpos.y -= 0.05f;
		}

		transform.position = playerpos;
	}

	private void CheckLineInBounds(){
		if(currLane <= 0){
			currLane = 0;
		}

		if(currLane >= 4){
			currLane = 4;
		}
	}

	private void CheckHearts(){
		for(var i = 0; i < 5; i++){
			hearts[i].sprite = hitsLeft <= i ? lostHeart : heart;
		}
	}

	private void ClearEffects(){
		isImmortal = false;
		_enemyManager.RestoreEnemySpeed();
	}

	private void WearOff(){
		if(_duration > 0){
			_duration -= Time.deltaTime;
		} else if(_duration <= 0){
			_duration = 0;
			ClearEffects();
			_enemyManager.RestoreEnemySpeed();
		}
	}

	public void Slow(){
		ClearEffects();

		var effectLevel = UpgradeManager.Instance.upgradeLevelGrochowka;
		switch(effectLevel){
			case 0:
				_enemyManager.SpeedUpEnemies(1, 1f);
				break;

			case 1:
				_enemyManager.SpeedUpEnemies(2, 2f);
				break;

			case 2:
				_enemyManager.SpeedUpEnemies(3, 3f);
				break;
		}

		_duration = 3;
	}

	public void Immortal(){
		ClearEffects();

		isImmortal = true;

		var effectLevel = UpgradeManager.Instance.upgradeLevelSchabowy;
		switch(effectLevel){
			case 0:
				_duration = 1;
				break;

			case 1:
				_duration = 2;
				break;

			case 2:
				_duration = 3;
				break;
		}
	}

	public void Explosion(int effectID){
		ClearEffects();

		var effectLevel = 0;

		switch(effectID){
			case SloikEffectController.EFFECT_EXPLOSION_CIRCLE:
				effectLevel = UpgradeManager.Instance.upgradeLevelBigos;
				break;

			case SloikEffectController.EFFECT_EXPLOSION_LINES:
				effectLevel = UpgradeManager.Instance.upgradeLevelMeksyk;
				break;
		}

		switch(effectLevel){
			case 0:
				TakeHeart(1);
				break;

			case 1:
				TakeHeart(2);
				break;

			case 2:
				TakeHeart(3);
				break;
		}
	}

	public void Bird(){
		return; //no effect
	}

	public void LifeUp(){
		ClearEffects();

		var effectLevel = UpgradeManager.Instance.upgradeLevelLazanki;
		switch(effectLevel){
			case 0:
				AddHeart(1);
				break;

			case 1:
				AddHeart(2);
				break;

			case 2:
				AddHeart(3);
				break;
		}
	}

	public void InstaKill(){
		ClearEffects();
		TakeHeart(hitsLeft);
	}

	private void AddHeart(int number){
		hitsLeft += number;
	}

	public void TakeHeart(int number){
		if(!isImmortal){
			hitsLeft -= number;
		}
	}
}