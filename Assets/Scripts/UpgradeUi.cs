using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUi : MonoBehaviour
{
    public void BigosUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelBigos++;
        }
        Reload();
    }

    public void GolabkiUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelGolabki++;
        }
        Reload();
    }

    public void GrochowaUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelGrochowka++;
        }
        Reload();
    }

    public void LazankiUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelLazanki++;
        }
        Reload();
    }

    public void MeksykUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelMeksyk++;
        }
        Reload();
    }

    public void ParowkiUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelParowki++;
        }
        Reload();
    }

    public void SchabowyUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLevelSchabowy++;
        }
        Reload();
    }

    public void LivesUpgrade()
    {
        if (UpgradeManager.instance.RemoveCoins(10))
        {
            UpgradeManager.instance.upgradeLivesLevel++;
        }
        Reload();
    }

    public void Reload()
    {
        FindObjectOfType<MenuScript>().Menu();
        FindObjectOfType<MenuScript>().Upgrades();
    }
}