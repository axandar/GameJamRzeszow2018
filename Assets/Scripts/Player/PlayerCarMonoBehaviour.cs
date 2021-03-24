using UnityEngine;
using UnityEngine.UI;

namespace Player{
	public class PlayerCarMonoBehaviour : MonoBehaviour{
		
		private const KeyCode LineUp = KeyCode.W;
		private const KeyCode LineDown = KeyCode.S;

		[SerializeField] private GameManager gameManager;
		[SerializeField] private Image[] hearts;
		[SerializeField] private Sprite lostHeart;
		[SerializeField] private Sprite heart;
		[SerializeField] private float mid = 0.25f;
		[SerializeField] private float startPos = 25f;
		[SerializeField] private Animator gameOverScreen;

		private PlayerCar _playerCar;

		private void Start(){
			_playerCar = gameManager.PlayerCar;
			_playerCar.Initialize();
			
			for(var i = 0; i < hearts.Length; i++){
				if(_playerCar.HitsLeft <= i){
					hearts[i].enabled = false;
				}
			}

			SetPlayerOnStart();
		}

		private void SetPlayerOnStart(){
			PlayerCar.SetStartingPosition(transform, startPos);
			StartCoroutine(_playerCar.PlayerSlide(transform, mid, 3f, 
				OnPlayerSlidedIn));
		}
		
		private void OnPlayerSlidedIn(){
			gameManager.InGame = true;
		}
		
		private void Update(){
			if(gameManager.Timer == 0){
				GameEnd();
			}
			CheckHearts();
			if(!_playerCar.IsAlive){
				GameEnd();
			}
			
			if(Input.GetKeyDown(LineUp)){
				_playerCar.MovePlayerByXLanes(-1);
			}

			if(Input.GetKeyDown(LineDown)){
				_playerCar.MovePlayerByXLanes(1);
			}

			_playerCar.Tick();
			CalculatePlayerPosition();
		}

		private void CalculatePlayerPosition(){
			var playerpos = transform.position;
			var target = _playerCar.Lanes[_playerCar.CurrentLane];
			playerpos.y = Mathf.MoveTowards(playerpos.y, target, Time.deltaTime * 9f);
			transform.position = playerpos;
		}

		private void CheckHearts(){
			for(var i = 0; i < hearts.Length; i++){
				hearts[i].sprite = _playerCar.HitsLeft <= i ? lostHeart : heart;
			}
		}

		private void GameEnd(){
			gameOverScreen.Play("GameOverPanel");
			StartCoroutine(_playerCar.PlayerSlide(transform, -startPos, 5f, null));
		}
	}
}