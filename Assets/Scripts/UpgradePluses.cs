using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePluses : MonoBehaviour{

	[SerializeField] private UpgradeManager upgradeManager;
	
	public int upgradeType;
	public int upgradeLvl;

	public Image[] pluses;

	public void OnEnable(){
		switch(upgradeType){
			case 0:
				upgradeLvl = upgradeManager.upgradeLevelGolabki;
				break;

			case 1:
				upgradeLvl = upgradeManager.upgradeLevelGrochowka;
				break;

			case 2:
				upgradeLvl = upgradeManager.upgradeLevelBigos;
				break;

			case 3:
				upgradeLvl = upgradeManager.upgradeLevelSchabowy;
				break;

			case 4:
				upgradeLvl = upgradeManager.upgradeLivesLevel;
				break;

			case 5:
				upgradeLvl = upgradeManager.upgradeLevelLazanki;
				break;

			case 6:
				upgradeLvl = upgradeManager.upgradeLevelParowki;
				break;

			case 7:
				upgradeLvl = upgradeManager.upgradeLevelMeksyk;
				break;
		}

		for(var i = 0; i < pluses.Length; i++){
			pluses[i].enabled = upgradeLvl >= i;
		}
	}
}