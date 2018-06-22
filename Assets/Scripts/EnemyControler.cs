using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour {
    public float speed;
    public int life;
    public bool isDead = false;
    public GameObject roadLine;
    public int coinValue = 1;

    private new Transform transform;
    private UpgradeManager upgradeManager;
    private int screenWidth;

    //Effects
    private float effectWearOff = 0f;
    private bool isImmortal = false;
    private int slowDownValue = 0;


	// Use this for initialization
	void Start () {
        upgradeManager = UpgradeManager.instance;

        transform = GetComponent<Transform>();
        SetStartingPosition();
	}

    private void SetStartingPosition() {
        float yPosition = roadLine.transform.GetPositionY();
        Vector3 vec = new Vector3(1, yPosition, 1);
        transform.Translate(vec);
    }
	
	// Update is called once per frame
	void Update () {
        DriveForward();
        EffectWearOff();
    }

    private void DriveForward() {
        Vector3 vec = new Vector3(speed-slowDownValue, 0, 0);
        transform.Translate(vec * Time.deltaTime);
    }

    void EffectWearOff() {
        if(effectWearOff < 0) {
            effectWearOff = 0;
            ClearEffects();
        } else if(effectWearOff > 0) {
            effectWearOff -= Time.deltaTime;
        }
    }

    public void Kill() {
        if(!isImmortal) {
            isDead = true;
            //todo death animation
            upgradeManager.coins += 1;
        }
    }

    private void ClearEffects() {
        isImmortal = false;
        slowDownValue = 0;
    }

    public void Slow() {
        ClearEffects();

        int effectLevel = upgradeManager.upgradeLivesLevel;
        switch(effectLevel) {
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

    public void Immortal() {
        ClearEffects();

        isImmortal = true;

        int effectLevel = upgradeManager.upgradeLivesLevel;
        switch(effectLevel) {
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

    public void Explosion() {
        ClearEffects();
        Kill();
    }

    public void Bird() {
        ClearEffects();
        Kill();
    }

    public void LifeUp() {
        return;// no effect
    }

    public void InstatKill() {
        ClearEffects();
        Kill();
    }
}
