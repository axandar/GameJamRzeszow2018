using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour {

    private SpriteRenderer _sr;
    public float speed = 3f;

	// Use this for initialization
	void Start () {
        _sr = _sr ?? GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 offset = _sr.material.GetTextureOffset("_MainTex");
        offset.y += Time.deltaTime * speed;
        _sr.material.SetTextureOffset("_MainTex", offset);
	}
}
