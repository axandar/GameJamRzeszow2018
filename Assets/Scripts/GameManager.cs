using Enemy;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour{

	public long points;
	public bool inGame;
	public float timer = 120f;

	private void Awake(){
		UpgradeManager = new UpgradeManager();
		UpgradeManager.LoadUpgrades();
		
		EnemyManager = new EnemyManager(this);
		PlayerCar = new PlayerCar(this);
	}

	private void OnDisable(){
		UpgradeManager.SaveUpgrades();
	}

	private void Update(){
		if(!inGame){
			return;
		}

		if(timer > 0){
			timer -= Time.deltaTime;
		} else{
			timer = 0f;
		}
	}
	
	public bool RemoveCoins(int amount){
		if(Mathf.Abs(amount) > UpgradeManager.coins){
			UnityEngine.Debug.Log("You can't afford that!");
			return false;
		}

		UpgradeManager.coins -= Mathf.Abs(amount);
		return true;
	}

	public void AddCoins(int amount){
		UpgradeManager.coins += amount;
		if(inGame){
			points += 100;
		}

		UnityEngine.Debug.Log("Added " + amount + " coins. Now having " + 
			UpgradeManager.coins);
	}

	public UpgradeManager UpgradeManager{ get; private set; }

	public EnemyManager EnemyManager{ get; private set; }

	public PlayerCar PlayerCar{ get; private set; }
}