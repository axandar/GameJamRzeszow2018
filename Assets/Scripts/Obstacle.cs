using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = _rb ?? GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _rb.MovePosition(new Vector3(transform.position.x - FindObjectOfType<MovingRoad>()._currSpeed * Time.deltaTime * speed, transform.position.y, 0f));
        if (transform.position.x < -100f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        int layer = col.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Player")){
            col.GetComponent<PlayerCar>().TakeHeart(1);
            col.GetComponent<PlayerCar>().Immortal();
        }else if(layer == LayerMask.NameToLayer("Enemy")){
            col.GetComponent<EnemyControler>().Kill();
            Destroy(gameObject);
        }
    }
}