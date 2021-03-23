using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;

public class Obstacle : MonoBehaviour{
	[SerializeField] private float speed = 5f;
	private Rigidbody2D _rb;
	private PlayerCar _playerCar;

	private void Start(){
		_rb = GetComponent<Rigidbody2D>();
	}

	public void Initialize(PlayerCar playerCar){
		_playerCar = playerCar;
	}

	// Update is called once per frame
	private void Update(){
		var position = transform.position;
		_rb.MovePosition(new Vector3(position.x - speed * Time.deltaTime * speed,
			position.y, 0f));
		if(transform.position.x < -100f){
			Destroy(gameObject);
		}
	}
	
	private void OnTriggerEnter2D(Collider2D col){
		var layer = col.gameObject.layer;
		if(layer == LayerMask.NameToLayer("Player")){
			_playerCar.TakeHeart(1);
			_playerCar.Immortal();
			Destroy(gameObject);
		} else if(layer == LayerMask.NameToLayer("Enemy")){
			col.GetComponent<EnemyController>().Kill();
			Destroy(gameObject);
		}
	}
}