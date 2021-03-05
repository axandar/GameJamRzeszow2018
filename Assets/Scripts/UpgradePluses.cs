using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePluses : MonoBehaviour{
	public int upgradeType;
	public int upgradeLvl;

	public Image[] pluses;

	public void OnEnable(){
		switch(upgradeType){
			case 0:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelGolabki;
				break;

			case 1:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelGrochowka;
				break;

			case 2:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelBigos;
				break;

			case 3:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelSchabowy;
				break;

			case 4:
				upgradeLvl = UpgradeManager.Instance.upgradeLivesLevel;
				break;

			case 5:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelLazanki;
				break;

			case 6:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelParowki;
				break;

			case 7:
				upgradeLvl = UpgradeManager.Instance.upgradeLevelMeksyk;
				break;
		}

		for(var i = 0; i < pluses.Length; i++){
			pluses[i].enabled = upgradeLvl >= i;
		}
	}
}