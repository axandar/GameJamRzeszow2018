using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool DEBUG { get { return Application.platform == RuntimePlatform.WindowsEditor; } }
    public long points = 0;
    public bool inGame = false;
    public float timer = 120f;
    public float timerStart = 120f;

    protected override void Awake()
    {
        base.Awake();
        timerStart = timer;
    }

    private void Update()
    {
        if (inGame)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0f;
                StartCoroutine(FindObjectOfType<PlayerCar>().PlayerSlideOut());
            }
        }
    }
}