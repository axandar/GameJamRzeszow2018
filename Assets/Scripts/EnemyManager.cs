using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyControler> spawnedEnemies = new List<EnemyControler>();

    public GameObject enemyPrefab;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public void SpawnEnemy()
    {
    }
}