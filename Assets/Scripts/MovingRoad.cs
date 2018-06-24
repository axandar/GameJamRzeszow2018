using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour
{
    private SpriteRenderer _sr;
    public float speed = 3f;
    public float _currSpeed;
    public bool slowdown = false;
    public bool slowup = true;
    public float slowTime = 2f;

    // Use this for initialization
    private void Start()
    {
        if (slowup)
        {
            _currSpeed = 0f;
        }
        else
        {
            _currSpeed = speed;
        }

        _sr = _sr ?? GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (slowup && _currSpeed <= speed)
        {
            _currSpeed += Time.deltaTime * slowTime;
        }
        if (_currSpeed >= speed)
        {
            _currSpeed = speed;
            slowup = false;
        }
        if (_currSpeed <= 0f)
        {
            _currSpeed = 0f;
            slowdown = false;
        }

        if (slowdown && _currSpeed > 0f)
        {
            _currSpeed -= Time.deltaTime * slowTime;
        }

        Vector2 offset = _sr.material.GetTextureOffset("_MainTex");
        offset.x += Time.deltaTime * _currSpeed;
        _sr.material.SetTextureOffset("_MainTex", offset);
    }
}