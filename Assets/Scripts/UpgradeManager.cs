using UnityEngine;

public class UpgradeManager{
	
	public int coins;

	public int playerLives = 3;
	public int upgradeLivesLevel; //0 - 1lvl, 1 - 2lvl, 2 - 3lvl

	public int upgradeLevelGolabki;
	public int upgradeLevelGrochowka;
	public int upgradeLevelBigos;
	public int upgradeLevelSchabowy;
	public int upgradeLevelMix;
	public int upgradeLevelLazanki;
	public int upgradeLevelParowki;
	public int upgradeLevelMeksyk;

	private const string EFFECT_LIVES_UPGRADE = "livesUpgrade";
	private const string EFFECT_GOLABKI_UPGRADE = "golabkiUpgrade";
	private const string EFFECT_GROCHOWKA_UPGRADE = "grochowkaUpgrade";
	private const string EFFECT_BIGOS_UPGRADE = "bigosUpgrade";
	private const string EFFECT_SCHABOWY_UPGRADE = "schabowyUpgrade";
	private const string EFFECT_MIX_UPGRADE = "mixUpgrade";
	private const string EFFECT_LAZANKI_UPGRADE = "lazankiUpgrade";
	private const string EFFECT_PAROWKI_UPGRADE = "parowkiUpgrade";
	private const string EFFECT_MEKSYK_UPGRADE = "meksykUpgrade";

	public UpgradeManager(){
	}

	public void LoadUpgrades(){
		coins = PlayerPrefs.GetInt("coins");
		upgradeLivesLevel = PlayerPrefs.GetInt(EFFECT_LIVES_UPGRADE, 0);

		upgradeLevelGolabki = PlayerPrefs.GetInt(EFFECT_GOLABKI_UPGRADE, 0);
		upgradeLevelGrochowka = PlayerPrefs.GetInt(EFFECT_GROCHOWKA_UPGRADE, 0);
		upgradeLevelBigos = PlayerPrefs.GetInt(EFFECT_BIGOS_UPGRADE, 0);
		upgradeLevelSchabowy = PlayerPrefs.GetInt(EFFECT_SCHABOWY_UPGRADE, 0);
		upgradeLevelMix = PlayerPrefs.GetInt(EFFECT_MIX_UPGRADE, 0);
		upgradeLevelLazanki = PlayerPrefs.GetInt(EFFECT_LAZANKI_UPGRADE, 0);
		upgradeLevelParowki = PlayerPrefs.GetInt(EFFECT_PAROWKI_UPGRADE, 0);
		upgradeLevelMeksyk = PlayerPrefs.GetInt(EFFECT_MEKSYK_UPGRADE, 0);
	}

	public void SaveUpgrades(){
		PlayerPrefs.SetInt("coins", coins);
		PlayerPrefs.SetInt(EFFECT_LIVES_UPGRADE, upgradeLivesLevel);

		PlayerPrefs.SetInt(EFFECT_GOLABKI_UPGRADE, upgradeLevelGolabki);
		PlayerPrefs.SetInt(EFFECT_GROCHOWKA_UPGRADE, upgradeLevelGrochowka);
		PlayerPrefs.SetInt(EFFECT_BIGOS_UPGRADE, upgradeLevelBigos);
		PlayerPrefs.SetInt(EFFECT_MIX_UPGRADE, upgradeLevelMix);
		PlayerPrefs.SetInt(EFFECT_LAZANKI_UPGRADE, upgradeLevelLazanki);
		PlayerPrefs.SetInt(EFFECT_PAROWKI_UPGRADE, upgradeLevelParowki);
		PlayerPrefs.SetInt(EFFECT_MEKSYK_UPGRADE, upgradeLevelMeksyk);
	}
}