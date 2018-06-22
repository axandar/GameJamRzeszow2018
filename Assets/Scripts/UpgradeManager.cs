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

    private const string livesUpgrade = "livesUpgrade";
    private const string golabkiUpgrade = "golabkiUpgrade";
    private const string grochowkaUpgrade = "grochowkaUpgrade";
    private const string bigosUpgrade = "bigosUpgrade";
    private const string schabowyUpgrade = "schabowyUpgrade";
    private const string mixUpgrade = "mixUpgrade";
    private const string lazankiUpgrade = "lazankiUpgrade";
    private const string parowkiUpgrade = "parowkiUpgrade";
    private const string meksykUpgrade = "meksykUpgrade";

    public bool RemoveCoins(int amount)
    {
        if (Mathf.Abs(amount) > coins)
        {
            Debug.Log("You can't afford that!");
            return false;
        }

        coins -= Mathf.Abs(amount);
        return true;
    }

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
        upgradeLivesLevel = PlayerPrefs.GetInt(livesUpgrade, 0);

        upgradeLevelGolabki = PlayerPrefs.GetInt(golabkiUpgrade, 0);
        upgradeLevelGrochowka = PlayerPrefs.GetInt(grochowkaUpgrade, 0);
        upgradeLevelBigos = PlayerPrefs.GetInt(bigosUpgrade, 0);
        upgradeLevelSchabowy = PlayerPrefs.GetInt(schabowyUpgrade, 0);
        upgradeLevelMix = PlayerPrefs.GetInt(mixUpgrade, 0);
        upgradeLevelLazanki = PlayerPrefs.GetInt(lazankiUpgrade, 0);
        upgradeLevelParowki = PlayerPrefs.GetInt(parowkiUpgrade, 0);
        upgradeLevelMeksyk = PlayerPrefs.GetInt(meksykUpgrade, 0);
    }

    public void SaveUpgrades()
    {
        PlayerPrefs.SetInt(livesUpgrade, upgradeLivesLevel);

        PlayerPrefs.SetInt(golabkiUpgrade, upgradeLevelGolabki);
        PlayerPrefs.SetInt(grochowkaUpgrade, upgradeLevelGrochowka);
        PlayerPrefs.SetInt(bigosUpgrade, upgradeLevelBigos);
        PlayerPrefs.SetInt(mixUpgrade, upgradeLevelMix);
        PlayerPrefs.SetInt(lazankiUpgrade, upgradeLevelLazanki);
        PlayerPrefs.SetInt(parowkiUpgrade, upgradeLevelParowki);
        PlayerPrefs.SetInt(meksykUpgrade, upgradeLevelMeksyk);
    }
}