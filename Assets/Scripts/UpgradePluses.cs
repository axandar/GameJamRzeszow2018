using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePluses : MonoBehaviour
{
    public int upgradeType = 0;
    public int upgradeLvl = 0;

    public Image[] pluses;

    public void OnEnable()
    {
        switch (upgradeType)
        {
            case 0:
                upgradeLvl = UpgradeManager.instance.upgradeLevelGolabki;
                break;

            case 1:
                upgradeLvl = UpgradeManager.instance.upgradeLevelGrochowka;
                break;

            case 2:
                upgradeLvl = UpgradeManager.instance.upgradeLevelBigos;
                break;

            case 3:
                upgradeLvl = UpgradeManager.instance.upgradeLevelSchabowy;
                break;

            case 4:
                upgradeLvl = UpgradeManager.instance.upgradeLivesLevel;
                break;

            case 5:
                upgradeLvl = UpgradeManager.instance.upgradeLevelLazanki;
                break;

            case 6:
                upgradeLvl = UpgradeManager.instance.upgradeLevelParowki;
                break;

            case 7:
                upgradeLvl = UpgradeManager.instance.upgradeLevelMeksyk;
                break;
        }

        for (int i = 0; i < pluses.Length; i++)
        {
            if (upgradeLvl >= i)
            {
                pluses[i].enabled = true;
            }
            else
            {
                pluses[i].enabled = false;
            }
        }
    }
}