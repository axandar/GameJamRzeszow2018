using System;
using System.Collections;
using Enemy;
using UnityEngine;

namespace Player{
	public class PlayerCar{
		private readonly GameManager _gameManager;
		private readonly EnemyManager _enemyManager;

		private float _duration;
		private bool _isImmortal;

		public bool IsAlive{ get; private set; }
		public int HitsLeft{ get; private set; }
		public int CurrentLane{ get; private set; }
		public float[] Lanes{ get; }

		public PlayerCar(GameManager gameManager){
			_gameManager = gameManager;
			_enemyManager = gameManager.EnemyManager;

			Lanes = new[]{5.25f, 3.1f, 1f, -1.1f, -3.25f};
			HitsLeft = 3;
			IsAlive = true;
			CurrentLane = 2;
		}

		public void Initialize(){
			_gameManager.Points = 0;
			var upgradeManager = _gameManager.UpgradeManager;
			HitsLeft = upgradeManager.playerLives + upgradeManager.upgradeLivesLevel;
		}

		public static void SetStartingPosition(Transform transform, float startPos){
			var position = transform.position;
			var vector = new Vector3(-startPos, position.y, position.z);
			transform.position = vector;
		}

		public void Tick(){
			if(!_gameManager.InGame){
				return;
			}

			IsAlive = HitsLeft > 0;
			if(IsAlive){
				_gameManager.Points++;
			} else{
				PlayerCrashed();
			}

			WearOff();
		}

		// ReSharper disable once MemberCanBeMadeStatic.Global
		public IEnumerator PlayerSlide(Transform transform, float target, float speed, Action callback){
			while(transform.position.x < target){
				var pos = transform.position;

				pos.x = Mathf.MoveTowards(pos.x, target, Time.deltaTime * speed);
				transform.position = pos;
				yield return null;
			}

			callback?.Invoke();
		}

		private void PlayerCrashed(){
			_gameManager.InGame = false;
		}

		public void MovePlayerByXLanes(int numberOfLanes){
			CurrentLane += numberOfLanes;
			if(CurrentLane <= 0){
				CurrentLane = 0;
			}

			if(CurrentLane > Lanes.Length + 1){
				CurrentLane = Lanes.Length + 1;
			}
		}

		private void WearOff(){
			if(_duration > 0){
				_duration -= Time.deltaTime;
			} else if(_duration <= 0){
				_duration = 0;
				ClearEffects();
			}
		}

		public void Slow(){
			var effectLevel = _gameManager.UpgradeManager.upgradeLevelGrochowka;
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
			_isImmortal = true;

			var effectLevel = _gameManager.UpgradeManager.upgradeLevelSchabowy;
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
			var effectLevel = 0;

			switch(effectID){
				case SloikEffectController.EFFECT_EXPLOSION_CIRCLE:
					effectLevel = _gameManager.UpgradeManager.upgradeLevelBigos;
					break;

				case SloikEffectController.EFFECT_EXPLOSION_LINES:
					effectLevel = _gameManager.UpgradeManager.upgradeLevelMeksyk;
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

		public void LifeUp(){
			var effectLevel = _gameManager.UpgradeManager.upgradeLevelLazanki;
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
			TakeHeart(HitsLeft);
		}

		private void ClearEffects(){
			_isImmortal = false;
		}

		private void AddHeart(int number){
			HitsLeft += number;
		}

		public void TakeHeart(int number){
			if(!_isImmortal){
				HitsLeft -= number;
			}
		}
	}
}