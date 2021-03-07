using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy{
	public class EnemyManagerMonoBehaviour : MonoBehaviour{

		[SerializeField] private GameManager gameManager;
		public GameObject enemyPrefab;
		public GameObject karetkaCar;

		private EnemyManager _enemyManager;


		private void Start(){
			_enemyManager = gameManager.EnemyManager;
		}

		private void Update(){
			if(!gameManager.inGame){
				return;
			}
			_enemyManager.Tick(InstantiateEnemy, InstantiateKaretka);
		}
		
		private static int DrawLineNumber(){
			return Random.Range(0, 5);
		}
		
		private EnemyController InstantiateKaretka(){
			return Instantiate(karetkaCar, _enemyManager.GenerateEnemySpawnVector(
						DrawLineNumber()), Quaternion.identity)
				.GetComponent<EnemyController>();
		}

		private EnemyController InstantiateEnemy(){
			return Instantiate(enemyPrefab, 
				_enemyManager.GenerateEnemySpawnVector(
					DrawLineNumber()), Quaternion.identity)
				.GetComponent<EnemyController>();
		}
	}
}