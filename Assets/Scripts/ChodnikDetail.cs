using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChodnikDetail : MonoBehaviour{
	public float speed = 200f;

	// Update is called once per frame
	private void Update(){
		var position = transform.position;
		position = new Vector3(calculateXPosition(position), position.y, 0f);
		transform.position = position;
		
		if(transform.position.x < -Screen.width - 200f){
			Destroy(gameObject);
		}
	}

	private float calculateXPosition(Vector3 position){
		return position.x - FindObjectOfType<MovingRoad>().currSpeed * Time.deltaTime * speed;
	}
}