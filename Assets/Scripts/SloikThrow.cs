using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SloikThrow : MonoBehaviour
{
    public GameObject sloikPrefab;

    private Vector2 mousePos;

    public int currSloik = 6;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.inGame)
        {
            mousePos = Input.mousePosition;
            GameObject sloik = Instantiate(sloikPrefab, transform.position, Quaternion.identity);
            sloik.GetComponent<Sloik>().SetTarget(Camera.main.ScreenToWorldPoint(mousePos));
            sloik.GetComponent<Sloik>().SetType(currSloik);
        }
    }
}