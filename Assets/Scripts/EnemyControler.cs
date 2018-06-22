using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour {
    public float speed;
    public int life;
    public bool isDead = false;
    public int lineNumber;
    public GameObject roadLine;

    private new Transform transform;
    private int screenWidth;



	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
        //y
        float yPosition = roadLine.transform.GetPositionY();
        Debug.Log(yPosition);
        Vector3 vec = new Vector3(1, yPosition, 1);
        transform.Translate(vec);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vec = new Vector3(speed, 0, 0);
        transform.Translate(vec * Time.deltaTime);
    }

    public void Kill() {
        isDead = true;
    }

    public void Slow(int effectLevel) {

    }

    public void Immortal(int effectLevel) {

    }

    public void Explosion(int effectLevel) {

    }

    public void Bird(int effectLevel) {

    }

    public void LifeUp(int effectLevel) {
        return;// no effect
    }

    public void InstatKill(int effectLevel) {
        Kill();
    }
}
