﻿using System;
using System.Collections;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public bool alive { get { return hitsLeft > 0; } }
    public int hitsLeft = 3;
    public KeyCode lineUp = KeyCode.W;
    public KeyCode lineDown = KeyCode.S;
    private float mid = 0.25f;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public int currLane = 2;

    public void Start()
    {
        hitsLeft = UpgradeManager.instance.playerLives + UpgradeManager.instance.upgradeLivesLevel;

        transform.position = new Vector3(-15f, transform.position.y, transform.position.z);
        StartCoroutine(PlayerSlideIn());
    }

    public IEnumerator PlayerSlideOut()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, 15f, Time.deltaTime * 6f);
        transform.position = pos;

        yield return null;
        if (transform.position.x < 15f)
        {
            StartCoroutine(PlayerSlideOut());
        }
        else
        {
            OnPlayerSlidedOut();
        }
    }

    public IEnumerator PlayerSlideIn()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(pos.x, mid, Time.deltaTime * 3f);
        transform.position = pos;

        yield return null;

        if (transform.position.x < mid)
        {
            StartCoroutine(PlayerSlideIn());
        }
        else
        {
            OnPlayerSlidedIn();
        }
    }

    //called when slide in finishes
    private void OnPlayerSlidedIn()
    {
        Debug.Log("OnPlayerSlidedIn()");
    }

    //called when slide out finishes
    private void OnPlayerSlidedOut()
    {
        Debug.Log("OnPlayerSlidedOut()");
    }

    public void Crashed()
    {
        hitsLeft = -1;
        FindObjectOfType<MovingRoad>().slowdown = true;
        StartCoroutine(CrashRoutine());
    }

    public IEnumerator CrashRoutine()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(pos.x, -15f, Time.deltaTime * 5f * FindObjectOfType<MovingRoad>()._currSpeed);
        transform.position = pos;

        yield return null;

        if (transform.position.x > -15f)
        {
            StartCoroutine(CrashRoutine());
        }
        else
        {
            OnCrashed();
        }
    }

    //called when crash animation finishes
    public void OnCrashed()
    {
        Debug.Log("OnCrashed()");
    }

    public void Update()
    {
        if (alive)
        {
            if (Input.GetKeyDown(lineUp))
            {
                currLane--;
            }
            if (Input.GetKeyDown(lineDown))
            {
                currLane++;
            }

            if (GameManager.instance.DEBUG)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    Vector3 pos = transform.position;
                    pos.x = mid;
                    transform.position = pos;
                    StartCoroutine(PlayerSlideOut());
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    Vector3 pos = transform.position;
                    pos.x = -15f;
                    transform.position = pos;
                    StartCoroutine(PlayerSlideIn());
                }

                if (Input.GetKeyDown(KeyCode.I))
                {
                    Crashed();
                }
            }
        }

        if (currLane <= 0)
        {
            currLane = 0;
        }

        if (currLane >= 4)
        {
            currLane = 4;
        }

        Vector3 playerpos = transform.position;
        playerpos.y = Mathf.MoveTowards(transform.position.y, lanes[currLane], Time.deltaTime * 9f);
        transform.position = playerpos;
    }

    public void Slow(int effectLevel) {

    }

    public void Immortal(int effectLevel) {

    }

    public void Explosion(int effectLevel) {

    }

    public void Bird(int effectLevel) {
        return;//no effect
    }

    public void LifeUp(int effectLevel) {

    }

    public void InstatKill(int effectLevel) {
        Crashed();
    }
}