using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SloikThrow : MonoBehaviour
{
    public GameObject sloikPrefab;
    public Outline[] sloikUI;

    private Vector2 mousePos;

    public int currSloik = 1;

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

        if (Input.GetMouseButtonDown(0) && GameManager.instance.inGame)
        {
            mousePos = Input.mousePosition;
            GameObject sloik = Instantiate(sloikPrefab, transform.position, Quaternion.identity);
            Sloik sloikComponent = sloik.GetComponent<Sloik>();
            sloikComponent.SetTarget(Camera.main.ScreenToWorldPoint(mousePos));
            sloikComponent.SetType(currSloik);
        }
    }
}