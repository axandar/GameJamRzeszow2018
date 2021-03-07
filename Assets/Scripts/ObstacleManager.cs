using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	public GameObject narrowRoadUp; //0
	public GameObject narrowRoadDown; //1
	public GameObject roadWorks; //2

	public float[] lanes = new float[]{5.25f, 3.1f, 1f, -1.1f, -3.25f};

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
			_cooldown = UnityEngine.Random.Range(minCooldownBetweenObstacles, maxCooldownBetweenObstacles);

			if(gameManager.inGame){
				SpawnRandomObstacle();
			}
		}
	}

	private void SpawnRandomObstacle(){
		var lane = UnityEngine.Random.Range(0, 5);

		switch(UnityEngine.Random.Range(0, 3)){
			case 0:
				Instantiate(narrowRoadUp, new Vector3(25f, lanes[lane]), Quaternion.identity);
				break;

			case 1:
				Instantiate(narrowRoadDown, new Vector3(25f, lanes[lane]), Quaternion.identity);
				break;

			case 2:
				Instantiate(roadWorks, new Vector3(25f, lanes[lane]), Quaternion.identity);
				break;
		}
	}
}