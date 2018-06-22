using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    private Text pointText;

    public void Start()
    {
        pointText = pointText ?? GetComponent<Text>();
    }

    public void Update()
    {
        pointText.text = GameManager.instance.points.ToString();
    }
}