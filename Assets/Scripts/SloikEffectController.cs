using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloikEffectController : MonoBehaviour {

    public const int EFFECT_SLOW = 1;
    public const int EFFECT_BIRD = 0;
    public const int EFFECT_EXPLOSION_CIRCLE = 2;
    public const int EFFECT_IMMORTAL = 3;
    public const int EFFECT_RANDOM = 4;
    public const int EFFECT_LIFE_UP = 5;
    public const int EFFECT_INSTANT_KILL = 6;
    public const int EFFECT_EXPLOSION_LINES = 7;

    public const int TYPE_CIRCLE = 1;
    public const int TYPE_LINE = 2;

    public float roadSpeed = 5f;
    public int effect;
    public float duration = 0;

    public Sprite effectGolabki;     //0 type
    public Sprite effectGrochowka;   //1
    public Sprite effectBigos;       //2
    public Sprite effectSchabowy;    //3
    public Sprite effectLazanki;     //5
    public Sprite effectParowki;     //6
    public Sprite effectMeksyk;      //7

    private Rigidbody2D rigidBody;
<<<<<<< HEAD
=======
    private SpriteRenderer spriteRendere;
    private bool isBird = false;
>>>>>>> Michal

    private void Awake() {
        rigidBody = rigidBody ?? GetComponent<Rigidbody2D>();
        spriteRendere = spriteRendere ?? GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        EffectWearOff();
	}

    private void EffectWearOff() {
        if(duration > 0) {
            duration -= Time.deltaTime;
        }

        if(duration < 0) {
            Debug.Log("Effect ended");
            Destroy(gameObject);
        }
    }

    private void MoveWithRoad() {
        rigidBody.MovePosition(new Vector3(
            transform.position.x - FindObjectOfType<MovingRoad>()._currSpeed * Time.deltaTime * roadSpeed, 
            transform.position.y,
            0f));
        if(transform.position.x < -100f) {
            Destroy(gameObject);
        }
    }

    public void SetSloikEffect(int sloikEffect) {
        effect = sloikEffect;
        duration = 1f;

        switch(effect) {
            case EFFECT_SLOW:
                SetSprite(effectGrochowka);
                SetSize(1f, 1f);
                break;
            case EFFECT_BIRD:
<<<<<<< HEAD
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                SetColliderSize(1, 1, TYPE_LINE);
=======
                SetSprite(effectGolabki);
                SetSize(1f, 1f);
                isBird = true;
                float rotation = UnityEngine.Random.Range(1, UpgradeManager.instance.upgradeLevelGolabki-1);
                rotation = 360f / rotation;
                transform.Rotate(new Vector3(0, 0, 45f));
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                SetSprite(effectBigos);
                SetSize(1f, 1f);
>>>>>>> Michal
                break;
            case EFFECT_IMMORTAL:
                SetSprite(effectSchabowy);
                SetSize(1f, 1f);
                break;
            case EFFECT_RANDOM:
                SetSloikEffect(GetRandomEffect());
                break;
            case EFFECT_LIFE_UP:
                SetSprite(effectLazanki);
                SetSize(1f, 1f);
                break;
            case EFFECT_INSTANT_KILL:
                SetSprite(effectParowki);
                SetSize(1f, 1f);
                break;
            case EFFECT_EXPLOSION_LINES:
                SetSprite(effectMeksyk);
                SetSize(1f, 1f);
                break;
        }
    }

    //dla kola bierze pod uwage tylko wartosc X
    private void SetSize(float x, float y) {
        transform.localScale = new Vector3(10, 10, 1);
        PolygonCollider2D collider2D = gameObject.AddComponent<PolygonCollider2D>();
        collider2D.isTrigger = true;
    }

    private void SetSprite(Sprite sprite) {
        spriteRendere.sprite = sprite;
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name != "Ałto") {
            EnemyControler enemyController = gameObject.GetComponent<EnemyControler>();
            OnEnemyContact(enemyController);
        } else {
            PlayerCar playerControler = gameObject.GetComponent<PlayerCar>();
            OnPlayerContact(playerControler);
        }
    }
    
    void OnEnemyContact(EnemyControler enemyControler) {
        switch(effect) {
            case EFFECT_SLOW:
                enemyControler.Slow();
                break;
            case EFFECT_BIRD:
                enemyControler.Bird();
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                enemyControler.Explosion();
                break;
            case EFFECT_IMMORTAL:
                enemyControler.Immortal();
                break;
            case EFFECT_LIFE_UP:
                enemyControler.LifeUp();
                break;
            case EFFECT_INSTANT_KILL:
                enemyControler.InstatKill();
                break;
            case EFFECT_EXPLOSION_LINES:
                enemyControler.Explosion();
                break;
        }
    }

    void OnPlayerContact(PlayerCar playerController) {
        switch(effect) {
            case EFFECT_SLOW:
                playerController.Slow();
                break;
            case EFFECT_BIRD:
                playerController.Bird();
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                playerController.Explosion(effect);
                break;
            case EFFECT_IMMORTAL:
                playerController.Immortal();
                break;
            case EFFECT_LIFE_UP:
                playerController.LifeUp();
                break;
            case EFFECT_INSTANT_KILL:
                playerController.InstatKill();
                break;
            case EFFECT_EXPLOSION_LINES:
                playerController.Explosion(effect);
                break;
        }
    }

    private int GetRandomEffect() {
<<<<<<< HEAD

        return (int)(Random.Range(0.0f, 7.0f)*10);
=======
        int number = UnityEngine.Random.Range(0, 8);
        if(number == EFFECT_RANDOM) {
            return GetRandomEffect();
        }
        return number;
>>>>>>> Michal
    }
}
