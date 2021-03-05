using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour{
	private SpriteRenderer _sr;
	public float speed = 3f;
	public float currSpeed;
	public bool slowdown;
	public bool slowup = true;
	public float slowTime = 2f;

	// Use this for initialization
	private void Start(){
		if(slowup){
			currSpeed = 0f;
		} else{
			currSpeed = speed;
		}

		_sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	private void Update(){
		if(slowup && currSpeed <= speed){
			currSpeed += Time.deltaTime * slowTime;
		}

		if(currSpeed >= speed){
			currSpeed = speed;
			slowup = false;
		}

		if(currSpeed <= 0f){
			currSpeed = 0f;
			slowdown = false;
		}

		if(slowdown && currSpeed > 0f){
			currSpeed -= Time.deltaTime * slowTime;
		}

		Vector2 offset = _sr.material.GetTextureOffset("_MainTex");
		offset.x += Time.deltaTime * currSpeed;
		_sr.material.SetTextureOffset("_MainTex", offset);
	}
}