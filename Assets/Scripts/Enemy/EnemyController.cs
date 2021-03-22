using Player;
using UnityEngine;

namespace Enemy{
	public class EnemyController : MonoBehaviour{

		private GameManager _gameManager;
	
		//todo dodawac wartosc predkosc z EnemyManager za kazdym refresh

		public float speed;

		public Sprite[] sprites;

		private int _screenWidth;
		private EnemyManager _enemyManager;
		private Rigidbody2D _rigidBody;
		private SpriteRenderer _spriteRenderer;

		//Effects
		private float _effectWearOff;
		private bool _isImmortal;
		private int _slowDownValue;

		public void Initialize(GameManager gameManager){
			_gameManager = gameManager;
			_enemyManager = gameManager.EnemyManager;
		}

		private void Start(){
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

			var random = Random.Range(0, sprites.Length - 1);
			
			_spriteRenderer.sprite = sprites[random];
		}

		private void Awake(){
			_rigidBody = GetComponent<Rigidbody2D>();
		}

		private void Update(){
			DriveForward();
			EffectWearOff();

			if(!(transform.position.x > 14f)){
				return;
			}
			
			_enemyManager.RemoveEnemyFromList(this);
			Destroy(gameObject);
		}

		private void DriveForward(){
			var actualEnemySpeed = speed + _enemyManager.speedUpValue - _slowDownValue;
			var position = transform.position;
			_rigidBody.MovePosition(new Vector3(position.x + FindObjectOfType<MovingRoad>().currSpeed * Time.deltaTime * actualEnemySpeed, position.y, 0f));
		}

		public void OnTriggerEnter2D(Collider2D col){
			var layer = col.gameObject.layer;
			if(layer == LayerMask.NameToLayer("Player")){
				col.GetComponent<PlayerCar>().TakeHeart(1);
				_enemyManager.spawnedEnemies.Remove(this);
				Destroy(gameObject);
			} else if(layer == LayerMask.NameToLayer("Enemy") || layer == LayerMask.NameToLayer("Obstacle")){
				_enemyManager.spawnedEnemies.Remove(this);
				Destroy(gameObject);
			}
		}

		private void EffectWearOff(){
			if(_effectWearOff < 0){
				_effectWearOff = 0;
				ClearEffects();
			} else if(_effectWearOff > 0){
				_effectWearOff -= Time.deltaTime;
			}
		}

		public void Kill(){
			if(_isImmortal){
				return;
			}

			Debug.Log("Kill");
			_gameManager.AddCoins(1);
			Destroy(gameObject);
		}

		private void ClearEffects(){
			_isImmortal = false;
			_slowDownValue = 0;
		}

		public void Slow(){
			ClearEffects();
			Debug.Log("Slow");
			var effectLevel = _gameManager.UpgradeManager.upgradeLevelGrochowka;
			switch(effectLevel){
				case 0:
					_slowDownValue = 1;
					break;
				case 1:
					_slowDownValue = 2;
					break;
				case 2:
					_slowDownValue = 3;
					break;
			}

			_effectWearOff = 3;
		}

		public void Immortal(){
			ClearEffects();

			_isImmortal = true;

			var effectLevel = _gameManager.UpgradeManager.upgradeLevelSchabowy;
			switch(effectLevel){
				case 0:
					_effectWearOff = 1;
					break;

				case 1:
					_effectWearOff = 2;
					break;

				case 2:
					_effectWearOff = 3;
					break;
			}
		}

		public void Explosion(){
			ClearEffects();
			Kill();
		}

		public void Bird(){
			ClearEffects();
			Kill();
		}

		public void LifeUp(){
			return; // no effect
		}

		public void InstaKill(){
			ClearEffects();
			Kill();
		}
	}
}