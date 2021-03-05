using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour{
	public List<EnemyControler> spawnedEnemies = new List<EnemyControler>();
	public List<long> killPoints = new List<long>();
	public GameObject enemyPrefab;
	public GameObject karetkaCar;

	public float speedUpValue;
	private float _duration;
	private float _enemyCooldown = 7f;
	private float _karetkaCooldown = 14f;

	//spawns properties
	public float maxEnemyCooldown = 2f;

	public float minEnemyCooldown = 1f;
	public float maxKaretkaCooldown = 16f;
	public float minKaretkaCooldown = 10f;

	public float[] lanes ={5.25f, 3.1f, 1f, -1.1f, -3.25f};

	private void SpawnKaretka(int lane){
		if(lane > 5 || lane < 0){
			return;
		}

		Instantiate(karetkaCar, new Vector3(-25f, lanes[lane], 0f), Quaternion.identity);
	}

	public void SpawnEnemy(int lane){
		if(lane > 5 || lane < 0){
			return;
		}

		//todo ustawianie zmiennych w enemyPrefab

		var e = Instantiate(enemyPrefab, new Vector3(-25f, lanes[lane], 0f), Quaternion.identity);
		spawnedEnemies.Add(e.GetComponent<EnemyControler>());
	}

	private void Update(){
		WearOff();

		if(!GameManager.Instance.inGame){
			return;
		}

		EnemySpawnCounter();
		KaretkaSpawnCounter();
	}

	private void WearOff(){
		if(_duration > 0){
			_duration -= Time.deltaTime;
		} else if(_duration < 0){
			_duration = 0;
			speedUpValue = 0;
		}
	}

	private void EnemySpawnCounter(){
		if(_enemyCooldown > 0){
			_enemyCooldown -= Time.deltaTime;
		} else if(_enemyCooldown < 0){
			SpawnEnemy(DrawLineNumber());
			_enemyCooldown = Random.Range(minEnemyCooldown, maxEnemyCooldown);
		}
	}

	private void KaretkaSpawnCounter(){
		if(_karetkaCooldown > 0){
			_karetkaCooldown -= Time.deltaTime;
		} else if(_karetkaCooldown < 0){
			SpawnKaretka(DrawLineNumber());
			_karetkaCooldown = Random.Range(minKaretkaCooldown, maxKaretkaCooldown);
		}
	}

	private int DrawLineNumber(){
		return Random.Range(0, 5);
	}

	public void SpeedUpEnemies(int addedSpeedValue, float wearOffValue){
		speedUpValue = addedSpeedValue;
		_duration = wearOffValue;
	}

	public void RestoreEnemySpeed(){
		speedUpValue = 0;
		_duration = 0;
	}

	public void RemoveEnemyFromList(EnemyControler enemyControler){
		spawnedEnemies.Remove(enemyControler);
	}
}