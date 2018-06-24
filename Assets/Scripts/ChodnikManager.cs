using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChodnikManager : MonoBehaviour
{
    public Transform detailParent;
    public GameObject[] details;

    public float minCooldownBetweenDetails = 1f;
    public float maxCooldownBetweenDetails = 6f;
    private float _cooldown;

    private void Start()
    {
        _cooldown = 10f;
    }

    private void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
        else
        {
            _cooldown = UnityEngine.Random.Range(minCooldownBetweenDetails, maxCooldownBetweenDetails);

            if (GameManager.instance.inGame)
            {
                SpawnRandomDetail();
            }
        }
    }

    private void SpawnRandomDetail()
    {
        int d = UnityEngine.Random.Range(0, details.Length);
        GameObject detail = Instantiate(details[d], new Vector3(Screen.width + 100, 25, 0), Quaternion.identity, detailParent);
    }
}