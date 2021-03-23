using Enemy;
using Player;
using UnityEngine;

public class SloikEffectController : MonoBehaviour{

	public const int EFFECT_SLOW = 1;
	public const int EFFECT_BIRD = 0;
	public const int EFFECT_EXPLOSION_CIRCLE = 2;
	public const int EFFECT_IMMORTAL = 3;
	public const int EFFECT_RANDOM = 4;
	public const int EFFECT_LIFE_UP = 5;
	public const int EFFECT_INSTANT_KILL = 6;
	public const int EFFECT_EXPLOSION_LINES = 7;

	public int effect;
	public float duration = 0;

	public Sprite effectGolabki; //0 type
	public Sprite effectGrochowka; //1
	public Sprite effectBigos; //2
	public Sprite effectSchabowy; //3
	public Sprite effectLazanki; //5
	public Sprite effectParowki; //6
	public Sprite effectMeksyk; //7

	private SpriteRenderer _spriteRendere;
	private bool _isBird;
	private GameManager _gameManager;

	private void Awake(){
		_spriteRendere = GetComponent<SpriteRenderer>();
	}

	public void Initialize(GameManager gameManager){
		_gameManager = gameManager;
	}

	void Update(){
		EffectWearOff();

		if(_isBird){
			MoveBird();
		}
	}

	private void EffectWearOff(){
		if(duration > 0){
			duration -= Time.deltaTime;
		}

		if(duration < 0){
			Debug.Log("Effect ended");
			Destroy(gameObject);
		}
	}

	private void MoveBird(){
		transform.Translate(Vector3.forward * Time.deltaTime);
	}

	public void SetSloikEffect(int sloikEffect){
		effect = sloikEffect;
		duration = 1f;

		switch(effect){
			case EFFECT_SLOW:
				SetSprite(effectGrochowka);
				SetSize();
				break;
			case EFFECT_BIRD:
				SetSprite(effectGolabki);
				SetSize();
				_isBird = true;
				transform.Rotate(new Vector3(0, 0, 45f));
				break;
			case EFFECT_EXPLOSION_CIRCLE:
				SetSprite(effectBigos);
				SetSize();
				break;
			case EFFECT_IMMORTAL:
				SetSprite(effectSchabowy);
				SetSize();
				break;
			case EFFECT_RANDOM:
				SetSloikEffect(GetRandomEffect());
				break;
			case EFFECT_LIFE_UP:
				SetSprite(effectLazanki);
				SetSize();
				break;
			case EFFECT_INSTANT_KILL:
				SetSprite(effectParowki);
				SetSize();
				break;
			case EFFECT_EXPLOSION_LINES:
				SetSprite(effectMeksyk);
				SetSize();
				break;
		}
	}
	
	private void SetSize(){
		transform.localScale = new Vector3(10, 10, 1);
		var collider2D = gameObject.AddComponent<PolygonCollider2D>();
		collider2D.isTrigger = true;
	}

	private void SetSprite(Sprite sprite){
		_spriteRendere.sprite = sprite;
	}

	public void OnTriggerEnter2D(Collider2D col){
		var layer = col.gameObject.layer;
		if(layer == LayerMask.NameToLayer("Enemy")){
			var enemyController = col.gameObject.GetComponent<EnemyController>();
			OnEnemyContact(enemyController);
		} else if(layer == LayerMask.NameToLayer("Player")){
			OnPlayerContact(_gameManager.PlayerCar);
		}
	}

	private void OnEnemyContact(EnemyController enemyController){
		Debug.Log(effect);
		switch(effect){
			case EFFECT_SLOW:
				enemyController.Slow();
				break;
			case EFFECT_BIRD:
				enemyController.Bird();
				break;
			case EFFECT_EXPLOSION_CIRCLE:
				enemyController.Explosion();
				break;
			case EFFECT_IMMORTAL:
				enemyController.Immortal();
				break;
			case EFFECT_LIFE_UP:
				enemyController.LifeUp();
				break;
			case EFFECT_INSTANT_KILL:
				enemyController.InstaKill();
				break;
			case EFFECT_EXPLOSION_LINES:
				enemyController.Explosion();
				break;
		}
	}

	private void OnPlayerContact(PlayerCar playerController){
		switch(effect){
			case EFFECT_SLOW:
				playerController.Slow();
				break;
			case EFFECT_BIRD:
				break;
			case EFFECT_EXPLOSION_CIRCLE:
				playerController.Explosion(effect);
				break;
			case EFFECT_IMMORTAL:
				playerController.Immortal();
				break;
			case EFFECT_LIFE_UP:
				playerController.LifeUp();
				break;
			case EFFECT_INSTANT_KILL:
				playerController.InstaKill();
				break;
			case EFFECT_EXPLOSION_LINES:
				playerController.Explosion(effect);
				break;
		}
	}

	private int GetRandomEffect(){
		while(true){
			var number = Random.Range(0, 8);
			if(number == EFFECT_RANDOM){
				continue;
			}

			return number;
		}
	}
}