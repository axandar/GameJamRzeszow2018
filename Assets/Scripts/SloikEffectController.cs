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

    //EFFECT_BIRD no effect on player
    void OnContact() {
        switch(Effect) {
            case EFFECT_SLOW:
                break;
            case EFFECT_BIRD:
                break;
            case EFFECT_EXPLOSION:
                break;
            case EFFECT_IMMORTAL:
                break;
            case EFFECT_RANDOM:
                Effect = GetRandomEffect();
                OnContact();
                break;
        }
    }

    private int GetRandomEffect() {
        return 3;// nie moze zwracac EFFECT_RANDOM
    }
}
