using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChodnikDetail : MonoBehaviour
{
    public float speed = 200f;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(transform.position.x - FindObjectOfType<MovingRoad>()._currSpeed * Time.deltaTime * speed, transform.position.y, 0f);

        if (transform.position.x < -Screen.width - 200f)
        {
            Destroy(gameObject);
        }
    }
}