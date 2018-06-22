using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int coins = 0;

    public int playerLives = 3;
    public int upgradeLivesLevel = 0; //0 - 1lvl, 1 - 2lvl, 2 - 3lvl

    public int upgradeLevelGolabki = 0;
    public int upgradeLevelGrochowka = 0;
    public int upgradeLevelBigos = 0;
    public int upgradeLevelSchabowy = 0;
    public int upgradeLevelMix = 0;
    public int upgradeLevelLazanki = 0;
    public int upgradeLevelParowki = 0;
    public int upgradeLevelMeksyk = 0;

    public void AddCoins(int amount)
    {
        coins += Mathf.Abs(amount);
        Debug.Log("Added " + amount + "coins. Now having " + coins);
    }

    public int GetUpgradePrice(int level)
    {
        if (level == 0)
        {
            return 10;
        }

        int b = 10;
        int r = 10;
        for (int i = 0; i < level; i++)
        {
            r *= b;
        }
        return r;
    }

    public void LoadUpgrades()
    {
        //Todo read from saved game
    }
}