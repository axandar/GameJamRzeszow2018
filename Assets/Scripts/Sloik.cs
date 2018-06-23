using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloik : MonoBehaviour
{
    public float rotateSpeed = 350f;
    public float flySpeed = 25f;

    public int type = 6;
    public Vector2 target = new Vector2();
    public Vector2 initialPos;

    private float _initialDistance;

    public Sprite sloikGolabki;     //0 type
    public Sprite sloikGrochowka;   //1
    public Sprite sloikBigos;       //2
    public Sprite sloikSchabowy;    //3
    public Sprite sloikMix;         //4
    public Sprite sloikLazanki;     //5
    public Sprite sloikParowki;     //6
    public Sprite sloikMeksyk;      //7

    private SpriteRenderer _sr;
    public GameObject sloikEffectPrefab;

    private void Awake()
    {
        _sr = _sr ?? GetComponent<SpriteRenderer>();
        _sr.sprite = sloikParowki;

        initialPos = transform.position;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));

        Vector2 pos = transform.position;
        pos = Vector2.MoveTowards(pos, target, Time.deltaTime * flySpeed);

        transform.position = pos;

        if ((Vector2)transform.position == target)
        {
            EagleHasLanded();
        }

        float dist = Vector2.Distance(transform.position, target);
        if (dist >= _initialDistance / 2f)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1.5f, Time.deltaTime),
                Mathf.Lerp(transform.localScale.y, 1.5f, Time.deltaTime),
                Mathf.Lerp(transform.localScale.z, 1.5f, Time.deltaTime));
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1f, Time.deltaTime),
                Mathf.Lerp(transform.localScale.y, 1f, Time.deltaTime),
                Mathf.Lerp(transform.localScale.z, 1f, Time.deltaTime));
        }
    }

    private void EagleHasLanded()
    {
        //spawn effect
        Debug.Log("Sloik effect spawn");
        Vector3 actualPosition = gameObject.transform.position;

<<<<<<< HEAD
        GameObject sloikEffectObject = Instantiate(sloikEffectPrefab, actualPosition, Quaternion.identity);
        SloikEffectController effectController = sloikEffectObject.GetComponent<SloikEffectController>();
        effectController.SetSloikEffect(type);
=======
        if(type == SloikEffectController.EFFECT_BIRD) {
            int birdsNumber = 0;
            switch(UpgradeManager.instance.upgradeLevelGolabki) {
                case 0:
                    birdsNumber = 2;
                    break;
                case 1:
                    birdsNumber = 3;
                    break;
                case 2:
                    birdsNumber = 4;
                    break;
            }
            Debug.Log(birdsNumber);
            for(int i = 0; i < birdsNumber; i++) {
                GameObject sloikEffectObject = Instantiate(sloikEffectPrefab, actualPosition, Quaternion.identity);
                SloikEffectController effectController = sloikEffectObject.GetComponent<SloikEffectController>();
                effectController.SetSloikEffect(type);
            }
        } else {
            GameObject sloikEffectObject = Instantiate(sloikEffectPrefab, actualPosition, Quaternion.identity);
            SloikEffectController effectController = sloikEffectObject.GetComponent<SloikEffectController>();
            effectController.SetSloikEffect(type);
        }
>>>>>>> Michal

        Destroy(gameObject);
    }

    public void SetTarget(Vector2 target)
    {
        this.target = target;
        _initialDistance = Vector2.Distance(target, initialPos);
        Debug.DrawLine(transform.position, target, Color.red, 15f);
    }

    public void SetType(int type)
    {
        this.type = type;
        _sr.sprite = GetSloikFromType(type);
    }

    private Sprite GetSloikFromType(int type)
    {
        switch (type)
        {
            case 0:
                return sloikGolabki;

            case 1:
                return sloikGrochowka;

            case 2:
                return sloikBigos;

            case 3:
                return sloikSchabowy;

            case 4:
                return sloikMix;

            case 5:
                return sloikLazanki;

            case 6:
                return sloikParowki;

            case 7:
                return sloikMeksyk;

            default:
                return null;
        }
    }
}