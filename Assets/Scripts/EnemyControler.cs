using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float speed;
    public int life;
    public bool isDead = false;
    public GameObject roadLine;
    public int coinValue = 1;

    private int screenWidth;

    //Effects
    private float effectWearOff = 0f;

    private bool isImmortal = false;
    private int slowDownValue = 0;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = _rb ?? GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        DriveForward();
        EffectWearOff();

        if (transform.position.x > 15f)
        {
            FindObjectOfType<PlayerCar>().TakeHeart(1);
            FindObjectOfType<EnemyManager>().spawnedEnemies.Remove(this);
            Destroy(gameObject);
        }
    }

    private void DriveForward()
    {
        _rb.MovePosition(new Vector3(transform.position.x + FindObjectOfType<MovingRoad>()._currSpeed * Time.deltaTime * speed, transform.position.y, 0f));
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Ałto")
        {
            col.GetComponent<PlayerCar>().TakeHeart(1);
            FindObjectOfType<EnemyManager>().spawnedEnemies.Remove(this);
            Destroy(gameObject);
        }
        if (col.name != "Ałto" && col.name != "Słoik")
        {
            FindObjectOfType<EnemyManager>().spawnedEnemies.Remove(this);
            Destroy(gameObject);
        }
    }

    private void EffectWearOff()
    {
        if (effectWearOff < 0)
        {
            effectWearOff = 0;
            ClearEffects();
        }
        else if (effectWearOff > 0)
        {
            effectWearOff -= Time.deltaTime;
        }
    }

    public void Kill()
    {
        if (!isImmortal)
        {
            isDead = true;
            //todo death animation
            UpgradeManager.instance.AddCoins(1);
        }
    }

    private void ClearEffects()
    {
        isImmortal = false;
        slowDownValue = 0;
    }

    public void Slow()
    {
        ClearEffects();

        int effectLevel = UpgradeManager.instance.upgradeLivesLevel;
        switch (effectLevel)
        {
            case 1:
                slowDownValue = 1;
                break;

            case 2:
                slowDownValue = 2;
                break;

            case 3:
                slowDownValue = 3;
                break;
        }

        effectWearOff = 3;
    }

    public void Immortal()
    {
        ClearEffects();

        isImmortal = true;

        int effectLevel = UpgradeManager.instance.upgradeLivesLevel;
        switch (effectLevel)
        {
            case 1:
                effectWearOff = 1;
                break;

            case 2:
                effectWearOff = 2;
                break;

            case 3:
                effectWearOff = 3;
                break;
        }
    }

    public void Explosion()
    {
        ClearEffects();
        Kill();
    }

    public void Bird()
    {
        ClearEffects();
        Kill();
    }

    public void LifeUp()
    {
        return;// no effect
    }

    public void InstatKill()
    {
        ClearEffects();
        Kill();
    }
}