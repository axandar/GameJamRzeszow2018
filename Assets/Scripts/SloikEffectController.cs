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

    public 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision col) {
        OnContact(col.gameObject);
    }
    
    void OnContact(GameObject gameObject) {
        switch(Effect) {
            case EFFECT_SLOW:
                break;
            case EFFECT_BIRD:
                if(gameObject.name != "Ałto") {
                    Kill(gameObject);
                }
                break;
            case EFFECT_EXPLOSION:
                Kill(gameObject);
                break;
            case EFFECT_IMMORTAL:
                break;
            case EFFECT_RANDOM:
                Effect = GetRandomEffect();
                OnContact(gameObject);
                break;
        }
    }

    private int GetRandomEffect() {
        return 3;// nie moze zwracac EFFECT_RANDOM
    }

    private void Kill(GameObject gameObject) {
        Destroy(gameObject);
    }
}
