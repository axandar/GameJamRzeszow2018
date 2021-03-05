using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUi : MonoBehaviour{
	public void BigosUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelBigos++;
		}

		Reload();
	}

	public void GolabkiUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelGolabki++;
		}

		Reload();
	}

	public void GrochowaUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelGrochowka++;
		}

		Reload();
	}

	public void LazankiUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelLazanki++;
		}

		Reload();
	}

	public void MeksykUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelMeksyk++;
		}

		Reload();
	}

	public void ParowkiUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelParowki++;
		}

		Reload();
	}

	public void SchabowyUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLevelSchabowy++;
		}

		Reload();
	}

	public void LivesUpgrade(){
		if(UpgradeManager.Instance.RemoveCoins(10)){
			UpgradeManager.Instance.upgradeLivesLevel++;
		}

		Reload();
	}

	public void Reload(){
		FindObjectOfType<MenuScript>().Menu();
		FindObjectOfType<MenuScript>().Upgrades();
	}
}