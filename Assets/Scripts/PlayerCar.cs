using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCar : MonoBehaviour
{
    public bool alive { get { return hitsLeft > 0; } }
    public int hitsLeft = 3;
    public KeyCode lineUp = KeyCode.W;
    public KeyCode lineDown = KeyCode.S;
    public Image[] hearts;
    public Sprite lostHeart;
    public Sprite heart;

    public Animator gameOverScreen;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public int currLane = 2;

    private float mid = 0.25f;
    private float startPos = 25f;
    private bool _crashed = false;

    //Effects
    private float duration = 0f;

    private EnemyManager enemyManager;
    public bool isImmortal = false;

    private void Start()
    {
        GameManager.instance.points = 0;
        enemyManager = FindObjectOfType<EnemyManager>();
        UpgradeManager.instance.LoadUpgrades();

        hitsLeft = UpgradeManager.instance.playerLives + UpgradeManager.instance.upgradeLivesLevel;

        for (int i = 0; i < 5; i++)
        {
            if (hitsLeft <= i)
            {
                hearts[i].enabled = false;
            }
        }

        transform.position = new Vector3(-startPos, transform.position.y, transform.position.z);
        StartCoroutine(PlayerSlideIn());
    }

    private void Update()
    {
        if (alive)
        {
            PlayerAliveLogic();
            if (GameManager.instance.inGame)
            {
                GameManager.instance.points++;
            }
        }
        else
        {
            if (!_crashed)
            {
                _crashed = true;
                Crashed();
            }
        }

        CheckHearts();
        WearOff();
    }

    public IEnumerator PlayerSlideOut()
    {
        GameManager.instance.inGame = false;

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, startPos, Time.deltaTime * 6f);
        transform.position = pos;

        yield return null;
        if (transform.position.x < startPos)
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
        GameManager.instance.inGame = true;
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
        GameManager.instance.inGame = false;

        StartCoroutine(CrashRoutine());
    }

    public IEnumerator CrashRoutine()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(pos.x, -startPos, Time.deltaTime * 5f * FindObjectOfType<MovingRoad>()._currSpeed);
        transform.position = pos;

        yield return null;

        if (transform.position.x > -11f)
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
        gameOverScreen.Play("GameOverPanel");
        UpgradeManager.instance.LoadUpgrades(); //remove all earned coins
    }

    private void PlayerAliveLogic()
    {
        if (Input.GetKeyDown(lineUp))
        {
            currLane--;
        }
        if (Input.GetKeyDown(lineDown))
        {
            currLane++;
        }

        CheckLineInBounds();

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
                pos.x = -startPos;
                transform.position = pos;
                StartCoroutine(PlayerSlideIn());
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                Crashed();
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                hitsLeft++;
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                hitsLeft--;
            }
        }

        Vector3 playerpos = transform.position;
        playerpos.y = Mathf.MoveTowards(transform.position.y, lanes[currLane], Time.deltaTime * 9f);
        if (GameManager.instance.points % 50 == 0) //random car bump
        {
            playerpos.y -= 0.05f;
        }
        transform.position = playerpos;
    }

    private void CheckLineInBounds()
    {
        if (currLane <= 0)
        {
            currLane = 0;
        }

        if (currLane >= 4)
        {
            currLane = 4;
        }
    }

    private void CheckHearts()
    {
        for (int i = 0; i < 5; i++)
        {
            if (hitsLeft <= i)
            {
                hearts[i].sprite = lostHeart;
            }
            else
            {
                hearts[i].sprite = heart;
            }
        }
    }

    private void ClearEffects()
    {
        //Debug.Log("Clear");
        isImmortal = false;
        enemyManager.RestoreEnemySpeed();
    }

    private void WearOff()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else if (duration <= 0)
        {
            duration = 0;
            ClearEffects();
            enemyManager.RestoreEnemySpeed();
        }
    }

    public void Slow()
    {
        ClearEffects();

        int effectLevel = UpgradeManager.instance.upgradeLevelGrochowka;
        switch (effectLevel)
        {
            case 1:
                enemyManager.SpeedUpEnemies(1, 1f);
                break;

            case 2:
                enemyManager.SpeedUpEnemies(2, 2f);
                break;

            case 3:
                enemyManager.SpeedUpEnemies(3, 3f);
                break;
        }

        duration = 3;
    }

    public void Immortal()
    {
        ClearEffects();

        isImmortal = true;

        int effectLevel = UpgradeManager.instance.upgradeLevelSchabowy;
        switch (effectLevel)
        {
            case 1:
                duration = 1;
                break;

            case 2:
                duration = 2;
                break;

            case 3:
                duration = 3;
                break;
        }
    }

    public void Explosion(int effectID)
    {
        ClearEffects();

        int effectLevel = 0;

        switch (effectID)
        {
            case SloikEffectController.EFFECT_EXPLOSION_CIRCLE:
                effectLevel = UpgradeManager.instance.upgradeLevelBigos;
                break;

            case SloikEffectController.EFFECT_EXPLOSION_LINES:
                effectLevel = UpgradeManager.instance.upgradeLevelMeksyk;
                break;
        }

        switch (effectLevel)
        {
            case 1:
                TakeHeart(1);
                break;

            case 2:
                TakeHeart(2);
                break;

            case 3:
                TakeHeart(3);
                break;
        }
    }

    public void Bird()
    {
        return;//no effect
    }

    public void LifeUp()
    {
        ClearEffects();

        int effectLevel = UpgradeManager.instance.upgradeLevelLazanki;
        switch (effectLevel)
        {
            case 1:
                AddHeart(1);
                break;

            case 2:
                AddHeart(2);
                break;

            case 3:
                AddHeart(3);
                break;
        }
    }

    public void InstatKill()
    {
        ClearEffects();
        TakeHeart(hitsLeft);
    }

    public void AddHeart(int number)
    {
        hitsLeft += number;
    }

    public void TakeHeart(int number)
    {
        if (!isImmortal)
        {
            hitsLeft -= number;
        }
    }
}