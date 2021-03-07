using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy{
	public class EnemyManager{
		private readonly GameManager _gameManager;
		public List<EnemyController> spawnedEnemies = new List<EnemyController>();

		public float speedUpValue;
		private float _duration;
		private float _enemyCooldown = 7f;
		private float _karetkaCooldown = 14f;

		//spawns properties
		private float maxEnemyCooldown = 2f;

		private float minEnemyCooldown = 1f;
		private float maxKaretkaCooldown = 16f;
		private float minKaretkaCooldown = 10f;

		private float[] lanes ={5.25f, 3.1f, 1f, -1.1f, -3.25f};

		public EnemyManager(GameManager gameManager){
			_gameManager = gameManager;
		}

		public void Tick(Func<EnemyController> instantiateEnemy, Func<EnemyController> instantiateKaretka){
			WearOff();
			EnemySpawnCounter(instantiateEnemy);
			KaretkaSpawnCounter(instantiateKaretka);
		}

		public Vector3 GenerateEnemySpawnVector(int lane){
			return new Vector3(-25f, lanes[lane], 0f);
		}

		private void WearOff(){
			if(_duration > 0){
				_duration -= Time.deltaTime;
			} else if(_duration < 0){
				RestoreEnemySpeed();
			}
		}

		private void EnemySpawnCounter(Func<EnemyController> instantiateEnemy){
			//todo ustawianie zmiennych w enemyPrefab
			if(_enemyCooldown > 0){
				_enemyCooldown -= Time.deltaTime;
			} else if(_enemyCooldown < 0){
				var e = instantiateEnemy.Invoke();
				e.Initialize(_gameManager);
				spawnedEnemies.Add(e);
				_enemyCooldown = Random.Range(minEnemyCooldown, maxEnemyCooldown);
			}
		}

		private void KaretkaSpawnCounter(Func<EnemyController> instantiateKaretka){
			//todo ustawianie zmiennych w enemyPrefab
			if(_karetkaCooldown > 0){
				_karetkaCooldown -= Time.deltaTime;
			} else if(_karetkaCooldown < 0){
				var e = instantiateKaretka.Invoke();
				e.Initialize(_gameManager);
				spawnedEnemies.Add(e);
				_karetkaCooldown = Random.Range(minKaretkaCooldown, maxKaretkaCooldown);
			}
		}

		public void SpeedUpEnemies(int addedSpeedValue, float wearOffValue){
			speedUpValue = addedSpeedValue;
			_duration = wearOffValue;
		}

		public void RestoreEnemySpeed(){
			speedUpValue = 0;
			_duration = 0;
		}

		public void RemoveEnemyFromList(EnemyController enemyController){
			spawnedEnemies.Remove(enemyController);
		}
	}
}