using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloikEffectController : MonoBehaviour {

    public const int EFFECT_SLOW = 1;
    public const int EFFECT_BIRD = 2;
    public const int EFFECT_EXPLOSION_CIRCLE = 3;
    public const int EFFECT_IMMORTAL = 4;
    public const int EFFECT_RANDOM = 5;
    public const int EFFECT_LIFE_UP = 6;
    public const int EFFECT_INSTANT_KILL = 7;
    public const int EFFECT_EXPLOSION_LINES = 8;

    public const int TYPE_CIRCLE = 1;
    public const int TYPE_LINE = 2;

    public float speed = 5f;
    public int effect;
    public float duration;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = rigidBody ?? GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        EffectWearOff();
	}

    private void EffectWearOff() {
        duration -= Time.deltaTime;

        if(duration <= 0) {
            Destroy(gameObject);
        }
    }

    private void MoveWithRoad() {
        rigidBody.MovePosition(new Vector3(
            transform.position.x - FindObjectOfType<MovingRoad>()._currSpeed * Time.deltaTime * speed, 
            transform.position.y,
            0f));
        if(transform.position.x < -100f) {
            Destroy(gameObject);
        }
    }

    //dla kola bierze pod uwage tylko wartosc X
    public void SetColliderSize(float x, float y, int type) {
        if(type == TYPE_CIRCLE) {
            CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = x;
        } else if(type == TYPE_LINE) {
            BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(x, y);
        }
    }

    void OnCollisionEnter(Collision col) {
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
            case EFFECT_RANDOM:
                effect = GetRandomEffect();
                OnEnemyContact(enemyControler);
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
            case EFFECT_RANDOM:
                effect = GetRandomEffect();
                OnPlayerContact(playerController);
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
        return (int)Random.Range(0.0f, 1.0f)*10;
    }
}
