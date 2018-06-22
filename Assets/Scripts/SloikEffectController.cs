using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloikEffectController : MonoBehaviour {

    public const int EFFECT_SLOW = 1;
    public const int EFFECT_BIRD = 2;
    public const int EFFECT_EXPLOSION = 3;
    public const int EFFECT_IMMORTAL = 4;
    public const int EFFECT_RANDOM = 5;

    public int Effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision col) {
        EnemyControler enemyController = (EnemyControler)gameObject.GetComponent(typeof(EnemyControler));
        OnEnemyContact(enemyController);
    }
    
    void OnEnemyContact(EnemyControler enemyControler) {
        switch(Effect) {
            case EFFECT_SLOW:
                enemyControler.Slow();
                break;
            case EFFECT_BIRD:
                enemyControler.Bird();
                break;
            case EFFECT_EXPLOSION:
                enemyControler.Explosion();
                break;
            case EFFECT_IMMORTAL:
                enemyControler.Immortal();
                break;
            case EFFECT_RANDOM:
                Effect = GetRandomEffect();
                OnEnemyContact(enemyControler);
                break;
        }
    }

    private int GetRandomEffect() {
        return 3;// nie moze zwracac EFFECT_RANDOM
    }
}
