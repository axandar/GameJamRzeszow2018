using Enemy;
using Player;
using UnityEngine;

public class GameManager : MonoBehaviour{

	[SerializeField] private long points;
	[SerializeField] private bool inGame;
	[SerializeField] private float timer = 120f;
	[SerializeField] private float[] lanes = {5.25f, 3.1f, 1f, -1.1f, -3.25f};
	
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
			Debug.Log("You can't afford that!");
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

		Debug.Log("Added " + amount + " coins. Now having " + 
			UpgradeManager.coins);
	}

	public UpgradeManager UpgradeManager{ get; private set; }

	public EnemyManager EnemyManager{ get; private set; }

	public PlayerCar PlayerCar{ get; private set; }

	public float Timer => timer;

	public float[] Lanes => lanes;

	public bool InGame{
		get => inGame;
		set => inGame = value;
	}

	public long Points{
		get => points;
		set => points = value;
	}
}