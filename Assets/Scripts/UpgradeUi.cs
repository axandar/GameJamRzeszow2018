using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUi : MonoBehaviour{

	[SerializeField] private GameManager gameManager;
	
	public void BigosUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelBigos++;
		}

		Reload();
	}

	public void GolabkiUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelGolabki++;
		}

		Reload();
	}

	public void GrochowaUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelGrochowka++;
		}

		Reload();
	}

	public void LazankiUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelLazanki++;
		}

		Reload();
	}

	public void MeksykUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelMeksyk++;
		}

		Reload();
	}

	public void ParowkiUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelParowki++;
		}

		Reload();
	}

	public void SchabowyUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLevelSchabowy++;
		}

		Reload();
	}

	public void LivesUpgrade(){
		if(gameManager.RemoveCoins(10)){
			gameManager.UpgradeManager.upgradeLivesLevel++;
		}

		Reload();
	}

	public void Reload(){
		FindObjectOfType<MenuScript>().Menu();
		FindObjectOfType<MenuScript>().Upgrades();
	}
}