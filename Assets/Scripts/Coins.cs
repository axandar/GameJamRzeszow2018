using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    private Text coinText;

    public void Start()
    {
        coinText = coinText ?? GetComponent<Text>();
    }

    public void Update()
    {
        coinText.text = UpgradeManager.instance.coins.ToString();
    }
}