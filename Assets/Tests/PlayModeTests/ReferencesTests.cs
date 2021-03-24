using System.Collections;
using Enemy;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests{
	public class ReferencesTests{

		[OneTimeSetUp]
		public void LoadScene(){
			SceneManager.LoadScene("RoadGame");
		}

		[UnityTest]
		public IEnumerator CheckObstacleManagerReferences(){
			var obstacleManager = Object.FindObjectOfType<ObstacleManager>();
			yield return null;
			Assert.NotNull(obstacleManager.GameManager);
			Assert.NotNull(obstacleManager.RoadWorks);
			Assert.NotNull(obstacleManager.NarrowRoadDown);
			Assert.NotNull(obstacleManager.NarrowRoadUp);
			Assert.Greater(obstacleManager.MAXCooldownBetweenObstacles, 
				obstacleManager.MINCooldownBetweenObstacles);
		}
		
		[UnityTest]
		public IEnumerator CheckEnemyManagerReferences(){
			var enemyManager = Object.FindObjectOfType<EnemyManagerMonoBehaviour>();
			yield return null;
			Assert.NotNull(enemyManager.GameManager);
			Assert.NotNull(enemyManager.EnemyPrefab);
			Assert.NotNull(enemyManager.KaretkaCar);
		}
	}
}