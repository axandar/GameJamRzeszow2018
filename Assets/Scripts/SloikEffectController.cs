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

    public int Effect;
    public float Duration;
    public int EffectLevel;

	// Use this for initialization
	void Start () {
		//todo nadawac wielkosc collidera i sprite'a
	}
	
	// Update is called once per frame
	void Update () {
        EffectWearOff();
	}

    void EffectWearOff() {
        Duration -= Time.deltaTime;

        if(Duration <= 0) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.name == "Ałto") {
            EnemyControler enemyController = gameObject.GetComponent<EnemyControler>();
            OnEnemyContact(enemyController);
        } else {
            PlayerCar playerControler = gameObject.GetComponent<PlayerCar>();
            OnPlayerContact(playerControler);
        }
    }
    
    void OnEnemyContact(EnemyControler enemyControler) {
        switch(Effect) {
            case EFFECT_SLOW:
                enemyControler.Slow(EffectLevel);
                break;
            case EFFECT_BIRD:
                enemyControler.Bird(EffectLevel);
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                enemyControler.Explosion(EffectLevel);
                break;
            case EFFECT_IMMORTAL:
                enemyControler.Immortal(EffectLevel);
                break;
            case EFFECT_RANDOM:
                Effect = GetRandomEffect();
                OnEnemyContact(enemyControler);
                break;
            case EFFECT_LIFE_UP:
                enemyControler.LifeUp(EffectLevel);
                break;
            case EFFECT_INSTANT_KILL:
                enemyControler.InstatKill(EffectLevel);
                break;
            case EFFECT_EXPLOSION_LINES:
                enemyControler.Explosion(EffectLevel);
                break;
        }
    }

    void OnPlayerContact(PlayerCar playerController) {
        switch(Effect) {
            case EFFECT_SLOW:
                playerController.Slow(EffectLevel);
                break;
            case EFFECT_BIRD:
                playerController.Bird(EffectLevel);
                break;
            case EFFECT_EXPLOSION_CIRCLE:
                playerController.Explosion(EffectLevel);
                break;
            case EFFECT_IMMORTAL:
                playerController.Immortal(EffectLevel);
                break;
            case EFFECT_RANDOM:
                Effect = GetRandomEffect();
                OnPlayerContact(playerController);
                break;
            case EFFECT_LIFE_UP:
                playerController.LifeUp(EffectLevel);
                break;
            case EFFECT_INSTANT_KILL:
                playerController.InstatKill(EffectLevel);
                break;
            case EFFECT_EXPLOSION_LINES:
                playerController.Explosion(EffectLevel);
                break;
        }
    }

    private int GetRandomEffect() {
        return 3;// nie moze zwracac EFFECT_RANDOM
    }
}
