using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyControler> spawnedEnemies = new List<EnemyControler>();

    public GameObject enemyPrefab;

    public GameObject narrowRoadUp;
    public GameObject narrowRoadDown;

    public GameObject roadWorks;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public void SpawnEnemy(int lane)
    {
        if (lane > 5 || lane < 0)
        {
            return;
        }

        GameObject e = Instantiate(enemyPrefab, new Vector3(-15f, lanes[lane], 0f), Quaternion.identity);
        spawnedEnemies.Add(e.GetComponent<EnemyControler>());
    }
}