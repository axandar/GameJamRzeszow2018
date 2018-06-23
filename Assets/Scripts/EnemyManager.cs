using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyControler> spawnedEnemies = new List<EnemyControler>();
    public GameObject enemyPrefab;
    public GameObject karetkaCar;

    public float speedUpValue = 0f;
    private float duration = 0f;
    private float enemyCooldown = 7f;
    private float karetkaCooldown = 24f;

    //spawns properties
    public float maxEnemyCooldown = 2f;

    public float minEnemyCooldown = 1f;
    public float maxKaretkaCooldown = 16f;
    public float minKaretkaCooldown = 10f;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public void SpawnKaretka(int lane)
    {
        if (lane > 5 || lane < 0)
        {
            return;
        }
        GameObject e = Instantiate(karetkaCar, new Vector3(-25f, lanes[lane], 0f), Quaternion.identity);
    }

    public void SpawnEnemy(int lane)
    {
        if (lane > 5 || lane < 0)
        {
            return;
        }

        //todo ustawianie zmiennych w enemyPrefab

        GameObject e = Instantiate(enemyPrefab, new Vector3(-25f, lanes[lane], 0f), Quaternion.identity);
        spawnedEnemies.Add(e.GetComponent<EnemyControler>());
    }

    private void Update()
    {
        WearOff();

        EnemySpawnCounter();
        KaretkaSpawnCounter();
    }

    private void WearOff()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else if (duration < 0)
        {
            duration = 0;
            speedUpValue = 0;
        }
    }

    private void EnemySpawnCounter()
    {
        if (enemyCooldown > 0)
        {
            enemyCooldown -= Time.deltaTime;
        }
        else if (enemyCooldown < 0)
        {
            SpawnEnemy(DrawLineNumber());
            enemyCooldown = UnityEngine.Random.Range(minEnemyCooldown, maxEnemyCooldown);
        }
    }

    private void KaretkaSpawnCounter()
    {
        if (karetkaCooldown > 0)
        {
            karetkaCooldown -= Time.deltaTime;
        }
        else if (karetkaCooldown < 0)
        {
            SpawnKaretka(DrawLineNumber());
            karetkaCooldown = UnityEngine.Random.Range(minKaretkaCooldown, maxKaretkaCooldown);
        }
    }

    private int DrawLineNumber()
    {
        return UnityEngine.Random.Range(0, 5);
    }

    public void SpeedUpEnemies(int addedSpeedValue, float wearOffValue)
    {
        speedUpValue = addedSpeedValue;
        duration = wearOffValue;
    }

    public void RestoreEnemySpeed()
    {
        speedUpValue = 0;
        duration = 0;
    }

    public void RemoveEnemyFromList(EnemyControler enemyControler)
    {
        spawnedEnemies.Remove(enemyControler);
    }
}