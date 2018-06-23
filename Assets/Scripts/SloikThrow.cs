using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SloikThrow : MonoBehaviour
{
    public GameObject sloikPrefab;
    public Outline[] sloikUI;

    public float cooldown = 1f;

    private float _cooldown = 0f;

    private Vector2 mousePos;
    public Image cldIndicator;

    public int currSloik = 6;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currSloik--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currSloik++;
        }

        if (currSloik < 0)
        {
            currSloik = 7;
        }
        if (currSloik > 7)
        {
            currSloik = 0;
        }

        for (int i = 0; i < sloikUI.Length; i++)
        {
            if (i == currSloik)
            {
                sloikUI[i].enabled = true;
            }
            else
            {
                sloikUI[i].enabled = false;
            }
        }

        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
            cldIndicator.fillAmount = _cooldown / cooldown;
        }
        if (Input.GetMouseButtonDown(0) && GameManager.instance.inGame && _cooldown <= 0f)
        {
            _cooldown = cooldown;

            mousePos = Input.mousePosition;
            GameObject sloik = Instantiate(sloikPrefab, transform.position, Quaternion.identity);
            Sloik sloikComponent = sloik.GetComponent<Sloik>();
            sloikComponent.SetTarget(Camera.main.ScreenToWorldPoint(mousePos));
            sloikComponent.SetType(currSloik);
        }
    }
}