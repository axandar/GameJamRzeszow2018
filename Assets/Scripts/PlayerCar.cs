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
    private float mid = 0.25f;
    private float startPos = 15f;

    public Image[] hearts;

    public Sprite lostHeart;
    public Sprite heart;

    public float[] lanes = new float[] {
        5.25f,
        3.1f,
        1f,
        -1.1f,
        -3.25f
    };

    public int currLane = 2;

    private bool _crashed = false;

    public void Start()
    {
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

    public IEnumerator PlayerSlideOut()
    {
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

        pos.x = Mathf.MoveTowards(pos.x, -startPos, Time.deltaTime * 5f * FindObjectOfType<MovingRoad>()._currSpeed);
        transform.position = pos;

        yield return null;

        if (transform.position.x > -startPos)
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
            PlayerAliveLogic();
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
    }

    private void PlayerAliveLogic() {
        if(Input.GetKeyDown(lineUp)) {
            currLane--;
        }
        if(Input.GetKeyDown(lineDown)) {
            currLane++;
        }

        CheckLineInBounds();

        if(GameManager.instance.DEBUG) {
            if(Input.GetKeyDown(KeyCode.P)) {
                Vector3 pos = transform.position;
                pos.x = mid;
                transform.position = pos;
                StartCoroutine(PlayerSlideOut());
            }

            if(Input.GetKeyDown(KeyCode.O)) {
                Vector3 pos = transform.position;
                pos.x = -startPos;
                transform.position = pos;
                StartCoroutine(PlayerSlideIn());
            }

            if(Input.GetKeyDown(KeyCode.I)) {
                Crashed();
            }

            if(Input.GetKeyDown(KeyCode.KeypadPlus)) {
                hitsLeft++;
            }

            if(Input.GetKeyDown(KeyCode.KeypadMinus)) {
                hitsLeft--;
            }
        }

        Vector3 playerpos = transform.position;
        playerpos.y = Mathf.MoveTowards(transform.position.y, lanes[currLane], Time.deltaTime * 9f);
        transform.position = playerpos;
    }

    private void CheckLineInBounds() {
        if(currLane <= 0) {
            currLane = 0;
        }

        if(currLane >= 4) {
            currLane = 4;
        }
    }

    private void CheckHearts() {
        for(int i = 0; i < 5; i++) {
            if(hitsLeft <= i) {
                hearts[i].sprite = lostHeart;
            } else {
                hearts[i].sprite = heart;
            }
        }
    }

    public void Slow()
    {
    }

    public void Immortal()
    {
    }

    public void Explosion()
    {
    }

    public void Bird()
    {
        return;//no effect
    }

    public void LifeUp()
    {
    }

    public void InstatKill()
    {
        Crashed();
    }
}