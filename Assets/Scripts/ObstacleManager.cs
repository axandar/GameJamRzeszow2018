using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	public Obstacle narrowRoadUp; //0
	public Obstacle narrowRoadDown; //1
	public Obstacle roadWorks; //2

	public float minCooldownBetweenObstacles = 1f;
	public float maxCooldownBetweenObstacles = 6f;

	private float _cooldown;

	private void Start(){
		_cooldown = 10f;
	}

	private void Update(){
		if(_cooldown > 0f){
			_cooldown -= Time.deltaTime;
		} else{
			_cooldown = UnityEngine.Random.Range(minCooldownBetweenObstacles, 
				maxCooldownBetweenObstacles);

			if(gameManager.InGame){
				SpawnRandomObstacle();
			}
		}
	}

	private void SpawnRandomObstacle(){
		var lane = UnityEngine.Random.Range(0, 5);

		switch(UnityEngine.Random.Range(0, 3)){
			case 0:
				InstantiateObject(narrowRoadUp, lane);
				break;

			case 1:
				InstantiateObject(narrowRoadDown, lane);
				break;

			case 2:
				InstantiateObject(roadWorks, lane);
				break;
		}
	}

	private void InstantiateObject(Obstacle obstacle, int lane){
		var position = new Vector3(25f, gameManager.Lanes[lane]);
		var instance = Instantiate(obstacle, position, Quaternion.identity);
		instance.Initialize(gameManager.PlayerCar);
	}
}