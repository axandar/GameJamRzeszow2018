using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloikEffectController : MonoBehaviour {

    public const int EFFECT_SLOW = 1;
    public const int EFFECT_BIRD = 2;
    public const int EFFECT_EXPLOSION_CIRCLE = 3;
    public const int EFFECT_IMMORTAL = 4;
    public const int EFFECT_RANDOM = 100;
    public const int EFFECT_LIFE_UP = 5;
    public const int EFFECT_INSTANT_KILL = 6;
    public const int EFFECT_EXPLOSION_LINES = 7;

    public const int TYPE_CIRCLE = 1;
    public const int TYPE_LINE = 2;

    public float roadSpeed = 5f;
    public int effect;
    public float duration = 0;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = rigidBody ?? GetComponent<Rigidbody2D>();
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
        duration = 2f;

        switch(effect) {
            case EFFECT_SLOW:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_BIRD:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_IMMORTAL:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_RANDOM:
                SetSloikEffect(GetRandomEffect());
                break;
            case EFFECT_LIFE_UP:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_INSTANT_KILL:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
            case EFFECT_EXPLOSION_LINES:
                SetColliderSize(1, 1, TYPE_LINE);
                break;
        }
    }

    //dla kola bierze pod uwage tylko wartosc X
    private void SetColliderSize(float x, float y, int type) {
        if(type == TYPE_CIRCLE) {
            CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = x;
            circleCollider.isTrigger = true;
        } else if(type == TYPE_LINE) {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(x, y);
            boxCollider.isTrigger = true;
        }
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

        return (int)(Random.Range(0.0f, 7.0f)*10);
    }
}
