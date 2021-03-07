using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class RoadBlock : MonoBehaviour{
	public float speed = 5f;
	private Rigidbody2D _rb;

	private void Awake(){
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update(){
		var position = transform.position;
		_rb.MovePosition(new Vector3(
			position.x - FindObjectOfType<MovingRoad>().currSpeed * Time.deltaTime * speed, 
			position.y, 0f));

		if(transform.position.x < -100f){
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D col){
		switch(col.name){
			case "Ałto":
				col.GetComponent<PlayerCar>().TakeHeart(1);
				col.GetComponent<PlayerCar>().Immortal();
				break;
			case "Enemy":
				//col.GetComponent<EnemyControler>().Kill();
				break;
		}

		//Destroy(gameObject);
	}
}