using System.IO;
using System.Linq;
using Enemy;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests.EditModeTests{
	public class PrefabTests{

		[Test]
		public void CheckIfEnemyControllerHaveOneOrMoreSprites(){
			var files = Directory.GetFiles(Application.dataPath + @"/Prefabs",
				"*.prefab", SearchOption.AllDirectories);
			var prefabList = files
				.Select(file => file.Replace(Application.dataPath, ""))
				.Select(file => "Assets" + file)
				.Select(file => AssetDatabase.LoadAssetAtPath(file, typeof(GameObject)) 
					as GameObject)
				.ToList();

			var enemyControllers = prefabList
				.Select(prefab => prefab.GetComponent<EnemyController>())
				.Where(enemyController => enemyController != null);
			
			foreach(var enemyController in enemyControllers){
				Assert.Greater(enemyController.sprites.Length, 0);
			}
		}
	}
}