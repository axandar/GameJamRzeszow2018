﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int coins = 0;

    public int playerLives = 3;
    public int upgradeLivesLevel = 0; //0 - 1lvl, 1 - 2lvl, 2 - 3lvl

    public void LoadUpgrades()
    {
        //Todo read from saved game
    }
}